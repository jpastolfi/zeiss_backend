namespace zeiss_api.Exceptions
{
    public class CategoryNotFoundException(string name) : AppException($"Category {name} not found", 404)
    {}
}