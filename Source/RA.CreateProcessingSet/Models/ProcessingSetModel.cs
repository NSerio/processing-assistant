using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Relativity.API;
using RA.CreateProcessingSet.Helpers;

namespace RA.CreateProcessingSet.Models
{
	public class ProcessingSetModel
	{
		public Int32 ArtifactId;
		public Int32 ArtifactTypeId;
		public String RedirectUrl;
		private kCura.Relativity.Client.DTOs.Artifact _artifact;

		[AllowHtml]
		[DisplayName("Name")]
		[Required(ErrorMessage = Helpers.Constants.Messages.NAME_REQUIRED_MESSAGE)]
		public String Name { get; set; }

		[DisplayName("Processing profile")]
		[Required(ErrorMessage = Helpers.Constants.Messages.PROFILE_REQUIRED_MESSAGE)]
		public int SelectedProfileArtifactId { get; set; }

		[DisplayName("Source path")]
		[Required(ErrorMessage = Helpers.Constants.Messages.SOURCE_PATH_REQUIRED_MESSAGE)]
		public String FolderPath { get; set; }

		[Range(0, Int32.MaxValue, ErrorMessage = Helpers.Constants.Messages.CUSTODIAN_LEVEL_POSITIVE)]
		[RegularExpression(@"^[\d]*$", ErrorMessage = Helpers.Constants.Messages.CUSTODIAN_LEVEL_WHOLE_NUMBER)]
		[DisplayName("Custodian level")]
		[Required(ErrorMessage = Helpers.Constants.Messages.CUSTODIAN_LEVEL_REQUIRED_MESSAGE)]
		public Int32? CustodianLevel { get; set; }

		[DisplayName("Email notification recipients")]
		public String EmailRecipients { get; set; }

		public IEnumerable<SelectListItem> ProfileItems { get; set; }
		public List<String> FolderList { get; set; }
		public IEnumerable<SelectListItem> FolderItems { get; set; }
        [DisplayName("Destination")]
        public DestinationEnum Destination { get; set; }
        public string SelectedSourcePath { get; set; }

		public void Run(IServicesMgr svcMgr, ExecutionIdentity identity, Int32 workspaceArtifactId)
		{
			Create(svcMgr, identity, workspaceArtifactId);
			var foldersToProcess = FolderHelper.GetSourcePaths(FolderPath, CustodianLevel, Destination);
			AddDataSources(foldersToProcess, svcMgr, identity, workspaceArtifactId);
			GetRedirectUrl(workspaceArtifactId);
		}

		public void GetRedirectUrl(Int32 workspaceArtifactId)
		{
			RedirectUrl = String.Format(Helpers.Constants.Urls.URL_TO_PROCESSING_SET, workspaceArtifactId, ArtifactId, ArtifactTypeId);
		}

		public void LoadProfiles(Int32 workspaceArtifactId)
		{
			var profileList = Rsapi.ProcessingProfile.GetAll(ServicesMgr.Mgr, ExecutionIdentity.CurrentUser, workspaceArtifactId);

			if (profileList != null)
			{
				ProfileItems = profileList.Select(p => new SelectListItem
				{
					Value = p.ArtifactID.ToString(CultureInfo.InvariantCulture),
					Text = p[Helpers.Constants.Guids.Fields.ProcessingProfile.Name].ValueAsFixedLengthText
				});
			}
		}


		private void AddDataSources(IEnumerable<FolderModel> sourcePaths, IServicesMgr svcMgr, ExecutionIdentity identity, Int32 workspaceArtifactId)
		{
			var orderIncrementor = 0;
			foreach (var folder in sourcePaths)
			{
				folder.AddToProcessingSet(svcMgr, 
                    identity, 
                    workspaceArtifactId, 
                    _artifact, 
                    SelectedProfileArtifactId, 
                    orderIncrementor, 
                    Destination);
				orderIncrementor += 10;
			}
		}

		private void Create(IServicesMgr svcMgr, ExecutionIdentity identity, Int32 workspaceArtifactId)
		{
			_artifact = Rsapi.ProcessingSet.Create(svcMgr, identity, workspaceArtifactId, Name, SelectedProfileArtifactId, EmailRecipients);
			ArtifactId = _artifact.ArtifactID;
			var processingSetRdo = Rsapi.ProcessingSet.Read(svcMgr, identity, workspaceArtifactId, ArtifactId);
			ArtifactTypeId = processingSetRdo.ArtifactTypeID.HasValue ? processingSetRdo.ArtifactTypeID.Value : 0;
		}

		public void LoadFolders(IDBContext eddsDbConnection, int workspaceArtifactId)
		{
			FolderItems = new List<SelectListItem>();
			var folders = Database.QueryHelper.RetrieveProcessingSourceLocations(eddsDbConnection, workspaceArtifactId);
			var folderList = new List<String>();

			if (folders != null)
			{
				folderList.AddRange(from DataRow row in folders.Rows select row[0].ToString());
				FolderItems = folderList.Select(p => new SelectListItem
				{
					Value = p,
					Text = p
				});
			}
		}

	}
}