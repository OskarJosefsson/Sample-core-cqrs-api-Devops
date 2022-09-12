using System;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Application.Customers;
using SampleProject.Application.Customers.RegisterCustomer;

namespace SampleProject.API.Customers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : Controller
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            this._mediator = mediator;
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
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id)
        {
            return Ok(new CustomerDto { Id = Guid.NewGuid() });
        }
    }
}
