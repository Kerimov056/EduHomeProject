using EduHome.Core.Entities;

namespace EduHome.UI.ShopServices.Interfaces;

public interface IUserOrderServices
{
    Task<IEnumerable<Order>> UserOrders();
}
