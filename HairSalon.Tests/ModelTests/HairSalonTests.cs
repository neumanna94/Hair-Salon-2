using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
namespace HairSalon.Tests
{
    [TestClass]
    public class HairSalonTests : IDisposable
    {
      public HairSalonTests()
     {
         DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=alexander_neumann_test;";
     }
     public void Dispose()
     {
       Stylist.DeleteAll();
       Client.DeleteAll();
     }

     [TestMethod]
     public void Equals_OverrideTrueForSameDescription_Stylist()
     {
       //Arrange, Act
       Stylist firstItem = new Stylist("Test", 1);
       Stylist secondItem = new Stylist("Test", 1);

       //Assert
       Assert.AreEqual(firstItem, secondItem);
     }

     [TestMethod]
     public void Save_SavesItemToDatabase_Stylist()
     {
       //Arrange
       Stylist testItem = new Stylist("Mow the lawn", 1);
       testItem.Save();

       //Act
       List<Stylist> result = Stylist.GetAll();
       List<Stylist> testList = new List<Stylist>{testItem};

       //Assert
       CollectionAssert.AreEqual(testList, result);
     }
    [TestMethod]
     public void Save_DatabaseAssignsIdToObject_StylistId()
     {
       //Arrange
       Stylist testItem = new Stylist("Mow the lawn", 1);
       testItem.Save();

       //Act
       Stylist savedItem = Stylist.GetAll()[0];

       int result = savedItem.GetId();
       int testId = testItem.GetId();

       //Assert
       Assert.AreEqual(testId, result);
     }

     [TestMethod]
     public void Find_FindsItemInDatabase_Stylist()
     {
       //Arrange
       Stylist testItem = new Stylist("Mow the lawn", 1);
       testItem.Save();

       //Act
       Stylist foundItem = Stylist.Find(testItem.GetId());

       //Assert
       Assert.AreEqual(testItem, foundItem);
     }
     //CLIENT Testing CLIENT Testing CLIENT Testing CLIENT Testing CLIENT Testing CLIENT Testing CLIENT Testing
     [TestMethod]
     public void Equals_OverrideTrueForSameDescription_Client()
     {
       //Arrange, Act
       Client firstItem = new Client("Test", 1);
       Client secondItem = new Client("Test", 1);

       //Assert
       Assert.AreEqual(firstItem, secondItem);
     }

     [TestMethod]
     public void Save_SavesItemToDatabase_ClientList()
     {
       //Arrange
       Client testItem = new Client("Mow the lawn", 1);
       testItem.Save();

       //Act
       List<Client> result = Client.GetAll();
       List<Client> testList = new List<Client>{testItem};

       //Assert
       CollectionAssert.AreEqual(testList, result);
     }
    [TestMethod]
     public void Save_DatabaseAssignsIdToObject_ClientId()
     {
       //Arrange
       Client testItem = new Client("Mow the lawn", 1);
       testItem.Save();

       //Act
       Client savedItem = Client.GetAll()[0];

       int result = savedItem.GetId();
       int testId = testItem.GetId();

       //Assert
       Assert.AreEqual(testId, result);
     }

     [TestMethod]
     public void Find_FindsItemInDatabase_Client()
     {
       //Arrange
       Client testItem = new Client("Mow the lawn", 1);
       testItem.Save();

       //Act
       Client foundItem = Client.Find(testItem.GetId());

       //Assert
       Assert.AreEqual(testItem, foundItem);
     }
 }
}
