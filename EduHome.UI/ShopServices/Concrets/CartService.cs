using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Data.Exception;
using EduHome.UI.ShopServices.Interfaces;
using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.ShopServices.Concrets;

public class CartService : ICartService
{
    private readonly AppDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _contextAccessor;
    public CartService(
        AppDbContext context,
        UserManager<User> userManager,
        IHttpContextAccessor contextAccessor
        )
    {
        _context = context;
        _userManager = userManager;
        _contextAccessor = contextAccessor;
    }

    public async Task<int> AddItem(int courseId, int qty)
    {
        string userId = GetUserId();
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            if (string.IsNullOrWhiteSpace(userId)) throw new NotFoundException("userId is null");
            var cart = await GetCart(userId);
            if (cart is null)
            {
                cart = new ShoppingCart
                {
                    UserId = userId
                };
                await _context.ShoppingCarts.AddAsync(cart);
            }
            await _context.SaveChangesAsync();

            //problem burda ola biler
            var cartItem = _context.CartDetails.FirstOrDefault(a => a.ShoppingCartId == cart.Id && a.CoursesId == courseId);
            if (cartItem is not null)
            {
                cartItem.Quantity += qty;
            }
            else
            {      //problem burdadi course duzgun tapmir
                var cours = _context.Coursess.Include(c => c.CoursesDetails).FirstOrDefault(a => a.Id == a.CoursesDetails.CoursesId);
                cartItem = new CartDetail
                {
                    CoursesId = courseId,
                    ShoppingCartId = cart.Id,
                    Quantity = qty,
                    UnitPrice = cours.CoursesDetails.CourseFee,
                };
                await _context.CartDetails.AddAsync(cartItem);
            }
            await _context.SaveChangesAsync();
            transaction.Commit();
        }
        catch (Exception ex)
        {
        }
        var cartItemCount = await GetCartItemCount(userId);
        return cartItemCount;
    }

    public async Task<int> RemoveItem(int courseId)
    {
        string userId = GetUserId();
        try
        {
            if (string.IsNullOrWhiteSpace(userId)) throw new NotFoundException("userId is null");

            var cart = await GetCart(userId);
            if (cart is null) throw new NotFoundException("cart is null");

            var cartItem = _context.CartDetails
                .FirstOrDefault(a => a.ShoppingCartId == cart.Id && a.CoursesId == courseId);
            if (cartItem is null) throw new NotFoundException("cartItem is null");

            else if (cartItem.Quantity == 1) _context.CartDetails.Remove(cartItem);

            else cartItem.Quantity = cartItem.Quantity - 1;

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
        }
        var cartItemCount = await GetCartItemCount(userId);
        return cartItemCount;
    }

    public async Task<ShoppingCart> GetUserCart()
    {
        var userId = GetUserId();
        if (userId is null) throw new NullReferenceException("Invalid userId");
        var shoppingCart = await _context.ShoppingCarts
                                .Include(a => a.CartDetails)
                                .ThenInclude(a => a.Courses)
                                .ThenInclude(a => a.Categories)
                                .ThenInclude(a => a.Courses)
                                .ThenInclude(a => a.CoursesDetails)
                                .Where(a => a.UserId == userId)
                                .FirstOrDefaultAsync();

        return shoppingCart;
    }

    public async Task<ShoppingCart> GetCart(string userId)
    {
        var cart = await _context.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == userId);
        return cart;
    }


    private string GetUserId()
    {
        var user = _contextAccessor.HttpContext.User;
        string userId = _userManager.GetUserId(user);
        return userId;
    }

    public async Task<int> GetCartItemCount(string userId = "")
    {
        if (!string.IsNullOrWhiteSpace(userId))
        {
            userId = GetUserId();
        }
        var data = await (from cart in _context.ShoppingCarts
                          join cartDetail in _context.CartDetails
                          on cart.Id equals cartDetail.ShoppingCartId
                          select new { cartDetail.Id }
                          ).ToListAsync();

        return data.Count;
    }

    public async Task<bool> DoCheckout()
    {
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            var userId = GetUserId();
            if (userId is null) throw new Exception("User is not");

            var cart = GetCart(userId);
            if (cart is null) throw new Exception("Invaliid Cart");

            var cartDetail = _context.CartDetails.Where(a => a.ShoppingCartId == cart.Id).ToList();
            if (cartDetail.Count == 0) throw new Exception("Cart is Empty");

            var order = new Order
            {
                UserId = userId,
                CreateDate = DateTime.Now,
                OrderStatusId = 1
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var item in cartDetail)
            {
                var orderDetail = new OrderDetail
                {
                    CoursesId = item.CoursesId,
                    OrderId = order.Id,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                };
                await _context.OrderDetails.AddRangeAsync(orderDetail);
            }
            await _context.SaveChangesAsync();

            _context.CartDetails.RemoveRange(cartDetail);
            await _context.SaveChangesAsync();
            transaction.Commit();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}


