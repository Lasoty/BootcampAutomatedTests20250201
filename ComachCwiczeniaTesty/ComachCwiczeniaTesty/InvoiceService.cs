using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComachCwiczeniaTesty;

public class InvoiceService
{
    private readonly ITaxService _taxService;
    private readonly IDiscountService _discountService;

    public InvoiceService(ITaxService taxService, IDiscountService discountService)
    {
        _taxService = taxService;
        _discountService = discountService;
    }

    public Task<decimal> CalculateTotal(decimal amount, string customerType)
    {
        var discount = _discountService.CalculateDiscount(amount, customerType);
        var taxableAmount = amount - discount;
        var tax = _taxService.GetTax(taxableAmount);
        return Task.FromResult(taxableAmount + tax);
    }

    public string GenerateInvoiceNumber()
    {
        var datePart = DateTime.Now.ToString("yyyyMMdd");
        var random = new Random().Next(100, 999).ToString();
        return $"INV-{datePart}-{random}";
    }

    public List<InvoiceItem> GetInvoiceItems()
    {
        return
        [
            new InvoiceItem { Name = "Product 1", Quantity = 1, UnitPrice = 10m },
            new InvoiceItem { Name = "Product 2", Quantity = 2, UnitPrice = 20m },
            new InvoiceItem { Name = "Product 3", Quantity = 3, UnitPrice = 30m }
        ];
    }
}

public class InvoiceItem
{
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public string Name { get; set; }
}
