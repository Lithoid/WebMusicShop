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
    public class OrderApiControllerTest
    {
        private IOrderRepository _orderRepository;
        public OrderApiControllerTest()
        {
            _orderRepository = A.Fake<IOrderRepository>();
        }

        [Fact]
        public async Task GetOrders_ReturnsOk()
        {
            //Arrange
            var orders = A.Fake<List<Order>>();
            A.CallTo(() => _orderRepository.GetAllItemsAsync(null, "")).Returns(orders);
            var controller = new OrderApiController(_orderRepository);
            //Act
            var actionResult = await controller.GetOrders();
            //Assert
            actionResult.Result.Should().BeOfType<OkObjectResult>();

        }

        [Fact]
        public async Task GetOrdersForUser_ReturnsOk()
        {
            //Arrange
            var user = A.Dummy<AppUser>();
            var orders = A.Fake<List<Order>>();
            A.CallTo(() => _orderRepository.GetAllItemsAsync(p => p.UserId== user.Id, "")).Returns(orders);
            var controller = new OrderApiController(_orderRepository);
            //Act
            var actionResult = await controller.GetOrdersForUser(user.Id);
            //Assert
            actionResult.Result.Should().BeOfType<OkObjectResult>();

        }

        [Fact]
        public async Task CreateProduct_ReturnsCreated()
        {
            //Arrange
            var model = A.Dummy<OrderViewModel>();
            A.CallTo(() => _orderRepository.AddItemAsync(model, true)).Returns(true);
            var controller = new OrderApiController(_orderRepository);
            //Act
            var actionResult = await controller.CreateOrder(model);
            //Assert
            actionResult.Result.Should().BeOfType<CreatedAtRouteResult>();

        }


        [Fact]
        public async Task DeleteProduct_ReturnsOk()
        {
            //Arrange
            var model = A.Dummy<Order>();
            A.CallTo(() => _orderRepository.GetItemAsync(null, true, null)).Returns(model);
            A.CallTo(() => _orderRepository.DeleteItemAsync(model.Id)).Returns(true);
            var controller = new OrderApiController(_orderRepository);
            //Act
            var actionResult = await controller.DeleteOrder(model.Id);
            //Assert
            actionResult.Result.Should().BeOfType<OkObjectResult>();

        }
        [Fact]
        public async Task UpdatOrder_ReturnsOk()
        {
            //Arrange
            var id = Guid.NewGuid();
            var model = A.Dummy<OrderViewModel>();
            A.CallTo(() => _orderRepository.GetItemAsync(p => p.Id == id, true, "")).Returns(model);
            A.CallTo(() => _orderRepository.ChangeItemAsync(model)).Returns(true);
            var controller = new OrderApiController(_orderRepository);
            //Act
            var actionResult = await controller.Update(model.Id, model);
            //Assert
            actionResult.Should().BeOfType<OkObjectResult>();

        }

       
    }
}
