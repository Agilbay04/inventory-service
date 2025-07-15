using InventoryService.Domain.Product.Dtos;
using InventoryService.Infrastructure.Databases;
using InventoryService.Infrastructure.Dtos;
using InventoryService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace InventoryService.Domain.Product.Repositories
{
    public class ProductQueryRepository(DataContext dbContext)
    {
        private readonly DataContext _dbContext = dbContext;

        public async Task<PaginationResult<Models.Product>> PaginationAsync(ProductQueryDto queryParams)
        {
            int skip = (queryParams.Page - 1) * queryParams.PerPage;
            var query = _dbContext.Products
                .AsNoTracking()
                .Include(data => data.Category)
                .Where(data => data.IsPublish == true
                    && data.DeletedAt == null
                    && data.Category.DeletedAt == null)
                .AsQueryable();

            query = QuerySearch(query, queryParams);
            query = QueryFilter(query, queryParams);
            query = QuerySort(query, queryParams);

            var data = await query.Skip(skip).Take(queryParams.PerPage).ToListAsync();
            var count = await CountAsync(query);

            return new PaginationResult<Models.Product>
            {
                Data = data,
                Count = count
            };
        }

        public PaginationResult<Models.Product> Pagination(ProductQueryDto queryParams)
        {
            int skip = (queryParams.Page - 1) * queryParams.PerPage;
            var query = _dbContext.Products
                .AsNoTracking()
                .Include(data => data.Category)
                .Where(data => data.IsPublish == true
                    && data.DeletedAt == null
                    && data.Category.DeletedAt == null)
                .AsQueryable();

            query = QuerySearch(query, queryParams);
            query = QueryFilter(query, queryParams);
            query = QuerySort(query, queryParams);

            var data = query.Skip(skip).Take(queryParams.PerPage).ToList();
            var count = Count(query);

            return new PaginationResult<Models.Product>
            {
                Data = data,
                Count = count
            };
        }

        private static IQueryable<Models.Product> QuerySearch(IQueryable<Models.Product> query, ProductQueryDto queryParams)
        {
            if (queryParams.Search != null)
            {
                var searchParam = $"%{queryParams.Search}%";
                query = query.Where(data =>
                    EF.Functions.Like(data.Name, searchParam) ||
                    EF.Functions.Like(data.Category.Name, searchParam) ||
                    EF.Functions.Like(data.Code, searchParam));
            }

            return query;
        }

        private static IQueryable<Models.Product> QueryFilter(IQueryable<Models.Product> query, ProductQueryDto queryParams)
        {
            if (!queryParams.Name.IsNullOrEmpty())
            {
                query = query.Where(data => data.Name.Equals(queryParams.Name));
            }

            if (!queryParams.Code.IsNullOrEmpty())
            {
                query = query.Where(data => data.Code.Equals(queryParams.Code));
            }

            return query;
        }

        private static IQueryable<Models.Product> QuerySort(IQueryable<Models.Product> query, ProductQueryDto queryParams)
        {
            queryParams.SortBy ??= "updated_at";

            Dictionary<string, Expression<Func<Models.Product, object>>> sortFunctions = new()
            {
                { "name", data => data.Name },
                { "created_at", data => data.CreatedAt },
                { "updated_at", data => data.UpdatedAt },
            };

            if (!sortFunctions.TryGetValue(queryParams.SortBy, out Expression<Func<Models.Product, object>> value))
            {
                throw new BadHttpRequestException($"Invalid sort column: {queryParams.SortBy}, available sort columns: " + string.Join(", ", sortFunctions.Keys));
            }

            query = queryParams.Order == SortOrder.Asc
                ? query.OrderBy(value).AsQueryable()
                : query.OrderByDescending(value).AsQueryable();

            return query;
        }

        public async Task<int> CountAsync(IQueryable<Models.Product> query)
        {
            return await query.Select(x => x.Id).CountAsync();
        }
        
        public int Count(IQueryable<Models.Product> query)
        {
            return query.Select(x => x.Id).Count();
        }

        public async Task<Models.Product> FindOneById(Guid id)
        {
            return await _dbContext.Products
                .AsNoTracking()
                .Include(data => data.Category)
                .Where(data => data.Id == id
                    && data.IsPublish == true
                    && data.DeletedAt == null
                    && data.Category.DeletedAt == null)
                .FirstOrDefaultAsync();
        }

        public List<Models.Product> FindByIds(List<Guid> ids)
        {
            return [.. _dbContext.Products
                .AsNoTracking()
                .Include(data => data.Category)
                .Where(data => ids.Contains(data.Id)
                    && data.IsPublish == true
                    && data.DeletedAt == null
                    && data.Category.DeletedAt == null)];
        }
    }
}