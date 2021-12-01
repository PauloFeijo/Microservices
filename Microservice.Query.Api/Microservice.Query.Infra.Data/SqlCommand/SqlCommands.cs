using Microservice.Query.Domain.Dtos;
using System.Diagnostics.CodeAnalysis;

namespace Microservice.Query.Infra.Data.SqlCommand
{
    [ExcludeFromCodeCoverage]
    public static class SqlCommands
    {
        public static string GetSqlEntryById() => $"{SqlEntry} WHERE ID = @Id ";

        public static string GetSqlEntries(EntryParamsDto param) => @$"
            {SqlEntry}
             WHERE 1 = 1
            {(param.UserName == null ? "" : " AND USER_NM LIKE @UserName")}
            {(param.InitialDate == null ? "" : " AND ENTRY_DT >= @InitialDate")}
            {(param.EndDate == null ? "" : " AND ENTRY_DT <= @EndDate")}
            {(param.AccountDescription == null ? "" : " AND ACCOUNT_DS LIKE @AccountDescription")}
            {(param.Description == null ? "" : " AND ENTRY_DS LIKE @Description")}
            {(param.Type == null ? "" : " AND ENTRY_TP LIKE @Type")}
            ORDER BY USER_NM, ENTRY_DT
            OFFSET @PageIndex ROWS 
            FETCH NEXT @PageSize ROWS ONLY
        ";

        private const string SqlEntry = @"
            SELECT ID AS Id,
                   USER_NM AS UserName,
                   ENTRY_DT AS Moment,
                   ENTRY_VL AS Value,
                   ENTRY_TP AS Type,
                   ACCOUNT_DS AS AccountDescription,
                   ENTRY_DS AS Description,
                   CREATED_AT_DT AS CreatedAt
              FROM TAB_ENTRY";
    }
}
