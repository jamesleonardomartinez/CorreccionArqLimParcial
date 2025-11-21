namespace Application.Interfaces;

public interface IOrderRepository
{
    void SaveOrder(Domain.Entities.Order order);
}
