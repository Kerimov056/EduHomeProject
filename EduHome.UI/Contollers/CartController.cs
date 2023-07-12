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
    
    public async Task<IActionResult> AddItem(int courseId, int qty = 1, int redicret = 0)
    {
        try
        {
            var cartCount = await _cartService.AddItem(courseId, qty);
            if (redicret == 0) return Ok(cartCount);
            //if (redicret == 0) return Ok(coursesId); 
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

    public async Task<IActionResult> Checkout()
    {
        bool IsCheckOut = await _cartService.DoCheckout();
        if (!IsCheckOut) throw new Exception("Server Error");

        return RedirectToAction("Index","Courses");
    }
}
