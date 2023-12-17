using FluentValidation;
using Hazar.EntityLayer.DTOs;

namespace Hazar.BusinessLayer.Validation
{
    public class CategoryValidation : AbstractValidator<CategoryDTO>
    {
        public CategoryValidation()
        {
            RuleFor(category => category.Name)
                .NotEmpty().WithMessage("Kategori adı boş olamaz")
                .MaximumLength(30).WithMessage("Kategori adı en fazla 35 karakter olmalıdır");
            RuleFor(category => category.Description)
                .NotEmpty().WithMessage("Description adı boş olamaz")
                .MaximumLength(50).WithMessage("Description adı en fazla 50 karakter olmalıdır");
        }
    }
}
