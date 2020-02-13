
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Backend.data;
using Backend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CashRegister.WebApi.Controllers
{
    [ApiController]
    [Route("api/receipts")]
    public class ReceiptsController : ControllerBase
    {
        private readonly BackendDataContext DataContext;

        public ReceiptsController(BackendDataContext dataContext)
        {
            DataContext = dataContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]List<ReceiptLineDto> receiptLineDto)
        {
            if (receiptLineDto == null || receiptLineDto.Count == 0)
            {
                return BadRequest("Missing receipt lines");
            }

            // Read product data from DB for incoming product IDs
            var products = new Dictionary<int, Product>();
            foreach (var rl in receiptLineDto)
            {
                products[rl.ProductID] = await DataContext.Product.FirstOrDefaultAsync(p => p.ID == rl.ProductID);
                if (products[rl.ProductID] == null)
                {
                    return BadRequest($"Unknown product ID {rl.ProductID}");
                }
            }

            // Build receipt from DTO
            var newReceipt = new Receipt
            {
                ReceiptTimestamp = DateTime.UtcNow,
                ReceiptLines = receiptLineDto.Select(rl => new ReceiptLine
                {
                    ID = 0,
                    Product = products[rl.ProductID],
                    Amount = rl.Amount,
                    TotalPrice = rl.Amount * products[rl.ProductID].UnitPrice
                }).ToList()
            };
            newReceipt.TotalPrice = newReceipt.ReceiptLines.Sum(rl => rl.TotalPrice);

            await DataContext.Receipt.AddAsync(newReceipt);
            await DataContext.SaveChangesAsync();

            return StatusCode((int)HttpStatusCode.Created, newReceipt);
        }
    }
}