using Bogus;

namespace despesas_backend_api_net_core.XUnit.Fakers
{
    public static class UsuarioFaker    
    {
        static int counter = 0;
        public static Usuario GetNewFaker()
        {
            var usuarioFaker = new Faker<Usuario>()
                .RuleFor(u => u.Id, f => counter++)
                .RuleFor(u => u.Nome, f => f.Name.FullName())
                .RuleFor(u => u.SobreNome, f => f.Name.LastName())
                .RuleFor(u => u.Telefone, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.StatusUsuario, f => f.PickRandom<StatusUsuario>())
                .RuleFor(u => u.PerfilUsuario, f => f.PickRandom<PerfilUsuario>());
            return usuarioFaker.Generate();
        }

        public static UsuarioVM GetNewFakerVM()
        {
            var usuarioFaker = new Faker<UsuarioVM>()
                .RuleFor(u => u.Id, f =>counter++)
                .RuleFor(u => u.Nome, f => f.Name.FullName())
                .RuleFor(u => u.SobreNome, f => f.Name.LastName())
                .RuleFor(u => u.Telefone, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.Email, f => f.Internet.Email());

            return usuarioFaker.Generate();
        }

        public static List<UsuarioVM> UsuariosVMs()
        {
            var listUsuarioVM = new List<UsuarioVM>();
            var usuario = UsuarioFaker.GetNewFaker();

            for (int i = 0; i < 10; i++)
            {
                var categoriaVM = CategoriaFaker.GetNewFakerVM(usuario.Id);
                var usuarioVM = GetNewFakerVM();
                listUsuarioVM.Add(usuarioVM);
                usuario = UsuarioFaker.GetNewFaker();
            }
            return listUsuarioVM;
        }
        public static List<Usuario> Usuarios()
        {
            var listUsuario = new List<Usuario>();
            var usuario = UsuarioFaker.GetNewFaker();

            for (int i = 0; i < 10; i++)
            {                
                listUsuario.Add(usuario);
                usuario = UsuarioFaker.GetNewFaker();
            }
            return listUsuario;
        }
    }
}
