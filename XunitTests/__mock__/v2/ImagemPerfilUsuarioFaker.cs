using Bogus;
using Business.Dtos.v2;

namespace __mock__.v2;
public sealed class ImagemPerfilUsuarioFaker
{
    static int counter = 1;
    static int counterVM = 1;
    private static ImagemPerfilUsuarioFaker? _instance;
    private static readonly object LockObject = new object();

    public static ImagemPerfilUsuarioFaker Instance
    {
        get
        {
            lock (LockObject)
            {
                return _instance ??= new ImagemPerfilUsuarioFaker();
            }
        }
    }
    public ImagemPerfilUsuario GetNewFaker(Usuario usuario)
    {
        lock (LockObject)
        {
            var imagemFaker = new Faker<ImagemPerfilUsuario>()
                .RuleFor(i => i.Id, f => Guid.NewGuid())
                .RuleFor(i => i.Url, f => f.Internet.Url())
                .RuleFor(i => i.Name, f => f.System.FileName())
                .RuleFor(i => i.ContentType, f => f.System.CommonFileType())
                .RuleFor(i => i.UsuarioId, usuario.Id)
                .RuleFor(i => i.Usuario, usuario)
                .Generate();
            counter++;
            return imagemFaker;
        }
    }

    public ImagemPerfilDto GetNewFakerDto(UsuarioDto usuarioDto)
    {
        lock (LockObject)
        {
            var imagemFaker = new Faker<ImagemPerfilDto>()
                .RuleFor(i => i.Id, f => Guid.NewGuid())
                .RuleFor(i => i.Url, f => f.Internet.Url())
                .RuleFor(i => i.Name, f => f.System.FileName())
                .RuleFor(i => i.UsuarioId, usuarioDto.Id)
                .RuleFor(i => i.ContentType, f => counter % 2 == 0 ? "image/png" : "image/jpg")
                .Generate();
            counterVM++;
            return imagemFaker;
        }
    }

    public ImagemPerfilDto GetNewDtoFrom(ImagemPerfilUsuario imagemPerfil)
    {
        var imagemFaker = new ImagemPerfilDto()
        {
            Id = imagemPerfil.Id,
            Url = imagemPerfil.Url,
            Name = imagemPerfil.Name,
            UsuarioId = imagemPerfil.UsuarioId,
            ContentType = imagemPerfil.ContentType
        };
        return imagemFaker;        
    }


    public List<ImagemPerfilUsuario> ImagensPerfilUsuarios(Usuario? usuario = null, Guid? idUsuario = null)
    {
        var imagens = new List<ImagemPerfilUsuario>();
        for (var i = 0; i < 10; i++)
        {
            if (idUsuario == null)
                usuario = UsuarioFaker.Instance.GetNewFaker();

            usuario = usuario ?? new();
            var imagem = GetNewFaker(usuario);
            imagens.Add(imagem);
        }
        return imagens;
    }

    public List<ImagemPerfilDto> ImagensPerfilUsuarioDtos(UsuarioDto? usuarioDto = null, Guid? idUsuario = null)
    {
        var imagensVM = new List<ImagemPerfilDto>();
        for (var i = 0; i < 10; i++)
        {
            if (idUsuario == null)
                usuarioDto = UsuarioFaker.Instance.GetNewFakerVM();

            usuarioDto = usuarioDto ?? new();
            var imagemVM = ImagemPerfilUsuarioFaker.Instance.GetNewFakerDto(usuarioDto);

            imagensVM.Add(imagemVM);
        }
        return imagensVM;
    }
}
