using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orders.Business.Abstract;
using Orders.Dal.Command.Request;
using Orders.Dal.Query.Request;
using Orders.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetOrder(GetOrderQueryRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState.ErrorCount);
            }

            var result = await _orderService.GetOrder(request);

            if (!result.Success || result.Data == null)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }

        [HttpPut("changestatus")]
        public async Task<IActionResult> ChangeStatus(ChangeStatusOrderCommandRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.ErrorCount);
            }

            var result = _orderService.ChangeStatusOrder(request);

            if (!result.Result.Success)
            {
                return BadRequest(result.Result.Message);
            }

            return Ok(result.Result.Success);

        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll(GetAllOrderQueryRequest request)
        {
            var result = await _orderService.GetAllOrder(request);
            if (!result.Success || !result.Data.Orders.Any())
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Data);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder(CreateOrderCommandRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.ErrorCount);
            }

            var result = await _orderService.CreateAsync(request);

            if (!result.Success || result.Data.Id == Guid.Empty)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data.Id);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateOrder(UpdateOrderCommandRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.ErrorCount);
            }

            var result = await _orderService.UpdateAsync(request);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Success);

        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteOrder(DeleteOrderCommandRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.ErrorCount);
            }

            var result = await _orderService.DeleteAsync(request);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Success);
        }

    }
}

