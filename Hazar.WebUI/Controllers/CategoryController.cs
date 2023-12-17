using FluentValidation;
using Hazar.BusinessLayer.Abstract;
using Hazar.EntityLayer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Hazar.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IValidator<CategoryDTO> _categoryValidator;

        public CategoryController(ICategoryService categoryService, IValidator<CategoryDTO> categoryValidator)
        {
            _categoryService = categoryService;
            _categoryValidator = categoryValidator;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }

        public async Task<IActionResult> CategoryAdd()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CategoryAdd(CategoryDTO response)
        {
            var validationResult = _categoryValidator.Validate(response);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }
            await _categoryService.CreateAsync(response); // await kullanımı
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _categoryService.GetByIdAsync(id);
            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryDTO request)
        {
            var validationResult = _categoryValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }
            await _categoryService.UpdateAsync(request);
            return RedirectToAction(nameof(Index));

        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _categoryService.DeleteAsync(id);
            return Json(new { redirectUrl = Url.Action("Index") });
        }

    }
}
