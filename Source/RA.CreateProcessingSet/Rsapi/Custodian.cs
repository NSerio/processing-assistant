using System;
using System.Collections.Generic;
using System.Linq;
using kCura.Relativity.Client;
using Relativity.API;
using kCura.Relativity.Client.DTOs;
using RA.CreateProcessingSet.Models;

namespace RA.CreateProcessingSet.Rsapi
{
	public class Custodian
	{
		public static Int32 GetByName(IServicesMgr svcMgr, ExecutionIdentity identity, Int32 workspaceArtifactId, String custodianName)
		{
			using (var client = svcMgr.CreateProxy<IRSAPIClient>(identity))
			{
				client.APIOptions.WorkspaceID = workspaceArtifactId;
				var q = new kCura.Relativity.Client.DTOs.Query<kCura.Relativity.Client.DTOs.RDO>
				{
					ArtifactTypeGuid = Helpers.Constants.Guids.ObjectType.Custodian,
					Condition = new TextCondition(Helpers.Constants.Guids.Fields.Custodian.Name, TextConditionEnum.EqualTo, custodianName)
				};

				var results = client.Repositories.RDO.Query(q);

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

		public static Int32 Create(IServicesMgr svcMgr, 
            ExecutionIdentity identity, 
            Int32 workspaceArtifactId, 
            String fullName,
			String firstName, 
            String lastName,
            DestinationEnum destination)
		{
			using (var client = svcMgr.CreateProxy<IRSAPIClient>(identity))
			{
				client.APIOptions.WorkspaceID = workspaceArtifactId;
				var r = new RDO
                {
					ArtifactTypeGuids = new List<Guid> { Helpers.Constants.Guids.ObjectType.Custodian }
				};
				r.Fields.Add(new FieldValue(Helpers.Constants.Guids.Fields.Custodian.Name, fullName));
                if(destination == DestinationEnum.Custodian)
                {
                    r.Fields.Add(new FieldValue(Helpers.Constants.Guids.Fields.Custodian.FirstName, firstName));
                    r.Fields.Add(new FieldValue(Helpers.Constants.Guids.Fields.Custodian.LastName, lastName));
                    r.Fields.Add(new FieldValue(Helpers.Constants.Guids.Fields.Custodian.CustodianType,
                        Helpers.Constants.Guids.Choices.ProcessingSet.CustodianTypePerson));
                }
                else
                {
                    r.Fields.Add(new FieldValue(Helpers.Constants.Guids.Fields.Custodian.CustodianType,
                        Helpers.Constants.Guids.Choices.ProcessingSet.CustodianTypeEntity));
                }

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
	}
}