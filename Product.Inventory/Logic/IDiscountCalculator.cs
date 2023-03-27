namespace Product.Inventory.Logic;

public class DiscountCalculator
{
    public decimal CalculateDiscount(decimal price, int quantity)
    {
        if (quantity > 100)
            return price * 0.9m;
        
        if (quantity > 10)
            return price * 0.95m;
        
        return price;
    }
}