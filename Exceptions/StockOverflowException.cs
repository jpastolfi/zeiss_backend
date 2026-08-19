namespace zeiss_api.Exceptions
{
    public class StockOverflowException(int id, int currentStock, int quantityToAdd) : AppException(
        $"Cannot add {quantityToAdd} to product {id}. Current stock {currentStock} would exceed maximum allowed value.",
        409)
    {}
}