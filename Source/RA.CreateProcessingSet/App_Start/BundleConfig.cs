using System;
using System.Web.Optimization;

namespace RA.CreateProcessingSet
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.IgnoreList.Clear();

			var finalBundle = new ScriptBundle("~/processingSetJS");
			String[] scriptArray =
			{
				"~/Scripts/jquery-{version}.js",
				"~/Scripts/jquery.validate*",
				"~/Scripts/bootstrap.js",
				"~/Scripts/jquery-treeview-1.4.0.min.js",
				"~/js/processingSet[index]-v1.0.js",
				"~/js/global[error]-v1.0.js",
				"~/js/processingSet[folderBrowser]-v1.0.js",
				"~/Scripts/jquery.placeholder.js"
			};
			finalBundle.Include(scriptArray);
			bundles.Add(finalBundle);

			bundles.Add(new StyleBundle("~/Content/ProcessingSet").Include(
				"~/Content/Site.css"));
			bundles.Add(new StyleBundle("~/Content/Bootstrap").Include(
					"~/Content/bootstrap.min.css",
					"~/Content/bootstrap-theme.min.css"));
			bundles.Add(new StyleBundle("~/Content/TreeView/bundle").Include(
				"~/Content/treeview/jquery-treeview.css"));
		}
	}
}