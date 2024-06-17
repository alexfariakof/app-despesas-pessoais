using Bogus;

namespace __mock__.Repository;
public sealed class MockImagemPerfilUsuario
{
    private static MockImagemPerfilUsuario? _instance;
    private static readonly object LockObject = new object();

    public static MockImagemPerfilUsuario Instance
    {
        get
        {
            lock (LockObject)
            {
                return _instance ??= new MockImagemPerfilUsuario();
            }
        }
    }

    public ImagemPerfilUsuario GetImagemPerfilUsuario()
    {
        lock (LockObject)
        {
            var mockImagem = new Faker<ImagemPerfilUsuario>()
            .RuleFor(i => i.Url, f => f.Internet.Url())
            .RuleFor(i => i.Name, f => f.System.FileName())
            .RuleFor(i => i.ContentType, f => f.System.CommonFileType())
            .Generate();
            return mockImagem;
        }
    }

    public List<ImagemPerfilUsuario> ImagensPerfilUsuarios(int count = 3)
    {
        var imagens = new List<ImagemPerfilUsuario>();
        for (var i = 0; i < count; i++)
        {
            var imagem = GetImagemPerfilUsuario();
            imagens.Add(imagem);
        }
        return imagens;
    } 
}
