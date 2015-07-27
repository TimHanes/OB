using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using OnlineBankingForManagers.Domain.Abstract;
using OnlineBankingForManagers.Domain.Personages;
using OnlineBankingForManagers.WebUI.Controllers;

namespace OnlineBankingForManagers.NUnitTests
{
    [TestFixture]
    class AdminTests
    {
        public void Index_Contains_All_Clients()
        {
            // Arrange - create the mock repository
            Mock<IClientRepository> mock = new Mock<IClientRepository>();
            mock.Setup(m => m.Clients)
                .Returns(new Client[]
                {
                    new Client {ClientId = 1, ContractNumber = "C1"},
                    new Client {ClientId = 2, ContractNumber = "C2"},
                    new Client {ClientId = 3, ContractNumber = "C3"}
                }
                .AsQueryable());
            // Arrange - create a controller
            AdminController target = new AdminController(mock.Object);
            // Action
            Client[] result = ((IEnumerable<Client>)target.Index().ViewData.Model).ToArray();
            // Assert
            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual("C1", result[0].ContractNumber);
            Assert.AreEqual("C2", result[1].ContractNumber);
            Assert.AreEqual("C3", result[2].ContractNumber);
        }

        [Test]
        public void Can_Edit_Client()
        {
            // Arrange - create the mock repository
            Mock<IClientRepository> mock = new Mock<IClientRepository>();
            mock.Setup(m => m.Clients).Returns(new Client[] {
    new Client {ClientId = 1, ContractNumber = "L1"},
    new Client {ClientId = 2, ContractNumber = "L2"},
    new Client {ClientId = 3, ContractNumber = "L3"}
  }.AsQueryable());
            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object);
            // Act
            Client l1 = target.Edit(1).ViewData.Model as Client;
            Client l2 = target.Edit(2).ViewData.Model as Client;
            Client l3 = target.Edit(3).ViewData.Model as Client;
            // Assert
            Assert.AreEqual(1, l1.ClientId);
            Assert.AreEqual(2, l2.ClientId);
            Assert.AreEqual(3, l3.ClientId);
        }

        [Test]
        public void Cannot_Edit_Nonexistent_Client()
        {
            // Arrange - create the mock repository
            Mock<IClientRepository> mock = new Mock<IClientRepository>();
            mock.Setup(m => m.Clients).Returns(new Client[] {
   new Client {ClientId = 1, ContractNumber = "L1"},
    new Client {ClientId = 2, ContractNumber = "L2"},
    new Client {ClientId = 3, ContractNumber = "L3"},
  }.AsQueryable());
            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object);
            // Act
            Client result = (Client)target.Edit(4).ViewData.Model;
            // Assert
            Assert.IsNull(result);
        }
        [Test]
        public void Can_Save_Valid_Changes()
        {
            // Arrange - create mock repository
            Mock<IClientRepository> mock = new Mock<IClientRepository>();
            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object);
            // Arrange - create a client
            Client client = new Client { ContractNumber = "Test" };
            // Act - try to save the client
            ActionResult result = target.Edit(client);
            // Assert - check that the repository was called
            mock.Verify(m => m.SaveClient(client));
            // Assert - check the method result type

            Assert.IsNotInstanceOfType( typeof(ViewResult), result);
        }

        [Test]
        public void Cannot_Save_Invalid_Changes()
        {
            // Arrange - create mock repository
            Mock<IClientRepository> mock = new Mock<IClientRepository>();
            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object);
            // Arrange - create a client
            Client client = new Client { ContractNumber = "Test" };
            // Arrange - add an error to the model state
            target.ModelState.AddModelError("error", "error");
            // Act - try to save the client
            ActionResult result = target.Edit(client);
            // Assert - check that the repository was not called
            mock.Verify(m => m.SaveClient(It.IsAny<Client>()), Times.Never());
            // Assert - check the method result type
            Assert.IsInstanceOfType(typeof(ViewResult), result);
        }
        [Test]
        public void Can_Delete_Valid_Clients()
        {
            // Arrange - create a client
            Client cl = new Client { ClientId = 2, ContractNumber = "Test" };
            // Arrange - create the mock repository
            Mock<IClientRepository> mock = new Mock<IClientRepository>();
            mock.Setup(m => m.Clients).Returns(new Client[] {
    new Client {ClientId = 1, ContractNumber = "L1"},
    cl,
    new Client {ClientId = 3, ContractNumber = "L3"},
  }.AsQueryable());
            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object);
            // Act - delete the client
            target.Delete(cl.ClientId);
            // Assert - ensure that the repository delete method was
            // called with the correct client
            mock.Verify(m => m.DeleteClient(cl.ClientId));
        }

    }
}
