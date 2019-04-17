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

        [HttpGet]
        public async Task<IActionResult> GetInventories()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var dtOs = await _provider.GetAllInventories();
            return Ok(dtOs);
        }
    }
}