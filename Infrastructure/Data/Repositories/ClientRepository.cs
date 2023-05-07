using Cadastro.Domain.Entities;
using Cadastro.Domain.Interfaces;
using Cadastro.Infrastructure.Data.Common;

namespace Cadastro.Infrastructure.Data.Repositories
{
    public class ClientRepository : EfRepository<Client>, IClientRepository
    {
        public ClientRepository(RegisterContext dbContext)
            : base(dbContext)
        {

        }
    }
}
