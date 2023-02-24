using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.Api.Data;
using Sales.Shared.Entities;

namespace Sales.Api.Controllers
{
	[ApiController]
	[Route("/api/countries")]
	public class CountriesController : ControllerBase
	{
		private readonly DataContext _context;

		public CountriesController(DataContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult> GetAsync()
		{
			return Ok(await _context.Countries.ToListAsync());
		}

		[HttpGet("{id:int}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);
            if (country == null)
            {
                return NotFound();
            }

			return Ok(country);
		}

		[HttpPost]
        public async Task<ActionResult> PostAsync(Country country)
        {
            _context.Add(country);
            try
            {
                await _context.SaveChangesAsync();
                return Ok(country);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un país con el mismo nombre.");
                }
                else
                {
                    return BadRequest(dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }


        [HttpPut]
		public async Task<ActionResult> PutAsync(Country country)
		{
			try
			{
                _context.Update(country);
                await _context.SaveChangesAsync();
                return Ok(country);
            }
			catch (DbUpdateException ex)
			{
				if (ex.InnerException!.Message.Contains("duplicate"))
				{
					return BadRequest("Ya existe un país con el mismo nombre.");
				}
                return BadRequest(ex.InnerException.Message);
            }
			catch(Exception ex)
			{
                return BadRequest(ex.Message);
            }
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult> DeleteAsync(int id)
		{
			var country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);
			if (country == null)
			{
				return NotFound();
			}

			_context.Remove(country);
			await _context.SaveChangesAsync();
			return NoContent();
		}
	}
}
