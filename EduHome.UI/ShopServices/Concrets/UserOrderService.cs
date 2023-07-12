using EduHome.Core.Entities;
using EduHome.UI.ShopServices.Interfaces;
using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.ShopServices.Concrets;

public class UserOrderService: IUserOrderServices
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly UserManager<User> _userManager;
    public UserOrderService(
        AppDbContext context,
        IHttpContextAccessor contextAccessor,
        UserManager<User> userManager)
    {
        _context = context;
        _contextAccessor = contextAccessor;
        _userManager = userManager;
    }

    public async Task<IEnumerable<Order>> UserOrders()
    {
        var userId = GetUserId();
        if (string.IsNullOrWhiteSpace(userId)) throw new Exception("User is not");

        var orders = await _context.Orders
                            .Include(x => x.OrderStatus)
                            .Include(x => x.orderDetails)
                            .ThenInclude(x => x.Courses)
                            .ThenInclude(x => x.Categories)
                            .Where(a => a.UserId == userId)
                            .ToListAsync();

        return orders;
    }

    private string GetUserId()
    {
        var user = _contextAccessor.HttpContext.User;
        string userId = _userManager.GetUserId(user);
        return userId;
    }
}
