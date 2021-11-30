using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DocBuilder.Data;
using DocBuilder.Data.Entities;
using DocBuilder.Data.Extensions;
using Microsoft.AspNetCore.OData.Query;

namespace DocBuilder.Web.Controllers
{
    [Route("api/[controller]")]
    public class DocController : Controller
    {
        private AppDbContext db;

        public DocController(AppDbContext db)
        {
            this.db = db;
        }

        [HttpGet("[action]")]
        [EnableQuery]
        public async Task<List<DocCategory>> GetDocCategories() => await db.GetDocCategories();
    }
}