using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Wedding.Data;
using Wedding.Shared.Models;

namespace Wedding.Server.Controllers
{
    /// <summary>
    /// Handle ONLINE/OFFLINE cf. DatabaseOptions
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        #region Properties
        private readonly ApplicationDbContext _context;
        private readonly string _jsonFilePath = "Data/Local/Accounts.json";
        private readonly DatabaseOptions _dbOptions;
        private readonly List<Account>? _accounts;
        #endregion

        #region Ctor
        public AccountController(ApplicationDbContext applicationDbContext, IOptions<DatabaseOptions> dbOptions)
        {
            _context = applicationDbContext;
            _dbOptions = dbOptions.Value;

            if (!_dbOptions.Online)
            {
                _accounts = JsonConvert.DeserializeObject<List<Account>>(System.IO.File.ReadAllText(_jsonFilePath));
            }
        }
        #endregion

        [HttpGet]
        public async Task<ActionResult<List<Account>>> GetAccounts()
        {
            List<Account>? accounts;

            try
            {
                if (_dbOptions.Online)
                {
                    accounts = await _context.Accounts.ToListAsync();
                }
                else
                {
                    accounts = _accounts?.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(string id)
        {
            Account? account;

            try
            {
                if (_dbOptions.Online)
                {
                    account = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == id);
                }
                else
                {
                    account = _accounts?.FirstOrDefault(x => x.Id == id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Ok(account);
        }
    }
}