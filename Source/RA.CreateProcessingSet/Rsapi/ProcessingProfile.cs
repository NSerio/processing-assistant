using System;
using System.Collections.Generic;
using kCura.Relativity.Client;
using kCura.Relativity.Client.DTOs;
using Relativity.API;

namespace RA.CreateProcessingSet.Rsapi
{
	public class ProcessingProfile
	{
		public static IEnumerable<RDO> GetAll(IServicesMgr svcMgr, ExecutionIdentity identity, Int32 workspaceArtifactId)
		{
			using (var client = svcMgr.CreateProxy<IRSAPIClient>(identity))
			{
				client.APIOptions.WorkspaceID = workspaceArtifactId;
				var r = new Query<RDO> { ArtifactTypeGuid = Helpers.Constants.Guids.ObjectType.ProcessingProfile };
				r.Fields.Add(new FieldValue(Helpers.Constants.Guids.Fields.ProcessingProfile.DefaultDestinationFolder));
				r.Fields.Add(new FieldValue(Helpers.Constants.Guids.Fields.ProcessingProfile.DefaultDocumentNumberingPrefix));
				r.Fields.Add(new FieldValue(Helpers.Constants.Guids.Fields.ProcessingProfile.DefaultOcrLanguages));
				r.Fields.Add(new FieldValue(Helpers.Constants.Guids.Fields.ProcessingProfile.DefaultTimeZone));
				r.Fields.Add(new FieldValue(Helpers.Constants.Guids.Fields.ProcessingProfile.Name));
				var sort = new Sort { Guid = Helpers.Constants.Guids.Fields.ProcessingProfile.Name };
				r.Sorts.Add(sort);

				var results = client.Repositories.RDO.Query(r);
				var res = Response<RDO>.CompileQuerySubsets(client, results);

				if (res.Success)
				{
					return res.Results;
				}
				throw new Exception(res.Message.ToString());
			}
		}

		public static RDO GetByArtifactId(IServicesMgr svcMgr, ExecutionIdentity identity,
			Int32 workspaceArtifactId, Int32 artifactId)
		{
			using (var client = svcMgr.CreateProxy<IRSAPIClient>(identity))
			{
				client.APIOptions.WorkspaceID = workspaceArtifactId;
				var results = client.Repositories.RDO.ReadSingle(artifactId);
				return results;
			}
		}
	}
}