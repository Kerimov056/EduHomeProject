using EduHome.Core.Entities;

namespace EduHome.UI.ShopServices.Interfaces;

public interface ICartService
{
    Task<int> AddItem(int courseId, int qty);
    Task<int> RemoveItem(int courseId);
    Task<ShoppingCart> GetUserCart();
    Task<int> GetCartItemCount(string userId="");
    Task<ShoppingCart> GetCart(string userId);
    Task<bool> DoCheckout();
}
