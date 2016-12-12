using Relativity.API;

namespace RA.CreateProcessingSet.Models
{
	public class ServicesMgr
	{
		public static IServicesMgr Mgr
		{
			get
			{
				var helper = Relativity.CustomPages.ConnectionHelper.Helper();
				return helper.GetServicesManager();
			}
		}
	}
}