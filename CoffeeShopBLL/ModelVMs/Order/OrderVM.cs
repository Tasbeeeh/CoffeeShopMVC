using CoffeeShopDAL.Entities;
using CoffeeShopDAL.Entities.Enums;
using System;

namespace CoffeeShopBLL.ModelVMs.Order
{
    public class OrderVM
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public int UserId { get; set; }
        public int CartId { get; set; }
    }
}