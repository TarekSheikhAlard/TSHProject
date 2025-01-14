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
    public class AccStudentConfigurationHandler : IRepository<AccStudentConfigrationViewModel>

    {
        private SchoolManagmentDBEntities context = new SchoolManagmentDBEntities();

        public AccStudentConfigrationViewModel Create()
        {
            AccStudentConfigrationViewModel model = new AccStudentConfigrationViewModel();
           
            return model;

        }

        public void Create(AccStudentConfigrationViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            AccStudentConfigration _AccStudentConfig = new AccStudentConfigration()
            {
                AcademicYear = model.AcademicYear,
                StudentAcdID = model.StudentAcdID,


                BusID = model.BusID,
                BusFees = model.BusFees,
                ISbusDiscount = model.ISbusDiscount,
                BusDiscount = model.BusDiscount,
                BusNetPrice = model.BusNetPrice,
                TripType = model.TripType,
                BooksPrice = model.BooksPrice,
                IsBooksDiscount = model.IsBooksDiscount,
                BooksDiscount = model.BooksDiscount,
                BooksNetPrice = model.BooksNetPrice,

                StudyFees = model.StudyFees,
                isStudyDiscount = model.isStudyDiscount,
                StudyFeesDiscount = model.StudyFeesDiscount,
                StudyNetFees = model.StudyNetFees,

                UniformPrice = model.UniformPrice,
                isUniformDiscount = model.isUniformDiscount,
                UniformDiscount = model.UniformDiscount,
                UniformNetPrice = model.UniformNetPrice,

                OperationDate = model.OperationDate,
                TaxRate = model.TaxRate,
                Size=model.Size, CreatedDate=DateTime.Now,
                ModifiedDate =DateTime.Now,
                DeletedDate =DateTime.Now,
                CreatedbyID=_dbUser.ID,
               SchoolID=_dbUser.SchoolID
            };
            context.AccStudentConfigrations.Add(_AccStudentConfig);
            context.SaveChanges();
            
        }



        public AccStudentConfigrationViewModel GetById(long StudentAcdID)
       {
            return context.AccStudentConfigrations.Where(x => x.StudentAcdID == StudentAcdID)
           .Select(x => new AccStudentConfigrationViewModel {ID=x.ID,AcademicYear=x.AcademicYear,
           StudentAcdID=x.StudentAcdID, BooksDiscount=x.BooksDiscount,BooksPrice=x.BooksPrice,TripType =x.TripType,
            IsBooksDiscount=x.IsBooksDiscount.Value,BooksNetPrice=x.BooksNetPrice,BusID=x.BusID, BusDiscount=x.BusDiscount,
            BusFees=x.BusFees,ISbusDiscount=x.ISbusDiscount.Value,BusNetPrice=x.BusNetPrice,StudyFees=x.StudyFees,
            isStudyDiscount=x.isStudyDiscount.Value,StudyFeesDiscount=x.StudyFeesDiscount,StudyNetFees=x.StudyNetFees,
            isUniformDiscount=x.isUniformDiscount.Value,Size=x.Size, UniformDiscount=x.UniformDiscount,UniformPrice=x.UniformPrice,
            UniformNetPrice=x.UniformNetPrice,OperationDate=x.OperationDate, TaxRate=x.TaxRate

           }).FirstOrDefault();

        }

      

        public AccStudentConfigrationViewModel Update(AccStudentConfigrationViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _AccStudentConfig = context.AccStudentConfigrations.Where(x => x.StudentAcdID == model.StudentAcdID).FirstOrDefault();

            _AccStudentConfig.AcademicYear = model.AcademicYear;
            _AccStudentConfig.StudentAcdID = model.StudentAcdID;

            _AccStudentConfig.TripType = model.TripType;
            _AccStudentConfig.BusID = model.BusID;
            _AccStudentConfig. BusFees = model.BusFees;
            _AccStudentConfig.ISbusDiscount = model.ISbusDiscount;
            _AccStudentConfig.BusDiscount = model.BusDiscount;
            _AccStudentConfig.BusNetPrice = model.BusNetPrice;

            _AccStudentConfig.BooksPrice = model.BooksPrice;
            _AccStudentConfig.IsBooksDiscount = model.IsBooksDiscount;
            _AccStudentConfig.BooksDiscount = model.BooksDiscount;
            _AccStudentConfig.BooksNetPrice = model.BooksNetPrice;

            _AccStudentConfig.StudyFees = model.StudyFees;
            _AccStudentConfig.isStudyDiscount = model.isStudyDiscount;
            _AccStudentConfig.StudyFeesDiscount = model.StudyFeesDiscount;
            _AccStudentConfig.StudyNetFees = model.StudyNetFees;

            _AccStudentConfig.UniformPrice = model.UniformPrice;
            _AccStudentConfig.isUniformDiscount = model.isUniformDiscount;
            _AccStudentConfig.UniformDiscount = model.UniformDiscount;
            _AccStudentConfig.UniformNetPrice = model.UniformNetPrice;

            _AccStudentConfig.OperationDate = model.OperationDate;
            _AccStudentConfig.Size = model.Size;
            _AccStudentConfig.ModifiedbyID = _dbUser.ID;
            _AccStudentConfig.TaxRate = model.TaxRate;

            context.SaveChanges();
            return model;


        }


        public bool Delete(int ID)
        {
            throw new NotImplementedException();
        }


        public AccStudentConfigrationViewModel GetById(int ID)
        {
            throw new NotImplementedException();
        }

        public List<AccStudentConfigrationViewModel> GetAll()
        {
            throw new NotImplementedException();
        }




















        public GenericVModel GetIncome(DateTime DateFrom, DateTime DateTo) {
           // List<GenericVModel> list = new List<GenericVModel>();
            List<GenericVModel> _DetailList = new List<GenericVModel>();
            var Studentconfiglist = context.AccStudentConfigrations.Where(x => x.OperationDate >= DateFrom && x.OperationDate <= DateTo).ToList();

            var BusFees = Studentconfiglist.Select(t => t.BusFees ?? 0).Sum();
            var UniformPrice = Studentconfiglist.Select(t => t.UniformPrice ?? 0).Sum();
            var StudyFees = Studentconfiglist.Select(t => t.StudyFees ?? 0).Sum();
            var BooksPrice = Studentconfiglist.Select(t => t.BooksPrice ?? 0).Sum();
            var totalIncom = BusFees + UniformPrice + StudyFees + BooksPrice;
            _DetailList.Add(new ViewModel.GenericVModel() {
                ItemName = "BusFees",
                Value = BusFees,
                TypeID = 1,
                Type = "Income",
            });
            _DetailList.Add(new ViewModel.GenericVModel()
            {
                ItemName = "UniformPrice",
                Value = UniformPrice,
                TypeID = 1,
                Type = "Income",
            });

            _DetailList.Add(new ViewModel.GenericVModel()
            {
                ItemName = "StudyFees",
                Value = StudyFees,
                TypeID = 1,
                Type = "Income",
            });
            _DetailList.Add(new ViewModel.GenericVModel()
            {
                ItemName = "BooksPrice",
                Value = BooksPrice,
                TypeID = 1,
                Type = "Income",
            });

            GenericVModel _listIncome = new GenericVModel()
            {

                ItemName = "Total Income",
                Value = totalIncom,
                TypeID = 1,
                Type = "Total Income",
                _DetailList = _DetailList
            };
            return _listIncome;
        }

        public GenericVModel GetDeduct(DateTime DateFrom, DateTime DateTo)
        {
            //List<GenericVModel> list = new List<GenericVModel>();
            List<GenericVModel> _DetailList = new List<GenericVModel>();
            var Studentconfiglist = context.AccStudentConfigrations.Where(x => x.OperationDate >= DateFrom && x.OperationDate <= DateTo).ToList();

            var BusFees = Studentconfiglist.Select(t => t.BusDiscount ?? 0).Sum();
            var UniformPrice = Studentconfiglist.Select(t => t.UniformDiscount ?? 0).Sum();
            var StudyFees = Studentconfiglist.Select(t => t.StudyFeesDiscount ?? 0).Sum();
            var BooksPrice = Studentconfiglist.Select(t => t.BooksDiscount ?? 0).Sum();
            var totalDeduct = BusFees + UniformPrice + StudyFees + BooksPrice;
            _DetailList.Add(new ViewModel.GenericVModel()
            {
                ItemName = "BusFees",
                Value = BusFees,
                TypeID = 2,
                Type = "Deduct",
            });
            _DetailList.Add(new ViewModel.GenericVModel()
            {
                ItemName = "UniformPrice",
                Value = UniformPrice,
                TypeID = 2,
                Type = "Deduct",
            });

            _DetailList.Add(new ViewModel.GenericVModel()
            {
                ItemName = "StudyFees",
                Value = StudyFees,
                TypeID = 2,
                Type = "Deduct",
            });
            _DetailList.Add(new ViewModel.GenericVModel()
            {
                ItemName = "BooksPrice",
                Value = UniformPrice,
                TypeID = 2,
                Type = "Deduct",
            });

            GenericVModel _listDeduct = new GenericVModel()
            {

                ItemName = " Student Fees Discount",
                Value = totalDeduct,
                TypeID = 2,
                Type = "Expenses",
                _DetailList = _DetailList
            };
            return _listDeduct;
        }
        
      

    }
}
