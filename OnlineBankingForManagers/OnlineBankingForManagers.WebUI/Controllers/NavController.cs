using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineBankingForManagers.Domain.Abstract;

namespace OnlineBankingForManagers.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IClientRepository repository;

        public NavController(IClientRepository repo)
        {
            repository = repo;
        }

        public PartialViewResult Menu(string status = null)
        {
            ViewBag.SelectedStatus = status;
            IEnumerable<string> statuses = repository.Clients
              .Select(x => x.Status.ToString())
              .Distinct()
              .OrderBy(x => x);
            return PartialView(statuses);
        }

    }
}
