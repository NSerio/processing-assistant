using System;
using System.Collections.Generic;
using System.Linq;
using kCura.Relativity.Client;
using Relativity.API;

namespace RA.CreateProcessingSet.Rsapi
{
	public class ProcessingSet
	{
		public static kCura.Relativity.Client.DTOs.Artifact Create(IServicesMgr svcMgr, ExecutionIdentity identity,
			Int32 workspaceArtifactId, String processingSetName, Int32 processingProfileArtifactId, string emailRecipients)
		{
			kCura.Relativity.Client.DTOs.WriteResultSet<kCura.Relativity.Client.DTOs.RDO> results;
			using (var client = svcMgr.CreateProxy<IRSAPIClient>(identity))
			{
				client.APIOptions.WorkspaceID = workspaceArtifactId;
				var r = new kCura.Relativity.Client.DTOs.RDO
				{
					ArtifactTypeGuids = new List<Guid> { Helpers.Constants.Guids.ObjectType.ProcessingSet }
				};
				r.Fields.Add(new kCura.Relativity.Client.DTOs.FieldValue(Helpers.Constants.Guids.Fields.ProcessingSet.Name,
					processingSetName));
				r.Fields.Add(
					new kCura.Relativity.Client.DTOs.FieldValue(Helpers.Constants.Guids.Fields.ProcessingSet.RelatedProcessingProfile,
						processingProfileArtifactId));
				r.Fields.Add(new kCura.Relativity.Client.DTOs.FieldValue(
					Helpers.Constants.Guids.Fields.ProcessingSet.DiscoverStatus,
					Helpers.Constants.Guids.Choices.ProcessingSet.DiscoverStatusNotStarted));
				r.Fields.Add(
					new kCura.Relativity.Client.DTOs.FieldValue(Helpers.Constants.Guids.Fields.ProcessingSet.InventoryStatus,
						Helpers.Constants.Guids.Choices.ProcessingSet.InventoryStatusNotStarted));
				r.Fields.Add(new kCura.Relativity.Client.DTOs.FieldValue(
					Helpers.Constants.Guids.Fields.ProcessingSet.PublishStatus,
					Helpers.Constants.Guids.Choices.ProcessingSet.PublishStatusNotStarted));

				if (emailRecipients != String.Empty)
				{
					r.Fields.Add(new kCura.Relativity.Client.DTOs.FieldValue(Helpers.Constants.Guids.Fields.ProcessingSet.EmailRecipients,
						emailRecipients));
				}

				results = client.Repositories.RDO.Create(r);
				var res = new Response<kCura.Relativity.Client.DTOs.Artifact>
				{
					Results = results.Results.Any() ? results.Results.FirstOrDefault().Artifact : null,
					Success = results.Success,
					Message =
						MessageFormatter.FormatMessage(results.Results.Select(x => x.Message).ToList(), results.Message, results.Success)
				};

				if (res.Success)
				{
					return res.Results;
				}
				throw new Exception(res.Message.ToString());
			}
		}

		public static kCura.Relativity.Client.DTOs.Artifact Read(IServicesMgr svcMgr, ExecutionIdentity identity,
			Int32 workspaceArtifactId, Int32 artifactId)
		{
			using (var client = svcMgr.CreateProxy<IRSAPIClient>(identity))
			{
				client.APIOptions.WorkspaceID = workspaceArtifactId;
				return client.Repositories.RDO.ReadSingle(artifactId);
			}
		}
	}
}