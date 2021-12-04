using Microsoft.AspNetCore.Mvc;
using DocBuilder.Core.ApiQuery;
using DocBuilder.Data;
using DocBuilder.Data.Entities;
using DocBuilder.Data.Extensions;
using DocBuilder.Data.Models;

namespace DocBuilder.Web.Controllers
{
    [Route("api/[controller]")]
    public class DocTController : Controller
    {
        private AppDbContext db;

        public DocTController(AppDbContext db)
        {
            this.db = db;
        }

        #region DocTs

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(QueryResult<DocT>), 200)]
        public async Task<IActionResult> QueryDocTs(
            [FromQuery]string page,
            [FromQuery]string pageSize,
            [FromQuery]string search,
            [FromQuery]string sort
        ) => Ok(await db.QueryDocTs(page, pageSize, search, sort));

        [HttpGet("[action]/{id}")]
        public async Task<DocT?> GetDocT([FromRoute]int id) => await db.GetDocT(id);

        [HttpPost("[action]")]
        public async Task<bool> VerifyTemplate([FromBody]DocT doc) => await doc.Verify(db);

        [HttpPost("[action]")]
        public async Task<DocT> CloneDocT([FromBody]DocT doc) => await doc.Clone(db);

        [HttpPost("[action]")]
        public async Task<Doc> GenerateDoc([FromBody]DocT doc) => await doc.Generate(db);

        [HttpPost("[action]")]
        public async Task<SaveResult> SaveDocT([FromBody]DocT doc) => await doc.Save(db);

        [HttpPost("[action]")]
        public async Task RemoveDocT([FromBody]DocT doc) => await doc.Remove(db);

        #endregion

        #region ItemTs

        [HttpGet("[action]/{docTId}")]
        public async Task<List<DocItemT>> GetDocItemTs([FromRoute]int docTId) => await db.GetDocItemTs(docTId);

        [HttpGet("[action]/{id}")]
        public async Task<DocItemT?> GetDocItemT([FromRoute]int id) => await db.GetDocItemT(id);

        [HttpPost("[action]")]
        public async Task<SaveResult> SaveDocItemT([FromBody]DocItemT item) => await item.Save(db);

        [HttpPost("[action]")]
        public async Task RemoveDocItemT([FromBody]DocItemT item) => await item.Remove(db);

        #endregion

        #region OptionTs

        [HttpGet("[action]/{selectId}")]
        public async Task<List<DocOptionT>> GetDocOptionTs([FromRoute]int selectId) => await db.GetDocOptionTs(selectId);

        [HttpGet("[action]/{id}")]
        public async Task<DocOptionT?> GetDocOptionT([FromRoute]int id) => await db.GetDocOptionT(id);

        [HttpPost("[action]")]
        public async Task<SaveResult> SaveDocOptionT([FromBody]DocOptionT option) => await option.Save(db);

        [HttpPost("[action]")]
        public async Task RemoveDocOptionT([FromBody]DocOptionT option) => await option.Remove(db);

        #endregion
    }
}