

namespace Catalog.API.Products.GetProducts
{
    public record GetProductByCategoryQuery(string Category) :IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product> Products);
    internal class GetProductByCategoryHandler(IDocumentSession session,ILogger<GetProductsHandler> logger) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductsHandler called with {@Query}", query);
            var products = await session.Query<Product>().Where(x=>x.Category.Contains(query.Category)).ToListAsync(cancellationToken)
                ;
            return new GetProductByCategoryResult(products);
        }
    }
}
