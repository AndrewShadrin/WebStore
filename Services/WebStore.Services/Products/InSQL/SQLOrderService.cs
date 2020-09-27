using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.DTO.Orders;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Entities.Orders;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Services.Products.InSQL
{
    public class SQLOrderService : IOrderService
    {
        private readonly WebStoreDB dB;
        private readonly UserManager<User> userManager;

        public SQLOrderService(WebStoreDB dB, UserManager<User> userManager)
        {
            this.dB = dB;
            this.userManager = userManager;
        }

        public async Task<OrderDTO> CreateOrder(string userName, CreateOrderModel orderModel)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user is null)
            {
                throw new InvalidOperationException($"Пользователь {userName} не найден!");
            }

            await using (var transaction = await dB.Database.BeginTransactionAsync())
            {
                var order = new Order
                {
                    Name = orderModel.Order.Name,
                    Address = orderModel.Order.Adress,
                    Phone = orderModel.Order.Phone,
                    Date = DateTime.Now,
                    User = user
                };

                foreach (var item in orderModel.Items)
                {
                    var product = await dB.Products.FindAsync(item.Id);
                    if (product is null)
                    {
                        continue;
                    }

                    var orderItem = new OrderItem
                    {
                        Order = order,
                        Product = product,
                        Quantity = item.Quantity,
                        Price = product.Price
                    };
                    order.Items.Add(orderItem);
                }
                await dB.Orders.AddAsync(order);
                await dB.SaveChangesAsync();
                await transaction.CommitAsync();

                return order.ToDTO();
            }
        }

        public async Task<OrderDTO> GetOrderById(int id)
        {
            return (await dB.Orders
                .Include(order => order.User)
                .FirstOrDefaultAsync(order => order.Id == id)).ToDTO();
        }

        public async Task<IEnumerable<OrderDTO>> GetUserOrders(string userName)
        {
            return (await dB.Orders
                .Include(order => order.User)
                .Include(order => order.Items)
                .Where(order => order.User.UserName == userName)
                .ToArrayAsync()).Select(o => o.ToDTO());
        }
    }
}
