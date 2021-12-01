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

        public static async Task<SaveResult> Save(this Doc doc, AppDbContext db)
        {
            var res = await doc.Validate(db);

            if (doc.Id > 0)
                await doc.Update(db);
            else
                await doc.Add(db);


            return res;
        }

        public static async Task Remove(this Doc doc, AppDbContext db)
        {
            db.Docs.Remove(doc);
            await db.SaveChangesAsync();
        }

        static async Task Add(this Doc doc, AppDbContext db)
        {
            await db.Docs.AddAsync(doc);
            await db.SaveChangesAsync();
        }

        static async Task Update(this Doc doc, AppDbContext db)
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

        public static async Task<SaveResult> Save(this DocCategory category, AppDbContext db)
        {
            var res = await category.Validate(db);

            if (res.IsValid)
            {
                if (category.Id > 0)
                    await category.Update(db);
                else
                    await category.Add(db);
            }

            return res;
        }

        public static async Task Remove(this DocCategory category, AppDbContext db)
        {
            db.DocCategories.Remove(category);
            await db.SaveChangesAsync();
        }

        static async Task Add(this DocCategory category, AppDbContext db)
        {
            await db.DocCategories.AddAsync(category);
            await db.SaveChangesAsync();
        }

        static async Task Update(this DocCategory category, AppDbContext db)
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

        public static async Task<SaveResult> Save(this DocItem item, AppDbContext db)
        {
            var res = item.Validate();

            if (item.Id > 0)
                await item.Update(db);
            else
                await item.Add(db);

            return res;
        }

        public static async Task Remove(this DocItem item, AppDbContext db)
        {
            db.DocItems.Remove(item);
            await db.SaveChangesAsync();
        }

        static async Task Add(this DocItem item, AppDbContext db)
        {
            await db.DocItems.AddAsync(item);
            await db.SaveChangesAsync();
        }

        static async Task Update(this DocItem item, AppDbContext db)
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

            if (item.Type == DocItemType.Select && item.Id < 1)
            {
                if (item.Options.Count < 1)
                    return new SaveResult { IsValid = false, Message = "A new Select Item must contain at least one Option." };

                var res = item.Options.Validate();

                if (!res.IsValid) return res;
            }

            return new SaveResult { IsValid = true };
        }

        #endregion

        #region Option

        public static async Task<List<DocOption>> GetDocOptions(this AppDbContext db, int selectId) =>
            await db.DocOptions
                .Where(x => x.DocItemId == selectId)
                .OrderBy(x => x.Value)
                .ToListAsync();

        public static async Task<DocOption?> GetDocOption(this AppDbContext db, int id) =>
            await db.DocOptions
                .FindAsync(id);

        public static async Task<SaveResult> Save(this DocOption option, AppDbContext db)
        {
            var res = await option.Validate(db);

            if (option.Id > 0)
                await option.Update(db);
            else
                await option.Add(db);

            return res;
        }

        public static async Task Remove(this DocOption option, AppDbContext db)
        {
            db.DocOptions.Remove(option);
            await db.SaveChangesAsync();
        }

        static async Task Add(this DocOption option, AppDbContext db)
        {
            await db.DocOptions.AddAsync(option);
            await db.SaveChangesAsync();
        }

        static async Task Update(this DocOption option, AppDbContext db)
        {
            db.DocOptions.Update(option);
            await db.SaveChangesAsync();
        }

        static async Task<SaveResult> Validate(this DocOption option, AppDbContext db)
        {
            if (string.IsNullOrEmpty(option.Value))
                return new SaveResult { IsValid = false, Message = "An Option must have a value." };

            if (option.DocItemId < 1)
                return new SaveResult { IsValid = false, Message = "An Option must be associated with a Select Item." };

            var check = await db.DocOptions
                .FirstOrDefaultAsync(x =>
                    x.Id != option.Id
                );
            return new SaveResult { IsValid = true };
        }

        static SaveResult Validate(this DocOption option)
        {
            if (string.IsNullOrEmpty(option.Value))
                return new SaveResult { IsValid = false, Message = "An Option must have a value." };

            return new SaveResult { IsValid = true };
        }

        static SaveResult Validate(this ICollection<DocOption> options)
        {
            foreach (var option in options)
            {
                var res = option.Validate();
                if (!res.IsValid) return res;

                if (options.Count(x => x.Value.ToLower() == option.Value.ToLower()) > 1)
                    return new SaveResult
                    {
                        IsValid = false,
                        Message = "Values in a Select Item Options List must be distinct."
                    };
            }

            return new SaveResult { IsValid = true };
        }

        #endregion

        #region Answer

        public static async Task<DocAnswer?> GetDocAnswer(this AppDbContext db, int questionId) =>
            await db.DocAnswers
                .FirstOrDefaultAsync(x => x.DocItemId == questionId);

        public static async Task<SaveResult> Save(this DocAnswer answer, AppDbContext db)
        {
            var res = answer.Validate();

            if (answer.Id > 0)
                await answer.Update(db);
            else
                await answer.Add(db);

            return res;
        }

        public static async Task Remove(this DocAnswer answer, AppDbContext db)
        {
            db.DocAnswers.Remove(answer);
            await db.SaveChangesAsync();
        }

        static async Task Add(this DocAnswer answer, AppDbContext db)
        {
            await db.DocAnswers.AddAsync(answer);
            await db.SaveChangesAsync();
        }

        static async Task Update(this DocAnswer answer, AppDbContext db)
        {
            db.DocAnswers.Update(answer);
            await db.SaveChangesAsync();
        }

        static SaveResult Validate(this DocAnswer answer)
        {
            if (string.IsNullOrEmpty(answer.Value))
                return new SaveResult { IsValid = false, Message = "An Answer must have a value." };

            if (answer.DocItemId < 1)
                return new SaveResult { IsValid = false, Message = "An Answer must be associated with a Question Item." };

            return new SaveResult { IsValid = true };
        }

        #endregion
    }
}