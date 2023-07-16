using EduHome.Core.Entities;
using EduHome.UI.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EduHome.UI.Controllers;

public class CourseDetailsController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _contextAccessor;
    public CourseDetailsController(
        AppDbContext context,
        IHttpContextAccessor contextAccessor,
        UserManager<User> userManager)
    {
        _context = context;
        _contextAccessor = contextAccessor;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index(int id)
    {
        if (id == 0) return NotFound();
        var cart = await _context.Coursess.FindAsync(id);
        if (cart is null) return NotFound();

        ViewBag.Id = cart.Id;

        var comments = _context.CourseComments.Where(c => c.CoursesId == id).ToList();
        TempData["CommentsSum"] = comments.Count();

        HomeViewModel homeViewModel = new()
        {
            blogs = await _context.Blogs.ToListAsync(),
            courses = await _context.Coursess
                    .Include(c => c.CoursesDetails)
                    .Include(c => c.CourseComments)
                    .ThenInclude(l => l.User)
                    .ToListAsync(),
            categories = await _context.Categoriess.ToListAsync(),
            likes = await _context.Likes.ToListAsync()
        };
        return View(homeViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddLike(string userId, int commentId)
    {
        if (!User.Identity.IsAuthenticated) return RedirectToAction("LogIn", "SignUp");
        if (string.IsNullOrEmpty(userId) || commentId == 0) return NotFound();

        var ByUser = GetUserId();
        var comment = await _context.CourseComments.FindAsync(commentId);

        if (comment == null) return NotFound();

        var existingLike = await _context.Likes.FirstOrDefaultAsync(l => l.UserId == ByUser && l.CourseCommentId == commentId);

        if (existingLike != null)
        {
            _context.Likes.Remove(existingLike);
            await _context.SaveChangesAsync();
            ViewBag.ErrorMessage = "You have already liked this comment.";
            return RedirectToAction("Index", new { id = comment.CoursesId });
        }

        Like like = new Like
        {
            UserId = ByUser,
            CourseCommentId = commentId,
            DateTime = DateTime.Now,
            like_sum = 1
        };

        ViewBag.LikeSum = like.like_sum;

        await _context.Likes.AddAsync(like);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", new { id = comment.CoursesId });
    }


    [HttpPost]
    public async Task<IActionResult> CommentEdit(int commentId, HomeViewModel homeViewModel)
    {
        var comment = await _context.CourseComments.FindAsync(commentId);
        if (comment == null)
        {
            return NotFound();
        }

        comment.Comment = homeViewModel.Comments;
        comment.CreatedDate = DateTime.Now;

        _context.CourseComments.Update(comment);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", new { id = comment.CoursesId });
    }


    [HttpPost]
    public async Task<IActionResult> DeleteComment(int commentId)
    {
        if (commentId == 0) return NotFound();
        CourseComment? courseComment = await _context.CourseComments.FindAsync(commentId);

        _context.CourseComments.Remove(courseComment);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index", "CourseDetails");
    }

    [HttpPost]
    public async Task<IActionResult> PostReply(int CID, string Reply)
    {
        var ByUser = GetUserId();
        if (ByUser is null) return RedirectToAction("LogIn", "SiginUp");

        Reply cReply = new()
        {
            ReplyText = Reply,
            CourseCommentId = CID,
            UserId = ByUser,
            DateTime = DateTime.Now
        };

        await _context.Replies.AddAsync(cReply);
        await _context.SaveChangesAsync();
        return RedirectToAction("CommentsPage");
    }


   
    [HttpPost]
    public async Task<IActionResult> AddComment(int id, HomeViewModel homeViewModel)
    {
        if (string.IsNullOrWhiteSpace(homeViewModel.Comments))
        {
            return Ok();
        }

        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("LogIn", "SiginUp");
        }


        if (User.Identity.IsAuthenticated)
        {
            User user = await _userManager.FindByNameAsync(User.Identity.Name);

            CourseComment comment = new CourseComment
            {
                CreatedDate = DateTime.Now,
                Comment = homeViewModel.Comments,
                CoursesId = id,
                UserId = user.Id
            };

            _context.CourseComments.Add(comment);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Index", new { id = id });
    }
    public async Task<IActionResult> CommentsPage(int id, string User, string sortOrder = "")
    {
        if (id == 0) return NotFound();
        var cart = await _context.Coursess.FindAsync(id);
        if (cart == null)
        {
            return NotFound();
        }
        ViewBag.Id = cart.Id;

        var comment = _context.CourseComments.Where(c => c.CoursesId == id).ToList();
        ViewBag.CommentsSum = comment.Count();

        var commentsQuery = _context.CourseComments.Where(c => c.CoursesId == id);

        if (!string.IsNullOrEmpty(User))
        {
            commentsQuery = commentsQuery.Where(c => c.User.UserName.Contains(User));
        }

        switch (sortOrder)
        {
            case "former":
                commentsQuery = commentsQuery.OrderBy(c => c.CreatedDate);
                break;
            case "newest":
            default:
                commentsQuery = commentsQuery.OrderByDescending(c => c.CreatedDate);
                break;
        }
        HomeViewModel homeViewModel = new()
        {
            blogs = await _context.Blogs.ToListAsync(),
            courses = await _context.Coursess
                                .Include(c => c.CoursesDetails)
                                .Include(c => c.CourseComments)
                                .ThenInclude(u => u.User)
                                .ToListAsync(),
            categories = await _context.Categoriess.ToListAsync(),
            courseComments = await commentsQuery
                                .Include(u => u.User)
                                .Include(r => r.Replies)
                                .ToListAsync(),
            likes = await _context.Likes.ToListAsync()
        };
        return View(homeViewModel);
    }



    [HttpPost]
    private string GetUserId()
    {
        var user = _contextAccessor.HttpContext.User;
        string userId = _userManager.GetUserId(user);
        return userId;
    }


}




