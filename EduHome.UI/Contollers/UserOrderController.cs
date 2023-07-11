using EduHome.UI.ShopServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.UI.Contollers;

[Authorize]
public class UserOrderController : Controller
{
    private readonly IUserOrderServices _userOrderServices;
    public UserOrderController(IUserOrderServices userOrderServices)
    {
        _userOrderServices = userOrderServices;
    }

    public async Task<IActionResult> UserOrders()
    {
        var orders = await _userOrderServices.UserOrders();
        return View(orders);
    }
}
