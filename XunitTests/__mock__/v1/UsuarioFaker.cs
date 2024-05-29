using Business.Dtos.v1;
using Domain.Entities.ValueObjects;

namespace Fakers.v1;
public sealed class UsuarioFaker
{
    static int counter = 1;
    static int counterVM = 1;

    private static UsuarioFaker? _instance;
    private static readonly object LockObject = new object();
    public static UsuarioFaker Instance
    {
        get
        {
            lock (LockObject)
            {
                return _instance ??= new UsuarioFaker();
            }
        }
    }

    private UsuarioFaker() { }

    public Usuario GetNewFaker(int? idUsuario = null)
    {
        lock (LockObject)
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
                .Generate();
            usuarioFaker.PerfilUsuario = counter % 2 == 0 ? new PerfilUsuario(PerfilUsuario.Perfil.Admin) : new PerfilUsuario(PerfilUsuario.Perfil.User);
            return usuarioFaker;
        }
    }

    public UsuarioDto GetNewFakerVM(int? idUsuario = null)
    {
        lock (LockObject)
        {
            if (idUsuario == null)
                idUsuario = counterVM++;

            var usuarioFaker = new Faker<UsuarioDto>()
                .RuleFor(u => u.Id, idUsuario)
                .RuleFor(u => u.Nome, f => f.Name.FullName())
                .RuleFor(u => u.SobreNome, f => f.Name.LastName())
                .RuleFor(u => u.Telefone, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.Email, f => f.Internet.Email());
            return usuarioFaker.Generate();
        }
    }

    public List<UsuarioDto> GetNewFakersUsuariosVMs(int count = 3)
    {
        lock (LockObject)
        {

            var listUsuarioDto = new List<UsuarioDto>();
            for (int i = 0; i < count; i++)
            {
                var usuarioDto = GetNewFakerVM();
                listUsuarioDto.Add(usuarioDto);
            }
            return listUsuarioDto;            
        }
    }

    public List<Usuario> GetNewFakersUsuarios(int count = 3)
    {
        lock (LockObject)
        {
            var listUsuario = new List<Usuario>();
            for (int i = 0; i < count; i++)
            {
                var usuario = UsuarioFaker.Instance.GetNewFaker();
                listUsuario.Add(usuario);
            }
            return listUsuario;
        }
    }
}
