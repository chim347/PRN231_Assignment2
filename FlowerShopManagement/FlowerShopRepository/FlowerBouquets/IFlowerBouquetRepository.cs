using FlowerShopBusinessObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopRepository.FlowerBouquets
{
    public interface IFlowerBouquetRepository
    {
        void SaveFlowerBouquet(FlowerBouquet flowerBouquet);
        FlowerBouquet GetFlowerBouquetById(string id);
        List<FlowerBouquet> GetFlowerBouquets();
        List<FlowerBouquet> Search(string keyword);
        void UpdateFlowerBouquet(FlowerBouquet flowerBouquet);
        void DeleteFlowerBouquet(FlowerBouquet flowerBouquet);
        //List<OrderDetail> GetOrderDetails(int flowerBouquetId);
    }
}
