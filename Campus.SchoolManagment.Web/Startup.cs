using Campus.School.Management.Logic.DataBaseLayer;
using Campus.SchoolManagment.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System.Configuration;
using System.Linq;

[assembly: OwinStartupAttribute("Campus.SchoolManagment.Web", typeof(Campus.SchoolManagment.Web.Startup))]
namespace Campus.SchoolManagment.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
           createRolesandUsers();
        }

        private void createRolesandUsers()
        {



            SchoolManagmentDBEntities mycontext = new SchoolManagmentDBEntities();



            // Retrieve users
            var users = mycontext.AspNetUsers.ToList();

            // Retrieve roles
            var roles = mycontext.AspNetRoles.ToList();

            // Retrieve students
            var students = mycontext.AdmStudents.ToList();

            // Retrieve AccBusCosts
            var accBusCosts = mycontext.AccBusCosts.ToList();

            // Retrieve AccountEmployeeSalaryItem
            var accountEmployeeSalaryItem = mycontext.AccountEmployeeSalaryItems.ToList();



            // Retrieve AccountSalaryItem
            var accountSalaryItem = mycontext.AccountSalaryItems.ToList();

            // Retrieve AccountStatementType
            var accountStatementType = mycontext.AccountStatementTypes.ToList();

            // Retrieve AccStudentDiscounts
            var accStudentDiscounts = mycontext.AccStudentDiscounts.ToList();

            // Retrieve AdmUser_Level
            var admUserLevel = mycontext.AdmUser_Level.ToList();

            // Retrieve AdmUsers
            var admUsers = mycontext.AdmUsers.ToList();

            // Retrieve BusDetails
            var busDetails = mycontext.BusDetails.ToList();

            // Retrieve BusExpenses
            var busExpenses = mycontext.BusExpenses.ToList();

            // Retrieve BusExpensesItem
            var busExpensesItem = mycontext.BusExpensesItems.ToList();

            // Retrieve BusTrips
            var busTrips = mycontext.BusTrips.ToList();

            // Retrieve PhysicalYear
            var physicalYear = mycontext.PhysicalYears.ToList();

            // Retrieve Bank_Action
            var bankAction = mycontext.Bank_Action.ToList();

            // Retrieve BankBranches
            var bankBranches = mycontext.BankBranches.ToList();

            // Retrieve BankCurrencies
            var bankCurrencies = mycontext.BankCurrencies.ToList();

            // Retrieve Nationalities
            var nationalities = mycontext.Nationalities.ToList();

            // Retrieve AdmCities
            var admCities = mycontext.AdmCities.ToList();

            // Retrieve AdmDepartments
            var admDepartments = mycontext.AdmDepartments.ToList();

            // Retrieve Buses
            var buses = mycontext.Buses.ToList();

            // Retrieve AdmAcademicYears
            var admAcademicYears = mycontext.AdmAcademicYears.ToList();

            // Retrieve AdmCompanies
            var admCompanies = mycontext.AdmCompanies.ToList();

            // Retrieve AdmEmployees
            var admEmployees = mycontext.AdmEmployees.ToList();

            // Retrieve AdmSchools
            var admSchools = mycontext.AdmSchools.ToList();

            // Retrieve MenuPage
            var menuPage = mycontext.MenuPages.ToList();

            // Retrieve SchoolUser
            var schoolUser = mycontext.SchoolUsers.ToList();

            // Retrieve AdmClassRoom
            var admClassRoom = mycontext.AdmClassRooms.ToList();

            // Retrieve AccountStudentFeesList
            var accountStudentFeesList = mycontext.AccountStudentFeesLists.ToList();

            // Retrieve AccAcademicCosts
            var accAcademicCosts = mycontext.AccAcademicCosts.ToList();

            // Retrieve AccountDailyJournal
            var accountDailyJournal = mycontext.AccountDailyJournals.ToList();

            // Retrieve __MigrationHistory
            var migrationHistory = mycontext.C__MigrationHistory.ToList();

            // Retrieve AspNetRoles
            var aspNetRoles = mycontext.AspNetRoles.ToList();

            // Retrieve AspNetUserClaims
            var aspNetUserClaims = mycontext.AspNetUserClaims.ToList();

            // Retrieve AspNetUserLogins
            var aspNetUserLogins = mycontext.AspNetUserLogins.ToList();

            // Retrieve AspNetUsers
            var aspNetUsers = mycontext.AspNetUsers.ToList();

            // Retrieve AccountCostCenter
            var accountCostCenter = mycontext.AccountCostCenters.ToList();

            // Retrieve AccFeeItem
            var accFeeItem = mycontext.AccFeeItems.ToList();

            // Retrieve AccAssetDepreciation
            var accAssetDepreciation = mycontext.AccAssetDepreciations.ToList();

            // Retrieve EmployeeReturnHistory
            var employeeReturnHistory = mycontext.EmployeeReturnHistories.ToList();

            // Retrieve HolidayTypes
            var holidayTypes = mycontext.HolidayTypes.ToList();

            // Retrieve EmployeeVacation
            var employeeVacation = mycontext.EmployeeVacations.ToList();

            // Retrieve EmployeeAssets
            var employeeAssets = mycontext.EmployeeAssets.ToList();

            // Retrieve EmployeeLoans
            var employeeLoans = mycontext.EmployeeLoans.ToList();

            // Retrieve EmployeeTermination
            var employeeTermination = mycontext.EmployeeTerminations.ToList();

            // Retrieve HRAllowanceEntry
            var hrAllowanceEntry = mycontext.HRAllowanceEntries.ToList();

            // Retrieve AdmStudents
            var admStudents = mycontext.AdmStudents.ToList();

            // Retrieve StudentReference
            var studentReference = mycontext.StudentReferences.ToList();

            // Retrieve admStudyear
            var admStudyear = mycontext.admStudyears.ToList();

            // Retrieve AdmGrade_Courses
            var admGrade_Courses = mycontext.AdmGrade_Courses.ToList();

            // Retrieve admGrades
            var admGrades = mycontext.admGrades.ToList();

            // Retrieve AdmGradesSchool
            var admGradesSchool = mycontext.AdmGradesSchools.ToList();

            // Retrieve InvenotryStoreKeepers
            var invenotryStoreKeepers = mycontext.InvenotryStoreKeepers.ToList();

            // Retrieve JournalTypes
            var journalTypes = mycontext.JournalTypes.ToList();

            // Retrieve MaterialManufacture
            var materialManufacture = mycontext.MaterialManufactures.ToList();

            // Retrieve StorePlaces
            var storePlaces = mycontext.StorePlaces.ToList();

            // Retrieve SchoolGroups
            var schoolGroups = mycontext.SchoolGroups.ToList();

            // Retrieve UserGroups
            var userGroups = mycontext.UserGroups.ToList();

            // Retrieve UserPermissions
            var userPermissions = mycontext.UserPermissions.ToList();

            // Retrieve AccountAssets
            var accountAssets = mycontext.AccountAssets.ToList();

            // Retrieve AccountchequePayable
            var accountchequePayable = mycontext.AccountchequePayables.ToList();

            // Retrieve Accountchequesreceivable
            var accountchequesreceivable = mycontext.Accountchequesreceivables.ToList();

            // Retrieve AccountCustomer
            var accountCustomer = mycontext.AccountCustomers.ToList();

            // Retrieve AccountCustomerTransaction
            var accountCustomerTransaction = mycontext.AccountCustomerTransactions.ToList();

            // Retrieve AccountDailyJournalDetail
            var accountDailyJournalDetail = mycontext.AccountDailyJournalDetails.ToList();

            // Retrieve AccountFirstBalance
            var accountFirstBalance = mycontext.AccountFirstBalances.ToList();

            // Retrieve AccountNotespayable
            var accountNotespayable = mycontext.AccountNotespayables.ToList();

            // Retrieve AccountNotesReceivables
            var accountNotesReceivables = mycontext.AccountNotesReceivables.ToList();

            // Retrieve AccountPayableCash
            var accountPayableCash = mycontext.AccountPayableCashes.ToList();

            // Retrieve AccountReciveCash
            var accountReciveCash = mycontext.AccountReciveCashes.ToList();

            // Retrieve AccountSupplier
            var accountSupplier = mycontext.AccountSuppliers.ToList();

            // Retrieve AccountTreasury
            var accountTreasury = mycontext.AccountTreasuries.ToList();

            // Retrieve Store
            var store = mycontext.Stores.ToList();

            // Retrieve UnitsMeasurement
            var unitsMeasurement = mycontext.UnitsMeasurements.ToList();

            // Retrieve AssetTree
            var assetTree = mycontext.AssetTrees.ToList();

            // Retrieve DepartmentTree
            var departmentTree = mycontext.DepartmentTrees.ToList();

            // Retrieve InventoryTree
            var inventoryTree = mycontext.InventoryTrees.ToList();

            // Retrieve AdmJobs
            var admJobs = mycontext.AdmJobs.ToList();

            // Retrieve InventoryItems
            var inventoryItems = mycontext.InventoryItems.ToList();

            // Retrieve BusStudentOccupation
            var busStudentOccupation = mycontext.BusStudentOccupations.ToList();

            // Retrieve SupplierForms
            var supplierForms = mycontext.SupplierForms.ToList();

            // Retrieve items
            var items = mycontext.items.ToList();

            // Retrieve GradesFeeConfig
            var gradesFeeConfig = mycontext.GradesFeeConfigs.ToList();

            // Retrieve SupplierFormDetails
            var supplierFormDetails = mycontext.SupplierFormDetails.ToList();

            // Retrieve AccStudentConfig
            var accStudentConfig = mycontext.AccStudentConfigs.ToList();

            // Retrieve AccStudentFees
            var accStudentFees = mycontext.AccStudentFees.ToList();

            // Retrieve FeeInstallmentTypes
            var feeInstallmentTypes = mycontext.FeeInstallmentTypes.ToList();

            // Retrieve AdmInitialStudRegistrations
            var admInitialStudRegistrations = mycontext.AdmInitialStudRegistrations.ToList();

            // Retrieve AccountTree
            var accountTree = mycontext.AccountTrees.ToList();

            // Retrieve Banks
            var banks = mycontext.Banks.ToList();

            // Retrieve BankTransactions
            var bankTransactions = mycontext.BankTransactions.ToList();

            // Retrieve TransactionTypes
            var transactionTypes = mycontext.TransactionTypes.ToList();

            // Retrieve EmployeeBenefitsOthersTypes
            var employeeBenefitsOthersTypes = mycontext.EmployeeBenefitsOthersTypes.ToList();

            // Retrieve EmployeeBenfitsOthers
            var employeeBenfitsOthers = mycontext.EmployeeBenfitsOthers.ToList();

            // Retrieve EmployeeAddedBenefitsOthers
            var employeeAddedBenefitsOthers = mycontext.EmployeeAddedBenefitsOthers.ToList();

            // Retrieve PurchaseOrderDetails
            var purchaseOrderDetails = mycontext.PurchaseOrderDetails.ToList();

            // Retrieve PurchaseOrders
            var purchaseOrders = mycontext.PurchaseOrders.ToList();

            // Retrieve HRPayroll
            var hrPayroll = mycontext.HRPayrolls.ToList();

            // Retrieve HRPayrollEmployeeBenfitsOthers
            var hrPayrollEmployeeBenfitsOthers = mycontext.HRPayrollEmployeeBenfitsOthers.ToList();

            // Retrieve HRPayrollEmployees
            var hrPayrollEmployees = mycontext.HRPayrollEmployees.ToList();

            // Retrieve HRSalaryMonths
            var hrSalaryMonths = mycontext.HRSalaryMonths.ToList();

            // Retrieve TransportBuses
            var transportBuses = mycontext.TransportBuses.ToList();

            // Retrieve TransportDrivers
            var transportDrivers = mycontext.TransportDrivers.ToList();

            // Retrieve TransportSupervisors
            var transportSupervisors = mycontext.TransportSupervisors.ToList();

            // Retrieve AccountEmployeesMonthlySalary

            // Retrieve AccountEmployeesSalaries

            var accountEmployeesMonthlySalary = mycontext.AccountEmployeesMonthlySalaries.ToList();

            
            var accountEmployeesSalaries = mycontext.AccountEmployeesSalaries.ToList();





            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("SuperAdmin"))
            {

                // first we create Admin rool   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "SuperAdmin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  
                string AdminEmail = ConfigurationManager.AppSettings["AdminEmail"];
                string AdminPassword = ConfigurationManager.AppSettings["AdminPassword"];
                var user = new ApplicationUser();
                user.UserName = AdminEmail;
                user.Email = AdminEmail;

                string userPWD = AdminPassword;

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "SuperAdmin");

                }
            }

            // creating Creating Manager role    
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

            }
            // creating Creating Employee role    
            if (!roleManager.RoleExists("Employee"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Employee";
                roleManager.Create(role);

            }
            // creating Creating Teacher role    
            if (!roleManager.RoleExists("Teacher"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Teacher";
                roleManager.Create(role);

            }

            // creating Creating Accountant role    
            if (!roleManager.RoleExists("Accountant"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Accountant";
                roleManager.Create(role);

            }

            // creating Creating HR role    
            if (!roleManager.RoleExists("HR"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "HR";
                roleManager.Create(role);

            }


            // creating Creating Student role    
            if (!roleManager.RoleExists("Student"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Student";
                roleManager.Create(role);





            }


            // creating Creating Parent role    
            if (!roleManager.RoleExists("Parent"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Parent";
                roleManager.Create(role);

                // need to create defaute user TODO Order

            }
        }

    }
}
