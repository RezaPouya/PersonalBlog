using DNTPersianUtils.Core;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Infrastructure.Persistence.SqlServer.DbExtensions;

public static class EfCoreValueConverter
{
    public static PropertyBuilder<T> HasJsonConversion<T>(this PropertyBuilder<T> propertyBuilder) where T : class, new()
    {
        ValueConverter<T, string> converter = new ValueConverter<T, string>
        (
            v => JsonConvert.SerializeObject(v),
            v => JsonConvert.DeserializeObject<T>(v) ?? new T()
        );

        ValueComparer<T> comparer = new ValueComparer<T>
        (
            (l, r) => JsonConvert.SerializeObject(l) == JsonConvert.SerializeObject(r),
            v => v == null ? 0 : JsonConvert.SerializeObject(v).GetHashCode(),
            v => JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(v))
        );

        propertyBuilder.HasConversion(converter);
        propertyBuilder.Metadata.SetValueConverter(converter);
        propertyBuilder.Metadata.SetValueComparer(comparer);
        propertyBuilder.HasColumnType("jsonb");

        return propertyBuilder;
    }

    public static string ConvertListOfIntToCommaSeparatedString(this List<int> prop)
    {
        if (prop is null)
            return string.Empty;

        return prop.Any() ? string.Join(',', prop) : "";

    }

    public static List<int> ConvertCommaSeparatedStringToListOfInt(this string column)
    {
        return string.IsNullOrEmpty(column) ? new List<int>() : column.Split(',').Select(p => int.Parse(p)).ToList();
    }

    public static ValueConverter<byte[], string> GetByteValueConverter()
    {
        return new ValueConverter<byte[], string>(
            i => string.Join(",", i),
            s => string.IsNullOrWhiteSpace(s) ?
                new byte[0] :
                s.Split(new[] { ',' }).Select(v => byte.Parse(v)).ToArray());
    }

    public static ValueConverter<int[], string> GetIntValueConverter()
    {
        return new ValueConverter<int[], string>(
            i => string.Join(",", i),
            s => string.IsNullOrWhiteSpace(s) ?
                new int[0] :
                s.Split(new[] { ',' }).Select(v => int.Parse(v)).ToArray());
    }

    public static ValueConverter<List<string>, string> HasStringValueConverter()
    {
        return new ValueConverter<List<string>, string>(
            i => string.Join(",", i.Select(p => p).ToList()),
            s => string.IsNullOrWhiteSpace(s) ?
                new List<string>() :
                s.Split(new[] { ',' }).Select(v => v.Trim()).ToList());
    }

    public static ValueConverter<List<int>, string> CreateIntListValueConverter()
    {
        return new ValueConverter<List<int>, string>(
            i => i == null ? string.Empty : string.Join(",", i),
            s => string.IsNullOrWhiteSpace(s)
                ? new List<int>()
                : s.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(v => int.Parse(v.Trim().ToEnglishNumbers()))
                    .ToList()
        );
    }
}