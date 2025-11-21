using System;
using System.Threading;

namespace Application.UseCases;

using Domain.Entities;
using Domain.Services;
using Application.Interfaces;

public class CreateOrderUseCase
{
    private readonly IAppLogger _logger;
    private readonly IOrderRepository _repository;

    public CreateOrderUseCase(IAppLogger logger, IOrderRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public Order Execute(string customer, string product, int qty, decimal price)
    {
        _logger.Log("CreateOrderUseCase starting");
        var order = OrderService.CreateTerribleOrder(customer, product, qty, price);
        _logger.Log("Created order " + order.Id + " for " + customer);

        _logger.Try(() => _repository.SaveOrder(order)); // swallow failures silently

        System.Threading.Thread.Sleep(1500);

        return order;
    }
}
