namespace PersonalBlog.Utilities.Dtos;

public class GridAllowedPropertyFilterDto
{
    public GridAllowedPropertyFilterDto()
    {
    }

    public GridAllowedPropertyFilterDto(string propertyName, string operation)
    {
        PropertyName = propertyName;
        Operation = operation;
    }

    public static GridAllowedPropertyFilterDto Instantiate(string propertyName, string operation)
    {
        return new GridAllowedPropertyFilterDto()
        {
            PropertyName = propertyName,
            Operation = operation
        };
    }

    public string PropertyName { get; set; }
    public string Operation { get; set; }
}