using Bogus;
using Domain.Entities.ValueObjects;

namespace __mock__.Repository;
public sealed class MockUsuario
{
    private static MockUsuario? _instance;
    private static readonly object LockObject = new object();
    public static MockUsuario Instance
    {
        get
        {
            lock (LockObject)
            {
                return _instance ??= new MockUsuario();
            }
        }
    }

    private MockUsuario() { }

    public Usuario GetUsuario()
    {
        lock (LockObject)
        {
            var mockUsuario = new Faker<Usuario>()
                .RuleFor(u => u.Nome, f => f.Name.FullName())
                .RuleFor(u => u.SobreNome, f => f.Name.LastName())
                .RuleFor(u => u.Telefone, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.StatusUsuario, f => f.PickRandom<StatusUsuario>())
                .Generate();
            mockUsuario.PerfilUsuario = new Random().Next(1, 2) % 2 == 0 ? new PerfilUsuario(PerfilUsuario.Perfil.Admin) : new PerfilUsuario(PerfilUsuario.Perfil.User);
            return mockUsuario;
        }
    }

    public List<Usuario> GetUsuarios(int count = 3)
    {
        lock (LockObject)
        {
            var listUsuario = new List<Usuario>();
            for (int i = 0; i < count; i++)
            {
                var usuario = MockUsuario.Instance.GetUsuario();
                listUsuario.Add(usuario);
            }
            return listUsuario;
        }
    }
}
