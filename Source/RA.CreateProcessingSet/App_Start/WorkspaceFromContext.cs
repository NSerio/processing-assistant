using System.Web;

namespace RA.CreateProcessingSet
{
    public static class WorkspaceFromContext
    {
        public static int? GetID(HttpContextBase httpContext)
        {
            string appIdQueryString = httpContext.Request.QueryString["AppID"];
            int _workspaceId;
            if (int.TryParse(appIdQueryString, out _workspaceId))
            {
                return _workspaceId;
            }
            if (httpContext.Session != null && httpContext.Session["WorkspaceID"] != null)
            {
                return (int)httpContext.Session["WorkspaceID"];
            }
            return null;
        }
    }
}