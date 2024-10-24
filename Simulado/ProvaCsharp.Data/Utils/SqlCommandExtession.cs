using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaCsharp.Data.Repository
{
    public static class SqlCommandExtession
    {
        public static string GetGeneratedQuery(this SqlCommand dbCommand)
        {
            var query = dbCommand.CommandText;
            foreach (SqlParameter parameter in dbCommand.Parameters)
            {
                if (parameter.DbType == System.Data.DbType.String || parameter.DbType == System.Data.DbType.DateTime)
                    query = query.Replace(parameter.ParameterName, $"'{parameter.Value.ToString()}'");
                else
                    query = query.Replace(parameter.ParameterName, parameter.Value.ToString());
            }

            return query;
        }
    }
}