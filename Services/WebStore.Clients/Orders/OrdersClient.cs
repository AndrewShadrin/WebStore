using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebStore.Clients.Base;
using WebStore.Domain;
using WebStore.Domain.DTO.Orders;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Orders
{
    public class OrdersClient : BaseClient, IOrderService
    {
        public OrdersClient(IConfiguration configuration) : base(configuration, WebApi.Orders)
        {
        }

        public async Task<OrderDTO> CreateOrder(string userName, CreateOrderModel orderModel)
        {
            var response = await PostAsync($"{serviceAddress}/{userName}", orderModel);
            return await response.Content.ReadAsAsync<OrderDTO>();
        }

        public async Task<OrderDTO> GetOrderById(int id)
        {
            return await GetAsync<OrderDTO>($"{serviceAddress}/{id}");
        }

        public async Task<IEnumerable<OrderDTO>> GetUserOrders(string userName)
        {
            return await GetAsync<IEnumerable<OrderDTO>>($"{serviceAddress}/user/{userName}");
        }
    }
}
