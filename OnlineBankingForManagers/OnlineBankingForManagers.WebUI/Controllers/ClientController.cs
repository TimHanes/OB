using System.Linq;
using System.Web.Mvc;
using System.Web.Helpers;
using System.Collections.Generic;
using OnlineBankingForManagers.Domain.Abstract;
using OnlineBankingForManagers.Domain.Personages;
using OnlineBankingForManagers.WebUI.Models;

namespace OnlineBankingForManagers.WebUI.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        //
        // GET: /Client/
        private IClientRepository repository;


        public int PageSize = 4;

        public ClientController(IClientRepository repo)
        {
            repository = repo;
        }
        public ViewResult List(string status, int page = 1)
        {
            ClientsListViewModel model = new ClientsListViewModel
            {
                Clients = repository.Clients
              .Where(c => ((status == null)||(c.Status == status)))
                    .OrderBy(c => c.ClientId)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = status == null ?
        repository.Clients.Count() :
        repository.Clients.Where(e => e.Status == status).Count()
                },
                CurrentStatus = status
            };


            return View(model);
        }




      
        public ViewResult Edit(int clientId)
        {
            Client client = repository.Clients
              .FirstOrDefault(p => p.ClientId == clientId);
            return View(client);
        }
        [HttpPost]
        public ActionResult Edit(Client client)
        {
            if (ModelState.IsValid)
            {
                repository.SaveClient(client);
                TempData["message"] = string.Format("{0} has been saved", client.ContractNumber);
                return RedirectToAction("List");
            }
            else
            {
                // there is something wrong with the data values
                return View(client);
            }
        }
        public ViewResult Create()
        {
            return View("Edit", new Client());
        }
        [HttpPost]
        public ActionResult Delete(int clientId)
        {
            Client deletedClient = repository.DeleteClient(clientId);
            if (deletedClient != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedClient.ContractNumber);
            }
            return RedirectToAction("List");
        }
    }
}
