using NetReactMovie.Server.Models.Entities;

namespace NetReactMovie.Server.Services.Interfaces
{
    public interface IJwtService
    {
     
     string GenerateToken(User user);
        
    }
}
