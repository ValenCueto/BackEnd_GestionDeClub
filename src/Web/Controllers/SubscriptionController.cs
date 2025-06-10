using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin,Gerente")]
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet]
        public IActionResult GetAllSubscriptions()
        {
            return Ok(_subscriptionService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetSubscriptionById(int id)
        {
            var subscription = _subscriptionService.GetById(id);
            return Ok(subscription);
        }

        [HttpPost]
        public IActionResult CreateSubscription([FromBody] SubscriptionCreateRequest rq)
        {
            _subscriptionService.CreateSubscription(rq);
            return Ok();
        }

        [HttpPut("{subscriptionId}")]
        public IActionResult UpdateSubscription([FromBody] SubscriptionCreateRequest rq, int subscriptionId)
        {
            var subscription = _subscriptionService.GetById(subscriptionId);

            if (subscription == null)
            {
                return NotFound($"No se encontró la suscripción con ID: {subscriptionId}");
            }

            _subscriptionService.UpdateSubscription(rq, subscriptionId);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSubscription(int id)
        {
            var subscription = _subscriptionService.GetById(id);
            _subscriptionService.DeleteSubscription(id);
            return Ok();
        }
    }
}
