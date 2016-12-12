using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RA.CreateProcessingSet.Rsapi
{
	public class MessageFormatter
	{
		public static StringBuilder FormatMessage(List<String> results, String message, bool success)
		{
			var messageList = new StringBuilder();

			if (!success)
			{
				messageList.AppendLine(message);
				results.ToList().ForEach(w => messageList.AppendLine(w));
			}

			return messageList;
		}
	}
}