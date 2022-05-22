using Customers.Business.Abstract;
using Customers.Dal.Command.Request;
using Customers.Dal.Query.Request;
using Customers.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(GetCustomerQueryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.ErrorCount);
            }

            var result =  await _customerService.GetCustomer(request);

            if(!result.Success || result.Data == null)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }

        [HttpGet("validate")]
        public async Task<IActionResult> Validate(ValidateCustomerQueryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.ErrorCount);
            }

            var result = _customerService.ValidateCustomer(request);

            if (!result.Result.Success)
            {
                return BadRequest(result.Result.Message);
            }

            return Ok(result.Result.Success);

        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll(GetAllCustomerQueryRequest request)
        {
            var result = await _customerService.GetAllCustomer(request);
            if(!result.Success || !result.Data.Customers.Any())
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Data);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateCustomerCommandRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.ErrorCount);
            }

            var result = await _customerService.CreateAsync(request);

            if(!result.Success || result.Data.Id == Guid.Empty)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data.Id);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateCustomerCommandRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.ErrorCount);
            }

            var result = await _customerService.UpdateAsync(request);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Success);

        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(DeleteCustomerCommandRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.ErrorCount);
            }

            var result = await _customerService.DeleteAsync(request);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Success);
        }

    }
}
