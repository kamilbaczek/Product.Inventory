namespace Product.Inventory.Logic;

using Controllers;

public sealed class ProductValidator
{ 
    public void Validate(ProductItem product)
    {
        if (product.Price > 100000)
            throw new InvalidOperationException("Price cannot be greater than 100000.");
        
        if (product.Price < 0)
            throw new InvalidOperationException("Price cannot be negative");
    }
}