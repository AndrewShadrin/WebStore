using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Mapping;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Services.InCookies
{
    public class CookiesCartService : ICartService
    {
        private readonly IProductData productData;
        private readonly HttpContextAccessor httpContextAccessor;
        private readonly string cartName;

        public Cart Cart
        {
            get 
            {
                var context = httpContextAccessor.HttpContext;
                var cookies = context.Response.Cookies;
                var cartCookies = context.Request.Cookies[cartName];
                if (cartCookies is null)
                {
                    var cart = new Cart();
                    cookies.Append(cartName, JsonConvert.SerializeObject(cart));
                    return cart;
                }

                ReplaceCookies(cookies, cartCookies);
                return JsonConvert.DeserializeObject<Cart>(cartCookies);
            }
            set => ReplaceCookies(httpContextAccessor.HttpContext.Response.Cookies, JsonConvert.SerializeObject(value));
        }

        private void ReplaceCookies(IResponseCookies cookies, string cookie)
        {
            cookies.Delete(cartName);
            cookies.Append(cartName, cookie);
        }

        public CookiesCartService(IProductData productData, HttpContextAccessor httpContextAccessor)
        {
            this.productData = productData;
            this.httpContextAccessor = httpContextAccessor;

            var user = httpContextAccessor.HttpContext.User;
            var userName = user.Identity.IsAuthenticated ? $"[{user.Identity.Name}]" : null;
            cartName = $"WebStore.Cart{userName}";
        }

        public void AddToCart(int id)
        {
            var cart = Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null)
            {
                cart.Items.Add(new CartItem { ProductId = id, Quantity = 1 });
            }
            else
            {
                item.Quantity++;
            }
            
            Cart = cart;
        }

        public void Clear()
        {
            var cart = Cart;

            cart.Items.Clear();

            Cart = cart;
        }

        public void DecrementFromCart(int id)
        {
            var cart = Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null) return;

            if (item.Quantity > 0)
            {
                item.Quantity--;
            }

            if (item.Quantity == 0)
            {
                cart.Items.Remove(item);
            }
            Cart = cart;
        }

        public void RemoveFromCart(int id)
        {
            var cart = Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null) return;

            cart.Items.Remove(item);

            Cart = cart;
        }

        public CartViewModel TransformFromCart()
        {
            var products = productData.GetProducts(new ProductFilter
            {
                Ids = Cart.Items.Select(item => item.ProductId).ToArray()
            });

            var productsViewModels = products.ToView().ToDictionary(p => p.Id);

            return new CartViewModel
            {
                Items = Cart.Items.Select(item => (productsViewModels[item.ProductId], item.Quantity))
            };
        }
    }
}
