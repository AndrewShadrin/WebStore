using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.DTO.Orders;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        public IActionResult Details() => View(new CartOrderViewModel { Cart = cartService.TransformFromCart() });

        public IActionResult AddToCart(int id)
        {
            cartService.AddToCart(id);
            return RedirectToAction(nameof(Details));
        }

        public IActionResult DecrementFromCart(int id)
        {
            cartService.DecrementFromCart(id);
            return RedirectToAction(nameof(Details));
        }

        public IActionResult RemoveFromCart(int id)
        {
            cartService.RemoveFromCart(id);
            return RedirectToAction(nameof(Details));
        }

        public IActionResult Clear()
        {
            cartService.Clear();
            return RedirectToAction(nameof(Details));
        }

        [Authorize]
        public async Task<IActionResult> CheckOut(OrderViewModel orderModel, [FromServices] IOrderService orderService)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Details), new CartOrderViewModel
                {
                    Cart = cartService.TransformFromCart(),
                    Order = orderModel
                });
            }

            var order_model = new CreateOrderModel
            {
                Order = orderModel,
                Items = cartService.TransformFromCart().Items.Select(item => new OrderItemDTO
                {
                    Id = item.Product.Id,
                    Price = item.Product.Price,
                    Quantity = item.Quantity
                })
            };

            var order = await orderService.CreateOrder(User.Identity.Name, order_model);

            cartService.Clear();

            return RedirectToAction(nameof(OrderConfirmed), new { id = order.Id });
        }

        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }
    }
}
