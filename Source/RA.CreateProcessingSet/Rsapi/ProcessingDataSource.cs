using System;
using System.Collections.Generic;
using System.Linq;
using kCura.Relativity.Client;
using kCura.Relativity.Client.DTOs;
using Relativity.API;


namespace RA.CreateProcessingSet.Rsapi
{
	public class ProcessingDataSource
	{
		public static Int32 Create(IServicesMgr svcMgr, ExecutionIdentity identity, Int32 workspaceArtifactId,
			Int32 custodianArtifactId, Models.ProcessingProfileModel processingProfile,
			String sourcePath, kCura.Relativity.Client.DTOs.Artifact processingSet, Int32 order, string docNumberingPrefix)
		{
			using (var client = svcMgr.CreateProxy<IRSAPIClient>(identity))
			{
				client.APIOptions.WorkspaceID = workspaceArtifactId;
				var r = new RDO
				{
					ArtifactTypeGuids = new List<Guid> { Helpers.Constants.Guids.ObjectType.ProcessingDataSource },
					ParentArtifact = processingSet
				};

				r.Fields.Add(new FieldValue(Helpers.Constants.Guids.Fields.ProcessingDataSource.RelatedCustodian, custodianArtifactId));
				r.Fields.Add(new FieldValue(Helpers.Constants.Guids.Fields.ProcessingDataSource.SourcePath, sourcePath));
				r.Fields.Add(new FieldValue(Helpers.Constants.Guids.Fields.ProcessingDataSource.DestinationFolder, processingProfile.ParentFolderArtifactId));
				r.Fields.Add(new FieldValue(Helpers.Constants.Guids.Fields.ProcessingDataSource.TimeZone, processingProfile.TimeZoneArtifactId));
				r.Fields.Add(new FieldValue(Helpers.Constants.Guids.Fields.ProcessingDataSource.OcrLanguages, processingProfile.OcrLanguages));
				r.Fields.Add(new FieldValue(Helpers.Constants.Guids.Fields.ProcessingDataSource.DocumentNumberingPrefix, docNumberingPrefix));
				r.Fields.Add(new FieldValue(Helpers.Constants.Guids.Fields.ProcessingDataSource.Status, "New"));
				r.Fields.Add(new FieldValue(Helpers.Constants.Guids.Fields.ProcessingDataSource.Order, order));

				var results = client.Repositories.RDO.Create(r);

				var res = new Response<Int32>
				{
					Results = results.Results.Any() ? results.Results.FirstOrDefault().Artifact.ArtifactID : 0,
					Success = results.Success,
					Message = MessageFormatter.FormatMessage(results.Results.Select(x => x.Message).ToList(), results.Message, results.Success)
				};

				if (res.Success)
				{
					return res.Results;
				}
				throw new Exception(res.Message.ToString());
			}
		}

		public static void Update(IServicesMgr svcMgr, ExecutionIdentity identity, Int32 workspaceArtifactId, Int32 processingDataSourceArtifactId)
		{
			using (var client = svcMgr.CreateProxy<IRSAPIClient>(identity))
			{
				client.APIOptions.WorkspaceID = workspaceArtifactId;
				var r = new RDO(Helpers.Constants.Guids.ObjectType.ProcessingDataSource, processingDataSourceArtifactId);
				r.Fields.Add(new FieldValue(Helpers.Constants.Guids.Fields.ProcessingDataSource.RelatedProcessingDataSource, processingDataSourceArtifactId));

				var results = client.Repositories.RDO.Update(r);

				var res = new Response<ResultSet<RDO>>
				{
					Results = results,
					Success = results.Success,
					Message = MessageFormatter.FormatMessage(results.Results.Select(x => x.Message).ToList(), results.Message, results.Success)
				};

				if (!res.Success)
				{
					throw new Exception(res.Message.ToString());
				}
			}
		}
	}
}