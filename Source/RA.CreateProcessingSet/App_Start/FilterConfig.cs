using System.Web.Mvc;

namespace RA.CreateProcessingSet
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
			filters.Add(new MyAuthorizeAttribute());
			filters.Add(new MyCustomErrorHandler());
		}
	}
}