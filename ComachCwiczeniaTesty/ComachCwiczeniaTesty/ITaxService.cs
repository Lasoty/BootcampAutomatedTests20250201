namespace ComachCwiczeniaTesty;

public interface ITaxService
{
    decimal GetTax(decimal amount);
}

public interface IDiscountService
{
    decimal CalculateDiscount(decimal amount, string customerType);
}