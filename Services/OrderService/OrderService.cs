using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyAppBack.Data.Repos.BasketRepository;
using MyAppBack.Data.Repos.GenericRepository;
using MyAppBack.Data.Spec;
using MyAppBack.Data.UnitOfWork;
using MyAppBack.Models;
using MyAppBack.Models.OrderAggregate;

namespace MyAppBack.Services.OrderService
{
  public class OrderService : IOrderService
  {
    private readonly IBasketRepository _basketRepo;
    private readonly IUnitOfWork _unitOfWork;
    public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepo)
    {
      _unitOfWork = unitOfWork;
      _basketRepo = basketRepo;
    }

    public async Task<Order> CreateOrderAsync(string byerEmail, int deliveryMethodId, string basketId, Address shipingAddress)
    {
      // get basket form repo
      var basket = await _basketRepo.GetBasketAsync(basketId);

      // get items from the product repo
      var items = new List<OrderItem>();
      foreach (var item in basket.Items)
      {
        var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
        var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
        var orderItem = new OrderItem(itemOrdered, productItem.ProductPrice, item.Quantity);
        items.Add(orderItem);
      }

      // get delivery method from repo
      var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

      // calc subtotal
      var subtotal = items.Sum(item => item.Price * item.Quantity);

      // create order
      var order = new Order(byerEmail, shipingAddress, deliveryMethod, items, subtotal);
      _unitOfWork.Repository<Order>().Add(order);

      // TO DO: save to db
      var result = await _unitOfWork.Complete();
      if (result <= 0) return null;

      // delete basket
      await _basketRepo.DeleteBasketAsync(basketId);

      // return order
      return order;
    }

    public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
    {
      return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
    }

    public async Task<Order> GetOrderById(int id, string byerEmail)
    {
      var spec = new OrderWithItemsAndOrderingSpecification(id, byerEmail);
      return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);

    }

    public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string byerEmail)
    {
      var spec = new OrderWithItemsAndOrderingSpecification(byerEmail);
      return await _unitOfWork.Repository<Order>().ListAsync(spec);
    }
  }
}