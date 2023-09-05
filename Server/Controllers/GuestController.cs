using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Wedding.Data;
using Wedding.Shared.Models;

namespace Wedding.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly string _jsonFilePath = "Data/Local/Guests.json";
        private readonly DatabaseOptions _dbOptions;
        private readonly List<Guest>? _guests;
        

        public GuestController(ApplicationDbContext applicationDbContext, IOptions<DatabaseOptions> dbOptions)
        {
            _context = applicationDbContext;
            _dbOptions = dbOptions.Value;

            if (!_dbOptions.Online)
            {
                _guests = JsonConvert.DeserializeObject<List<Guest>>(System.IO.File.ReadAllText(_jsonFilePath));
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Guest>>> Getguests()
        {
            List<Guest>? guests;

            try
            {
                if (_dbOptions.Online)
                {
                    guests = await _context.guests.ToListAsync();
                }
                else
                {
                    guests = _guests?.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }    
            
            return Ok(guests);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Guest>> Getguest(Guid id)
        {
            Guest? guest;

            try
            {
                if (_dbOptions.Online)
                {
                    guest = await _context.guests.FirstOrDefaultAsync(x => x.Id == id);
                }
                else
                {
                    guest = _guests?.FirstOrDefault(x => x.Id == id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Ok(guest);

        }

        [HttpPatch]
        public async Task<ActionResult> Updateguest(Guest guest)
        {
            Guest? existingguest;

            if (guest == null)
            {
                return BadRequest();
            }

            if (_dbOptions.Online)
            {
                existingguest = await _context.guests.FirstOrDefaultAsync(x => x.Id == guest.Id);

                if(existingguest == null)
                {
                    return NotFound();
                }
                else
                {
                    existingguest.Name = guest.Name;
                    await _context.SaveChangesAsync();
                }    
            }
            else
            {
                existingguest = _guests?.FirstOrDefault(x => x.Id == guest.Id);

                if(existingguest != null)
                {
                    int? index = _guests?.IndexOf(existingguest);

                    if(index != null && index != -1 && _guests != null)
                    {
                        _guests[index.Value] = guest;
                        System.IO.File.WriteAllText(_jsonFilePath, JsonConvert.SerializeObject(_guests, Formatting.Indented));
                    }
                    else
                    {
                        return NotFound(guest.Name);
                    }                       
                }
            }

            return NoContent();
        }
    }
}