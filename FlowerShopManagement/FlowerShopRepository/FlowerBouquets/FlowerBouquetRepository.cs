using FlowerShopBusinessObject.Entities;
using FlowerShopDAO.FlowerBoquets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopRepository.FlowerBouquets
{
    public class FlowerBouquetRepository : IFlowerBouquetRepository
    {
        public void SaveFlowerBouquet(FlowerBouquet flowerBouquet) => FlowerBouquetDAO.Instance.SaveFlowerBouquet(flowerBouquet);
        public FlowerBouquet GetFlowerBouquetById(string id) => FlowerBouquetDAO.Instance.FindFlowerBouquetById(id);
        public List<FlowerBouquet> GetFlowerBouquets() => FlowerBouquetDAO.Instance.GetFlowerBouquets();
        public List<FlowerBouquet> Search(string keyword) => FlowerBouquetDAO.Instance.Search(keyword);
        public void UpdateFlowerBouquet(FlowerBouquet flowerBouquet) => FlowerBouquetDAO.Instance.UpdateFlowerBouquet(flowerBouquet);
        public void DeleteFlowerBouquet(FlowerBouquet flowerBouquet) => FlowerBouquetDAO.Instance.DeleteFlowerBouquet(flowerBouquet);
        //public List<OrderDetail> GetOrderDetails(int flowerBouquetId) => OrderDetailDAO.Instance.FindAllOrderDetailsByFlowerBouquetId(flowerBouquetId);
    }
}
