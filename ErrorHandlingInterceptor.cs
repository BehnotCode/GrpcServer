using Grpc.Core;
using Grpc.Core.Interceptors;

namespace WareHouse.Server
{
    public class ErrorHandlingInterceptor : Interceptor
    {

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                return await continuation(request, context);
            }
            catch (RpcException ex)
            {

                throw;
            }
            catch (Exception ex)
            {
                throw new RpcException (new Status(StatusCode.Internal, ex.Message));
            }
        }
    }
}
