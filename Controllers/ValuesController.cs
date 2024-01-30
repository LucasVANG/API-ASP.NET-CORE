using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesCont : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ValuesCont(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        [Route("Read_Product")]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Product>>> ListProducts()
        {
            try
            {
                var products = await _context.Products.ToListAsync();

                if (products == null || products.Count == 0)
                    return NotFound();

                return Ok(products);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ListProducts: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("Read_Shop")]
        [ProducesResponseType(typeof(List<Shop>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Shop>>> ListShops()
        {
            try
            {
                var products = await _context.Shops.ToListAsync();

                if (products == null || products.Count == 0)
                    return NotFound();

                return Ok(products);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ListProducts: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("Read_Region")]
        [ProducesResponseType(typeof(List<Region>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Region>>> ListRegions()
        {
            try
            {
                var products = await _context.Regions.ToListAsync();

                if (products == null || products.Count == 0)
                    return NotFound();

                return Ok(products);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ListProducts: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("Create_Sale")]
        [ProducesResponseType(typeof(Sale), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Sale>> CreateSale([FromBody] Sale newSale)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _context.Sales.Add(newSale);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(ListSales), new { id = newSale.Sale_id }, newSale);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateSale: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }

        }

        [HttpGet]
        [Route("Read_Sale")]
        [ProducesResponseType(typeof(List<Sale>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Sale>>> ListSales()
        {
            try
            {
                var products = await _context.Sales.ToListAsync();

                if (products == null || products.Count == 0)
                    return NotFound();

                return Ok(products);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ListProducts: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("Read_Sale/{saleId}")]
        [ProducesResponseType(typeof(Sale), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Sale>> GetSale(int saleId)
        {
            try
            {
                var sale = await _context.Sales.FindAsync(saleId);

                if (sale == null)
                    return NotFound();

                return Ok(sale);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetSale: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }


        

        [HttpPut]
        [Route("Update_Sale/{saleId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateSale(int saleId, [FromBody] Sale updatedSale)
        {
            try
            {
                if (saleId != updatedSale.Sale_id)
                {
                    return BadRequest("There is a mismatch in the Id");
                }

                var existingSale = await _context.Sales.FindAsync(saleId);

                if (existingSale is null) 
                {
                    return NotFound("The Sale Id doesn't exist");
                }

                _context.Entry(existingSale).CurrentValues.SetValues(updatedSale);

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error in updating the Sale: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("Delete_Sale/{saleId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteSale(int saleId)
        {
            try
            {
                var sale = await _context.Sales.FindAsync(saleId);
                if (sale == null)
                {
                    return NotFound();
                }

                _context.Sales.Remove(sale);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteSale: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("CA_This_Month")]
        public async Task<ActionResult> GetMonthSales(int year,int month) 
        {
            var sales = await _context.Sales
                .Where(s => s.Sale_date.Year == year && s.Sale_date.Month == month)
                .Join(_context.Products, sale => sale.id_Product, product => product.Product_id, (sale, product) => new { sale, product })
                .Join(_context.Shops, sp => sp.sale.id_shop, shop => shop.Shop_id, (sp, shop) => new { sp.sale, sp.product, shop })
                .Join(_context.Regions, spp => spp.shop.id_Region, region => region.Region_id, (spp, region) => new { spp.sale, spp.product, spp.shop, region })
                .GroupBy(x => x.sale.id_Product)
                .Select(g => new
                {
                    CA = g.Sum(x => x.sale.Quantity * x.product.Price),
                    g.Key,
                    g.First().product.Name_Product,
                    g.First().product.Desc_Product
                })
                .OrderByDescending(x => x.CA)
                .ToListAsync();

            return Ok(sales);
        }
        [HttpGet("CA_Aggregated")]
        public async Task<ActionResult> GetSalesBeforeDate(int year, int month)
        {
            var sales = await _context.Sales
                .Where(s => s.Sale_date.Year <= year && s.Sale_date.Month <= month)
                .Join(_context.Products, sale => sale.id_Product, product => product.Product_id, (sale, product) => new { sale, product })
                .Join(_context.Shops, sp => sp.sale.id_shop, shop => shop.Shop_id, (sp, shop) => new { sp.sale, sp.product, shop })
                .Join(_context.Regions, spp => spp.shop.id_Region, region => region.Region_id, (spp, region) => new { spp.sale, spp.product, spp.shop, region })
                .GroupBy(x => x.sale.id_Product)
                .Select(g => new
                {
                    CA = g.Sum(x => x.sale.Quantity * x.product.Price),
                    g.Key,
                    g.First().product.Name_Product,
                    g.First().product.Desc_Product
                })
                .OrderByDescending(x => x.CA)
                .ToListAsync();

            return Ok(sales);
        }
    }
}

