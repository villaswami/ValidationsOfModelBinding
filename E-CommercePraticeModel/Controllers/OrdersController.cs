using E_CommercePraticeModel.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_CommercePraticeModel.Controllers
{
    public class OrdersController : Controller
    {
        [Route("order")]
        public IActionResult Index([Bind(nameof(Order.Products))] Order order)
        {
            if (!ModelState.IsValid)
            {
                string messages = string.Join("\n", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest(messages);
            }
            Random random = new Random();
            int randomNumber = random.Next(0, 9999);
            return Json(new {orderNumber = randomNumber});
        }
    }
}
