using System;
using Relativity.API;
using RA.CreateProcessingSet.Helpers;

namespace RA.CreateProcessingSet.Models
{
	public class FolderModel
	{
		public String FullPath { get; set; }
        public String FolderName { get; set; }
        public String Description { get; set; }
        public Names CustodianNames { get; set; }

        public FolderModel(String folderPath, String folderName, DestinationEnum destination)
		{
			FullPath = folderPath;
			FolderName = folderName;
            CustodianNames = NameSeparator.Separate(FolderName, destination);
		}

		public void AddToProcessingSet(IServicesMgr svcMgr, 
            ExecutionIdentity identity, 
            Int32 workspaceArtifactId, 
            kCura.Relativity.Client.DTOs.Artifact processingSet,
			int processingProfileArtifactId, 
            Int32 order,
            DestinationEnum destination)
		{
			var processingProfile = new ProcessingProfileModel(processingProfileArtifactId);
			processingProfile.Initialize(svcMgr, identity, workspaceArtifactId);

			var custodian = new CustodianModel(CustodianNames);
			custodian.LoadOrCreateCustodianArtifact(svcMgr, identity, workspaceArtifactId, destination);

			var processingDataSource = new ProcessingDataSourceModel(FullPath, custodian.ArtifactId, order, processingProfile, processingSet);
			processingDataSource.Create(svcMgr, identity, workspaceArtifactId, processingProfile.DocumentNumberPrefix);
			processingDataSource.Update(svcMgr, identity, workspaceArtifactId);
		}

	}
}