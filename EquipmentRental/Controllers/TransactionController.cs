using EquipmentRental.Domain.DTOs;
using EquipmentRental.Provider;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EquipmentRental.Controllers
{
    [Route("api/transactions")]
    public class TransactionController : Controller
    {
        private readonly ITransactionProvider _provider;
        public TransactionController(ITransactionProvider provider)
        {
            _provider = provider;
        }

        [HttpPost("{customerId}/transaction")]
        public IActionResult Post(string customerId, [FromBody]TransactionDTo dto)
        {
            if (dto == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = _provider.AddTransaction(customerId, dto);
            if (result == null) return StatusCode(500, "A problem occurred while handling your request.");
            return CreatedAtRoute("GetTransaction", new { id = result.TransactionID }, result);
        }
    }
}