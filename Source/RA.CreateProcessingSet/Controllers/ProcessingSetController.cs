using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using RA.CreateProcessingSet.Models;
using Relativity.API;
using Relativity.CustomPages;
using Lib.Web.Mvc.JQuery.TreeView;

namespace RA.CreateProcessingSet.Controllers
{

	public class ProcessingSetController : Controller
	{
		[HttpGet]
		public ActionResult Index(Int32 AppID)
		{
			var model = new ProcessingSetModel();
			model.LoadProfiles(AppID);
			model.LoadFolders(ConnectionHelper.Helper().GetDBContext(-1), AppID);
			return View(model);
		}

		[HttpPost]
		public ContentResult Index(ProcessingSetModel model, Int32 AppID)
		{
			model.Run(ServicesMgr.Mgr, ExecutionIdentity.CurrentUser, AppID);
			return new ContentResult
			{
				Content =
					String.Format(
						@"<html><head><script type=""text/javascript"">window.parent.location.href = '{0}';</script></head></html>",
						model.RedirectUrl),
				ContentType = "text/html"
			};
		}

        [HttpPost]
        public ActionResult CustodianSummaryAtLevel(string path, int level, DestinationEnum destination)
        {
            var paths = Helpers.FolderHelper.GetSourcePaths(path, level, destination, true);
            return Json(paths);
        }

		[HttpGet]
		public ActionResult Browse(Int32 AppID, String sourcePath)
		{
			return PartialView("_FolderBrowser");
		}

		[HttpPost]
		public ActionResult GetSubFolders(String root, String sourcePath, Int32 AppID)
		{
			DirectoryInfo rootDirectory = null;
			if (root == "source")
			{
				rootDirectory = (sourcePath == String.Empty) ? null : new DirectoryInfo(sourcePath);
			}
			else
			{
				rootDirectory = new DirectoryInfo(root);
			}

			var nodes = new List<TreeViewNode>();
			if (rootDirectory != null)
			{
				var children = from child in rootDirectory.GetFileSystemInfos()
											 orderby child is DirectoryInfo descending
											 select child;

				foreach (var child in children)
				{
					var leaf = child is FileInfo;
					var treeNode = new TreeViewNode();
					try
					{
						treeNode.id = child.FullName;
						treeNode.text = child.Name;
						treeNode.classes = leaf ? "file" : "folder";
						treeNode.hasChildren = !leaf;

					}
					catch (Exception ex)
					{
						treeNode.id = "badFile";
						treeNode.text = "WARNING: " + ex.Message;
						treeNode.classes = "badFile";
						treeNode.hasChildren = false;
					}
					nodes.Add(treeNode);
				}
			}
			var jsonResult = Json(nodes);
			jsonResult.MaxJsonLength = int.MaxValue;
			return jsonResult;
		}
	}
}
