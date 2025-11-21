using System;
using System.Collections.Generic;

namespace Domain.Entities;

public class Order
{
    // Corregido: Campos convertidos a propiedades para mejor encapsulación
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public decimal CalculateTotal()
    {
        return Quantity * UnitPrice;
    }
}
