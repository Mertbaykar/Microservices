using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.RabbitMQ;
using Shared.RabbitMQ.Events.Product;

namespace ProductMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public ProductController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult> CreateProduct(ProductCreated createProductDTO)
        {
            try
            {
                await _publishEndpoint.Publish(createProductDTO);
                return Ok(createProductDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
