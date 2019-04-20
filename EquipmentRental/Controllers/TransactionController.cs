using EquipmentRental.Domain.DTOs;
using EquipmentRental.Provider;
using EquipmentRental.Provider.Utilities;
using EquipmentRental.Provider.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EquipmentRental.Controllers
{
    [Route("api/transactions")]
    public class TransactionController : Controller
    {
        private readonly ITransactionProvider _transactionProvider;
        public TransactionController(ITransactionProvider transactionProvider)
        {
            _transactionProvider = transactionProvider;
        }

        //You might wonder why the ids are sent in as separate parameters as opposed to 
        //sending them with the request body and receive it in the Transaction object. 
        //The reason is, ids should be passed into the action with the URL to follow the
        //REST standard. If we do decide to send in the ids with the Transaction object as well,
        //we should check that they are the same as the ones in the URL before taking any action.

        /// <summary>
        /// Creates a new Transaction.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /{customerId}/transaction
        ///     {
        ///        "days": 1,
        ///        "equipmentId": 1,
        ///        "type": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="customerId"></param>
        /// <param name="dto"></param>
        /// <returns>A newly created TransactionDTo</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="500">If the dto is null or the ModelState is invalid</response> 
        
        [HttpPost("{customerId}/transaction")]
        public IActionResult Post(string customerId, [FromBody]TransactionDTo dto)
        {
            if (dto == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = _transactionProvider.AddTransaction(customerId, dto);
            if (result == null) return StatusCode(500, "A problem occurred while handling your request.");
            return CreatedAtRoute("GetTransaction", new { id = result.TransactionID }, result);
        }

        /// <summary>
        /// Get all Transaction of a customer (Invoice).
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /{customerId}/transaction
        ///
        /// </remarks>
        /// <param name="customerId"></param>
        /// <returns code="200">A newly created Invoice</returns>
        /// <response code="500">If the ModelState is invalid</response> 

        [HttpGet("{customerId}/transaction")]
        public async Task<IActionResult> Get(string customerId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var invoice = await _transactionProvider.GetAllTransactions(customerId);
            return Ok(invoice);
        }

        /// <summary>
        /// Get a specific Transaction of a customer.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /{customerId}/transaction/{id}
        ///
        /// </remarks>
        /// <param name="customerId"></param>
        /// <param name="id">TranactionId</param>
        /// <returns code="200">A newly created Invoice</returns>
        /// <response code="404">If the TransactionDTO based on the customerId or TranactionId could not be found</response>
        /// <response code="500">If the ModelState is invalid</response>

        [HttpGet("{customerId}/transaction/{id}", Name = "GetTransaction")]
        public async Task<IActionResult> Get(string customerId, int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var item = await _transactionProvider.GetTransaction(customerId, id);
            if (item == null) return NotFound();//404 Not Found (Client Error Status Code)
            return Ok(item);//Get Successfull (Success Status Code)
        }

        /// <summary>
        /// Saving the Invoice.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /saveInvoice
        ///
        /// </remarks>
        /// <param name="invoice"></param>
        /// <returns code="204">No Content, for delete usually, successfull request that shouldn't return anything</returns>
        /// <response code="404">If the TransactionDTO based on the customerId or TranactionId could not be found</response>
        /// <response code="500">If the invoice is null or the ModelState is invalid or Internal Server Error</response>

        [HttpPost("saveInvoice")]
        public async Task<IActionResult> Post([FromBody]Invoice invoice)
        {
            if (invoice == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            invoice.SaveToTxt();
            var item = await _transactionProvider.DeleteTransactions();
            if (item == null)
                return StatusCode(500, "A problem occurred while handling your request.");
            return NoContent();
        }
    }
}