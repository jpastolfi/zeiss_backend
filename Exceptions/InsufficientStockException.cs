namespace zeiss_api.Exceptions
{
    public class InsufficientStockException(int id, int requested, int available) : AppException(
        $"Cannot decrement {requested} from product {id}. {available} available in stock.",
        409
        ) {}
}