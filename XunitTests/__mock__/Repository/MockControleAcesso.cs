using Bogus;

namespace __mock__.Repository;
public sealed class MockControleAcesso
{
    private static MockControleAcesso? _instance;
    private static readonly object LockObject = new object();

    public static MockControleAcesso Instance
    {
        get
        {
            lock (LockObject)
            {
                return _instance ??= new MockControleAcesso();
            }
        }
    }

    public ControleAcesso GetControleAcesso(Usuario? usuario = null)
    {
        lock (LockObject)
        {
            if (usuario == null) usuario = MockUsuario.Instance.GetUsuario();

            var mockControleAcesso = new Faker<ControleAcesso>()
            .RuleFor(ca => ca.Login, usuario.Email)
            .RuleFor(ca => ca.Senha, "!12345")
            .RuleFor(ca => ca.UsuarioId, usuario.Id)
            .RuleFor(ca => ca.Usuario, usuario);

            return mockControleAcesso.Generate();
        }
    }

    public List<ControleAcesso> GetControleAcessos(int count = 3)
    {
        lock (LockObject)
        {
            var listControleAcesso = new List<ControleAcesso>();
            for (int i = 0; i < count; i++)
            {
                var usuario = MockUsuario.Instance.GetUsuario();
                var controleAcesso = GetControleAcesso(usuario);
                listControleAcesso.Add(controleAcesso);
            }
            return listControleAcesso;
        }
    }
}
