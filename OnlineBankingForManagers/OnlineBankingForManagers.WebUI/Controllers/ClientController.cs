using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Helpers;
using System.Collections.Generic;
using OnlineBankingForManagers.Domain.Abstract;
using OnlineBankingForManagers.Domain.Models;
using OnlineBankingForManagers.WebUI.Models;
using System.Linq.Dynamic;


namespace OnlineBankingForManagers.WebUI.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private IClientRepository repository;

        public int PageSize = 4;

        public ClientController(IClientRepository repo)
        {
            repository = repo;
        }
        public ViewResult List(string status, string sort = "ClientId", int page = 1)
        {
            ClientsListViewModel model = new ClientsListViewModel();
         
            model.PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    
                };
            
           
                var a = status == null
                ? repository.Clients
                : repository.Clients.Where(e => e.Status.ToString() == status);
                model.CurrentStatus = status;
                model.CurrentSort = sort;

            model.PagingInfo.TotalItems = a.Count();

            model.Clients = repository.Clients
                .Where(c => ((status == null) || (c.Status.ToString() == status)))
                .OrderBy(sort)
                .Skip((page - 1)*PageSize)
                .Take(PageSize);

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
            return View("Edit", new Client() { DateOfBirth = DateTime.Today, ContractNumber = null});
        }
        [HttpPost]
        public ActionResult Delete(int clientId)
        {
            Client deletedClient = repository.DeleteClient(clientId);
            if (deletedClient != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedClient.ContractNumber.ToString());
            }
            return RedirectToAction("List");
        }
    }
}
