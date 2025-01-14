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
   public class PurchaseOrderHandler : IRepository<PurchaseOrdersViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();
        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

       
        public void Create(PurchaseOrdersViewModel model)
        {
            PurchaseOrder purchaseOrder = new PurchaseOrder
            {
                PONo = model.PONo,
                Supplier = model.Supplier,
                Tax = model.Tax,
                Currency = model.Currency,
                PODate = model.PODate,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                CompanyID = _dbUser.CompanyID,
                SchoolID = _dbUser.SchoolID
            };
            purchaseOrder.ID = model.POId;

            foreach (var item in model.POItemsList)
            {
                if (item.ItemId != 0)
                {
                    PurchaseOrderDetail detail = new  PurchaseOrderDetail
                    {
                        ItemId = item.ItemId,
                      
                        CreatedbyID = _dbUser.ID,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        DeletedDate = DateTime.Now,
                    };

                }
            };
        }

        public PurchaseOrdersViewModel Create()
        {
            string query = @"SELECT CAST(IDENT_CURRENT('PurchaseOrders') AS NVARCHAR(MAX)) PONo";


            var id = Context.Database.SqlQuery<string>(query).SingleOrDefault();
            PurchaseOrdersViewModel model = new PurchaseOrdersViewModel();
            model.PONo = "PO#"+ id.ToString();
            ///model.PODate = DateTime.Now;
            return model;
        }

        public bool Delete(int ID)
        {
            throw new NotImplementedException();
        }

        public List<PurchaseOrdersViewModel> GetAll()
        {
            return Context.PurchaseOrders.Where(x => x.SchoolID == _dbUser.SchoolID && x.CompanyID == _dbUser.CompanyID)
                                .Select(x => new PurchaseOrdersViewModel {
                                    PONo =x.PONo,
                                    PODate=x.PODate,
                                    Supplier=x.Supplier,
                                    TotalCredit=x.TotalCredit
                                }).ToList();      

        }

        public PurchaseOrdersViewModel GetById(int ID)
        {
            throw new NotImplementedException();
        }

        public PurchaseOrdersViewModel Update(PurchaseOrdersViewModel model)
        {
            throw new NotImplementedException();
        }

     
    }
}
