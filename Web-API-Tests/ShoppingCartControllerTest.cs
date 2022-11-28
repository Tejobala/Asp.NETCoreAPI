using Asp.NETCoreAPI.Controllers;
using Asp.NETCoreAPI.Models;
using Asp.NETCoreAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Web_API_Tests
{
    public class ShoppingCartControllerTest
    {
        private readonly ShoppingCartController _Controller;
        private readonly IShoppingCartServices _services;
        public ShoppingCartControllerTest()
        {
            _services = new ShoppingCartServiceFake();
            _Controller = new ShoppingCartController(_services);
        }

        [Fact]
        public void Get_WhenCalled_ReturnOkResult()
        {
            //Act
            var okResult = _Controller.Get();
            //Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }
        [Fact]
        public void Get_WhenCalled_ReturnAllItems()
        {
            //Act
            var okResult = _Controller.Get() as OkObjectResult;
            //Assert
            var items = Assert.IsType<List<ShoppingItem>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }
        [Fact]
        public void GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            //Act
            var notFoundResult = _Controller.Get(Guid.NewGuid());
            //Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }
        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsOkResults()
        {
            //Arrange
            var testGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");
            //Act
            var okResult = _Controller.Get(testGuid);
            //Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }
        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsRightItem()
        {
            //Arrange
            var testGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");
            //Act
            var okResult = _Controller.Get(testGuid) as OkObjectResult;
            //Assert
            Assert.IsType<ShoppingItem>(okResult.Value);
            Assert.Equal(testGuid, (okResult.Value as ShoppingItem).Id);
        }
        [Fact]
        public void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            //Arrange
            var nameMissingItem = new ShoppingItem()
            {
                Manufacturing = "Guinness",
                Price = 12.00M
            };
            _Controller.ModelState.AddModelError("Name", "Required");
            //Act
            var badResponse = _Controller.Post(nameMissingItem);
            //Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
        [Fact]
        public void Add_InvalidObjectPassed_ReturnsCreatedResponse()
        {
            //Arrange
            ShoppingItem testItem = new ShoppingItem()
            {
                Name = "Guinness Original 6Pack",
                Manufacturing = "Guinness",
                Price = 12.00M
            };
            //Act
            var createdResponse = _Controller.Post(testItem);
            //Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }
        [Fact]
        public void Add_InvalidObjectPassed_ReturnsResponseAsCreatedItem()
        {
            //Arrange
            var testItem = new ShoppingItem()
            {
                Name = "Guinness Original 6 pack",
                Manufacturing = "Guinness",
                Price = 12.00M
            };
            //Act
            var createdResponse = _Controller.Post(testItem) as CreatedAtActionResult;
            var item = createdResponse.Value as ShoppingItem;
            //Assert
            Assert.IsType<ShoppingItem>(item);
            Assert.Equal(testItem.Name, item.Name);
        }

        [Fact]
        public void Remove_NotExistingGuidPassed_ReturnsNotFoundResponse()
        {
            //Arrange
            var notExistingGuid = Guid.NewGuid();
            //Act
            var badResponse = _Controller.Delete(notExistingGuid);
            //Assert
            Assert.IsType<NotFoundResult>(badResponse);
        }
        [Fact]
        public void Remove_NotExistingGuidPassed_ReturnsNoContentResult()
        {
            //Arrange
            var existingGuid = new Guid("815accac-fb5b-478a-a9d6-f171a2f6ae7f");
            //Act
            var noContectResponse = _Controller.Delete(existingGuid);
            //Assert
            Assert.IsType<NoContentResult>(noContectResponse);
        }
        [Fact]
        public void Remove_ExistingGuidPassed_RemovesOneItem()
        {
            //Arrange
            var existingGuid = new Guid("33704c4a-5b87-464c-bfb6-51971b4d18ad");
            //Act
            var okResponse=_Controller.Delete(existingGuid);
            //Assert
            Assert.Equal(2,_services.GetAllItems().Count());
        }
    }
}