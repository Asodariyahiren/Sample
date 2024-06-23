using FoodApplication.Data;
using FoodApplication.Models;
using FoodApplication.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace FoodApplication.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IData data;
        private readonly FoodDBContext context;
        public CartController(IData data, FoodDBContext context)
        {
            this.data = data;
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            var user = await data.GetUser(HttpContext.User);
            var cartList = context.Carts.Where(c => c.UserId == user.Id).ToList();
            return View(cartList);
        }
        [HttpPost]
        public async Task<IActionResult> SaveCart(Cart cart)
        {
            var user = await data.GetUser(HttpContext.User);
            cart.UserId = user.Id;
            if (ModelState.IsValid)
            {
                await context.Carts.AddAsync(cart);
                await context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAddedCarts()
        {
            var user = await data.GetUser(HttpContext.User);
            var carts = context.Carts.Where(c => c.UserId == user.Id).Select(c => c.RecipeId).ToList();
            return Ok(carts);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCartFromList(string Id)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                var cart = await context.Carts.Where(c => c.RecipeId == Id).FirstOrDefaultAsync();
                if (cart != null)
                {
                    context.Carts.Remove(cart);
                    await context.SaveChangesAsync();
                    return Ok();
                }
            }
            return BadRequest();
        }
        [HttpGet]

        public async Task<IActionResult> GetCartList()
        {
            var user = await data.GetUser(HttpContext.User);
            var cartList = context.Carts.Where(x => x.UserId == user.Id).Take(3).ToList();
            return PartialView("_CartList", cartList);
        }
    }
}
