namespace despesas_backend_api_net_core.XUnit.Fakers
{
    public class UsuarioFaker
    {
        static int counter = 1;
        static int counterVM = 1;

        private UsuarioFaker UsuarioFakerDataset { get; set; }

        public Usuario Usuario { get; set; }

        public UsuarioVM UsuarioVM { get; set; }

        public static Usuario GetNewFaker(int? idUsuario = null)
        {
            if (idUsuario == null)
                idUsuario = counter++;

            var usuarioFaker = new Faker<Usuario>()
                .RuleFor(u => u.Id, idUsuario)
                .RuleFor(u => u.Nome, f => f.Name.FullName())
                .RuleFor(u => u.SobreNome, f => f.Name.LastName())
                .RuleFor(u => u.Telefone, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.StatusUsuario, f => f.PickRandom<StatusUsuario>())
                .RuleFor(u => u.PerfilUsuario, f => f.PickRandom<PerfilUsuario>());

            return usuarioFaker.Generate();
        }

        public static UsuarioVM GetNewFakerVM(int? idUsuario = null)
        {
            if (idUsuario == null)
                idUsuario = counterVM++;

            var usuarioFaker = new Faker<UsuarioVM>()
                .RuleFor(u => u.Id, idUsuario)
                .RuleFor(u => u.Nome, f => f.Name.FullName())
                .RuleFor(u => u.SobreNome, f => f.Name.LastName())
                .RuleFor(u => u.Telefone, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.Email, f => f.Internet.Email());

            return usuarioFaker.Generate();
        }

        public static List<UsuarioVM> GetNewFakersUsuariosVMs()
        {
            var listUsuarioVM = new List<UsuarioVM>();
            for (int i = 0; i < 10; i++)
            {
                var usuarioVM = GetNewFakerVM();
                listUsuarioVM.Add(usuarioVM);
            }
            return listUsuarioVM;
        }

        public static List<Usuario> GetNewFakersUsuarios()
        {
            var listUsuario = new List<Usuario>();

            counter += 10;
            for (int i = 0; i < 10; i++)
            {
                var usuario = UsuarioFaker.GetNewFaker();
                listUsuario.Add(usuario);
            }
            return listUsuario;
        }
    }
}
