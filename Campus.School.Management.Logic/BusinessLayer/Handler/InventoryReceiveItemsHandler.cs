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
    public class InventoryReceiveItemsHandler : IRepository<InventoryReceiveItemsViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();
        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
        public void Create(InventoryReceiveItemsViewModel model)
        {
            //StockLot stockLot = new StockLot
            //{

            //    DocumentNo = model.DocumentNumber,
            //    LotDate=model.LotDate,
            //    StoreId = model.Store,
            //    PONumber = model.PONumber,
            //    CreatedbyID = _dbUser.ID,
            //    CreatedDate = DateTime.Now,
            //    ModifiedDate = DateTime.Now,
            //    DeletedDate = DateTime.Now,
            //    CompanyID = _dbUser.CompanyID,
            //    SchoolID = _dbUser.SchoolID
            //};

            //stockLot.LotId = model.LotId;

            //foreach (var item in model.ItemsList)
            //{
            //    if (item.ItemId != 0) {

            //        StockItem stockItem = new StockItem
            //        {
            //            ItemId = item.ItemId,
            //            ReceivedQty = item.ReceivedQty,
            //            LotId = model.LotId,
            //            Discount = item.Discount,
            //            CreatedbyID = _dbUser.ID,
            //            CreatedDate = DateTime.Now,
            //            ModifiedDate = DateTime.Now,
            //            DeletedDate = DateTime.Now,
            //        };
                    
            //        //stockLot.StockItems.Add(stockItem);
            //    }
            //};



            //Context.StockLots.Add(stockLot);
            Context.SaveChanges();
        }

        public InventoryReceiveItemsViewModel Create()
        {
            throw new NotImplementedException();
        }

        public bool Delete(int ID)
        {
            throw new NotImplementedException();
        }

        public List<InventoryReceiveItemsViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public InventoryReceiveItemsViewModel GetById(int ID)
        {
            throw new NotImplementedException();
        }

        public InventoryReceiveItemsViewModel Update(InventoryReceiveItemsViewModel model)
        {
            throw new NotImplementedException();
        }
    }

}
