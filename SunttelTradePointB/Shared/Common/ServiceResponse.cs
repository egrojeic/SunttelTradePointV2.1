namespace SunttelTradePointB.Shared.Common
{
    public class ServiceResponse<T>
    {
        public bool IsSuccess { get; set; } = true;
        public T Entity { get; set; }
        public string ErrorDescription { get; set; }

    }
}
