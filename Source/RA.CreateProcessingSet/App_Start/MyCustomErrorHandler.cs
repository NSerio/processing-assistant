using System.Web.Mvc;
using RA.CreateProcessingSet.Models;
using Relativity.API;

namespace RA.CreateProcessingSet
{
	public class MyCustomErrorHandler : HandleErrorAttribute
	{
		public override void OnException(ExceptionContext filterContext)
		{
			base.OnException(filterContext);
			var caseArtifactId = filterContext.HttpContext.Session != null ? (int)filterContext.HttpContext.Session["WorkspaceID"] : 0;


			if (filterContext.Exception != null)
			{
				try
				{
					//try to log the error to the errors tab in Relativity
					Rsapi.Error.WriteError(ServicesMgr.Mgr,
					ExecutionIdentity.CurrentUser, caseArtifactId, filterContext.Exception);
				}
				catch
				{
					//if the error cannot be logged, just return the error view
				}
			}
		}

	}
}