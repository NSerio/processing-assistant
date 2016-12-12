using System;
using System.Collections.Generic;
using kCura.Relativity.Client;
using kCura.Relativity.Client.DTOs;
using Relativity.API;

namespace RA.CreateProcessingSet.Rsapi
{
	public class Error
	{
		public static void WriteError(IServicesMgr svcMgr, ExecutionIdentity identity, Int32 workspaceArtifactId, Exception ex)
		{
			using (var client = svcMgr.CreateProxy<IRSAPIClient>(identity))
			{
				client.APIOptions.WorkspaceID = workspaceArtifactId;
				client.APIOptions.StrictMode = true;

				var res = WriteError(client, workspaceArtifactId, ex);
				if (!res.Success)
				{
					throw new Exception(res.Message.ToString());
				}
			}
		}

		private static Response<IEnumerable<kCura.Relativity.Client.DTOs.Error>> WriteError(IRSAPIClient proxy, Int32 workspaceArtifactId, Exception ex)
		{
			var artifact = new kCura.Relativity.Client.DTOs.Error
			{
				FullError = ex.StackTrace,
				Message = ex.Message,
				SendNotification = false,
				Server = Environment.MachineName,
				Source =
					String.Format("{0} [Guid={1}]", Helpers.Constants.Names.ApplicationName,
						Helpers.Constants.Guids.Application.CreateProcessingSet),
				Workspace = new Workspace(workspaceArtifactId)
			};
			var theseResults = proxy.Repositories.Error.Create(artifact);
			return Response<kCura.Relativity.Client.DTOs.Error>.CompileWriteResults(theseResults);
		}
	}
}