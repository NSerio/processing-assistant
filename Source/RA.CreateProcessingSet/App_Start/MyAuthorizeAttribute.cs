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
            var workspaceID = WorkspaceFromContext.GetID(httpContext);
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