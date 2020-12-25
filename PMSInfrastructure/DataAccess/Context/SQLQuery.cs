using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Types;
using System;
using System.Data;

namespace PMS.Infrastructure.DataAccess.Contextd
{
    public class SQLQuery
    {
        private void ShowHierarchyId(SqlConnection conn)
        {
            Console.WriteLine("Testing SQL Server HiearachyID Data Type");
            Console.WriteLine();

            var n1 = SqlHierarchyId.Parse("/1/1.1/2/");

            var n2 = conn.ExecuteScalar<SqlHierarchyId>("SELECT @h.GetAncestor(2) AS HID", new { @h = n1 });

            Console.WriteLine("Is {0} a descendant of {1}? {2}", n1, n2, n1.IsDescendantOf(n2));

            Console.WriteLine();
        }
    }
}

