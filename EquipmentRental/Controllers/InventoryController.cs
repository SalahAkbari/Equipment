using EquipmentRental.Provider;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EquipmentRental.Controllers
{
    [Route("api/inventories")]
    public class InventoryController : Controller
    {
        private readonly IInventoryProvider _provider;

        public InventoryController(IInventoryProvider provider)
        {
            _provider = provider;
        }

        /// <summary>
        /// Get all Equipments.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /
        ///
        /// </remarks>
        /// <returns code="200">A list of Inventories</returns>
        /// <response code="500">If the ModelState is invalid</response> 
        
        [HttpGet]
        public async Task<IActionResult> GetInventories()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var dtOs = await _provider.GetAllInventories();
            return Ok(dtOs);
        }
    }
}