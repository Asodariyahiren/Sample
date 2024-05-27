using FoodApplication.Data;
using FoodApplication.Models;
using FoodApplication.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            return View();
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
    }
}
