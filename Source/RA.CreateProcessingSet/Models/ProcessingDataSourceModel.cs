using System;
using Relativity.API;

namespace RA.CreateProcessingSet.Models
{
	public class ProcessingDataSourceModel
	{
		public Int32 ArtifactId;
		public readonly String SourcePath;
		public readonly Int32 CustodianArtifactId;
		public readonly Int32 Order;
		public readonly ProcessingProfileModel ProcessingProfile;
		public readonly kCura.Relativity.Client.DTOs.Artifact ProcessingSet;

		public ProcessingDataSourceModel(String sourcePath, Int32 custodianArtifactId, Int32 order, ProcessingProfileModel processingProfile,
			kCura.Relativity.Client.DTOs.Artifact processingSet)
		{
			SourcePath = sourcePath;
			CustodianArtifactId = custodianArtifactId;
			Order = order;
			ProcessingProfile = processingProfile;
			ProcessingSet = processingSet;
		}

		public void Create(IServicesMgr svcMgr, ExecutionIdentity identity, Int32 workspaceArtifactId, string docNumberingPrefix)
		{
			ArtifactId = Rsapi.ProcessingDataSource.Create(svcMgr, identity, workspaceArtifactId, CustodianArtifactId,
				ProcessingProfile, SourcePath, ProcessingSet, Order, docNumberingPrefix);
		}

		public void Update(IServicesMgr svcMgr, ExecutionIdentity identity, Int32 workspaceArtifactId)
		{
			Rsapi.ProcessingDataSource.Update(svcMgr, identity, workspaceArtifactId, ArtifactId);
		}
	}
}