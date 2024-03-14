using FlowerShopBusinessObject.Entities;
using FlowerShopDAO.FlowerBoquets;
using FlowerShopDAO.Suppliers;

namespace FlowerShopRepository.Suppliers
{
    public class SupplierRepository : ISupplierRepository
    {
        public void SaveSupplier(Supplier supplier) => SupplierDAO.Instance.SaveSupplier(supplier);
        public Supplier GetSupplierById(string id) => SupplierDAO.Instance.FindSupplierById(id);
        public List<Supplier> GetSuppliers() => SupplierDAO.Instance.GetSuppliers();
        public void UpdateSupplier(Supplier supplier) => SupplierDAO.Instance.UpdateSupplier(supplier);
        public void DeleteSupplier(Supplier supplier) => SupplierDAO.Instance.DeleteSupplier(supplier);
        public List<FlowerBouquet> GetFlowerBouquets(string supplierId) => FlowerBouquetDAO.Instance.FindAllFlowerBouquetsBySupplierId(supplierId);
    }
}
