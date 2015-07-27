using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using OnlineBankingForManagers.Domain.Abstract;
using OnlineBankingForManagers.Domain.Personages;
using OnlineBankingForManagers.WebUI.Controllers;
using OnlineBankingForManagers.WebUI.HtmlHelpers;
using OnlineBankingForManagers.WebUI.Models;

namespace OnlineBankingForManagers.NUnitTests
{
     [TestFixture]
    public class NUnitTest1
    {
         [Test]
         public void Can_Paginate()
            {
                // Arrange
                Mock<IClientRepository> mock = new Mock<IClientRepository>();
                mock.Setup(m => m.Clients).Returns(new Client[] {
            new Client {ClientId = 1, ContractNumber = "C1"},
            new Client {ClientId = 2, ContractNumber = "C2"},
            new Client {ClientId = 3, ContractNumber = "C3"},
            new Client {ClientId = 4, ContractNumber = "C4"},
            new Client {ClientId = 5, ContractNumber = "C5"}
           }.AsQueryable());
                ClientController controller = new ClientController(mock.Object);

                controller.PageSize = 3;
                // Act
               
                ClientsListViewModel result = (ClientsListViewModel)controller.List(null, 2).Model;
            
                // Assert
                Client[] clientArray = result.Clients.ToArray();
                Assert.IsTrue(clientArray.Length == 2);
                Assert.AreEqual(clientArray[0].ContractNumber, "C4");
                Assert.AreEqual(clientArray[1].ContractNumber, "C5");
            }

         [Test]
         public void Can_Generate_Page_Links()
         {
             // Arrange - define an HTML helper - we need to do this
             // in order to apply the extension method
             HtmlHelper myHelper = null;

             // Arrange - create PagingInfo data
             PagingInfo pagingInfo = new PagingInfo
             {
                 CurrentPage = 2,
                 TotalItems = 28,
                 ItemsPerPage = 10
             };

             // Arrange - set up the delegate using a lambda expression
             Func<int, string> pageUrlDelegate = i => "Page" + i;

             // Act
             MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

             // Assert
             Assert.AreEqual(result.ToString(), @"<a href=""Page1"">1</a>"
                                                + @"<a class=""selected"" href=""Page2"">2</a>"
                                                + @"<a href=""Page3"">3</a>");
         }
         [Test]
         public void Can_Send_Pagination_View_Model()
         {
             // Arrange
             Mock<IClientRepository> mock = new Mock<IClientRepository>();
             mock.Setup(m => m.Clients).Returns(new Client[] {
            new Client {ClientId = 1, ContractNumber = "C1"},
            new Client {ClientId = 2, ContractNumber = "C2"},
            new Client {ClientId = 3, ContractNumber = "C3"},
            new Client {ClientId = 4, ContractNumber = "C4"},
            new Client {ClientId = 5, ContractNumber = "C5"}
    }.AsQueryable());

             // Arrange
             ClientController controller = new ClientController(mock.Object);
             controller.PageSize = 3;

             // Act
             ClientsListViewModel result = (ClientsListViewModel)controller.List(null, 2).Model;

             // Assert
             PagingInfo pageInfo = result.PagingInfo;
             Assert.AreEqual(pageInfo.CurrentPage, 2);
             Assert.AreEqual(pageInfo.ItemsPerPage, 3);
             Assert.AreEqual(pageInfo.TotalItems, 5);
             Assert.AreEqual(pageInfo.TotalPages, 2);
         }
         [Test]
         public void Can_Filter_Clients()
         {
             // Arrange
             // - create the mock repository
             Mock<IClientRepository> mock = new Mock<IClientRepository>();
             mock.Setup(m => m.Clients).Returns(new Client[] {
            new Client {ClientId = 1, ContractNumber = "C1", Address = "A1"},
            new Client {ClientId = 2, ContractNumber = "C2", Address = "A2"},
            new Client {ClientId = 3, ContractNumber = "C3", Address = "A3"},
            new Client {ClientId = 4, ContractNumber = "C4", Address = "A2"},
            new Client {ClientId = 5, ContractNumber = "C5", Address = "A1"}
    }.AsQueryable());

             // Arrange - create a controller and make the page size 3 items
             ClientController controller = new ClientController(mock.Object);
             controller.PageSize = 3;

             // Action
             Client[] result = ((ClientsListViewModel)controller.List("A2", 1).Model)
               .Clients.ToArray();
             
             // Assert
             Assert.AreEqual(result.Length, 2);
             Assert.IsTrue(result[0].ContractNumber == "C2" && result[0].Address == "A2");
             Assert.IsTrue(result[1].ContractNumber == "C4" && result[1].Address == "A2");
         }
         [Test]
         public void Can_Create_Addresses()
         {
             // Arrange
             // - create the mock repository
             Mock<IClientRepository> mock = new Mock<IClientRepository>();
             mock.Setup(m => m.Clients).Returns(new Client[] {
            new Client {ClientId = 1, ContractNumber = "C1", Address = "Ukrain"},
            new Client {ClientId = 2, ContractNumber = "C2", Address = "Russia"},
            new Client {ClientId = 3, ContractNumber = "C3", Address = "American"},
            new Client {ClientId = 4, ContractNumber = "C4", Address = "Ukrain"},
            new Client {ClientId = 5, ContractNumber = "C5", Address = "Russia"}
    }.AsQueryable());

             // Arrange - create the controller
             NavController target = new NavController(mock.Object);

             // Act = get the set of categories
             string[] results = ((IEnumerable<string>)target.Menu().Model).ToArray();

             // Assert
             Assert.AreEqual(results.Length, 3);
             Assert.AreEqual(results[0], "American");
             Assert.AreEqual(results[1], "Russia");
             Assert.AreEqual(results[2], "Ukrain");
         }
         [Test]
         public void Indicates_Selected_Address()
         {
             // Arrange
             // - create the mock repository
             Mock<IClientRepository> mock = new Mock<IClientRepository>();
             mock.Setup(m => m.Clients).Returns(new Client[] {
            new Client {ClientId = 1, ContractNumber = "C1", Address = "Ukrain"},
            new Client {ClientId = 2, ContractNumber = "C2", Address = "Russia"},
              }.AsQueryable());

             // Arrange - create the controller
             NavController target = new NavController(mock.Object);

             // Arrange - define the category to selected
             string addressToSelect = "Russia";

             // Action
             string result = target.Menu(addressToSelect).ViewBag.SelectedAddress;

             // Assert
             Assert.AreEqual(addressToSelect, result);
         }
    } 
    }

