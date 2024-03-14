using FlowerShopBusinessObject.Entities;
using FlowerShopManagement.Models;
using FlowerShopRepository.Categories;
using FlowerShopRepository.FlowerBouquets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShopManagement.Controllers.FlowerBouquets
{
    [Route("api/v1/FlowerBouquet")]
    [ApiController]
    public class FlowerBouquetController : ControllerBase
    {
        private IFlowerBouquetRepository _repository;

        public FlowerBouquetController(IFlowerBouquetRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<FlowerBouquet>> GetFlowerBouquets() => _repository.GetFlowerBouquets();

        [HttpGet("Search/{keyword}")]
        public ActionResult<IEnumerable<FlowerBouquet>> Search(string keyword) => _repository.Search(keyword);

        [HttpGet("{id}")]
        public ActionResult<FlowerBouquet> GetFlowerBouquetById(string id) => _repository.GetFlowerBouquetById(id);

        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult PostFlowerBouquet(CreateFlowerBouquet postFlowerBouquet)
        {
            if (_repository.GetFlowerBouquets().FirstOrDefault(f => f.FlowerBouquetName.ToLower().Equals(postFlowerBouquet.FlowerBouquetName.ToLower())) != null) {
                return BadRequest();
            }
            var f = new FlowerBouquet
            {
                FlowerBouquetName = postFlowerBouquet.FlowerBouquetName,
                Description = postFlowerBouquet.Description,
                UnitPrice = postFlowerBouquet.UnitPrice,
                UnitsInStock = postFlowerBouquet.UnitsInStock,
                FlowerBouquetStatus = postFlowerBouquet.FlowerBouquetStatus,
                CategoryID = Guid.Parse(postFlowerBouquet.CategoryID),
                SupplierID = Guid.Parse(postFlowerBouquet.SupplierID)
            };
            _repository.SaveFlowerBouquet(f);
            return NoContent();
        }

        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public IActionResult DeleteFlowerBouquet(string id)
        {
            var f = _repository.GetFlowerBouquetById(id);
            if (f == null) {
                return NotFound();
            }
            if (f.OrderDetails != null && f.OrderDetails.Count > 0) {
                f.FlowerBouquetStatus = 2;
                _repository.UpdateFlowerBouquet(f);
            }
            else {
                _repository.DeleteFlowerBouquet(f);
            }
            return NoContent();
        }

        [Authorize(Roles = "1")]
        [HttpPut("{id}")]
        public IActionResult PutFlowerBouquet(string id, CreateFlowerBouquet postFlowerBouquet)
        {
            var fTmp = _repository.GetFlowerBouquetById(id);
            if (fTmp == null) {
                return NotFound();
            }

            if (!fTmp.FlowerBouquetName.ToLower().Equals(postFlowerBouquet.FlowerBouquetName.ToLower())
                && _repository.GetFlowerBouquets().FirstOrDefault(f => f.FlowerBouquetName.ToLower().Equals(postFlowerBouquet.FlowerBouquetName.ToLower())) != null) {
                return BadRequest();
            }
            else {
                fTmp.FlowerBouquetName = postFlowerBouquet.FlowerBouquetName;
            }

            fTmp.Description = postFlowerBouquet.Description;
            fTmp.UnitPrice = postFlowerBouquet.UnitPrice;
            fTmp.UnitsInStock = postFlowerBouquet.UnitsInStock;
            fTmp.CategoryID = Guid.Parse(postFlowerBouquet.CategoryID);
            fTmp.SupplierID = Guid.Parse(postFlowerBouquet.SupplierID);

            _repository.UpdateFlowerBouquet(fTmp);
            return NoContent();
        }
    }
}
