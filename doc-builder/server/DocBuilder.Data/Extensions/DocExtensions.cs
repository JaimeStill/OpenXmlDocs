using DocBuilder.Core.ApiQuery;
using DocBuilder.Core.Extensions;
using DocBuilder.Data.Entities;
using DocBuilder.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DocBuilder.Data.Extensions
{
    public static class DocExtensions
    {
        #region Doc

        static IQueryable<Doc> SetIncludes(this DbSet<Doc> docs) =>
            docs.Include(x => x.Category);

        static IQueryable<Doc> Search(this IQueryable<Doc> docs, string search) =>
            docs.Where(x =>
                x.Name.ToLower().Contains(search.ToLower())
                || (string.IsNullOrEmpty(x.Description) ? false : x.Description.ToLower().Contains(search.ToLower()))
                || (x.Category == null ? false : x.Category.Value.ToLower().Contains(search.ToLower()))
            );

        public static async Task<QueryResult<Doc>> QueryDocs(
            this AppDbContext db,
            string page, string pageSize,
            string search, string sort
        )
        {
            var container = new QueryContainer<Doc>(
                db.Docs
                  .SetIncludes(),
                page, pageSize, search, sort
            );

            return await container.Query((docs, s) =>
                docs.SetupSearch(s, (values, term) =>
                    values.Search(term)
                )
            );
        }

        public static async Task<Doc?> GetDoc(this AppDbContext db, int id) =>
            await db.Docs
                .SetIncludes()
                .FirstOrDefaultAsync(x => x.Id == id);

        public static async Task<SaveResult> SaveDoc(this AppDbContext db, Doc doc)
        {
            var res = await doc.Validate(db);

            if (doc.Id > 0)
                await db.UpdateDoc(doc);
            else
                await db.AddDoc(doc);


            return res;
        }

        public static async Task RemoveDoc(this AppDbContext db, Doc doc)
        {
            db.Docs.Remove(doc);
            await db.SaveChangesAsync();
        }

        static async Task AddDoc(this AppDbContext db, Doc doc)
        {
            await db.Docs.AddAsync(doc);
            await db.SaveChangesAsync();
        }

        static async Task UpdateDoc(this AppDbContext db, Doc doc)
        {
            db.Docs.Update(doc);
            await db.SaveChangesAsync();
        }

        static async Task<SaveResult> Validate(this Doc doc, AppDbContext db)
        {

            if (string.IsNullOrEmpty(doc.Name))
                return new SaveResult { IsValid = false, Message = "Document must have a name." };

            var check = await db.Docs
                .FirstOrDefaultAsync(x =>
                    x.Id != doc.Id
                    && x.Name.ToLower() == doc.Name.ToLower()
                );

            return check is null
                ? new SaveResult { IsValid = true }
                : new SaveResult { IsValid = false, Message = $"Document name {doc.Name} is already in use." };
        }

        #endregion

        #region Category

        static IQueryable<DocCategory> Search(this IQueryable<DocCategory> categories, string search) =>
            categories.Where(x =>
                x.Value.ToLower().Contains(search.ToLower())
            );

        public static async Task<QueryResult<DocCategory>> QueryDocCategories(
            this AppDbContext db,
            string page, string pageSize,
            string search, string sort
        )
        {
            var container = new QueryContainer<DocCategory>(
                db.DocCategories,
                page, pageSize, search, sort
            );

            return await container.Query((categories, s) =>
                categories.SetupSearch(s, (data, term) =>
                    data.Search(term)
                )
            );
        }

        public static async Task<DocCategory?> GetDocCategory(this AppDbContext db, int id) =>
            await db.DocCategories
                .FindAsync(id);

        public static async Task<SaveResult> SaveDocCategory(this AppDbContext db, DocCategory category)
        {
            var res = await category.Validate(db);

            if (res.IsValid)
            {
                if (category.Id > 0)
                    await db.UpdateDocCategory(category);
                else
                    await db.AddDocCategory(category);
            }

            return res;
        }

        public static async Task RemoveDocCategory(this AppDbContext db, DocCategory category)
        {
            db.DocCategories.Remove(category);
            await db.SaveChangesAsync();
        }

        static async Task AddDocCategory(this AppDbContext db, DocCategory category)
        {
            await db.DocCategories.AddAsync(category);
            await db.SaveChangesAsync();
        }

        static async Task UpdateDocCategory(this AppDbContext db, DocCategory category)
        {
            db.DocCategories.Update(category);
            await db.SaveChangesAsync();
        }

        static async Task<SaveResult> Validate(this DocCategory category, AppDbContext db)
        {
            var res = new SaveResult();

            if (string.IsNullOrEmpty(category.Value))
                return new SaveResult { IsValid = false, Message = "Category must have a value." };

            var check = await db.DocCategories
                .FirstOrDefaultAsync(x =>
                    x.Id != category.Id
                    && x.Value.ToLower() == category.Value.ToLower()
                );

            return check is null
                ? new SaveResult { IsValid = false, Message = $"Category {category.Value} is already in use."}
                : new SaveResult { IsValid = true };
        }

        #endregion

        #region Item

        public static async Task<List<DocItem>> GetDocItems(this AppDbContext db, int docId) =>
            await db.DocItems
                .Where(x => x.DocId == docId)
                .OrderBy(x => x.Index)
                .ToListAsync();

        public static async Task<DocItem?> GetDocItem(this AppDbContext db, int id) =>
            await db.DocItems
                .FindAsync(id);

        public static async Task<SaveResult> SaveDocItem(this AppDbContext db, DocItem item)
        {
            var res = item.Validate();

            if (item.Id > 0)
                await db.UpdateDocItem(item);
            else
                await db.AddDocItem(item);

            return res;
        }

        public static async Task RemoveDocItem(this AppDbContext db, DocItem item)
        {
            db.DocItems.Remove(item);
            await db.SaveChangesAsync();
        }

        static async Task AddDocItem(this AppDbContext db, DocItem item)
        {
            await db.DocItems.AddAsync(item);
            await db.SaveChangesAsync();
        }

        static async Task UpdateDocItem(this AppDbContext db, DocItem item)
        {
            db.DocItems.Update(item);
            await db.SaveChangesAsync();
        }

        static SaveResult Validate(this DocItem item)
        {
            if (item.DocId < 1)
                return new SaveResult { IsValid = false, Message = "An Item must be associated with a Document." };

            if (string.IsNullOrEmpty(item.Value))
                return new SaveResult { IsValid = false, Message = "An Item must have a value." };

            return new SaveResult { IsValid = true };
        }

        #endregion
    }
}