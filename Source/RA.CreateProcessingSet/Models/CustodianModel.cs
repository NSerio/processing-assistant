using System;
using Relativity.API;

namespace RA.CreateProcessingSet.Models
{
    public class CustodianModel
	{
		public Int32 ArtifactId;
		public readonly String FullName;
		public readonly String FirstName;
		public readonly String LastName;

		public CustodianModel(Names names)
		{
            FirstName = names.FirstName;
            LastName = names.LastName;
            FullName = names.FullName;

			//Truncate names
            if(FirstName != null)
            {
                FirstName = FirstName.Length > Helpers.Constants.MaxLengths.MAX_LENGTH_FIRST_NAME
                    ? FirstName.Substring(0, Helpers.Constants.MaxLengths.MAX_LENGTH_FIRST_NAME)
                    : FirstName;
            }
            if(LastName != null)
            {
                LastName = LastName.Length > Helpers.Constants.MaxLengths.MAX_LENGTH_LAST_NAME
                    ? LastName.Substring(0, Helpers.Constants.MaxLengths.MAX_LENGTH_LAST_NAME)
                    : LastName;
            }
			FullName = FullName.Length > Helpers.Constants.MaxLengths.MAX_LENGTH_FULL_NAME
				? FullName.Substring(0, Helpers.Constants.MaxLengths.MAX_LENGTH_FULL_NAME)
				: FullName;
		}

		public void LoadOrCreateCustodianArtifact(IServicesMgr svcMgr, 
            ExecutionIdentity identity, 
            Int32 workspaceArtifactId, 
            DestinationEnum destination)
		{
			LoadByName(svcMgr, identity, workspaceArtifactId);
			if (ArtifactId == 0)
			{
				Create(svcMgr, identity, workspaceArtifactId, destination);
			}
		}

		private void LoadByName(IServicesMgr svcMgr, ExecutionIdentity identity, Int32 workspaceArtifactId)
		{
			ArtifactId = Rsapi.Custodian.GetByName(svcMgr, identity, workspaceArtifactId, FullName);
		}

		private void Create(IServicesMgr svcMgr, ExecutionIdentity identity, Int32 workspaceArtifactId, DestinationEnum destination)
		{
			ArtifactId = Rsapi.Custodian.Create(svcMgr, identity, workspaceArtifactId, FullName, FirstName, LastName, destination);
		}
	}
}