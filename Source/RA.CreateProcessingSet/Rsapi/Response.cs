using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kCura.Relativity.Client;
using kCura.Relativity.Client.DTOs;

namespace RA.CreateProcessingSet.Rsapi
{
	public class Response<TResultType>
	{
		public StringBuilder Message { get; set; }
		public bool Success { get; set; }
		public TResultType Results { get; set; }

		public static Response<IEnumerable<RDO>> CompileQuerySubsets(IRSAPIClient proxy, QueryResultSet<RDO> theseResults)
		{
			bool success = true;
			string message = "";
			var resultList = new List<Result<RDO>>();
			int iterator = 0;

			message += theseResults.Message;
			if (!theseResults.Success)
			{
				success = false;
			}

			resultList.AddRange(theseResults.Results);
			if (!String.IsNullOrEmpty(theseResults.QueryToken))
			{
				string queryToken = theseResults.QueryToken;
				int batchSize = theseResults.Results.Count();
				iterator += batchSize;
				do
				{
					theseResults = proxy.Repositories.RDO.QuerySubset(queryToken, iterator + 1, batchSize);
					resultList.AddRange(theseResults.Results);
					message += theseResults.Message;
					if (!theseResults.Success)
					{
						success = false;
					}
					iterator += batchSize;
				} while (iterator < theseResults.TotalCount);
			}

			var res = new Response<IEnumerable<RDO>>
			{
				Results = resultList.Select(x => x.Artifact),
				Success = success,
				Message = MessageFormatter.FormatMessage(resultList.Select(x => x.Message).ToList(), message, success)
			};
			return res;
		}

		public static Response<IEnumerable<RDO>> CompileWriteResults(WriteResultSet<RDO> theseResults)
		{
			bool success = true;
			string message = "";

			message += theseResults.Message;
			if (!theseResults.Success)
			{
				success = false;
			}

			var res = new Response<IEnumerable<RDO>>
			{
				Results = theseResults.Results.Select(x => x.Artifact),
				Success = success,
				Message = MessageFormatter.FormatMessage(theseResults.Results.Select(x => x.Message).ToList(), message, success)
			};
			return res;
		}

		public static Response<IEnumerable<kCura.Relativity.Client.DTOs.Error>> CompileWriteResults(WriteResultSet<kCura.Relativity.Client.DTOs.Error> theseResults)
		{
			bool success = true;
			string message = "";

			message += theseResults.Message;
			if (!theseResults.Success)
			{
				success = false;
			}

			var res = new Response<IEnumerable<kCura.Relativity.Client.DTOs.Error>>
			{
				Results = theseResults.Results.Select(x => x.Artifact),
				Success = success,
				Message = MessageFormatter.FormatMessage(theseResults.Results.Select(x => x.Message).ToList(), message, success)
			};
			return res;
		}
	}
}