using Campus.SchoolManagment.Web.Helper;
using FastMember;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PdfSharp;
using IronPdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using System.Drawing.Printing;
using System.Text;
using PdfSharp.Pdf;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.tool.xml;
using NReco.PdfGenerator;
using System.Data.SqlClient;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.BusinessLayer;
using System.Text.RegularExpressions;

namespace Campus.SchoolManagment.Web.Helper
{

    public class ExcelResult : ActionResult
    {
        private readonly Excelizer _data;
        private readonly string _fileName;

       

        public ExcelResult(dynamic data, string fileName, string sheetName, params String[] columnOrder)
        {

            DataTable table = new DataTable();

            using (var reader = ObjectReader.Create(data, columnOrder))
            {
                table.Load(reader);

                table.SetColumnsOrder(columnOrder);
            }
            _data = new Excelizer(table, sheetName);

            _fileName = fileName + ".xlsx";
        }

        public ExcelResult(string[] headers, System.Data.DataTable data, string fileName, string sheetName)
        {
            _data = new Excelizer(headers, data, sheetName);
            _fileName = fileName;
        }

        public ExcelResult(System.Data.DataTable data, string fileName, string sheetName)
        {
            _data = new Excelizer(data, sheetName);
            _fileName = fileName;
        }

        public ExcelResult(string[] headers, Excelizer.DataType[] colunmTypes, System.Data.DataTable data, string fileName, string sheetName)
        {
            _data = new Excelizer(headers, colunmTypes, data, sheetName);
            _fileName = fileName;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.ClearContent();
            //response.ClearHeaders();
            response.Cache.SetMaxAge(new TimeSpan(0));

            using (var stream = new MemoryStream())
            {
                _data.CreateXlsxAndFillData(stream);

                //Return it to the client - strFile has been updated, so return it. 
                response.AddHeader("content-disposition", "attachment; filename=" + _fileName);

                // see http://filext.com/faq/office_mime_types.php
                response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                response.ContentEncoding = System.Text.Encoding.UTF8;
                stream.WriteTo(response.OutputStream);
            }
            response.End();
            //response.Flush();
            //response.Close();
        }
    }


    public class PDFResult : ActionResult
    {

        private readonly Byte[] _bufferPDF;
        private readonly string _fileName;
        private IronPdf.PdfDocument _PDF;
        private Document _iPDF;
        private System.IO.Stream _stream;
        private string _html;
        private bool _direct;

        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

        public void ironPDFResult(string html, string filename, string title)
        {
            //    _html = html;
            //    //WkHtmlToPdfConverter wol = new WkHtmlToPdfConverter();

            //    //// Get PDF in bytes
            //    //// _bufferPDF = wol.Convert(html);
            //    //_bufferPDF = wol.Convert(html);

            //IRONPDF
            var Renderer = new HtmlToPdf();
            Renderer.PrintOptions.CssMediaType = PdfPrintOptions.PdfCssMediaType.Print;
            //Renderer.PrintOptions.EnableJavaScript = true;
            // Renderer.PrintOptions.CustomCssUrl  =new Uri ("https://cdnjs.cloudflare.com/ajax/libs/semantic-ui/2.4.1/semantic.min.css");
            Renderer.PrintOptions.PaperSize = PdfPrintOptions.PdfPaperSize.A4;
            //Renderer.PrintOptions.PaperOrientation = PdfPrintOptions.PdfPaperOrientation.Landscape;
            Renderer.PrintOptions.Title = title;
            Renderer.PrintOptions.MarginBottom = 10;
            Renderer.PrintOptions.MarginTop = 30;
            Renderer.PrintOptions.MarginLeft = 10;
            Renderer.PrintOptions.MarginRight = 10;
            Renderer.PrintOptions.FirstPageNumber = 1;
            //Renderer.PrintOptions.FitToPaperWidth = true;



            //Renderer.PrintOptions.PrintHtmlBackgrounds = true;

            //Renderer.PrintOptions.PrintHtmlBackgrounds = true;

            //Renderer.PrintOptions.Header = new SimpleHeaderFooter()
            //{
            //    CenterText = "{pdf-title}",
            //    DrawDividerLine = true,
            //    FontSize = 12
            //};


            Renderer.PrintOptions.HtmlHeader = new HtmlHeaderFooter()
            {
                HtmlFragment = @"<div>
                              <b style="" text-align:left;  "">Campus ERP</b>
                              <br/>
                              <b>Date: </b> <span style="" text-align:left;  "">{date} {time}</span>
                              <div style="" text-align:center; font-size:16pt;"">{pdf-title}</div>
                               <hr/>
                              </div>"
            };


            //Renderer.PrintOptions.Footer = new SimpleHeaderFooter()
            //{
            //    LeftText = "{date} {time}",
            //    RightText = "Page {page} of {total-pages}",
            //    DrawDividerLine = true,
            //    FontSize = 11
            //};

            Renderer.PrintOptions.HtmlFooter = new HtmlHeaderFooter()
            {
                HtmlFragment = @"<div style='text-align:center'><em style='color:rgba(0, 0, 0, 0.87); font-size:12pt'>page {page} of {total-pages}</em></div>"
            };


            _PDF = Renderer.RenderHtmlAsPdf(html);


            // _bufferPDF = _PDF.BinaryData;




            //    //PdfDocument pdf = PdfGenerator.GeneratePdf(html, PageSize.A4, 20, null, null, null);


            // _fileName = string.Format("{0}_{1}",filename,DateTime.Now);


        }


        public void itextPDFResult(string html, string filename, string title, bool direct = false)
        {
            //_fileName = filename;
            using (var ms = new MemoryStream())
            {
                using (Document doc = new Document())
                {
                    doc.AddTitle(title);
                    doc.AddHeader("Campus ERP", title);

                    doc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());


                    using (var writer = PdfWriter.GetInstance(doc, ms))
                    {
                        doc.Open();

                        using (var srHtml = new StringReader(html))
                        {

                            //Parse the HTML
                            XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, srHtml);

                        }
                        doc.Close();
                    }


                }

                //_bufferPDF = ms.ToArray();
            }

        }



        public PDFResult(string html, string filename, string title, bool direct = false)
        {

            _html = html;
            _fileName = string.Format("{0}_{1}.pdf", filename, DateTime.Now);
            _direct = direct;
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    PdfSharp.Pdf.PdfDocument pdf = PdfGenerator.GeneratePdf(_html, PdfSharp.PageSize.Letter);

            //    pdf.Save(ms);

            //    _bufferPDF = ms.ToArray();
            //}

           
            var imgPath = HttpContext.Current.Server.MapPath(Statistics.SchoolImagesPath + _dbUser.logo);
            var Renderer = new HtmlToPdfConverter();
            Renderer.Orientation = NReco.PdfGenerator.PageOrientation.Landscape;
            Renderer.Margins.Bottom = 20;
            Renderer.Margins.Top = 30;
            Renderer.Margins.Left = 10;
            Renderer.Margins.Right = 10;

            Renderer.Size = NReco.PdfGenerator.PageSize.A4;


            Renderer.PageHeaderHtml = string.Format(@"<div style="" font-family:Arial;"">
                              <img src=""{2}"" style=""position:absolute;right:5px;"" width=""270"" height=""70"" /> 
                              <b style="" text-align:left;  "">Campus ERP</b>
                              <br/>
                              <b>Date: </b><span style="" text-align:left;"">{0}</span>
                              <div style="" text-align:center; font-size:16pt;"">{1}</div>
                              <hr/>
                              <br/>
                              </div>", DateTime.Now, title, imgPath);



            Renderer.PageFooterHtml = @"<div style='text-align:center'><em style='color:rgba(0, 0, 0, 0.87); font-size:12pt'>page <span  class=""page"" ></span>  of <span class=""topage""></span></em></div>";

            //Renderer.Quiet = false;
            //Renderer.LogReceived += (sender, e) => {
            //    File.WriteAllText(@"D:\Masood\Projects\NEOMIT\NeomERP V3 Sem\NeomERP V3\Campus.SchoolManagment.Web\Content\log.txt", e.Data);
            //};

            _bufferPDF = Renderer.GeneratePdf(html);


        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (_direct)
            {
                var OutputPath = @"D:\PDFTest\" + _fileName + ".pdf";

                File.WriteAllBytes(OutputPath, _bufferPDF);

                System.Diagnostics.Process.Start(OutputPath);
            }
            else
            {
                HttpContextDownload(context);
            }
        }

        private void HttpContextDownload(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.ClearContent();
            //response.ClearHeaders();
            response.Cache.SetMaxAge(new TimeSpan(0));



            using (var stream = new MemoryStream(_bufferPDF))
            {

                //Return it to the client - strFile has been updated, so return it. 
                response.AddHeader("content-disposition", "attachment; filename=" + _fileName);

                // see http://filext.com/faq/office_mime_types.php

                response.ContentType = "application/pdf";
                // response.ContentEncoding = System.Text.Encoding.UTF8;
                //response.ContentEncoding = Encoding.GetEncoding("windows-1256");
                stream.WriteTo(response.OutputStream);

            }
            //response.End();
            response.Flush();
            //response.Close();
        }
    }


    public class sql {

        public static object ExecuteSelectQuery(string query, string Connection)
        {
            object result;

            SqlCommand sqlCommand = new SqlCommand(query);
            string conString = Connection;
            SqlConnection con = new SqlConnection(conString);

            try
            {

                con.Open();
                sqlCommand.Connection = con;

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {

                    sda.SelectCommand = sqlCommand;

                    using (System.Data.DataTable dt = new System.Data.DataTable())
                    {
                        sda.Fill(dt);
                        con.Close();

                        return result = new
                        {
                            IsError = false,
                            data = dt,
                            Message = "Success"
                        };
                    }

                }

            }
            catch (Exception ex)
            {

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    return result = new
                    {
                        IsError = true,
                        data = new { },
                        Message = ex.Message
                    };
                }
                if (con.State == ConnectionState.Closed)
                    return result = new
                    {
                        IsError = true,
                        data = new { },
                        Message = "Error Occured While Connecting... "
                    };


            }

            return result = new
            {
                IsError = true,
                data = new { },
                Message = "Error Occured While Connecting... "
            };

        }

       
    }

    public static class DataTableExtensions
    {
        public static void SetColumnsOrder(this DataTable table, params String[] columnNames)
        {
            int columnIndex = 0;
            foreach (var columnName in columnNames)
            {
                table.Columns[columnName].SetOrdinal(columnIndex);
                columnIndex++;
            }
        }
    }

    public static class HttpPostedFileBaseExtensions {

        public const int ImageMinimumBytes = 512;

        public static object isImage(this HttpPostedFileBase postedFile)
        {
            var obj = new
            {
                IsError = true,
                message = "NULL"
            };

            if (postedFile.ContentType.ToLower() != "image/jpg" &&
                        postedFile.ContentType.ToLower() != "image/jpeg" &&
                        postedFile.ContentType.ToLower() != "image/pjpeg" &&
                        postedFile.ContentType.ToLower() != "image/gif" &&
                        postedFile.ContentType.ToLower() != "image/x-png" &&
                        postedFile.ContentType.ToLower() != "image/png")
            {
                return new
                {
                    IsError = true,
                    message = "Invalid File Type, Allowed file types are .jpg, .png, .gif and .jpeg."
                };
            }

            if (Path.GetExtension(postedFile.FileName).ToLower() != ".jpg"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".png"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".gif"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".jpeg")
            {
                return new
                {
                    IsError = true,
                    message = "Invalid File Type, Allowed file types are k.jpg, .png, .gif and .jpeg."
                };
            }

            try
            {
                if (!postedFile.InputStream.CanRead)
                {
                    return new
                    {
                        IsError = true,
                        message = "Error Occurred While Reading File.Check File Type and Try Again..."
                    };
                }
                if (postedFile.ContentLength < ImageMinimumBytes)
                {
                    return new
                    {
                        IsError = true,
                        message = "File Too Small..."
                    };
                }

                byte[] buffer = new byte[ImageMinimumBytes];
                postedFile.InputStream.Read(buffer, 0, ImageMinimumBytes);
                string content = System.Text.Encoding.UTF8.GetString(buffer);
                if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                {
                    return new
                    {
                        IsError = true,
                        message = "Invalid File, not an image..."
                    };
                }
            }
            catch (Exception)
            {
                return new
                {
                    IsError = true,
                    message = "Error Occurred While Reading File.Check File Type and Try Again..."
                }; ;
            }


            try
            {
                using (var bitmap = new System.Drawing.Bitmap(postedFile.InputStream))
                {

                }
            }
            catch (Exception)
            {
                return new
                {
                    IsError = true,
                    message = "Not an Valid Image.."
                };
            }
            finally
            {
                postedFile.InputStream.Position = 0;
            }

            return new
            {
                IsError = false,
                message = "OK"
            };
        }


    }


    public class Uploads {

        public void Delete(string type) {

        }
    }


}
