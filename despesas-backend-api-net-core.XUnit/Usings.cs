global using Xunit;
global using Moq;
global using despesas_backend_api_net_core.Domain.Entities;
global using despesas_backend_api_net_core.Domain.VM;
global using despesas_backend_api_net_core.Infrastructure.Data.Common;
global using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Generic;
global using despesas_backend_api_net_core.Infrastructure.Data.Repositories.Implementations;
global using Microsoft.EntityFrameworkCore;
using System.Linq;
using despesas_backend_api_net_core.XUnit.Fakers;

public static class Usings
{
    public static List<CategoriaVM> lstCategoriasVM = CategoriaFaker.CategoriasVM();
    
    public static List<Categoria> lstCategorias = CategoriaFaker.Categorias();

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