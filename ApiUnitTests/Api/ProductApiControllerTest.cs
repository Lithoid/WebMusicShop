 using BL;
using Entities;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using StoreWebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUnitTests.Api
{
    public class ProductApiControllerTest
    {

        private IProductRepository _productRepository;
        public ProductApiControllerTest()
        {
            _productRepository = A.Fake<IProductRepository>();
        }
       
        [Fact]
        public async Task GetAllProduct_ReturnsOk()
        {
            //Arrange
            var products = A.Fake<List<Product>>();
            A.CallTo(() => _productRepository.GetAllItemsAsync(null, "")).Returns(products);
            var controller = new ProductApiController(_productRepository);
            //Act
            var actionResult = await controller.GetProducts();
            //Assert
            actionResult.Result.Should().BeOfType<OkObjectResult>();

        }
        [Fact]
        public async Task CreateProduct_ReturnsCreated()
        {
            //Arrange
            var model = A.Dummy<ProductViewModel>();
            A.CallTo(() => _productRepository.AddItemAsync(model,true)).Returns(true);
            var controller = new ProductApiController(_productRepository);
            //Act
            var actionResult = await controller.CreateProduct(model);
            //Assert
            actionResult.Result.Should().BeOfType<CreatedAtRouteResult>();

        }
        

        [Fact]
        public async Task DeleteProduct_ReturnsOk()
        {
            //Arrange
            var model = A.Dummy<Product>();
            A.CallTo(() => _productRepository.GetItemAsync(null,true,null)).Returns(model);
            A.CallTo(() => _productRepository.DeleteItemAsync(model.Id)).Returns(true);
            var controller = new ProductApiController(_productRepository);
            //Act
            var actionResult = await controller.DeleteProduct(model.Id);
            //Assert
            actionResult.Result.Should().BeOfType<OkObjectResult>();

        }
        [Fact]
        public async Task UpdateProduct_ReturnsOk()
        {
            //Arrange
            var id = Guid.NewGuid();
            var model = A.Dummy<ProductViewModel>();
            A.CallTo(() => _productRepository.GetItemAsync(p => p.Id == id, true, "Brand,Category,Assets,Favourites,Reviews")).Returns(model);
            A.CallTo(() => _productRepository.ChangeItemAsync(model)).Returns(true);
            var controller = new ProductApiController(_productRepository);
            //Act
            var actionResult = await controller.UpdateProduct(model.Id, model);
            //Assert
            actionResult.Result.Should().BeOfType<OkObjectResult>();

        }

        [Fact]
        public async Task GetFavouriteForUser_ReturnsOk()
        {
            //Arrange
            var model = A.Dummy<AppUser>();
            var products = A.Fake<List<Product>>();
            A.CallTo(() => _productRepository.GetAllItemsAsync(p => p.Favourites.Any(f => f.UserId == model.Id), "Brand,Category,Assets,Favourites,Reviews")).Returns(products);
            var controller = new ProductApiController(_productRepository);
            //Act
            var actionResult = await controller.GetFavouriteForUser(model.Id);
            //Assert
            actionResult.Result.Should().BeOfType<OkObjectResult>();

        }

    }
}
