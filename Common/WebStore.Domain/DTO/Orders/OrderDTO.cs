﻿using System;
using System.Collections.Generic;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.DTO.Orders
{
    public class OrderDTO : INamedEntity
    {
        public string Name { get; set; }
        
        public int Id { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public DateTime Date { get; set; }

        public IEnumerable<OrderItemDTO> Items { get; set; } = new List<OrderItemDTO>();
    }
}
