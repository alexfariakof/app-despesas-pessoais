global using Xunit;
global using Moq;
global using despesas_backend_api_net_core.Domain.Entities;
global using despesas_backend_api_net_core.Domain.VM;
global using despesas_backend_api_net_core.Infrastructure.Data.Common;
global using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic;
global using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Implementations;
global using Microsoft.EntityFrameworkCore;
using System.Linq;

public static class Usings
{
    public static List<CategoriaVM> lstCategoriasVM = new List<CategoriaVM>
    {
        new CategoriaVM { Id = 1, IdUsuario = 1, Descricao = "Alimentação", IdTipoCategoria = 1 },
        new CategoriaVM { Id = 2,  IdUsuario = 1, Descricao = "Transporte", IdTipoCategoria = 1 },
        new CategoriaVM { Id = 3, IdUsuario = 1, Descricao = "Salário", IdTipoCategoria = 2 },
        new CategoriaVM { Id = 4,  IdUsuario = 1, Descricao = "Lazer", IdTipoCategoria = 1 },
        new CategoriaVM { Id = 5, IdUsuario = 1, Descricao = "Moradia", IdTipoCategoria = 1 },
        new CategoriaVM { Id = 6, IdUsuario = 1, Descricao = "Investimentos", IdTipoCategoria = 2 },
        new CategoriaVM { Id = 7, IdUsuario = 1, Descricao = "Presentes", IdTipoCategoria = 1 },
        new CategoriaVM { Id = 8, IdUsuario = 1, Descricao = "Educação", IdTipoCategoria = 1 },
        new CategoriaVM { Id = 9, IdUsuario = 1, Descricao = "Prêmios", IdTipoCategoria = 2 },
        new CategoriaVM { Id = 10, IdUsuario = 1, Descricao = "Saúde", IdTipoCategoria = 1 }
    };


    public static List<Categoria> lstCategorias = new List<Categoria>
    {
        new Categoria { Id = 1, Descricao = "Alimentação", UsuarioId = 1, TipoCategoria = TipoCategoria.Despesa, Usuario = new Mock<Usuario>().Object  },
        new Categoria { Id = 2, Descricao = "Transporte", UsuarioId= 1, TipoCategoria = TipoCategoria.Despesa, Usuario = new Mock<Usuario>().Object },
        new Categoria { Id = 3, Descricao = "Salário", UsuarioId= 1, TipoCategoria = TipoCategoria.Receita, Usuario = new Mock<Usuario>().Object },
        new Categoria { Id = 4, Descricao = "Lazer", UsuarioId= 1, TipoCategoria = TipoCategoria.Despesa, Usuario = new Mock<Usuario>().Object },
        new Categoria { Id = 5, Descricao = "Moradia", UsuarioId= 1, TipoCategoria = TipoCategoria.Despesa , Usuario = new Mock < Usuario >().Object},
        new Categoria { Id = 6, Descricao = "Investimentos", UsuarioId= 1, TipoCategoria = TipoCategoria.Receita , Usuario = new Mock < Usuario >().Object},
        new Categoria { Id = 7, Descricao = "Presentes", UsuarioId= 1, TipoCategoria = TipoCategoria.Despesa , Usuario = new Mock < Usuario >().Object},
        new Categoria { Id = 8, Descricao = "Educação", UsuarioId= 1, TipoCategoria = TipoCategoria.Despesa , Usuario = new Mock < Usuario >().Object},
        new Categoria { Id = 9, Descricao = "Prêmios", UsuarioId= 1, TipoCategoria = TipoCategoria.Receita , Usuario = new Mock < Usuario >().Object},
        new Categoria { Id = 10, Descricao = "Saúde", UsuarioId= 1, TipoCategoria = TipoCategoria.Despesa , Usuario = new Mock < Usuario >().Object}
    };

    public static List<Despesa> lstDespesas = new List<Despesa>
    {
            new Despesa { Id = 1, Data = DateTime.Now.AddDays(new Random().Next(99)) , Descricao = "Teste Despesas 1", Valor = new Random().Next(1, 90001) + (decimal)new Random().NextDouble(), DataVencimento = DateTime.Now.AddDays(new Random().Next(99)),  UsuarioId = 1, Usuario = new Mock<Usuario>().Object, CategoriaId = 1, Categoria = Mock.Of<Categoria>() },
            new Despesa { Id = 2, Data = DateTime.Now.AddDays(new Random().Next(99)) , Descricao = "Teste Despesas 2", Valor = new Random().Next(1, 90001) + (decimal)new Random().NextDouble(), DataVencimento = DateTime.Now.AddDays(new Random().Next(99)),  UsuarioId = 1, Usuario = new Mock<Usuario>().Object, CategoriaId = 1, Categoria = Mock.Of<Categoria>() },
            new Despesa { Id = 3, Data = DateTime.Now.AddDays(new Random().Next(99)) , Descricao = "Teste Despesas 3", Valor = new Random().Next(1, 90001) + (decimal)new Random().NextDouble(), DataVencimento = DateTime.Now.AddDays(new Random().Next(99)),  UsuarioId = 1, Usuario = new Mock<Usuario>().Object, CategoriaId = 1, Categoria = Mock.Of<Categoria>() },
            new Despesa { Id = 4, Data = DateTime.Now.AddDays(new Random().Next(99)) , Descricao = "Teste Despesas 4", Valor = new Random().Next(1, 90001) + (decimal)new Random().NextDouble(), DataVencimento = DateTime.Now.AddDays(new Random().Next(99)),  UsuarioId = 1, Usuario = new Mock<Usuario>().Object, CategoriaId = 1, Categoria = Mock.Of<Categoria>() },
            new Despesa { Id = 5, Data = DateTime.Now.AddDays(new Random().Next(99)) , Descricao = "Teste Despesas 5", Valor = new Random().Next(1, 90001) + (decimal)new Random().NextDouble(), DataVencimento = DateTime.Now.AddDays(new Random().Next(99)),  UsuarioId = 1, Usuario = new Mock<Usuario>().Object, CategoriaId = 1, Categoria = Mock.Of<Categoria>() },
            new Despesa { Id = 6, Data = DateTime.Now.AddDays(new Random().Next(99)) , Descricao = "Teste Despesas 6", Valor = new Random().Next(1, 90001) + (decimal)new Random().NextDouble(), DataVencimento = DateTime.Now.AddDays(new Random().Next(99)),  UsuarioId = 1, Usuario = new Mock<Usuario>().Object, CategoriaId = 1, Categoria = Mock.Of<Categoria>() },
            new Despesa { Id = 7, Data = DateTime.Now.AddDays(new Random().Next(99)) , Descricao = "Teste Despesas 7", Valor = new Random().Next(1, 90001) + (decimal)new Random().NextDouble(), DataVencimento = DateTime.Now.AddDays(new Random().Next(99)),  UsuarioId = 1, Usuario = new Mock<Usuario>().Object, CategoriaId = 1, Categoria = Mock.Of<Categoria>() },
            new Despesa { Id = 8, Data = DateTime.Now.AddDays(new Random().Next(99)) , Descricao = "Teste Despesas 8", Valor = new Random().Next(1, 90001) + (decimal)new Random().NextDouble(), DataVencimento = DateTime.Now.AddDays(new Random().Next(99)),  UsuarioId = 1, Usuario = new Mock<Usuario>().Object, CategoriaId = 1, Categoria = Mock.Of<Categoria>() },
            new Despesa { Id = 9, Data = DateTime.Now.AddDays(new Random().Next(99)) , Descricao = "Teste Despesas 9", Valor = new Random().Next(1, 90001) + (decimal)new Random().NextDouble(), DataVencimento = DateTime.Now.AddDays(new Random().Next(99)),  UsuarioId = 1, Usuario = new Mock<Usuario>().Object, CategoriaId = 1, Categoria = Mock.Of<Categoria>() },
            new Despesa { Id = 10, Data = DateTime.Now.AddDays(new Random().Next(99)) , Descricao = "Teste Despesas 10", Valor = new Random().Next(1, 90001) + (decimal)new Random().NextDouble(), DataVencimento = DateTime.Now.AddDays(new Random().Next(99)),  UsuarioId = 1, Usuario = new Mock<Usuario>().Object, CategoriaId = 1, Categoria = Mock.Of<Categoria>() },
    };


    public static List<ControleAcesso> lstControleAcessos = new List<ControleAcesso>
    {
        new ControleAcesso { Login = "teste1@teste.com", Senha = "teste1", Usuario = Mock.Of<Usuario>()  },
        new ControleAcesso { Login = "teste2@teste.com", Senha = "teste1", Usuario = Mock.Of<Usuario>()  },
        new ControleAcesso { Login = "teste3@teste.com", Senha = "teste1", Usuario = Mock.Of<Usuario>()  },
        new ControleAcesso { Login = "teste4@teste.com", Senha = "teste1", Usuario = Mock.Of<Usuario>()  },
        new ControleAcesso { Login = "teste5@teste.com", Senha = "teste1", Usuario = Mock.Of<Usuario>()  },
        new ControleAcesso { Login = "teste6@teste.com", Senha = "teste1", Usuario = Mock.Of<Usuario>()  },
        new ControleAcesso { Login = "teste7@teste.com", Senha = "teste1", Usuario = Mock.Of<Usuario>()  },
        new ControleAcesso { Login = "teste8@teste.com", Senha = "teste1", Usuario = Mock.Of<Usuario>()  },
        new ControleAcesso { Login = "teste9@teste.com", Senha = "teste1", Usuario = Mock.Of<Usuario>()  },
        new ControleAcesso { Login = "teste10@teste.com", Senha = "teste1", Usuario = Mock.Of<Usuario>()  },

    };    
}