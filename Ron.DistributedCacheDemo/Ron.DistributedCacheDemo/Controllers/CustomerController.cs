using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ron.DistributedCacheDemo.BLL;

namespace Ron.DistributedCacheDemo.Controllers
{
    [Route("api/Customer")]
    [ApiController]
    public class CustomerController : Controller
    {
        private IDistributedCache cache;
        public CustomerController(IDistributedCache cache)
        {
            this.cache = cache;
        }

        [HttpGet("NewId")]
        public async Task<ActionResult<string>> NewId()
        {
            var id = Guid.NewGuid().ToString("N");
            var timeSpan = new TimeSpan(0, 0, 20);
            await this.cache.SetStringAsync("CustomerId101111", id, new DistributedCacheEntryOptions().SetSlidingExpiration(timeSpan));         
            return id;
        }

        [HttpGet("GetId")]
        public async Task<ActionResult<string>> GetId()
        {
            var id = await this.cache.GetStringAsync("CustomerId101111");
            
            return id;
        }
    }
}
