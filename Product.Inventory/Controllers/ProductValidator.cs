namespace Product.Inventory.Controllers;

using FluentValidation;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(product => product.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(255).WithMessage("Name cannot be longer than 255 characters.");

        RuleFor(product => product.Description)
            .MaximumLength(512).WithMessage("Description cannot be longer than 512 characters.");

        RuleFor(product => product.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative.");
    }
}
