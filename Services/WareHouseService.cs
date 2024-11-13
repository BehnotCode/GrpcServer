using Grpc.Core;

namespace WareHouse.Server.Services
{
    public class WareHouseService : Warehouse.WarehouseBase
    {
        public static Dictionary<string, Product> _prod = new Dictionary<string, Product>();
        public override Task<Product> GetProductById(ProductId request, ServerCallContext context)
        {
            if (_prod.TryGetValue(request.Id, out Product prod) && prod != null)
            {
                return Task.FromResult(prod);

            }
            else
            {
                var error = new ErrorResponse
                {
                    Reason = "Product not found.",
                    Details = { $"No producti with id: {request.Id} exists." }
                };
                context.Status = new Status(StatusCode.NotFound, $"{error.Reason}. Detail: {error.Details}");
                return Task.FromResult(new Product());
            }
        }

        public override Task<Product> GetProductByName(ProductoName request, ServerCallContext context)
        {   

            Product prod = _prod.Where(z => z.Value.Name == request.Name).Select(x=> x.Value).FirstOrDefault();
            if (prod == null)
            {
                var error = new ErrorResponse
                {
                    Reason = "Product not found.",
                    Details = { $"No producti with id: {request.Name} exists." }
                };
                context.Status = new Status(StatusCode.NotFound, $"{error.Reason}. Detail: {error.Details}");
                return Task.FromResult(new Product());
            }
            return Task.FromResult(prod);
        }

        public override Task<ProductId> AddProduct(Product request, ServerCallContext context)
        {   
            _prod[request.Id] = request;
            return Task.FromResult(new ProductId { Id = request.Id});
        }

        public override Task<Product> UpdateProduct(Product request, ServerCallContext context)
        {
            if (!_prod.ContainsKey(request.Id))
            {
                var error = new ErrorResponse
                {
                    Reason = "Product not found.",
                    Details = { $"No producti with id: {request.Name} exists." }
                };
                context.Status = new Status(StatusCode.NotFound, $"{error.Reason}. Detail: {error.Details}");
                return Task.FromResult(new Product());
            }
            _prod[request.Id] = request;
            return Task.FromResult(request);
        }
    }
}
