namespace Product.Inventory.Logic;

using Controllers;

public interface IProductValidator
{
    void Validate(Product product);
}