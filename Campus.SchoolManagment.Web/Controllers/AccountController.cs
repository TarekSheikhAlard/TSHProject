using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Campus.SchoolManagment.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.BusinessLayer;
using Campus.SchoolManagment.Web.Helper;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Campus.School.Management.Logic.DataBaseLayer;

namespace Campus.SchoolManagment.Web.Controllers
{

    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();
        private SchoolUserHandler _Handler = new SchoolUserHandler();
        private UserPermissionHandler userPermissionHandler = new UserPermissionHandler();
        private CompanyHandler _CompanyHandler = new CompanyHandler();



        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            var lang = Utility.getCultureCookie(false);
            ViewBag.ReturnUrl = returnUrl;

            ViewBag.Companies = new SelectList(_CompanyHandler.GetAll(), "CompanyID", "CompanyName" + lang);

            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            var lang = Utility.getCultureCookie(false);

            if (!ModelState.IsValid)
            {
                ViewBag.Companies = new SelectList(_CompanyHandler.GetAll(), "CompanyID", "CompanyName" + lang);

                return View(model);
            }

            //check user has access to school 
           
        
            // This doesn't count login failures towards account lockout3456
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            ApplicationUser usr = UserManager.FindByEmail(model.Email);

            try
            {
                SchoolUserViewModel dbusr = _Handler.GetById(_Handler.GetByMemberShipId(usr.Id));

               
                var rol = usr.Roles.SingleOrDefault();

                if (int.Parse(model.CompanyID) == dbusr.CompanyID)
                {
                   
                    if (rol==null || (rol !=null && rol.RoleId != "a585d60d-dfd4-4157-b22f-d9721319ee68")) {

                        long id = _Handler.checkUserSchoolAccess(dbusr.AspUserID, model.SchoolID);

                        if (id == 0)
                        {
                            ViewBag.Companies = new SelectList(_CompanyHandler.GetAll(), "CompanyID", "CompanyName" + lang);
                            ModelState.AddModelError("", "Invalid Login, Access Denied.");

                            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

                            return View(model);
                        }

                    }


                    dbusr.SchoolID = int.Parse(model.SchoolID);
                    int sId = int.Parse(model.SchoolID);
                    //   dbusr.SchoolName
                    var school = Context.AdmSchools.Where(x => x.SchoolID == sId && x.IsDeleted == false)
                       .Select(x => new {
                           SchoolName = lang == "ar" ? x.SchoolNameAr : x.SchoolNameEn,
                           connection = x.ConnectionString,
                           isArabic=x.AdmCompany.showArabic,
                           logo= x.Logo,
                           x = x.XMap,
                           y = x.YMap
                       }).SingleOrDefault();

                    dbusr.SchoolName = school.SchoolName;
                    dbusr.logo = school.logo;
                    dbusr.isArabic = school.isArabic;
                    dbusr.Location = school.x + "," + school.y;
                    bool IsError = GetAcadmicDates(ref dbusr, school.connection);

                    if (!IsError)
                    {
                        Setcookie(dbusr);
                    }
                    else
                    {
                        ViewBag.Companies = new SelectList(_CompanyHandler.GetAll(), "CompanyID", "CompanyName" + lang);
                        ModelState.AddModelError("", "Error occured.. Invalid School Database. ");

                        AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

                        return View(model);
                    }


                    result = SignInStatus.Success;
                    Session["CompanyID"] = model.CompanyID;
                    Session["SchoolID"] = model.SchoolID;
                    Session["SchoolUrl"] = dbusr.Url;

                  
                }
                else
                {
                    result = SignInStatus.Failure;
                }
            }
            catch(Exception ex)
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                result = SignInStatus.Failure;
            }


            //SingletoneUser.Instance.SetUser(dbusr);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);

                case SignInStatus.LockedOut:
                    return PartialView("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    if(User.Identity.IsAuthenticated)
                        AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

                    ViewBag.Companies = new SelectList(_CompanyHandler.GetAll(), "CompanyID", "CompanyName" + lang);
                   
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        private void Setcookie(SchoolUserViewModel dbusr)
        {
            string _user = JsonConvert.SerializeObject(dbusr);
            var cookie = new HttpCookie("dbusr", _user)
            {
                Expires = DateTime.Now.AddYears(1)
            };
            Response.SetCookie(cookie);
        }
        private void Setcookie(object obj, string cookieName)
        {
            string _obj = JsonConvert.SerializeObject(obj);
            var cookie = new HttpCookie(cookieName, _obj)
            {
                Expires = DateTime.Now.AddYears(1)
            };
            Response.SetCookie(cookie);
        }

        private bool GetAcadmicDates(ref SchoolUserViewModel model  ,string connectionString) {


          
            string query = "SELECT TOP 1 YearStDate,YearEdDate from dbo.admStudyear WHERE [CurrentYear] = 1";


            dynamic result = sql.ExecuteSelectQuery(query, connectionString);
            if (!result.IsError) {
                model.startYearDate = result.data.Rows[0][0];
                model.endYearDate = result.data.Rows[0][1];
            }

            return result.IsError;
        }


        public SchoolUserViewModel Getcookie()
        {
            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies["dbusr"];
            return cookie != null ? JsonConvert.DeserializeObject<SchoolUserViewModel>(cookie.Value) : new SchoolUserViewModel();
        }



       

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                 return PartialView("Error");
            }
             return PartialView(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                 return PartialView(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                     return PartialView("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                     return PartialView(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
             return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
             return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                 return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
             return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
             return PartialView();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);//|| !(await UserManager.IsEmailConfirmedAsync(user.Id))
                if (user == null)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                     return PartialView("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);

                await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
             return PartialView(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
             return PartialView();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                 return PartialView(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
             return PartialView();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
             return PartialView();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                 return PartialView("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
             return PartialView(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                 return PartialView();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                 return PartialView("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                     return PartialView("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                     return PartialView("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                     return PartialView("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
             return PartialView(model);
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            // return RedirectToAction("Index", "Home");
            return RedirectToAction("Login");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
             return PartialView();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}