using CoffeeShopBLL.ModelVMs.Cart;
using CoffeeShopBLL.ModelVMs.CartItem;

public class OrderDetailsVM
{
    public int OrderId { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal TotalPrice { get; set; }

    public List<CartItemVM> Items { get; set; }
}
