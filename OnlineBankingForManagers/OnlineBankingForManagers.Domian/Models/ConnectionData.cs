using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBankingForManagers.Domain.Models
{
    class ConnectionData : ConfigurationSection
    {
        [ConfigurationProperty("connectionString", DefaultValue = "sadfasdf", IsRequired = false)]
        public string ConnectionString
        {
            get
            {
                return (string)this["connectionString"];
            }
        }
    }
}
