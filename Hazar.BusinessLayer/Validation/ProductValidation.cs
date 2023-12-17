using FluentValidation;
using Hazar.EntityLayer.DTOs;

namespace Hazar.BusinessLayer.Validation
{
    public class ProductValidation : AbstractValidator<ProductDTO>
    {
        public ProductValidation()
        {
            RuleFor(product => product.Name)
                .NotEmpty().WithMessage("Ürün adı boş olamaz")
                .MaximumLength(30).WithMessage("Ürün adı en fazla 30 karakter olmalıdır");

            RuleFor(product => product.Price)
                .NotNull().WithMessage("Ürün fiyatı boş olamaz")
                .GreaterThan(0).WithMessage("Ürün fiyatı sıfırdan büyük olmalıdır");

            RuleFor(product => product.Stock)
                .NotNull().WithMessage("Stok değeri boş olamaz")
                .GreaterThan(0).WithMessage("Stok miktarı sıfırdan büyük olmalıdır");
        }

    }
}
