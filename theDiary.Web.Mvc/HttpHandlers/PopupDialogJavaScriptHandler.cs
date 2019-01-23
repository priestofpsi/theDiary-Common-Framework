using System;
using System.Web;
using System.Web.Routing;

namespace System.Web.Mvc
{
    public class PopupDialogJavaScriptHandler
        : IHttpHandler, IRouteHandler
    {
        public PopupDialogJavaScriptHandler()
            : base()
        {
        }

        #region Private Constant Declarations
        private const string ConditionalEndBlock = "{$ConditionEndStatement$}";
        private const string ConditionalStartBlock = "{$ConditionStartStatement$}";
        private const string MvcAction = "{$PopupAction$}";
        private const string ConditionName = "{$ConditionName$}";
        private const string ConditionValue = "{$ConditionValue$}";

        private const string ConditionStartStatement = "if ($('{$ConditionName$}').val() == '{$ConditionValue$}') {";
        private const string ConditionalEndStatement = "}";
        #endregion

        public bool IsReusable
        {
            get { return true; }
        }
        
        public void ProcessRequest(HttpContext context)
        {
            string actionName, controllerName, conditionName, conditionValue;
            actionName = context.Request.QueryString["Action"];
            controllerName = context.Request.QueryString["Controller"];
            conditionName = context.Request.QueryString["Name"];
            conditionValue = context.Request.QueryString["Value"];
            bool addConditionalStatement = !conditionName.IsNullEmptyOrWhiteSpace();

            System.StringBuilder scriptContent = this.GetType().Assembly.GetManifestResourceStream("").ToString();
            if (addConditionalStatement)
            {
                scriptContent.Replace(ConditionalStartBlock, ConditionStartStatement);
                scriptContent.Replace(ConditionalEndBlock, ConditionalEndStatement);
                scriptContent.Replace(ConditionName, conditionName);
                scriptContent.Replace(ConditionValue, conditionValue);
            }
            else
            {
                scriptContent.Replace(ConditionalStartBlock, string.Empty);
                scriptContent.Replace(ConditionalEndBlock, string.Empty);
            }
            context.Response.Clear();
            context.Response.ContentType = "text/javascript";
            context.Response.Write(scriptContent);
            context.Response.Flush();
            context.Response.End();
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return this;
        }
    }
}
