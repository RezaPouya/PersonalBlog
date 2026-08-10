using PersonalBlog.Utilities.Dtos;
using PersonalBlog.Utilities.Expressions;
using PersonalBlog.Utilities.Extensions;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace PersonalBlog.Utilities.Expressions;

public static class ExpressionExtensions
{
    public static IQueryable<T> Where<T>(this IQueryable<T> source, IList<GridPropertyFilterDto> filters)
    {
        var predicate = GetExpression<T>(filters);
        return predicate != null ? source.Where(predicate) : source;
    }

    public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, GridSortDto sortProperty)
    {
        if (sortProperty is null)
            return source;

        Type entityType = typeof(T);

        var hasPropertyName = entityType.ContainsProperty(sortProperty.PropertyName);

        if (hasPropertyName == false)
            return source;

        // we have overwritten the default look-up flags,
        // if we specify new flags, we need to provide all the info so that the property can be found.
        // For example: BindingFlags.IgnoreCase |  BindingFlags.Public | BindingFlags.Instance
        PropertyInfo property = entityType.GetProperty(sortProperty.PropertyName,
            BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

        var parameter = Expression.Parameter(entityType, "p");

        var propertyAccess = Expression.MakeMemberAccess(parameter, property);

        var orderByExp = Expression.Lambda(propertyAccess, parameter);

        var typeArguments = new Type[] { entityType, property.PropertyType };

        var methodName = sortProperty.Ascending ? "OrderBy" : "OrderByDescending";

        var resultExp = Expression.Call(typeof(Queryable), methodName, typeArguments, source.Expression,
            Expression.Quote(orderByExp));

        return source.Provider.CreateQuery<T>(resultExp);
    }

    public static Expression<Func<T, bool>> GetExpression<T>(IEnumerable<GridPropertyFilterDto> filters)
    {
        var param = Expression.Parameter(typeof(T), "p");

        var body = filters
            .Select(filter => GetExpression<T>(param, filter))
            .DefaultIfEmpty()
            .Aggregate(Expression.AndAlso);

        return body != null ? Expression.Lambda<Func<T, bool>>(body, param) : null;
    }

    /// <summary>Where expression generator.</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filters">The filters.</param>
    /// <returns></returns>
    public static Expression<Func<T, bool>> GetExpression<T>(IList<GridPropertyFilterDto> filters)
    {
        if (filters.Count == 0)
            return null;

        ParameterExpression param = Expression.Parameter(typeof(T), "t");

        Expression exp = null;

        if (filters.Count == 1)
        {
            var innerExpression = GetExpression<T>(param, filters[0]);
            if (innerExpression is null)
                return null;
            return Expression.Lambda<Func<T, bool>>(innerExpression, param);
        }

        if (filters.Count == 2)
        {
            var innerExpression = GetExpression<T>(param, filters[0], filters[1]);
            if (innerExpression is null)
                return null;
            return Expression.Lambda<Func<T, bool>>(innerExpression, param);
        }

        while (filters.Count > 0)
        {
            var f1 = filters[0];
            var f2 = filters[1];

            if (exp == null)
                exp = GetExpression<T>(param, filters[0], filters[1]);
            else
                exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0], filters[1]));

            filters.Remove(f1);
            filters.Remove(f2);

            if (filters.Count == 1)
            {
                exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0]));
                filters.RemoveAt(0);
            }
        }

        if (exp is null)
            return null;
        return Expression.Lambda<Func<T, bool>>(exp, param);
    }

    /// <summary>And logic connector expression generator.</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="param">The parameter.</param>
    /// <param name="filter1">The filter1.</param>
    /// <param name="filter2">The filter2.</param>
    /// <returns></returns>
    private static BinaryExpression GetExpression<T>(ParameterExpression param, GridPropertyFilterDto filter1, GridPropertyFilterDto filter2)
    {
        return Expression.AndAlso(GetExpression<T>(param, filter1), GetExpression<T>(param, filter2));
    }

    public static Expression GetExpression<T>(ParameterExpression param, GridPropertyFilterDto filter)
    {
        Type t = typeof(T);

        var hasPropertyName = t.ContainsProperty(filter.PropertyName);

        if (hasPropertyName == false)
            return null;

        var filterOp = filter.Operation.ToLower().Trim();

        // نکته بسیار مهم (این باگ باعث می‌شد عملاً هیچ فیلتر عمومی‌ای در کل برنامه کار نکند):
        // ContainsProperty بالا به‌صورت case-insensitive بررسی می‌کند (مثلاً "title" را با
        // "Title" یکسان می‌داند)، اما Expression.Property(param, string) که قبلاً اینجا
        // استفاده می‌شد، یک جستجوی case-SENSITIVE انجام می‌دهد. یعنی فیلتر با مقدار
        // "title" (همان چیزی که فرانت‌اند/جاوااسکریپت همیشه camelCase می‌فرستد) از چک
        // بالا رد می‌شد ولی همینجا با Exception متوقف می‌شد، چون پراپرتی واقعی در سی‌شارپ
        // "Title" (با حرف بزرگ) است. نتیجه: هر درخواست لیست که فیلتر عمومی داشت با خطا
        // مواجه می‌شد. اصلاح شد با پیدا کردن PropertyInfo به‌صورت case-insensitive (دقیقاً
        // همان روشی که متد OrderBy در همین فایل از قبل درست استفاده می‌کرد).
        PropertyInfo propertyInfo = t.GetProperty(filter.PropertyName,
            BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

        if (propertyInfo is null)
            return null;

        MemberExpression member = Expression.Property(param, propertyInfo);

        var propertyType = ((PropertyInfo)member.Member).PropertyType;

        var converter = TypeDescriptor.GetConverter(propertyType); // 1

        if (!converter.CanConvertFrom(typeof(string))) // 2
            throw new NotSupportedException();

        ConstantExpression constant = Expression.Constant(converter.ConvertFromInvariantString(filter.Value.ToString()));

        constant = ValidateInputType(filter, member.Type, constant, ref filterOp);

        if (filterOp.Trim().ToLower() == GridFilterOperationConstants.Equals.ToLower())
            return Expression.Equal(member, constant);

        if (filterOp.Trim().ToLower() == GridFilterOperationConstants.BooleanEquals.ToLower())
            return Expression.Equal(member, Expression.Convert(constant, typeof(bool)));

        if (filterOp.Trim().ToLower() == GridFilterOperationConstants.GreaterThan.ToLower())
            return Expression.GreaterThan(member, constant);

        if (filterOp.Trim().ToLower() == GridFilterOperationConstants.GreaterThanOrEqual.ToLower())
            return Expression.GreaterThanOrEqual(member, constant);

        if (filterOp.Trim().ToLower() == GridFilterOperationConstants.LessThan.ToLower())
            return Expression.LessThan(member, constant);

        if (filterOp.Trim().ToLower() == GridFilterOperationConstants.LessThanOrEqual.ToLower())
            return Expression.LessThanOrEqual(member, constant);

        if (filterOp.Trim().ToLower() == GridFilterOperationConstants.Contains.ToLower())
            return Expression.Call(member, typeof(String).GetMethod("Contains", new Type[] { typeof(string) }), constant);

        if (filterOp.Trim().ToLower() == GridFilterOperationConstants.StartsWith.ToLower())
            return Expression.Call(member, typeof(String).GetMethod("StartsWith", new Type[] { typeof(string) }), constant);

        if (filterOp.Trim().ToLower() == GridFilterOperationConstants.EndsWith.ToLower())
            return Expression.Call(member, typeof(String).GetMethod("EndsWith", new Type[] { typeof(string) }), constant);

        return null;
    }

    private static ConstantExpression ValidateInputType(GridPropertyFilterDto filter, Type type, ConstantExpression constant, ref string filterOp)
    {
        switch (type.FullName)
        {
            case "System.Text.Json.JsonElement":
                {
                    constant = Expression.Constant(filter.Value);
                    filterOp = GridFilterOperationConstants.Contains;
                    break;
                }

            case "System.Guid":
                Guid outGuid;
                if (filter.Value.ToString().IsGuid(out outGuid))
                    constant = Expression.Constant(outGuid);
                break;

            case "System.DateTime":
                if (filter.Value.ToString().IsDate())
                    constant = Expression.Constant(Convert.ToDateTime(filter.Value));
                break;

            case "System.Single":
                if (filter.Value.ToString().IsNum())
                    constant = Expression.Constant(Convert.ToSingle(filter.Value));
                break;

            case "System.Int16":
                if (filter.Value.ToString().IsNum())
                    constant = Expression.Constant(Convert.ToInt16(filter.Value));
                break;

            case "System.Byte":
                if (filter.Value.ToString().IsNum())
                    constant = Expression.Constant(Convert.ToByte(filter.Value));
                break;

            case "System.Int32":
                if (filter.Value.ToString().IsNum())
                    constant = Expression.Constant(Convert.ToInt32(filter.Value));
                break;

            case "System.Int64":
                if (filter.Value.ToString().IsNum())
                    constant = Expression.Constant(Convert.ToInt64(filter.Value));
                break;

            case "System.Double":
                if (filter.Value.ToString().IsNum())
                    constant = Expression.Constant(Convert.ToDouble(filter.Value));
                break;

            case "System.Decimal":
                if (filter.Value.ToString().IsNum())
                    constant = Expression.Constant(Convert.ToDecimal(filter.Value));
                break;

            case "System.Boolean":
                if (filter.Value.ToString().ToLower().IsBoolean())
                {
                    constant = Expression.Constant(Convert.ToBoolean(filter.Value));
                    filterOp = GridFilterOperationConstants.BooleanEquals;
                }

                break;

            case "System.String":
                constant = Expression.Constant(filter.Value);
                //filterOp = GridFilterOperationConstants.Contains;
                break;
        }

        return constant;
    }
}

