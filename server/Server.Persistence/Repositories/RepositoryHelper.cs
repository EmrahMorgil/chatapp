using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Persistence.Repositories
{
    public class RepositoryHelper
    {
        public static string GetInsertFields<T>()
        {
            var properties = typeof(T).GetProperties();
            var fieldNames = properties.Select(p => p.Name);
            return string.Join(", ", fieldNames);
        }

        public static string GetInsertFieldParams<T>()
        {
            var properties = typeof(T).GetProperties();
            var paramNames = properties.Select(p => "@" + p.Name);
            return string.Join(", ", paramNames);
        }
    }
}
