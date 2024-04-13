namespace XUnit.Fakers;
public class ControleAcessoFaker
{
    static int counter = 1;
    private static ControleAcessoFaker? _instance;
    private static readonly object LockObject = new object();

    public static ControleAcessoFaker Instance
    {
        get
        {
            lock (LockObject)
            {
                return _instance ??= new ControleAcessoFaker();
            }
        }
    }

    public ControleAcesso GetNewFaker(Usuario? usuario = null)
    {
        lock (LockObject)
        {
            if (usuario == null) usuario = UsuarioFaker.Instance.GetNewFaker(); 

            var controleAcessoFaker = new Faker<ControleAcesso>()
            .RuleFor(ca => ca.Id, counter++)
            .RuleFor(ca => ca.Login, usuario.Email)
            .RuleFor(ca => ca.Senha, "!12345")
            .RuleFor(ca => ca.UsuarioId, usuario.Id)
            .RuleFor(ca => ca.Usuario, usuario);

            return controleAcessoFaker.Generate();
        }
    }

    public ControleAcessoDto GetNewFakerVM(Usuario? usuario = null)
    {
        lock (LockObject)
        {
            if (usuario == null) usuario = UsuarioFaker.Instance.GetNewFaker();

            var controleAcessoVMFaker = new Faker<ControleAcessoDto>()
            .RuleFor(ca => ca.Nome, usuario.Nome)
            .RuleFor(ca => ca.SobreNome, usuario.SobreNome)
            .RuleFor(ca => ca.Email, usuario.Email)
            .RuleFor(ca => ca.Telefone, usuario.Telefone)
            .RuleFor(ca => ca.Senha, "!12345")
            .RuleFor(ca => ca.ConfirmaSenha, "!12345");


            return controleAcessoVMFaker.Generate();
        }
    }

    public List<ControleAcessoDto> ControleAcessoVMs(int count = 3)
    {           
        var listControleAcessoVM = new List<ControleAcessoDto>();                        
        for (int i = 0; i < count; i++)
        {
            var usuario = UsuarioFaker.Instance.GetNewFaker();
            var controleAcessoVM = GetNewFakerVM(usuario);
            listControleAcessoVM.Add(controleAcessoVM);                
        }

        return listControleAcessoVM;
    }

    public List<ControleAcesso> ControleAcessos(int count = 3)
    {
        lock (LockObject)
        {

            var listControleAcesso = new List<ControleAcesso>();
            for (int i = 0; i < count; i++)
            {
                var usuario = UsuarioFaker.Instance.GetNewFaker();
                var controleAcesso = GetNewFaker(usuario);
                listControleAcesso.Add(controleAcesso);
            }
            return listControleAcesso;
        }
    }
}
