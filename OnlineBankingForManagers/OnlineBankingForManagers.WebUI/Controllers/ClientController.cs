using System;
using System.Linq;
using System.Web.Mvc;
using OnlineBankingForManagers.Domain.Abstract;
using OnlineBankingForManagers.Domain.Models;
using OnlineBankingForManagers.WebUI.Models;
using System.Linq.Dynamic;
using OnlineBankingForManagers.Domain.Components;


namespace OnlineBankingForManagers.WebUI.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private IClientRepository repository;
        public ClientController(IClientRepository repository)
        {
            this.repository = repository;
        }
        public ViewResult List(string status, string sort = "ClientId", int page = 1, int pageSize = 10)
        {
            ClientsListViewModel model = new ClientsListViewModel();
         
            model.PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,                    
                };
            
           
                var _clients = status == null
                ? repository.Clients
                : repository.Clients.Where(e => e.Status.ToString() == status);
                model.CurrentStatus = status;
                model.CurrentSort = sort;
            model.CurrentPageSaze = pageSize;

            model.PagingInfo.TotalItems = _clients.Count();

            model.Clients = repository.Clients
                .Where(c => ((status == null) || (c.Status.ToString() == status)))
                .OrderBy(sort)
                .Skip((page - 1)*pageSize)
                .Take(pageSize);

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
              var result = repository.SaveClient(client);
                if (result == DbResultType.Executed)
                {
                    TempData["message"] = string.Format("{0} has been saved", client.ContractNumber);
                    return RedirectToAction("List");
                }
                TempData["message"] = string.Format("{0} {1} hasn't been saved (error {2})", client.FirstName, client.LastName, result);                            
            }                                           
            return View(client);            
        }
        public ViewResult Create()
        {
            return View("Edit", new Client() { DateOfBirth = DateTime.Today, ContractNumber = null});
        }
//        [HttpPost]
        public ActionResult Delete(int clientId)
        {
            string name = "";
           var result = repository.DeleteClient(clientId, ref name);
            if (result == DbResultType.Executed)
            {
                TempData["message"] = string.Format("{0} was deleted", name);
            }
            else
            {
                TempData["message"] = string.Format("{0} wasn't deleted (error: {1})",name ,result);
            }
            return RedirectToAction("List");
        }
    }
}
