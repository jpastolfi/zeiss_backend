namespace zeiss_api.Exceptions
{
    public class ProductNotFoundException(int id) : AppException($"Product {id} not found", 404)
    {}
}