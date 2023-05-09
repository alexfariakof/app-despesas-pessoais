using AutoMapper;
using despesas_backend_api_net_core.Domain.Entities;
using despesas_backend_api_net_core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace despesas_backend_api_net_core.Infrastructure.Web
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Categoria, CategoriaViewModel>();

        }
    }
}
