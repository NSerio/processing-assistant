
using System;
using System.Collections.Generic;
using System.Linq;
using RA.CreateProcessingSet.Helpers;
using Relativity.API;

namespace RA.CreateProcessingSet.Models
{
	public class ProcessingProfileModel
	{
		public readonly Int32 ArtifactId;
		public Int32 ParentFolderArtifactId;
		public List<Int32> OcrLanguages;
		public String DocumentNumberPrefix;
		public Int32 TimeZoneArtifactId;

		public ProcessingProfileModel(Int32 artifactId)
		{
			ArtifactId = artifactId;
		}

		public void Initialize(IServicesMgr svcMgr, ExecutionIdentity identity, Int32 workspaceArtifactId)
		{
			var processingProfileRdo = Rsapi.ProcessingProfile.GetByArtifactId(svcMgr, identity, workspaceArtifactId, ArtifactId);
			if (processingProfileRdo != null)
			{
				ParentFolderArtifactId = processingProfileRdo[Constants.Guids.Fields.ProcessingProfile.DefaultDestinationFolder].ValueAsSingleObject.ArtifactID;
				OcrLanguages = processingProfileRdo[Constants.Guids.Fields.ProcessingProfile.DefaultOcrLanguages].GetValueAsMultipleObject<kCura.Relativity.Client.DTOs.Artifact>().Select(y => y.ArtifactID).ToList();
				DocumentNumberPrefix = processingProfileRdo[Constants.Guids.Fields.ProcessingProfile.DefaultDocumentNumberingPrefix].ValueAsFixedLengthText;
				TimeZoneArtifactId = processingProfileRdo[Constants.Guids.Fields.ProcessingProfile.DefaultTimeZone].ValueAsSingleObject.ArtifactID;
			}
		}

	}
}