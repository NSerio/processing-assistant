using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RA.CreateProcessingSet.Models
{
	public class TreeViewModel
	{
		#region Fields
		private static readonly List<String> _nodeIdMappings;
		#endregion

		#region Constructor
		static TreeViewModel()
		{
			_nodeIdMappings = new List<String>();
		}
		#endregion

		#region Methods
		public static Int32? GetNodeId(FileSystemInfo item, String rootSourcePath)
		{
			if (item.FullName == rootSourcePath)
			{
				return null;
			}
			if (_nodeIdMappings.Contains(item.FullName))
			{
				return _nodeIdMappings.IndexOf(item.FullName);
			}
			_nodeIdMappings.Add(item.FullName);
			return (_nodeIdMappings.Count - 1);
		}

		public static DirectoryInfo GetDirectoryInfo(Int32? nodeId, String rootSourcePath)
		{
			if (nodeId.HasValue)
			{
				if ((_nodeIdMappings.Count > nodeId.Value) && Directory.Exists(_nodeIdMappings[nodeId.Value]))
				{
					return new DirectoryInfo(_nodeIdMappings[nodeId.Value]);
				}
				return null;
			}
			return new DirectoryInfo(rootSourcePath);
		}

		public static IEnumerable<FileSystemInfo> GetFileSystemInfos(Int32? rootNodeId, String rootSourcePath)
		{
			DirectoryInfo rootDirectoryInfo = GetDirectoryInfo(rootNodeId, rootSourcePath);

			if (rootDirectoryInfo != null)
			{
				return from childFileSystemInfo in rootDirectoryInfo.GetFileSystemInfos()
					   orderby childFileSystemInfo is DirectoryInfo descending
					   select childFileSystemInfo;
			}
			return null;
		}
		#endregion
	}

}