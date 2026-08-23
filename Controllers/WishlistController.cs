using CampTravelGear.Data;
using CampTravelGear.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampTravelGear.Controllers;

public class WishlistController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public WishlistController(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        // Guest
        if (!(User.Identity?.IsAuthenticated ?? false))
        {
            return View(null);
        }

        // Logged-in user
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
            return View(null);

        var wishlist = await _context.Wishlists
            .Include(w => w.WishlistItems)
                .ThenInclude(wi => wi.Product)
                    .ThenInclude(p => p!.ProductImages)
            .FirstOrDefaultAsync(w => w.UserId == user.Id);

        return View(wishlist);
    }


    // =========================================================
    // ADD - LOGGED IN USER
    // =========================================================

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(int productId)
    {
        if (!(User.Identity?.IsAuthenticated ?? false))
        {
            return Unauthorized();
        }

        var user = await _userManager.GetUserAsync(User);

        if (user == null)
            return Unauthorized();


        // Check product
        var product = await _context.Products
            .FirstOrDefaultAsync(p =>
                p.Id == productId &&
                p.IsActive &&
                !p.IsDeleted);

        if (product == null)
        {
            return NotFound(new
            {
                success = false,
                message = "Product not found."
            });
        }


        // Get or create wishlist
        var wishlist = await _context.Wishlists
            .FirstOrDefaultAsync(w => w.UserId == user.Id);

        if (wishlist == null)
        {
            wishlist = new Wishlist
            {
                UserId = user.Id
            };

            _context.Wishlists.Add(wishlist);

            await _context.SaveChangesAsync();
        }


        // Check duplicate
        var alreadyExists = await _context.WishlistItems
            .AnyAsync(wi =>
                wi.WishlistId == wishlist.Id &&
                wi.ProductId == productId);

        if (alreadyExists)
        {
            return Json(new
            {
                success = true,
                exists = true,
                message = "Product is already in your wishlist."
            });
        }


        // Add
        var wishlistItem = new WishlistItem
        {
            WishlistId = wishlist.Id,
            ProductId = productId
        };

        _context.WishlistItems.Add(wishlistItem);

        await _context.SaveChangesAsync();


        return Json(new
        {
            success = true,
            exists = false,
            message = "Product added to wishlist."
        });
    }


    // =========================================================
    // REMOVE - LOGGED IN USER
    // =========================================================

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Remove(int wishlistItemId)
    {
        if (!(User.Identity?.IsAuthenticated ?? false))
        {
            return Unauthorized();
        }

        var user = await _userManager.GetUserAsync(User);

        if (user == null)
            return Unauthorized();


        var wishlistItem = await _context.WishlistItems
            .Include(wi => wi.Wishlist)
            .FirstOrDefaultAsync(wi =>
                wi.Id == wishlistItemId &&
                wi.Wishlist!.UserId == user.Id);

        if (wishlistItem == null)
        {
            return NotFound(new
            {
                success = false,
                message = "Wishlist item not found."
            });
        }


        _context.WishlistItems.Remove(wishlistItem);

        await _context.SaveChangesAsync();


        return Json(new
        {
            success = true,
            message = "Product removed from wishlist."
        });
    }

    // =========================================================
    // TOGGLE - LOGGED IN USER (add if not exists, remove if exists)
    // =========================================================

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Toggle(int productId)
    {
        if (!(User.Identity?.IsAuthenticated ?? false))
        {
            return Unauthorized();
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var product = await _context.Products
            .FirstOrDefaultAsync(p =>
                p.Id == productId &&
                p.IsActive &&
                !p.IsDeleted);

        if (product == null)
        {
            return NotFound(new { success = false, message = "Product not found." });
        }

        var wishlist = await _context.Wishlists
            .Include(w => w.WishlistItems)
            .FirstOrDefaultAsync(w => w.UserId == user.Id);

        if (wishlist == null)
        {
            wishlist = new Wishlist { UserId = user.Id };
            _context.Wishlists.Add(wishlist);
            await _context.SaveChangesAsync();
        }

        var existingItem = wishlist.WishlistItems
            .FirstOrDefault(wi => wi.ProductId == productId);

        bool added;

        if (existingItem != null)
        {
            _context.WishlistItems.Remove(existingItem);
            added = false;
        }
        else
        {
            _context.WishlistItems.Add(new WishlistItem
            {
                WishlistId = wishlist.Id,
                ProductId = productId
            });
            added = true;
        }

        await _context.SaveChangesAsync();

        return Json(new
        {
            success = true,
            added,
            message = added ? "Product added to wishlist." : "Product removed from wishlist."
        });
    }


    // =========================================================
    // GET IDS - for syncing heart icons on listing pages
    // =========================================================

    [HttpGet]
    public async Task<IActionResult> GetIds()
    {
        if (!(User.Identity?.IsAuthenticated ?? false))
        {
            return Json(new { success = true, ids = Array.Empty<int>() });
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Json(new { success = true, ids = Array.Empty<int>() });

        var ids = await _context.WishlistItems
            .Where(wi => wi.Wishlist!.UserId == user.Id)
            .Select(wi => wi.ProductId)
            .ToListAsync();

        return Json(new { success = true, ids });
    }

    // =========================================================
    // MERGE GUEST WISHLIST INTO DATABASE
    // =========================================================

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Merge([FromBody] List<int> productIds)
    {
        if (!(User.Identity?.IsAuthenticated ?? false))
        {
            return Unauthorized();
        }

        var user = await _userManager.GetUserAsync(User);

        if (user == null)
            return Unauthorized();

        if (productIds == null || productIds.Count == 0)
        {
            return Json(new
            {
                success = true,
                message = "Nothing to merge."
            });
        }


        // Get or create wishlist
        var wishlist = await _context.Wishlists
            .Include(w => w.WishlistItems)
            .FirstOrDefaultAsync(w => w.UserId == user.Id);

        if (wishlist == null)
        {
            wishlist = new Wishlist
            {
                UserId = user.Id
            };

            _context.Wishlists.Add(wishlist);

            await _context.SaveChangesAsync();
        }


        // Remove duplicates from localStorage list
        var uniqueProductIds = productIds
            .Distinct()
            .ToList();


        // Existing products in DB
        var existingProductIds = wishlist.WishlistItems
            .Select(x => x.ProductId)
            .ToHashSet();


        // Only valid products
        var validProductIds = await _context.Products
            .Where(p =>
                uniqueProductIds.Contains(p.Id) &&
                p.IsActive &&
                !p.IsDeleted)
            .Select(p => p.Id)
            .ToListAsync();


        foreach (var productId in validProductIds)
        {
            if (existingProductIds.Contains(productId))
                continue;

            wishlist.WishlistItems.Add(new WishlistItem
            {
                ProductId = productId
            });
        }


        await _context.SaveChangesAsync();


        return Json(new
        {
            success = true,
            message = "Wishlist synchronized successfully."
        });
    }
}
