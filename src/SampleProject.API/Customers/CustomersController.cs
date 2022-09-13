using System;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.Extensions.Logging;
using SampleProject.API.Orders;
using SampleProject.Application.Customers;
using SampleProject.Application.Customers.RegisterCustomer;
using SampleProject.Domain.Customers;

namespace SampleProject.API.Customers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : Controller
    {
        private readonly ILogger<CustomerOrdersController> _logger;
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator, ILogger<CustomerOrdersController> _logger)
        {
            this._mediator = mediator;
            this._logger = _logger;
        }

        /// <summary>
        /// Register customer.
        /// </summary>
        [Route("")]
        [HttpPost]
        [ProducesResponseType(typeof(CustomerDto), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> RegisterCustomer([FromBody]RegisterCustomerRequest request)
        {
           var customer = await _mediator.Send(new RegisterCustomerCommand(request.Email, request.Name));

           return Created(string.Empty, customer);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            return Ok(new CustomerDto { Id = Guid.NewGuid() });
            _logger.LogInformation("Customerorder " + id+ " removed.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id)
        {
            return Ok(new CustomerDto { Id = Guid.NewGuid() });
        }
    }
}
