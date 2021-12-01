using Microsoft.AspNetCore.Mvc;
using DocBuilder.Core.ApiQuery;
using DocBuilder.Data;
using DocBuilder.Data.Entities;
using DocBuilder.Data.Extensions;
using DocBuilder.Data.Models;

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

        #region Docs

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(QueryResult<Doc>), 200)]
        public async Task<IActionResult> QueryDocs(
            [FromQuery]string page,
            [FromQuery]string pageSize,
            [FromQuery]string search,
            [FromQuery]string sort
        ) => Ok(await db.QueryDocs(page, pageSize, search, sort));

        [HttpGet("[action]/{id}")]
        public async Task<Doc?> GetDoc([FromRoute]int id) => await db.GetDoc(id);

        [HttpPost("[action]")]
        public async Task<SaveResult> SaveDoc([FromBody]Doc doc) => await db.SaveDoc(doc);

        [HttpPost("[action]")]
        public async Task RemoveDoc([FromBody]Doc doc) => await db.RemoveDoc(doc);

        #endregion

        #region Category

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(QueryResult<DocCategory>), 200)]
        public async Task<IActionResult> QueryDocCategories(
            [FromQuery]string page,
            [FromQuery]string pageSize,
            [FromQuery]string search,
            [FromQuery]string sort
        ) => Ok(await db.QueryDocCategories(page, pageSize, search, sort));

        [HttpGet("[action]/{id}")]
        public async Task<DocCategory?> GetDocCategory([FromRoute]int id) => await db.GetDocCategory(id);

        [HttpPost("[action]")]
        public async Task<SaveResult> SaveDocCategory([FromBody]DocCategory category) => await db.SaveDocCategory(category);

        [HttpPost("[action]")]
        public async Task RemoveDocCategory([FromBody]DocCategory category) => await db.RemoveDocCategory(category);

        #endregion

        #region Items

        [HttpGet("[action]/{docId}")]
        public async Task<List<DocItem>> GetDocItems([FromRoute]int docId) => await db.GetDocItems(docId);

        [HttpGet("[action]/{id}")]
        public async Task<DocItem?> GetDocItem([FromRoute]int id) => await db.GetDocItem(id);

        [HttpPost("[action]")]
        public async Task<SaveResult> SaveDocItem([FromBody]DocItem item) => await db.SaveDocItem(item);

        [HttpPost("[action]")]
        public async Task RemoveDocItem([FromBody]DocItem item) => await db.RemoveDocItem(item);

        #endregion
    }
}