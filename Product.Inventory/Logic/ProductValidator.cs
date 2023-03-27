namespace Product.Inventory.Logic;

using Controllers;

internal sealed class ProductValidator : IProductValidator
{
    public void Validate(Product product)
    {
        if (product.Price > 100000)
            throw new InvalidOperationException("Price cannot be greater than 100000.");
        
        if (product.Price < 0)
            throw new InvalidOperationException("Price cannot be negative");
    }
}