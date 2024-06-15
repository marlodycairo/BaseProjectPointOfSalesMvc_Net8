using BaseProjectMvc_Net8.Data.Entities;
using BaseProjectMvc_Net8.Extensions;
using BaseProjectMvc_Net8.Models;
using BaseProjectMvc_Net8.Repositories.Interfaces;
using BaseProjectMvc_Net8.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace BaseProjectMvc_Net8.Controllers
{
    public class HomeController(IProductRepository productRepository, ICategoryRepository categoryRepository,
    IHttpContextAccessor httpContext, IInventoryRepository inventoryRepository,
    IInvoiceRepository invoiceRepository,
    IUserService userService) : Controller
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IHttpContextAccessor _httpContext = httpContext;
        private readonly IInventoryRepository _inventoryRepository = inventoryRepository;
        private readonly IInvoiceRepository _invoiceRepository = invoiceRepository;
        private readonly IUserService _userService = userService;

        public List<ItemModel> items = httpContext.HttpContext?.Session.GetObjectFromJson<List<ItemModel>>("Items") ?? [];

        public IActionResult Index()
        {
            var model = new ResponseViewModel
            {
                ProductsList = [],
                ItemsSelected = [],
                ItemSelected = new ItemModel()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SearchProduct([Required]string productName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var products = await _productRepository.GetProductByName(productName);

            var model = new ResponseViewModel
            {
                ProductsList = [.. products.Result!],
                ItemSelected = new ItemModel()
            };

            return View("Index", model);
        }

        [HttpPost]
        public IActionResult AddItem([FromBody]ItemModel itemModel)
        {
            var items = _httpContext.HttpContext?.Session.GetObjectFromJson<List<ItemModel>>("Items") ?? [];

            foreach (var item in items)
            {
                if (item.Product_Name.Equals(itemModel.Product_Name))
                {
                    return View("ItemAlreadyExists", itemModel);
                }
            }

            items.Add(itemModel);

            _httpContext.HttpContext?.Session.SetObjectAsJson("Items", items);

            var model = new ResponseViewModel
            {
                //ItemsSelected = items,
                ProductsList = [],
                ItemSelected = itemModel
            };

            return View("Index", model);
        }


        public IActionResult DeleteItem(Guid id)
        {
            var item = items.Where(x => x.Id == id).FirstOrDefault();

            items.Remove(item!);

            _httpContext.HttpContext?.Session.SetObjectAsJson("Items", items);

            var model = new ResponseViewModel
            {
                ItemSelected = new ItemModel(),
                ProductsList = []
            };

            return View("Index", model);
        }

        [HttpPost]
        public IActionResult CalculateSubtotal([FromBody]ItemModel itemModel)
        {
            var item = items.FirstOrDefault(x => x.Id == itemModel.Id);

            if (item is null)
            {
                return Json(new { success = false, message = "Item not found"});
            }

			item.Quantity = itemModel.Quantity;

            item.Total = items.Sum(x => x.Subtotal);

			_httpContext.HttpContext?.Session.SetObjectAsJson("Items", items);

            TempData["quantity"] = item.Quantity;

            TempData["Subtotal"] = item.Subtotal;

            TempData["Total"] = item.Total;

			return Json(new { success = true, subtotal = itemModel.Subtotal, total = item.Total });
        }

        public async Task<IActionResult> CreateInvoice()
        {
            foreach (var item in items)
            {
                var product = await _inventoryRepository.GetInventoryByProductId(item.Product_Id);

                //actualizar inventario
                product!.Stock = product.Stock - item.Quantity;

				await _inventoryRepository.UpdateInventory(product);
            }

            var model = new ResponseViewModel
            {
                ItemSelected = new ItemModel(),
                ItemsSelected = items,
                ProductsList = []
            };

            ViewBag.Total = $"Total invoice: $ {items.Sum(x => x.Subtotal)}";

            return View("CreateInvoice", model);
        }

        [HttpPost]
        public IActionResult ClearItems()
        {
            _httpContext.HttpContext?.Session.Remove("Items");

            return RedirectToAction("Index", "Home");
        }

		public IActionResult Privacy()
		{
			return View();
		}

        [HttpPost]
		public async Task<IActionResult> Privacy(UserTest test)
        {
            var user = await _userService.AddUser(test);

            ViewBag.Response = $"User {user.Name} created successful";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Beneficiaries(UserTest userTest)
        {
            await _userService.AddBeneficiaries(userTest);

            return View("Privacy");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
