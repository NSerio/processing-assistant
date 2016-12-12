using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RA.CreateProcessingSet.Database
{
	public class QueryHelper
	{
		public static DataTable RetrieveProcessingSourceLocations(Relativity.API.IDBContext eddsDbConnection, Int32 workspaceArtifactId)
		{
			const String sql = @"
				DECLARE @codeTypeID INT
				SET @codeTypeID = 
					(
						SELECT TOP 1 CodeTypeID
						FROM [ExtendedCode]
						WHERE CodeType = 'ProcessingSourceLocation'
					)

				DECLARE @resourceGroupArtifactID INT
				SET @resourceGroupArtifactID =
					(
						SELECT ResourceGroupArtifactID 
						FROM EDDSDBO.ExtendedCase
						WHERE ArtifactID = @caseArtifactID 
					)

				DECLARE @SQL NVARCHAR(MAX)
				SET @SQL = '
				SELECT C.Name ProcessingSourceLocation
				FROM EDDSDBO.ZCodeArtifact_'+CAST(@codeTypeID AS VARCHAR)+' Z
					INNER JOIN EDDSDBO.Code C ON Z.CodeArtifactID = C.ArtifactID
				WHERE AssociatedArtifactID = '+CAST(@resourceGroupArtifactID AS VARCHAR)+' 
				ORDER BY C.[Order], C.[Name]
				'
				EXEC(@SQL)";

			var paramList = new List<SqlParameter>();
			var param = new SqlParameter("@caseArtifactID", SqlDbType.Int) { Value = workspaceArtifactId };
			paramList.Add(param);

			return eddsDbConnection.ExecuteSqlStatementAsDataTable(sql, paramList);
		}
	}
}