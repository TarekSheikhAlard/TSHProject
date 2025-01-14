using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace Campus.SchoolManagment.Web
{
    public static class CustomControle
    {

        public static IDisposable BeginCollectionItem<TModel>(this HtmlHelper<TModel> html, string collectionName)
        {
            string itemIndex = Guid.NewGuid().ToString();
            string collectionItemName = String.Format("{0}[{1}]", collectionName, itemIndex);

          
           TagBuilder indexField = new TagBuilder("input");
            indexField.MergeAttributes(new Dictionary<string, string>() {
        { "name", String.Format("{0}.Index", collectionName) },
        { "value", itemIndex },
        { "type", "hidden" }
        //{ "autocomplete", "off" }
    });
          

            html.ViewContext.Writer.WriteLine(indexField.ToString(TagRenderMode.SelfClosing));

            return new CollectionItemNamePrefixScope(html.ViewData.TemplateInfo, collectionItemName);
        }

    }


    class CollectionItemNamePrefixScope : IDisposable
    {
        private readonly TemplateInfo _templateInfo;
        private readonly string _previousPrefix;

        public CollectionItemNamePrefixScope(TemplateInfo templateInfo, string collectionItemName)
        {
            this._templateInfo = templateInfo;

            _previousPrefix = templateInfo.HtmlFieldPrefix;
            templateInfo.HtmlFieldPrefix = collectionItemName;
        }

        public void Dispose()
        {
            _templateInfo.HtmlFieldPrefix = _previousPrefix;
        }
    }


    //public static MvcHtmlString CustomIDFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
    //{
    //    var data = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
    //    string propertyValue = data.Model.ToString();
    //    TagBuilder Input = new TagBuilder("Input");
    //    Input.Attributes.Add("name", data.PropertyName);
    //    Input.Attributes.Add("type", "button");
    //    Input.Attributes.Add("id", data.PropertyName);
    //    Input.Attributes.Add("value", propertyValue);

    //    if (htmlAttributes != null)
    //    {
    //        var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
    //        Input.MergeAttributes(attributes);
    //    }

    //    return new MvcHtmlString(Input.ToString());
    //}
    //public static MvcHtmlString CustomEditorFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
    //{
    //    var data = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
    //    string propertyValue = data.Model.ToString();
    //    TagBuilder Input = new TagBuilder("Input");
    //    Input.Attributes.Add("name", data.PropertyName);
    //    Input.Attributes.Add("type", "text");
    //    Input.Attributes.Add("class", data.PropertyName);
    //    Input.Attributes.Add("id", propertyValue);

    //    Input.Attributes.Add("value", propertyValue);

    //    if (htmlAttributes != null)
    //    {
    //        var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
    //        Input.MergeAttributes(attributes);
    //    }

    //    return new MvcHtmlString(Input.ToString());
    //}

}
