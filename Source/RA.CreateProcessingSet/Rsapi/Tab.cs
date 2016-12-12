using System;
using System.Linq;
using kCura.Relativity.Client;
using kCura.Relativity.Client.DTOs;
using Relativity.API;

namespace RA.CreateProcessingSet.Rsapi
{
	public class Tab
	{
		public static bool DoesUserHaveAccess(IServicesMgr svcMgr, ExecutionIdentity identity, Int32 workspaceArtifactId, Guid guid)
		{
			var result = RetrieveRdoByGuidAndArtifactTypeName(svcMgr, identity, workspaceArtifactId, guid);
			bool hasAccess = result.Success;

			return hasAccess;
		}

		private static Response<bool> RetrieveRdoByGuidAndArtifactTypeName(IServicesMgr svcMgr, ExecutionIdentity identity, Int32 workspaceArtifactId, Guid guid)
		{
			ResultSet<RDO> results;
			using (var client = (RSAPIClient)svcMgr.CreateProxy<IRSAPIClient>(identity))
			{
				client.APIOptions.WorkspaceID = workspaceArtifactId;
				var relApp = new RDO(guid)
				{
					ArtifactTypeName = "Tab"
				};

				results = client.Repositories.RDO.Read(relApp);
			}

			var res = new Response<bool>
			{
				Results = results.Success,
				Success = results.Success,
				Message = MessageFormatter.FormatMessage(results.Results.Select(x => x.Message).ToList(), results.Message, results.Success)
			};

			return res;
		}
	}
}