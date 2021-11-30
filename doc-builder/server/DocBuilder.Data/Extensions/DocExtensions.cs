using DocBuilder.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocBuilder.Data.Extensions
{
    public static class DocExtensions
    {
        #region Category

        public static async Task<List<DocCategory>> GetDocCategories(this AppDbContext db) =>
            await db.DocCategories
                .OrderBy(x => x.Value)
                .ToListAsync();

        #endregion
    }
}