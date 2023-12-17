using AutoMapper;
using FluentValidation;
using Hazar.BusinessLayer.Abstract;
using Hazar.EntityLayer.DTOs;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Hazar.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IValidator<ProductDTO> _productValidator;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public ProductController(IProductService productService, IValidator<ProductDTO> productValidator, ICategoryService categoryService, IMapper mapper)
        {
            _productService = productService;
            _productValidator = productValidator;
            _categoryService = categoryService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var products = await _productService.GetAllAsync();

            var productsWithCategories = new List<ProductDTO>();

            foreach (var p in products)
            {
                var category = await _categoryService.GetByIdAsync(p.CategoryId);
                var productWithCategory = new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Stock = p.Stock,
                    Price = p.Price,
                    CategoryName = category.Name,
                };
                productsWithCategories.Add(productWithCategory);
            }

            var pagedList = productsWithCategories.ToPagedList(page, 10);
            return View(pagedList);
        }


        public async Task<IActionResult> ProductAdd()
        {
            var categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = categories;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ProductAdd(ProductDTO response)
        {
            var validationResult = _productValidator.Validate(response);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }
            await _productService.CreateAsync(response);
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _productService.GetByIdAsync(id);
            if (response == null)
                return NotFound();
            ViewBag.Categories = await _categoryService.GetAllAsync(); // Assuming you have a category service
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductDTO request)
        {
            var validationResult = _productValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }
            await _productService.UpdateAsync(request);
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _productService.DeleteAsync(id);
            return Json(new { redirectUrl = Url.Action("Index") });
        }

    }
}
