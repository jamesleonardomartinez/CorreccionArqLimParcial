using System;
using System.Collections.Generic;

namespace Domain.Services;

using Domain.Entities;

public static class OrderService
{
    // Corregido: Campo privado readonly para prevenir modificación externa
    private static readonly List<Order> _lastOrders = new List<Order>();
    
    // Corregido: Propiedad de solo lectura usando IReadOnlyList
    public static IReadOnlyList<Order> LastOrders => _lastOrders.AsReadOnly();

    public static Order CreateTerribleOrder(string customer, string product, int qty, decimal price)
    {
        var o = new Order 
        { 
            Id = new Random().Next(1, 9999999), 
            CustomerName = customer, 
            ProductName = product, 
            Quantity = qty, 
            UnitPrice = price 
        };
        _lastOrders.Add(o);
        return o;
    }
}
