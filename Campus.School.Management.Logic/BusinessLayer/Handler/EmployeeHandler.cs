using Campus.School.Management.Logic.BusinessLayer.Abstract;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Campus.School.Management.Logic.BusinessLayer.Handler
{
    public class EmployeeHandler : IRepository<EmployeeViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();
        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

        public EmployeeViewModel Update(EmployeeViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _Employee = Context.AdmEmployees.Where(c => c.Employee_ID == model.Employee_ID).FirstOrDefault();
            EmployeeAddedBenefitsOther benefitsOther = new EmployeeAddedBenefitsOther();


            if (_Employee != null)
            {
                string employeeAddedBenefitsValues = string.Format("{0},{1},{2},{3}", model.BenefitID, model.DeductID, model.TaxesID, model.GovID);
                int Val = 0;
                _Employee.Address = model.Address;
                _Employee.BirthDate = model.BirthDate;
                //_Employee.DeptID = model.DeptID;
                _Employee.Mail = model.Mail;
                _Employee.NameA = model.NameAr;
                _Employee.NameE = model.NameEn;
                _Employee.WorkDate = model.WorkDate;

                _Employee.Mobile = model.Mobile;
                _Employee.JobID = model.jobID;
                _Employee.National_ID = model.National_ID;
                _Employee.Tel = model.Tel;
                _Employee.NationalityID = model.NationalityID;
                _Employee.ImgUrl = model.ImgUrl;
                _Employee.ModifiedbyID = _dbUser.ID;
                _Employee.ModifiedDate = DateTime.Now;
                _Employee.Notes = model.Notes;
                _Employee.passportpleace = model.passportpleace;
                _Employee.religion = model.religion;
                _Employee.sex = model.sex;
                _Employee.visadate = model.visadate;
                _Employee.visaNumber = model.visaNumber;
                _Employee.visajob = model.visajob;
                _Employee.scientificcertificate = model.scientificcertificate;
                _Employee.NumberEnterBorder = model.NumberEnterBorder;
                _Employee.passportNumber = model.passportNumber;
                _Employee.childNumber = model.childNumber;
                _Employee.companionscount = model.companionscount;
                _Employee.ContractDuration = model.ContractDuration;
                _Employee.EmployeeStatus = model.EmployeeStatus;
                _Employee.EmployeeNumber = model.EmployeeNumber;
                _Employee.BirthPlace = model.BirthPlace;
                _Employee.Maritalstatus = model.Maritalstatus;
                _Employee.Educationallevel = model.Educationallevel;
                _Employee.EnteranceDate = model.EnteranceDate;
                _Employee.EnteranceBorder = model.EnteranceBorder;
                _Employee.EndNational_ID_date = model.EndNational_ID_date;
                _Employee.JoblicenseNumber = model.JoblicenseNumber;
                _Employee.Endpassport_date = model.Endpassport_date;
                _Employee.ReleaseNational_ID_date = model.ReleaseNational_ID_date;
                _Employee.Releasepassport_date = model.Releasepassport_date;
                _Employee.pleaceNational_ID = model.pleaceNational_ID;
                _Employee.BasePay = model.BasePay;
                _Employee.WorkHrs = model.WorkHrs;
                var employeeBenefits = Context.EmployeeAddedBenefitsOthers.Where(c => c.EmployeeId == model.Employee_ID).ToList();
                employeeBenefits.ForEach(x => { x.IsDeleted = true; x.DeletedbyID = _dbUser.ID; x.DeletedDate = DateTime.Now; });

                foreach (var value in employeeAddedBenefitsValues.Split(','))
                {
                    if (!string.IsNullOrWhiteSpace(value) && !string.IsNullOrEmpty(value))
                    {
                        Val = int.Parse(value);

                       benefitsOther = employeeBenefits.Where(x => x.EmployeeId == model.Employee_ID && x.EmployeeBenefitsOtherId == Val).SingleOrDefault();

                        if (benefitsOther != null)
                        {
                            benefitsOther.IsDeleted = false;
                            benefitsOther.DeletedDate = benefitsOther.CreatedDate;
                            benefitsOther.DeletedbyID = null;
                            benefitsOther.ModifiedDate = DateTime.Now;
                            benefitsOther.ModifiedbyID =_dbUser.ID;
                        }
                        else {

                            EmployeeAddedBenefitsOther employeeAddedBenefitsOther = new EmployeeAddedBenefitsOther
                            {
                                EmployeeId = model.Employee_ID,
                                EmployeeBenefitsOtherId = int.Parse(value),
                                CreatedbyID = _dbUser.ID,
                                CreatedDate = DateTime.Now,
                                ModifiedDate = DateTime.Now,
                                DeletedDate = DateTime.Now
                            };

                            Context.EmployeeAddedBenefitsOthers.Add(employeeAddedBenefitsOther);
                        }

                    }
                }



                Context.SaveChanges();
            }
            return model;

        }

        public void UpdatePayroll(EmployeePayrollSetupViewModel model) {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            EmployeeAddedBenefitsOther benefitsOther = new EmployeeAddedBenefitsOther();
            int Val = 0;
            string employeeAddedBenefitsValues = string.Format("{0},{1},{2},{3}", model.BenefitID, model.DeductID, model.TaxesID, model.GovID);

            var employeeBenefits = Context.EmployeeAddedBenefitsOthers.Where(c => c.EmployeeId == model.Employee_ID).ToList();
            employeeBenefits.ForEach(x => { x.IsDeleted = true; x.DeletedbyID = _dbUser.ID; x.DeletedDate = DateTime.Now; });

            foreach (var value in employeeAddedBenefitsValues.Split(','))
            {
                if (!string.IsNullOrWhiteSpace(value) && !string.IsNullOrEmpty(value))
                {
                    Val = int.Parse(value);

                    benefitsOther = employeeBenefits.Where(x => x.EmployeeId == model.Employee_ID && x.EmployeeBenefitsOtherId == Val).SingleOrDefault();

                    if (benefitsOther != null)
                    {
                        benefitsOther.IsDeleted = false;
                        benefitsOther.DeletedDate = benefitsOther.CreatedDate;
                        benefitsOther.DeletedbyID = null;
                        benefitsOther.ModifiedDate = DateTime.Now;
                        benefitsOther.ModifiedbyID = _dbUser.ID;
                    }
                    else
                    {

                        EmployeeAddedBenefitsOther employeeAddedBenefitsOther = new EmployeeAddedBenefitsOther
                        {
                            EmployeeId = model.Employee_ID,
                            EmployeeBenefitsOtherId = int.Parse(value),
                            CreatedbyID = _dbUser.ID,
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                            DeletedDate = DateTime.Now
                        };

                        Context.EmployeeAddedBenefitsOthers.Add(employeeAddedBenefitsOther);
                    }

                }
            }



            Context.SaveChanges();


        }

        public List<EmployeeViewModel> GetAll()
        {
            return Context.AdmEmployees.Where(x => x.IsDeleted == false).Select(c => new EmployeeViewModel
            {
                Address = c.Address,
                BirthDate = c.BirthDate,
                DeptID = c.AdmJob.DepartmentTree.DepartmentTreeID,
                Mail = c.Mail,
                NameAr = c.NameA,
                NameEn = c.NameE,
                WorkDate = c.WorkDate,
                Mobile = c.Mobile,
                jobID = c.JobID,
                National_ID = c.National_ID,
                Tel = c.Tel,
                Employee_ID = c.Employee_ID,
                NationalityID = c.NationalityID,
                DepartmentName = c.AdmJob.DepartmentTree.DepartmentNameEN,
                IsNew = false,
                jobName = c.AdmJob.JobNameEn,
                NationalityName = c.Nationality.NationalityEn,
                ImgUrl = c.ImgUrl,
                BirthPlace = c.BirthPlace,
                childNumber = c.childNumber,
                companionscount = c.companionscount,
                ContractDuration = c.ContractDuration,
                Educationallevel = c.Educationallevel,
                EmployeeNumber = c.EmployeeNumber,
                EmployeeStatus = c.EmployeeStatus,
                EndNational_ID_date = c.EndNational_ID_date,
                Endpassport_date = c.Endpassport_date,
                EnteranceBorder = c.EnteranceBorder,
                EnteranceDate = c.EnteranceDate,
                JoblicenseNumber = c.JoblicenseNumber,
                Maritalstatus = c.Maritalstatus,
                Notes = c.Notes,
                pleaceNational_ID = c.pleaceNational_ID,
                NumberEnterBorder = c.NumberEnterBorder,
                passportNumber = c.passportNumber,
                passportpleace = c.passportpleace,
                ReleaseNational_ID_date = c.ReleaseNational_ID_date,
                Releasepassport_date = c.Releasepassport_date,
                religion = c.religion,
                scientificcertificate = c.scientificcertificate,
                sex = c.sex,
                visadate = c.visadate,
                visajob = c.visajob,
                visaNumber = c.visaNumber,
                IsTerminated = c.IsTerminated

            }).ToList();

        }

        public List<EmployeeViewModel> GetAllByCompany(int companyId, int schoolId)
        {
            return Context.AdmEmployees.Where(x => x.IsDeleted == false
                                                 && x.CompanyID == companyId
                                                 && x.SchoolID == schoolId

                                                 )
                                       .Select(c => new EmployeeViewModel
                                       {
                                           Address = c.Address,
                                           BirthDate = c.BirthDate,
                                           DeptID = c.AdmJob.DepartmentTree.DepartmentTreeID,
                                           Mail = c.Mail,
                                           NameAr = c.NameA,
                                           NameEn = c.NameE,
                                           WorkDate = c.WorkDate,
                                           Mobile = c.Mobile,
                                           jobID = c.JobID,
                                           National_ID = c.National_ID,
                                           Tel = c.Tel,
                                           Employee_ID = c.Employee_ID,
                                           NationalityID = c.NationalityID,
                                           DepartmentName = c.AdmJob.DepartmentTree.DepartmentNameEN,
                                           IsNew = false,
                                           jobName = c.AdmJob.JobNameEn,
                                           NationalityName = c.Nationality.NationalityEn,
                                           ImgUrl = c.ImgUrl,
                                           BirthPlace = c.BirthPlace,
                                           childNumber = c.childNumber,
                                           companionscount = c.companionscount,
                                           ContractDuration = c.ContractDuration,
                                           Educationallevel = c.Educationallevel,
                                           EmployeeNumber = c.EmployeeNumber,
                                           EmployeeStatus = c.EmployeeStatus,
                                           EndNational_ID_date = c.EndNational_ID_date,
                                           Endpassport_date = c.Endpassport_date,
                                           EnteranceBorder = c.EnteranceBorder,
                                           EnteranceDate = c.EnteranceDate,
                                           JoblicenseNumber = c.JoblicenseNumber,
                                           Maritalstatus = c.Maritalstatus,
                                           Notes = c.Notes,
                                           pleaceNational_ID = c.pleaceNational_ID,
                                           NumberEnterBorder = c.NumberEnterBorder,
                                           passportNumber = c.passportNumber,
                                           passportpleace = c.passportpleace,
                                           ReleaseNational_ID_date = c.ReleaseNational_ID_date,
                                           Releasepassport_date = c.Releasepassport_date,
                                           religion = c.religion,
                                           scientificcertificate = c.scientificcertificate,
                                           sex = c.sex,
                                           visadate = c.visadate,
                                           visajob = c.visajob,
                                           visaNumber = c.visaNumber,
                                           IsTerminated = c.IsTerminated,
                                           WorkHrs =c.WorkHrs
                                           
                                       }).ToList();

        }

        public EmployeeViewModel GetById(int ID)
        {
            return Context.AdmEmployees.Where(c => c.Employee_ID == ID && c.IsDeleted == false && c.SchoolID == _dbUser.SchoolID && c.CompanyID == _dbUser.CompanyID).Select(c => new EmployeeViewModel
            {
                Address = c.Address,
                BirthDate = c.BirthDate,
                DeptID = c.AdmJob.DepartmentTree.DepartmentTreeID,
                Mail = c.Mail,
                NameAr = c.NameA,
                NameEn = c.NameE,
                WorkDate = c.WorkDate,
                Mobile = c.Mobile,
                jobID = c.JobID,
                National_ID = c.National_ID,
                Tel = c.Tel,
                Employee_ID = c.Employee_ID,
                NationalityID = c.NationalityID,
                DepartmentName = c.AdmJob.DepartmentTree.DepartmentNameEN,
                IsNew = false,
                jobName = c.AdmJob.JobNameEn,
                NationalityName = c.Nationality.NationalityEn,
                ImgUrl = c.ImgUrl,
                BirthPlace = c.BirthPlace,
                childNumber = c.childNumber,
                companionscount = c.companionscount,
                ContractDuration = c.ContractDuration,
                Educationallevel = c.Educationallevel,
                EmployeeNumber = c.EmployeeNumber,
                EmployeeStatus = c.EmployeeStatus,
                EndNational_ID_date = c.EndNational_ID_date,
                Endpassport_date = c.Endpassport_date,
                EnteranceBorder = c.EnteranceBorder,
                EnteranceDate = c.EnteranceDate,
                JoblicenseNumber = c.JoblicenseNumber,
                Maritalstatus = c.Maritalstatus,
                Notes = c.Notes,
                pleaceNational_ID = c.pleaceNational_ID,
                NumberEnterBorder = c.NumberEnterBorder,
                passportNumber = c.passportNumber,
                passportpleace = c.passportpleace,
                ReleaseNational_ID_date = c.ReleaseNational_ID_date,
                Releasepassport_date = c.Releasepassport_date,
                religion = c.religion,
                scientificcertificate = c.scientificcertificate,
                sex = c.sex,
                visadate = c.visadate,
                visajob = c.visajob,
                visaNumber = c.visaNumber,
                IsTerminated = c.IsTerminated,
                CurrentSponsor = c.SponsorName,
                 BasePay = c.BasePay,
                 WorkHrs=c.WorkHrs,
                _BenefitID = c.EmployeeAddedBenefitsOthers.Where(x => x.EmployeeBenfitsOther.Type == 1 && x.EmployeeBenfitsOther.IsDeleted == false && x.IsDeleted == false).Select(t => t.EmployeeBenefitsOtherId).ToList(),
                _DeductID = c.EmployeeAddedBenefitsOthers.Where(x => x.EmployeeBenfitsOther.Type == 2 && x.EmployeeBenfitsOther.IsDeleted == false && x.IsDeleted == false).Select(t => t.EmployeeBenefitsOtherId).ToList(),
                _TaxesID = c.EmployeeAddedBenefitsOthers.Where(x => x.EmployeeBenfitsOther.Type == 3 && x.EmployeeBenfitsOther.IsDeleted == false && x.IsDeleted == false).Select(t => t.EmployeeBenefitsOtherId).ToList(),
                _GovID = c.EmployeeAddedBenefitsOthers.Where(x => x.EmployeeBenfitsOther.Type == 4 && x.EmployeeBenfitsOther.IsDeleted == false && x.IsDeleted == false).Select(t => t.EmployeeBenefitsOtherId).ToList()
            }).FirstOrDefault();
        }

        public void Create(EmployeeViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            string employeeAddedBenefitsValues = string.Format("{0},{1},{2},{3}", model.BenefitID, model.DeductID, model.TaxesID, model.GovID);
            //List<EmployeeAddedBenefitsOther> ListOfemployeeAddedBenefitsOther = new List<EmployeeAddedBenefitsOther>();

            AdmEmployee _Employee = new AdmEmployee
            {
                Address = model.Address,
                Mail = model.Mail,
                Mobile = model.Mobile,
                BirthDate = model.BirthDate,
                //DeptID = model.DeptID, --no need referenced by job id 
                NameE = model.NameEn,
                NameA = model.NameAr,
                WorkDate = model.WorkDate,
                JobID = model.jobID,
                National_ID = model.National_ID,
                Tel = model.Tel,
                NationalityID = model.NationalityID,
                ImgUrl = model.ImgUrl,
                CreatedbyID = _dbUser.ID,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                visaNumber = model.visaNumber,
                visajob = model.visajob,
                visadate = model.visadate,
                sex = model.sex,
                scientificcertificate = model.scientificcertificate,
                religion = model.religion,
                Releasepassport_date = model.Releasepassport_date,
                ReleaseNational_ID_date = model.ReleaseNational_ID_date,
                pleaceNational_ID = model.pleaceNational_ID,
                BirthPlace = model.BirthPlace,
                passportpleace = model.passportpleace,
                passportNumber = model.passportNumber,
                childNumber = model.childNumber,
                companionscount = model.companionscount,
                ContractDuration = model.ContractDuration,
                NumberEnterBorder = model.NumberEnterBorder,
                Notes = model.Notes,
                Maritalstatus = model.Maritalstatus,
                JoblicenseNumber = model.JoblicenseNumber,
                EnteranceDate = model.EnteranceDate,
                EmployeeStatus = model.EmployeeStatus,
                Educationallevel = model.Educationallevel,
                Endpassport_date = model.Endpassport_date,
                EmployeeNumber = model.EmployeeNumber,
                EndNational_ID_date = model.EndNational_ID_date,
                EnteranceBorder = model.EnteranceBorder,
                SchoolID = _dbUser.SchoolID,
                CompanyID = _dbUser.CompanyID,
            };



            foreach (var value in employeeAddedBenefitsValues.Split(','))
            {
                if (!string.IsNullOrWhiteSpace(value) && !string.IsNullOrEmpty(value))
                {
                    EmployeeAddedBenefitsOther employeeAddedBenefitsOther = new EmployeeAddedBenefitsOther
                    {
                        EmployeeId = model.Employee_ID,
                        EmployeeBenefitsOtherId = int.Parse(value),
                        CreatedbyID = _dbUser.CreatedbyID,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        DeletedDate = DateTime.Now
                    };
                    Context.EmployeeAddedBenefitsOthers.Add(employeeAddedBenefitsOther);
                }
            }


            Context.AdmEmployees.Add(_Employee);

            Context.SaveChanges();
        }

        public EmployeeViewModel CreateEmployee(EmployeeViewModel model, int companyId, int schoolId)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            string employeeAddedBenefitsValues = string.Format("{0},{1},{2},{3}", model.BenefitID, model.DeductID, model.TaxesID, model.GovID);
            AdmEmployee _Employee = new AdmEmployee
            {
                Address = model.Address,
                Mail = model.Mail,
                Mobile = model.Mobile,
                BirthDate = model.BirthDate,
                DeptID = model.DeptID,
                NameE = model.NameEn,
                NameA = model.NameAr,
                WorkDate = model.WorkDate,
                JobID = model.jobID,
                National_ID = model.National_ID,
                Tel = model.Tel,
                NationalityID = model.NationalityID,
                ImgUrl = model.ImgUrl,
                CreatedbyID = _dbUser.ID,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                visaNumber = model.visaNumber,
                visajob = model.visajob,
                visadate = model.visadate,
                sex = model.sex,
                scientificcertificate = model.scientificcertificate,
                religion = model.religion,
                Releasepassport_date = model.Releasepassport_date,
                ReleaseNational_ID_date = model.ReleaseNational_ID_date,
                pleaceNational_ID = model.pleaceNational_ID,
                BirthPlace = model.BirthPlace,
                passportpleace = model.passportpleace,
                passportNumber = model.passportNumber,
                childNumber = model.childNumber,
                companionscount = model.companionscount,
                ContractDuration = model.ContractDuration,
                NumberEnterBorder = model.NumberEnterBorder,
                Notes = model.Notes,
                Maritalstatus = model.Maritalstatus,
                JoblicenseNumber = model.JoblicenseNumber,
                EnteranceDate = model.EnteranceDate,
                EmployeeStatus = model.EmployeeStatus,
                Educationallevel = model.Educationallevel,
                Endpassport_date = model.Endpassport_date,
                EmployeeNumber = model.EmployeeNumber,
                EndNational_ID_date = model.EndNational_ID_date,
                EnteranceBorder = model.EnteranceBorder,
                BasePay = model.BasePay,
                WorkHrs = model.WorkHrs,
                CompanyID = _dbUser.CompanyID,
                SchoolID = _dbUser.SchoolID

            };


            model.Employee_ID = _Employee.Employee_ID;

            Context.AdmEmployees.Add(_Employee);

            foreach (var value in employeeAddedBenefitsValues.Split(','))
            {
                if (!string.IsNullOrWhiteSpace(value) && !string.IsNullOrEmpty(value))
                {
                    EmployeeAddedBenefitsOther employeeAddedBenefitsOther = new EmployeeAddedBenefitsOther
                    {
                        EmployeeId = model.Employee_ID,
                        EmployeeBenefitsOtherId = int.Parse(value),
                        CreatedbyID = _dbUser.ID,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        DeletedDate = DateTime.Now
                    };
                    Context.EmployeeAddedBenefitsOthers.Add(employeeAddedBenefitsOther);
                }
            }

            AccountCostCenter accountCostCenter = new AccountCostCenter
            {

                Comment = "Cost Center Of Empployee",
                CostCenter = model.Employee_ID.ToString(),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                CreatedbyID = _dbUser.ID,
                SchoolID = _dbUser.SchoolID,
                CostCenterAR = model.Employee_ID.ToString(),
                Code = model.Employee_ID.ToString(),
                CompanyID = _dbUser.CompanyID


            };


            Context.AccountCostCenters.Add(accountCostCenter);

            Context.SaveChanges();

            return model;
        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _Employee = Context.AdmEmployees.Where(c => c.Employee_ID == ID).FirstOrDefault();
            if (_Employee != null)
            {
                _Employee.DeletedbyID = _dbUser.ID;
                _Employee.IsDeleted = true;
                _Employee.DeletedDate = DateTime.Now;
                Context.SaveChanges();
                return true;
            }
            else return false;

        }

        public EmployeeViewModel Create()
        {
            EmployeeViewModel model = new EmployeeViewModel();
            model.IsNew = true;
            return model;
        }


        public bool ChangeDepartment(EmployeeViewModel model)
        {
            var _Employee = Context.AdmEmployees.Where(c => c.Employee_ID == model.Employee_ID).FirstOrDefault();
            if (_Employee != null)
            {
                _Employee.JobID = model.jobID;
                Context.SaveChanges();
            }
            return true;
        }
        public bool ChangeSponsor(EmployeeViewModel model)
        {
            var _Employee = Context.AdmEmployees.Where(c => c.Employee_ID == model.Employee_ID).FirstOrDefault();
            if (_Employee != null)
            {
                _Employee.SponsorName = model.NewSponsor;
                Context.SaveChanges();
            }
            return true;
        }

        public bool EmployeePromotion(EmployeePromotionViewModal model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            //var _AccountEmployeesSalary = Context.AccountEmployeesSalaries.Where(x => x.Employee_ID == model.Employee_ID && x.IsDeleted == false)
            //if (_AccountEmployeesSalary != null)
            //{
            //    SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            //    _AccountEmployeesSalary.DeletedbyID = _dbUser.ID;
            //    _AccountEmployeesSalary.IsDeleted = true;
            //    _AccountEmployeesSalary.DeletedDate = DateTime.Now;
            //    Context.SaveChanges();

            //}
            var _EmployeeSalary = Context.AccountEmployeesSalaries.Where(c => c.Employee_ID == model.Employee_ID).FirstOrDefault();
            if (_EmployeeSalary != null)
            {
                _EmployeeSalary.BasicSalary = model.BasicSalary;
                _EmployeeSalary.AdditionalSalary = model.AdditionalSalary;
                _EmployeeSalary.BonusesSalary = model.BonusesSalary;
                _EmployeeSalary.AllowanceSalary = model.AllowanceSalary;
                _EmployeeSalary.extraSalary = model.extraSalary;
                _EmployeeSalary.TotalSalary = model.TotalSalary;
                _EmployeeSalary.Medicalinsurance = model.Medicalinsurance;
                _EmployeeSalary.TotalDedecated = model.TotalDedecated;
                _EmployeeSalary.Taxes = model.Taxes;
                _EmployeeSalary.JobID = model.jobID;

                _EmployeeSalary.Socialinsurance = model.Socialinsurance;
                _EmployeeSalary.NetSalary = model.NetSalary;
                _EmployeeSalary.Dedecation = model.Dedecation;
                _EmployeeSalary.ModifiedDate = DateTime.Now;

                _EmployeeSalary.accommodationallowanc = model.accommodationallowanc;
                _EmployeeSalary.accommodationallowancetype = model.accommodationallowancetype;
                _EmployeeSalary.AccountNumber = model.AccountNumber;
                _EmployeeSalary.Airline = model.Airline;
                _EmployeeSalary.workingHours = model.workingHours;
                _EmployeeSalary.AllowanceSalary3 = model.AllowanceSalary3;
                _EmployeeSalary.AllowanceSalary2 = model.AllowanceSalary2;
                _EmployeeSalary.BankBranchID = model.BankBranchID;
                _EmployeeSalary.valuedate = model.valuedate;
                _EmployeeSalary.Airlineclass = model.Airlineclass;
                _EmployeeSalary.Drivingallowance = model.Drivingallowance;
                _EmployeeSalary.conditionsworkallowance = model.conditionsworkallowance;
                _EmployeeSalary.Subsistenceallowance = model.Subsistenceallowance;
                _EmployeeSalary.Transitionallowance = model.Transitionallowance;
                _EmployeeSalary.flighttickets = model.flighttickets;
                _EmployeeSalary.FTperMonth = model.FTperMonth;
                _EmployeeSalary.leavebalance = model.leavebalance;
                _EmployeeSalary.LBperMonth = model.LBperMonth;



            }





            var _Employee = Context.AdmEmployees.Where(c => c.Employee_ID == model.Employee_ID).FirstOrDefault();
            if (_Employee != null)
            {
                _Employee.JobID = model.jobID;

            }
            Context.SaveChanges();
            return true;
        }


        // public bool PayHousing(EmployeeViewModel model)

    }
}