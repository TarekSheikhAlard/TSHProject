using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Campus.SchoolManagment.Web.Helper;
using System.Net;
using System.Net.Mail;
using NReco.PdfGenerator;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Campus.SchoolManagment.Web.Controllers
{
    public class DocumentController : BaseController
    {
        SupplierFormHandler formHandler = new SupplierFormHandler();
        SupplierHandler SupplierHandler = new SupplierHandler();
        // GET: ApproveDocument
        public ActionResult Index()
        {
            var model = formHandler.GetAll().Where(x => x.Type == "Q");

            return PartialView(model);
        }
        public JsonResult Approve(int id)
        {
            var result = formHandler.ChangeStatus(id, 'A');
            var obj = new
            {
                IsError = result,
                type = "Approve"
            };
            return Json(obj);
        }
        public JsonResult Reject(int id)
        {
            var result = formHandler.ChangeStatus(id, 'R');
            var obj = new
            {
                IsError = result,
                type = "Reject"
            };
            return Json(obj);
        }
        public JsonResult Undo(int id)
        {
            var result = formHandler.ChangeStatus(id, 'P');
            var obj = new
            {
                IsError = result,
                type = "Pending"
            };
            return Json(obj);
        }

        public ActionResult ViewQuotation(int id)
        {
            var list = formHandler.GetById(id);

            return View("ViewQuotation", list);

        }
        public ActionResult ViewPurchaseOrder(int id)
        {
            var list = formHandler.GetById(id);

            return View("PurchaseOrder", list);

        }



        [Filters.FileDownload]
        public ActionResult ExportQuotation(int id)
        {

            var list = formHandler.GetById(id);

            string html = RenderViewToString(this.ControllerContext, "ViewQuotation", list);

            return new PDFResult(html, "Quotation", "Quotation");

        }

        [ActionName("SendDocument")]
        public async Task<JsonResult> SendDocumentAsync(int id)
        {
            try
            {
                // var body = "<p>Email From: {0} ({1})</div>{2}</div><div>{3}</div>";
                var message = new MailMessage();


                var list = formHandler.GetById(id);

                string html = RenderViewToString(this.ControllerContext, "PurchaseOrder", list);

                message.To.Add(new MailAddress(list.Email));  // replace with valid value 
                message.From = new MailAddress("campus.erp@outlook.com");  // replace with valid value

                message.Subject = "Purchase Order #" + list.ID;


                message.Body = "Please find the attached purschase order for the products required by us.";//string.Format(body, model.firstname, model.email, orderTitle, orderListBuilder.ToString());


                message.Attachments.Add(new Attachment(GetPdf(html, "Purchase Order"), new System.Net.Mime.ContentType("application/pdf")));

                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "campus.erp@outlook.com",  // replace with valid value
                        Password = "P@ssword@!@#"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp-mail.outlook.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);

                }
                return Json(true); // JsonConvert.SerializeObject(true);
            }
            catch (Exception ex)
            {
                return Json(false);
            }




        }


        public Stream GetPdf(string html, string title)
        {
            var Renderer = new HtmlToPdfConverter();
            byte[] _bufferPDF;
            Renderer.Orientation = NReco.PdfGenerator.PageOrientation.Portrait;
            Renderer.Margins.Bottom = 20;
            Renderer.Margins.Top = 30;
            Renderer.Margins.Left = 10;
            Renderer.Margins.Right = 10;

            Renderer.Size = NReco.PdfGenerator.PageSize.A4;

            Renderer.PageHeaderHtml = string.Format(@"<div style="" font-family:Arial;"">
                              <b style="" text-align:left;  "">Campus ERP</b>
                              <br/>
                              <b>Date: </b><span style="" text-align:left;"">{0}</span>
                              <div style="" text-align:center; font-size:16pt;"">{1}</div>
                              <hr/>
                              <br/>
                              </div>", DateTime.Now, title);



            Renderer.PageFooterHtml = @"<div style='text-align:center'><em style='color:rgba(0, 0, 0, 0.87); font-size:12pt'>page <span  class=""page"" ></span>  of <span class=""topage""></span></em></div>";



            _bufferPDF = Renderer.GeneratePdf(html);

            Stream stream = new MemoryStream(_bufferPDF);

            return stream;

        }

        public ActionResult CreateFeeReceipt(List<AccStudentFeeViewModel> model)
        {



            return PartialView("_Receipt", model);

        }


        [Authorize]
        [HttpPost]
        public object uploader(uploadFileParams data) {

            HttpPostedFileBase file = data.uploadedFile;
            string path,folder,savePath,fileName,dateFolderName;
            dynamic isImg;
            Guid guid;
            dateFolderName = DateTime.Now.ToShortDateString().Replace("/", "_");


            isImg = file.isImage();
            if (!isImg.IsError) {
                try
                {
                    folder = string.Format("~/Content/Uploads/{0}/{1}/{2}", companyId, schoolId, data.folderName);
                    path = Path.Combine(HttpContext.Server.MapPath(folder), dateFolderName);

                    Directory.CreateDirectory(path);

                    guid = Guid.NewGuid();

                    savePath = Path.Combine(path, guid + Path.GetExtension(file.FileName).ToLower());

                    file.SaveAs(savePath);

                    fileName = dateFolderName +"/"+guid + Path.GetExtension(file.FileName).ToLower();


                    return JsonConvert.SerializeObject(new
                    {
                        IsError = false,
                        message = "OK",
                        name= fileName
                    });
                }
                catch (Exception ex ) {

                    return JsonConvert.SerializeObject(new
                    {
                        IsError=true,
                        message=ex.Message
                    });
                }
            }

            return JsonConvert.SerializeObject(new
            {
                IsError = true,
                message = "Error Occurred while Uploading, Try Again Later."
            });
        }
        public class uploadFileParams
        {
            public string id { get; set; }
            public string folderName { get; set; }
            public string imgName { get; set; }
            public HttpPostedFileBase uploadedFile { get; set; }

        }
    }
}