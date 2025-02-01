using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComachCwiczeniaTesty;

public class InvoiceService
{
    public string GenerateInvoiceNumber()
    {
        var datePart = DateTime.Now.ToString("yyyyMMdd");
        var random = new Random().Next(100, 999).ToString();
        return $"INV-{datePart}-{random}";
    }
}
