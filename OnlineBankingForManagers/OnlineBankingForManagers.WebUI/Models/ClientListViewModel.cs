using System.Collections.Generic;
using OnlineBankingForManagers.Domain.Personages;
using System.Web.Helpers;

namespace OnlineBankingForManagers.WebUI.Models
{
    public class ClientsListViewModel
    {

        public IEnumerable<Client> Clients { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentStatus { get; set; }
        public string CurrentSort { get; set; }
       
    }
}