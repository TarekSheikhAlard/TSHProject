using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.ComponentModel;

namespace Campus.SchoolManagment.Web.Helper
{
    public static class SemanticControls
    {
        public static MvcHtmlString DropDownFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, List<DropdownList> list, object htmlAttributes = null)
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            var selectedValue = metadata.Model == null ? string.Empty : metadata.Model.ToString();

            var placeholder = string.Empty;
            StringBuilder htmlString = new StringBuilder();
            string CssClass = "ui dropdown", Style = string.Empty;
            bool isSelected = false;
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(htmlAttributes))
            {
                //tagBuilder.MergeAttribute(prop.Name.Replace('_', '-'), prop.GetValue(htmlAttributes).ToString(), true);
                if (prop.Name == "class")
                {
                    CssClass = prop.GetValue(htmlAttributes).ToString();
                }
                if (prop.Name == "placeholder")
                {
                    placeholder = prop.GetValue(htmlAttributes).ToString();
                }
            }

            htmlString.AppendFormat("<div class=\"{0} \">", CssClass);

            if (CssClass.Contains("multiple"))
            {
                List<string> selectedVals = new List<string>();

                if (!string.IsNullOrEmpty(selectedValue) && !string.IsNullOrWhiteSpace(selectedValue))
                {
                    htmlString.AppendFormat("<input type=\"hidden\" name=\"{0}\" value=\"{1}\">", name,selectedValue);
                }
                else {
                    if (!CssClass.Contains("noselected")) {
                        foreach (var item in list)
                        {
                            if (item.selected)
                            {
                                selectedVals.Add(item.value);
                            }
                        }
                    }
                    htmlString.AppendFormat("<input type=\"hidden\" name=\"{0}\" value=\"{1}\">", name, string.Join(",", selectedVals));
                }

            }
            else
            {

                htmlString.AppendFormat("<input type=\"hidden\" name=\"{0}\" value=\"{1}\">", name, selectedValue);
            }

            htmlString.Append("<div class=\"text\">#</div><i class=\"dropdown icon\"></i><div class=\"menu\">");



            foreach (var item in list)
            {
                if (item.value == selectedValue && !CssClass.Contains("multiple"))
                {

                    htmlString.Replace("#", item.name);

                    if (item.text != null)
                    {
                        htmlString.AppendFormat("<div class=\"item active selected\" data-value=\"{0}\" data-text=\"{1}\" >{2}</div>", item.value, item.text, item.name);
                    }
                    else
                    {
                        htmlString.AppendFormat("<div class=\"item active selected\" data-value=\"{0}\">{1}</div>", item.value, item.name);
                    }
                    isSelected = true;
                }

                //else if (item.selected == true && CssClass.Contains("multiple"))
                //{

                //    if (item.text != null)
                //    {
                //        htmlString.AppendFormat("<div class=\"item active filtered\" data-value=\"{0}\" data-text=\"{1}\" >{2}</div>", item.value, item.text, item.name);
                //    }
                //    else
                //    {
                //        htmlString.AppendFormat("<div class=\"item active filtered\" data-value=\"{0}\">{1}</div>", item.value, item.name);
                //    }
                //}
                else
                {
                    if (item.text != null)
                    {
                        htmlString.AppendFormat("<div class=\"item\" data-value=\"{0}\" data-text=\"{1}\">{2}</div>", item.value, item.text, item.name);
                    }
                    else
                    {
                        htmlString.AppendFormat("<div class=\"item\" data-value=\"{0}\">{1}</div>", item.value, item.name);
                    }
                }

            }
            if (!isSelected)
            {
                htmlString.Replace("#", placeholder);
            }
            htmlString.AppendFormat("</div></div>");
            return new MvcHtmlString(htmlString.ToString());
        }
        public static MvcHtmlString DropDown(this HtmlHelper htmlHelper, string ID, List<DropdownList> list, object htmlAttributes = null)
        {
            var name = ID;//ExpressionHelper.GetExpressionText(expression);
                          //var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            //var selectedValue = metadata.Model == null ? string.Empty : metadata.Model.ToString();

            var placeholder = string.Empty;
            StringBuilder htmlString = new StringBuilder();
            string CssClass = "ui dropdown", Style = string.Empty;

            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(htmlAttributes))
            {
                //tagBuilder.MergeAttribute(prop.Name.Replace('_', '-'), prop.GetValue(htmlAttributes).ToString(), true);
                if (prop.Name == "class")
                {
                    CssClass = prop.GetValue(htmlAttributes).ToString();
                }
                if (prop.Name == "placeholder")
                {
                    placeholder = prop.GetValue(htmlAttributes).ToString();
                }
            }

            htmlString.AppendFormat("<div class=\"{0} \">", CssClass);

            htmlString.AppendFormat("<input type=\"hidden\" name=\"{0}\" value=\"\">", name);



            htmlString.AppendFormat("<div class=\"default text\">{0}</div><i class=\"dropdown icon\"></i><div class=\"menu\">", placeholder);



            foreach (var item in list)
            {


                if (item.text != null)
                {
                    htmlString.AppendFormat("<div class=\"item\" data-value=\"{0}\" data-text=\"{1}\">{2}</div>", item.value, item.text, item.name);
                }
                else
                {
                    htmlString.AppendFormat("<div class=\"item\" data-value=\"{0}\">{1}</div>", item.value, item.name);
                }


            }

            htmlString.AppendFormat("</div></div>");
            return new MvcHtmlString(htmlString.ToString());
        }
        public class DropdownList
        {

            public string name { get; set; }
            public string value { get; set; }
            public string text { get; set; }
            public bool selected { get; set; }


        }
    }
}