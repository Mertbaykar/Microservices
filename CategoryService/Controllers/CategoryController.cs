using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.RabbitMQ;
using Shared.RabbitMQ.Events;
using Shared.RabbitMQ.Events.Category;

namespace CategoryMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public CategoryController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult> CreateCategory(CategoryCreated createCategoryDTO)
        {
            try
            {
                await _publishEndpoint.Publish(createCategoryDTO);
                return Ok(createCategoryDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
