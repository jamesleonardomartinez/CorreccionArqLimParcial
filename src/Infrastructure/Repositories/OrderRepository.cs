using System;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    public void SaveOrder(Order order)
    {
        var sql = "INSERT INTO Orders(Id, Customer, Product, Qty, Price) VALUES (" 
            + order.Id + ", '" 
            + order.CustomerName + "', '" 
            + order.ProductName + "', " 
            + order.Quantity + ", " 
            + order.UnitPrice + ")";
        BadDb.ExecuteNonQueryUnsafe(sql);
    }
}
