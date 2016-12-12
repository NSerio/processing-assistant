using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using RA.CreateProcessingSet.Models;
using Relativity.API;

namespace RA.CreateProcessingSet
{
	public class MyAuthorizeAttribute : AuthorizeAttribute
	{
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = false;
            var workspaceID = WorkspaceIDFromContext(httpContext);
            if (workspaceID.HasValue)
			{
				var res = Rsapi.Tab.DoesUserHaveAccess(
				ServicesMgr.Mgr,
				ExecutionIdentity.CurrentUser,
				workspaceID.Value,
				Helpers.Constants.Guids.Tab.CreateProcessingSet);
				isAuthorized = res;
			}

			return isAuthorized;
		}

        private int? WorkspaceIDFromContext (HttpContextBase httpContext)
        {
            string appIdQueryString = httpContext.Request.QueryString["AppID"];
            int _workspaceId;
            if(int.TryParse(appIdQueryString, out _workspaceId))
            {
                return _workspaceId;
            }
            if(httpContext.Session != null && httpContext.Session["WorkspaceID"] != null)
            {
                return (int)httpContext.Session["WorkspaceID"];
            }
            return null;
        }

		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		{
			filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
            {
                {"action", "AccessDenied"},
                {"controller", "Error"}
            });

		}
	}
}