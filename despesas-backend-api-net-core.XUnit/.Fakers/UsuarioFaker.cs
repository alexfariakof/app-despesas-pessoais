using despesas_backend_api_net_core.Infrastructure.Data.EntityConfig;

namespace despesas_backend_api_net_core.XUnit.Fakers
{
    public class UsuarioFaker    
    {
        static int counter = 1;
        static int counterVM = 1;
        private UsuarioFaker UsuarioFakerDataset { get; set; }
        public Usuario Usuario { get; set; }
        public UsuarioVM UsuarioVM { get; set; }
        public ControleAcesso ControleAcesso { get; set; }
        public ControleAcessoVM ControleAcessoVM { get; set; }
        public List<Categoria> ListCategorias { get; set; }
        public  List<CategoriaVM> ListCategoriaVMs { get; set; }
        public List<Despesa> ListDespesas { get; set; }
        public List<DespesaVM> ListDespesasVMs { get; set; }
        public List<Receita> ListReceitas{ get; set; }
        public List<ReceitaVM> ListReceitaVMs { get; set; }
        public List<Lancamento> ListLancamentos { get; set; }
        public List<LancamentoVM> ListLancamentoVMs { get; set; }

        public UsuarioFaker()
        {
            var fakerUsuario = new UsuarioFaker();
            
            fakerUsuario.Usuario = new Faker<Usuario>()
                .RuleFor(u => u.Id, f => new Random().Next(1, 10))
                .RuleFor(u => u.Nome, f => f.Name.FullName())
                .RuleFor(u => u.SobreNome, f => f.Name.LastName())
                .RuleFor(u => u.Telefone, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.StatusUsuario, f => f.PickRandom<StatusUsuario>())
                .RuleFor(u => u.PerfilUsuario, f => f.PickRandom<PerfilUsuario>());

            fakerUsuario.UsuarioVM = new UsuarioMap().Parse(fakerUsuario.Usuario);
            fakerUsuario.ControleAcesso = ControleAcessoFaker.GetNewFaker(fakerUsuario.Usuario);
            fakerUsuario.ControleAcessoVM = ControleAcessoFaker.GetNewFakerVM(fakerUsuario.UsuarioVM);
            fakerUsuario.ListCategorias = CategoriaFaker.Categorias(fakerUsuario.Usuario, fakerUsuario.Usuario.Id);
            fakerUsuario.ListCategoriaVMs = CategoriaFaker.CategoriasVMs(fakerUsuario.UsuarioVM, fakerUsuario.Usuario.Id);
            fakerUsuario.ListDespesas = DespesaFaker.Despesas(fakerUsuario.Usuario, fakerUsuario.Usuario.Id);
            fakerUsuario.ListDespesasVMs = DespesaFaker.DespesasVMs(fakerUsuario.UsuarioVM, fakerUsuario.Usuario.Id);
            fakerUsuario.ListReceitas = ReceitaFaker.Receitas(fakerUsuario.Usuario, fakerUsuario.Usuario.Id);
            fakerUsuario.ListReceitaVMs = ReceitaFaker.ReceitasVMs(fakerUsuario.UsuarioVM, fakerUsuario.Usuario.Id);
            fakerUsuario.ListLancamentos = LancamentoFaker.Lancamentos(fakerUsuario.Usuario, fakerUsuario.Usuario.Id);
            fakerUsuario.ListLancamentoVMs = LancamentoFaker.LancamentoVMs(fakerUsuario.Usuario, fakerUsuario.Usuario.Id);

            fakerUsuario.UsuarioFakerDataset = fakerUsuario;
        }

        public UsuarioFaker GetUsuarioFakerDataSet()
        {
            return this.UsuarioFakerDataset;
        }

        public static Usuario GetNewFaker(int? idUsuario = null)
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

        public static UsuarioVM GetNewFakerVM(int? idUsuario = null)
        {
            var usuarioFaker = new Faker<UsuarioVM>()
                .RuleFor(u => u.Id, f => counterVM++)
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
