using Campus.School.Management.Logic.BusinessLayer.Abstract;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.Handler
{
    public class ItemsHandler : IRepository<ItemViewModel>
    {
        private SchoolManagmentDBEntities context = new SchoolManagmentDBEntities();

        public void Create(ItemViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            InventoryItem item = new InventoryItem
            {
                ItemId = model.ItemId,
                ItemNameEn = model.nameEn,
                ItemNameAr = model.nameAr,

                ItemInventoryStructureId = model.ItemInventoryStructureId,
                ItemNumber = model.ItemNumber,
                PlaceofManufacture = model.PlaceofManufacture,
                Material = model.material,

                Model = model.ItemModel,
                TotalCost = model.TotalCost,
                purchasingCost = model.purchasingCost,
                lastPurchasingCost = model.lastPurchasingCost,
                unitNumber = model.unitNumber,
                barcode = model.barcode,
                exchangeRate = model.exchangeRate,
                unitPrice = model.unitPrice,
                wholesalePrice = model.wholeSalePrice,
                StoreNumber = model.storeName,

                ModifiedDate = DateTime.Now,
                CreatedDate = DateTime.Now,
                CreatedbyID = _dbUser.ID,
                DeletedDate = DateTime.Now,
                SchoolID = _dbUser.SchoolID,
                CompanyID = _dbUser.CompanyID,


            };
            context.InventoryItems.Add(item);
            context.SaveChanges();



        }

        public ItemViewModel Create()
        {
            throw new NotImplementedException();
        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var item = context.InventoryItems.Where(x => x.ItemId == ID && x.IsDeleted == false && x.SchoolID == _dbUser.SchoolID).FirstOrDefault();
            if (item != null)
            {
                item.DeletedbyID = _dbUser.ID;
                item.IsDeleted = true;
                item.DeletedDate = DateTime.Now;
                context.SaveChanges();
                return true;
            }
            else return false;
        }

        public List<ItemViewModel> GetAll()
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            return context.InventoryItems.Where(x => x.IsDeleted == false && x.SchoolID == _dbUser.SchoolID).Select(x => new ItemViewModel
            {
                ItemId = x.ItemId,
                nameEn = x.ItemNameEn,
                nameAr = x.ItemNameAr,
                ItemInventoryStructureId = x.ItemInventoryStructureId,
                ItemNumber = x.ItemNumber,
                PlaceofManufacture = x.PlaceofManufacture,
                material = x.Material,
                ItemModel = x.Model,
                TotalCost = x.TotalCost,
                purchasingCost = x.purchasingCost,
                lastPurchasingCost = x.lastPurchasingCost,
                unitNumber = x.unitNumber,
                barcode = x.barcode,
                exchangeRate = x.exchangeRate,
                unitPrice = x.unitPrice,
                wholeSalePrice = x.wholesalePrice,
                storeName = x.StoreNumber


            }).ToList();

        }
        public ItemViewModel GetByBarcode(string barcode)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            return context.InventoryItems.Where(x => x.barcode == barcode && x.IsDeleted == false && x.SchoolID == _dbUser.SchoolID).Select(x => new ItemViewModel
            {
                ItemId = x.ItemId,
                nameEn = x.ItemNameEn,
                nameAr = x.ItemNameAr,
                ItemInventoryStructureId = x.ItemInventoryStructureId,
                ItemNumber = x.ItemNumber,
                PlaceofManufacture = x.PlaceofManufacture,
                material = x.Material,
                ItemModel = x.Model,
                TotalCost = x.TotalCost,
                purchasingCost = x.purchasingCost,
                lastPurchasingCost = x.lastPurchasingCost,
                unitNumber = x.unitNumber,
                barcode = x.barcode,
                exchangeRate = x.exchangeRate,
                unitPrice = x.unitPrice,
                wholeSalePrice = x.wholesalePrice,
                storeName = x.StoreNumber


            }).FirstOrDefault();
        }
        public ItemViewModel GetById(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            return context.InventoryItems.Where(x => x.ItemId == ID && x.IsDeleted == false && x.SchoolID == _dbUser.SchoolID).Select(x => new ItemViewModel
            {
                ItemId = x.ItemId,
                nameEn = x.ItemNameEn,
                nameAr = x.ItemNameAr,
                ItemInventoryStructureId = x.ItemInventoryStructureId,
                ItemNumber = x.ItemNumber,
                PlaceofManufacture = x.PlaceofManufacture,
                material = x.Material,
                ItemModel = x.Model,
                TotalCost = x.TotalCost,
                purchasingCost = x.purchasingCost,
                lastPurchasingCost = x.lastPurchasingCost,
                unitNumber = x.unitNumber,
                barcode = x.barcode,
                exchangeRate = x.exchangeRate,
                unitPrice = x.unitPrice,
                wholeSalePrice = x.wholesalePrice,
                storeName = x.StoreNumber


            }).FirstOrDefault();
        }

        public ItemViewModel Update(ItemViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var items = context.InventoryItems.Where(x => x.ItemId == model.ItemId).FirstOrDefault();
            items.ItemNameEn = model.nameEn;
            items.ItemNameAr = model.nameAr;
            items.ItemNumber = model.ItemNumber;

            items.ItemInventoryStructureId = model.ItemInventoryStructureId;

            items.PlaceofManufacture = model.PlaceofManufacture;
            items.Material = model.material;

            items.Model = model.ItemModel;
            items.TotalCost = model.TotalCost;
            items.purchasingCost = model.purchasingCost;
            items.lastPurchasingCost = model.lastPurchasingCost;
            items.unitNumber = model.unitNumber;
            items.barcode = model.barcode;
            items.exchangeRate = model.exchangeRate;
            items.unitPrice = model.unitPrice;
            items.wholesalePrice = model.wholeSalePrice;
            items.StoreNumber = model.storeName;
            items.ModifiedDate = DateTime.Now;
            items.ModifiedbyID = _dbUser.ID;

            context.SaveChanges();
            return model;
        }
    }
}
