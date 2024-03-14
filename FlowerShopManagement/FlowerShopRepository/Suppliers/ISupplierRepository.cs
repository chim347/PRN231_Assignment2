using FlowerShopBusinessObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopRepository.Suppliers
{
    public interface ISupplierRepository
    {
        void SaveSupplier(Supplier supplier);
        Supplier GetSupplierById(string id);
        List<Supplier> GetSuppliers();
        void UpdateSupplier(Supplier supplier);
        void DeleteSupplier(Supplier supplier);
        List<FlowerBouquet> GetFlowerBouquets(string supplierId);
    }
}
