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
    public class SupplierFormHandler : IRepository<SupplierFormViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();
        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

        public void Create(SupplierFormViewModel model)
        {
            var total = 0;
            SupplierForm formHead = new SupplierForm
            {
                SupplierId = model.SupplierId,
                Status = model.Status,
                Type = model.Type,
                Validity = model.Validity,
                CreatedbyID = _dbUser.ID,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                SchoolID = _dbUser.SchoolID


            };
            foreach (var item in model.ItemsList)
            {
                if (item.ItemId != 0)
                {
                    SupplierFormDetail formDetail = new SupplierFormDetail
                    {
                        FormId = model.ID,
                        ItemId = item.ItemId,
                        Description = item.Description,
                        UnitPrice = item.UnitPrice,
                        Qty = item.Qty,
                        CreatedbyID = _dbUser.ID,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        DeletedDate = DateTime.Now,
                        IsClosed = false

                    };

                    formHead.SupplierFormDetails.Add(formDetail);
                   // total = Convert.ToDecimal(item.Qty);

                }
            }

            Context.SupplierForms.Add(formHead);
            Context.SaveChanges();
        }

        public SupplierFormViewModel Create()
        {
            throw new NotImplementedException();
        }

        public bool Delete(int ID)
        {
            throw new NotImplementedException();
        }

        public List<SupplierFormViewModel> GetAll()
        {
            return Context.SupplierForms.Where(x => x.SchoolID == _dbUser.SchoolID && x.IsDeleted == false)
                .Select(x => new SupplierFormViewModel
                {
                    ID = x.ID,
                    Status = x.Status,
                    Type = x.Type,
                    CreatedDate = x.CreatedDate,
                    IsClosed = x.IsClosed
                }).ToList();
        }
        public List<SupplierFormViewModel> GetAllBySupplier(int ID)
        {
            return Context.SupplierForms.Where(x => x.SchoolID == _dbUser.SchoolID && x.IsDeleted == false && x.SupplierId == ID)
                .Select(x => new SupplierFormViewModel
                {
                    ID = x.ID,
                    Status = x.Status,
                    Type = x.Type,
                    CreatedDate = x.CreatedDate,
                    IsClosed = x.IsClosed
                }).ToList();
        }

        public SupplierFormViewModel GetById(int ID)
        {
            return Context.SupplierForms.Where(x => x.SchoolID == _dbUser.SchoolID && x.IsDeleted == false && x.ID == ID)
                .Select(x => new SupplierFormViewModel
                {
                    ID = x.ID,
                    Status = x.Status,
                    Type = x.Type,
                    SupplierId = x.SupplierId,
                    SupplierName=x.AccountSupplier.SupplierName,
                    Address=x.AccountSupplier.Address,
                    Email=x.AccountSupplier.Email,
                    CreatedbyID=x.CreatedbyID,
                    Validity=x.Validity,
                  
                    ItemsList = x.SupplierFormDetails.Select(c => new FormDetails
                    {
                        ID = c.ID,
                        FormId = c.FormId,
                        ItemId = c.ItemId,
                        itemName = c.InventoryItem.ItemNameEn,
                        Description = c.Description,
                        UnitPrice = c.UnitPrice,
                        Qty = c.Qty,
                        Amount = c.Amount
                    }).ToList(),
                    CreatedDate = x.CreatedDate,
                    IsClosed = x.IsClosed
                }).SingleOrDefault();
        }
        public bool ChangeStatus(int id, char Status)
        {
            var supplierForm = Context.SupplierForms.Where(c => c.ID == id && c.SchoolID == _dbUser.SchoolID).FirstOrDefault();
            supplierForm.Status = Status.ToString();
            Context.SaveChanges();
            return true;

        }
        public bool ChangeType(int id)
        {
            var supplierForm = Context.SupplierForms.Where(c => c.ID == id && c.SchoolID == _dbUser.SchoolID && c.Status=="A").FirstOrDefault();
            supplierForm.Type = "P";
            supplierForm.Status = "C";
            Context.SaveChanges();
           
            return true;

        }

        public SupplierFormViewModel Update(SupplierFormViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
