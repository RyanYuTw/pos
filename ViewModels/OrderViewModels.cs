namespace Pos.ViewModels;

public class OrderCreateViewModel
{
    public string TableNo { get; set; } = default!;
    public List<OrderItemViewModel> Items { get; set; } = new();
}

public class OrderItemViewModel
{
    public string ProductNo { get; set; } = default!;
    public string ProductName { get; set; } = default!;
    public float Amount { get; set; }
    public float? Discount { get; set; }
    public float? DiscountPrice { get; set; }
}

public class CheckoutViewModel
{
    public string OrderNo { get; set; } = default!;
    public float? Cash { get; set; }
    public float? CreditCard { get; set; }
    public float? Point { get; set; }
    public float? Coupons { get; set; }
    public float Allowance { get; set; }
}

public class SalesProductViewModel
{
    public string ProductNo { get; set; } = default!;
    public string? ProductName { get; set; }
    public float TotalAmount { get; set; }
    public float TotalPrice { get; set; }
}

public class DailySalesViewModel
{
    public int Day { get; set; }
    public float TotalSales { get; set; }
    public int OrderCount { get; set; }
}
