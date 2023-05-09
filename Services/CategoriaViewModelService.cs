using AutoMapper;
using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.Domain.Interfaces;
using despesas_backend_api_net_core.Interfaces;
using despesas_backend_api_net_core.ViewModels;

namespace despesas_backend_api_net_core.Services
{
    public class CategoriaViewModelService : ICategoriaViewModelService
    {
        private readonly ICategoriaRepository _CategoriaRepository;
        private readonly IMapper _mapper;

        public CategoriaViewModelService(ICategoriaRepository CategoriaRepository, IMapper mapper)
        {
            _CategoriaRepository = CategoriaRepository;
            _mapper = mapper;
        }

       public CategoriaViewModel Get(int id)
        {
            var entity = _CategoriaRepository.Get(id);
            if (entity == null)
                return null;

            CategoriaViewModel viewModel = new CategoriaViewModel { Id = entity.Id, Descricao = entity.Descricao, IdTipoCategoria  = entity.IdTipoCategoria };

            return viewModel;
        }

        public List<CategoriaViewModel> GetAll()
        {
            var list = _CategoriaRepository.GetAll();
            if (list == null)
                return new List<CategoriaViewModel>();

            List<CategoriaViewModel> listCategory = new List<CategoriaViewModel>();
            foreach ( Categoria categoria in list)
            {
                listCategory.Add(new CategoriaViewModel { Id = categoria.Id, Descricao = categoria.Descricao, IdTipoCategoria = categoria.IdTipoCategoria});
            }

            return listCategory;
        }
        
        public CategoriaViewModel Insert(CategoriaViewModel viewModel)
        {
            var entity = _mapper.Map<Categoria>(viewModel);
            entity = _CategoriaRepository.Insert(entity);   
            viewModel = _mapper.Map<CategoriaViewModel>(entity);
            return viewModel;
        }

        public CategoriaViewModel Update(CategoriaViewModel viewModel)
        {
            var entity = _mapper.Map<Categoria>(viewModel);
            entity = _CategoriaRepository.Update(entity);
            viewModel = _mapper.Map<CategoriaViewModel>(entity);
            return viewModel;
        }

        public void Delete(int id)
        {
            _CategoriaRepository.Delete(id);
        }

    }
}
