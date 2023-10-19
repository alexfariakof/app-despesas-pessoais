
using System.Reflection;

namespace despesas_backend_api_net_core.XUnit.Fakers
{
    public class ImagemPerfilUsuarioFaker
    {
        static int counter = 1;
        static int counterVM = 1;
        public static ImagemPerfilUsuario GetNewFaker(Usuario usuario)
        {
            var imagemFaker = new Faker<ImagemPerfilUsuario>()
                .RuleFor(i => i.Id, f => counter++)
                .RuleFor(i => i.Url, f => f.Internet.Url())
                .RuleFor(i => i.Name, f => f.System.FileName())
                .RuleFor(i => i.Type, f => f.System.CommonFileType())
                .RuleFor(i => i.UsuarioId, usuario.Id)
                .RuleFor(i => i.Usuario, usuario);

            return imagemFaker.Generate();
        }
        public static ImagemPerfilUsuarioVM GetNewFakerVM(UsuarioVM usuarioVM)
        {
            var imagemFaker = new Faker<ImagemPerfilUsuarioVM>()
                .RuleFor(i => i.Id, f => counterVM++)
                .RuleFor(i => i.Url, f => f.Internet.Url())
                .RuleFor(i => i.Name, f => f.System.FileName())
                .RuleFor(i => i.Type, f => f.System.CommonFileType())
                .RuleFor(i => i.IdUsuario, usuarioVM.Id)
                .RuleFor(i => i.ContentType, f => counter % 2 ==0 ? "image/png" : "image/jpg");


            return imagemFaker.Generate();
        }
        public static List<ImagemPerfilUsuario> ImagensPerfilUsuario(Usuario usuario = null, int? idUsuario = null)
        {
            var imagens = new List<ImagemPerfilUsuario>();
            for (var i = 0; i < 10; i++)
            {
                if (idUsuario == null)
                    usuario = UsuarioFaker.GetNewFaker(new Random(1).Next(1, 10));

                var imagem = GetNewFaker(usuario);
                imagens.Add(imagem);
            }
            return imagens;
        }
        public static List<ImagemPerfilUsuarioVM> ImagensPerfilUsuarioVM(UsuarioVM usuarioVM = null, int? idUsaurio = null)
        {
            var imagensVM = new List<ImagemPerfilUsuarioVM>();
            for (var i = 0; i < 10; i++)
            {
                if (idUsaurio == null)
                    usuarioVM = UsuarioFaker.GetNewFakerVM(new Random(1).Next(1, 10));

                var imagemVM = GetNewFakerVM(usuarioVM);
                imagensVM.Add(imagemVM);
            }
            return imagensVM;
        }
    }
}
