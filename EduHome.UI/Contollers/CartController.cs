using EduHome.UI.ShopServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.UI.Contollers;

    [Authorize]
public class CartController : Controller
{
    private readonly ICartService _cartService;
    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<IActionResult> AddItem(int coursesId, int qty = 1)
    {
        try
        {
            var cartCount = await _cartService.AddItem(coursesId, qty);
            if (cartCount == 0) { return Ok(cartCount); }
            return RedirectToAction("GetUserCart");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    public async Task<IActionResult> RemoveItem(int coursesId)
    {
        var cartCount = await _cartService.RemoveItem(coursesId);
        return RedirectToAction("GetUserCart");
    }

    public async Task<IActionResult> GetUserCart()
    {
        var cart = await _cartService.GetUserCart();
        return View(cart);
    }

    public async Task<IActionResult> GetTotalItemInCart()
    {
        int cartItem = await _cartService.GetCartItemCount();
        return Ok(cartItem);
    }
}
