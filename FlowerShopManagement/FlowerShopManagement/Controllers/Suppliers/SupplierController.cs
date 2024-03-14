using FlowerShopBusinessObject.Entities;
using FlowerShopRepository.FlowerBouquets;
using FlowerShopRepository.Suppliers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShopManagement.Controllers.Suppliers
{
    [Route("api/v1/supplier")]
    [ApiController]
    [Authorize(Roles = "1")]
    public class SupplierController : ControllerBase
    {
        private ISupplierRepository _repository;
        public SupplierController(ISupplierRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Supplier>> GetSuppliers() => _repository.GetSuppliers();

        [HttpPost]
        public IActionResult PostSupplier(Supplier supplier)
        {
            _repository.SaveSupplier(supplier);
            return NoContent();
        }
    }
}
