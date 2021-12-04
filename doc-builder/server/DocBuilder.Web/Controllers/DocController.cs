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
        public async Task<bool> VerifyDoc([FromBody]Doc doc) => await doc.Verify(db);

        [HttpPost("[action]")]
        public async Task<Doc> CloneDoc([FromBody]Doc doc) => await doc.Clone(db);

        [HttpPost("[action]")]
        public async Task<SaveResult> SaveDoc([FromBody]Doc doc) => await doc.Save(db);

        [HttpPost("[action]")]
        public async Task RemoveDoc([FromBody]Doc doc) => await doc.Remove(db);

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

        [HttpGet("[action]")]
        public async Task<List<DocCategory>> GetDocCategories() => await db.GetDocCategories();

        [HttpGet("[action]/{id}")]
        public async Task<DocCategory?> GetDocCategory([FromRoute]int id) => await db.GetDocCategory(id);

        [HttpPost("[action]")]
        public async Task<bool> VerifyCategory([FromBody]DocCategory category) => await category.Verify(db);

        [HttpPost("[action]")]
        public async Task<SaveResult> SaveDocCategory([FromBody]DocCategory category) => await category.Save(db);

        [HttpPost("[action]")]
        public async Task RemoveDocCategory([FromBody]DocCategory category) => await category.Remove(db);

        #endregion

        #region Items

        [HttpGet("[action]/{docId}")]
        public async Task<List<DocItem>> GetDocItems([FromRoute]int docId) => await db.GetDocItems(docId);

        [HttpGet("[action]/{id}")]
        public async Task<DocItem?> GetDocItem([FromRoute]int id) => await db.GetDocItem(id);

        [HttpPost("[action]")]
        public async Task<SaveResult> SaveDocItem([FromBody]DocItem item) => await item.Save(db);

        [HttpPost("[action]")]
        public async Task RemoveDocItem([FromBody]DocItem item) => await item.Remove(db);

        #endregion

        #region Options

        [HttpGet("[action]/{selectId}")]
        public async Task<List<DocOption>> GetDocOptions([FromRoute]int selectId) => await db.GetDocOptions(selectId);

        [HttpGet("[action]/{id}")]
        public async Task<DocOption?> GetDocOption([FromRoute]int id) => await db.GetDocOption(id);

        [HttpPost("[action]")]
        public async Task<SaveResult> SaveDocOption([FromBody]DocOption option) => await option.Save(db);

        [HttpPost("[action]")]
        public async Task RemoveDocOption([FromBody]DocOption option) => await option.Remove(db);

        #endregion

        #region Answers

        [HttpGet("[action]/{questionId}")]
        public async Task<DocAnswer?> GetDocAnswer([FromRoute]int questionId) => await db.GetDocAnswer(questionId);

        [HttpPost("[action]")]
        public async Task<SaveResult> SaveDocAnswer([FromBody]DocAnswer answer) => await answer.Save(db);

        [HttpPost("[action]")]
        public async Task RemoveDocAnswer([FromBody]DocAnswer answer) => await answer.Remove(db);

        #endregion
    }
}