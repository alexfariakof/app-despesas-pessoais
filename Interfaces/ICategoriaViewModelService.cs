using despesas_backend_api_net_core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace despesas_backend_api_net_core.Interfaces
{
    public interface ICategoriaViewModelService
    {
        CategoriaViewModel Get(int id);
        List<CategoriaViewModel> GetAll();
        CategoriaViewModel Insert(CategoriaViewModel viewModel);
        CategoriaViewModel Update(CategoriaViewModel viewModel);
        void Delete(int id);
    }
}
