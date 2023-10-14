namespace despesas_backend_api_net_core.XUnit.Fakers
{
    public class ControleAcessoFaker
    {
        static int counter = 1;
        public static ControleAcesso GetNewFaker(Usuario usuario)
        {

            var controleAcessoFaker = new Faker<ControleAcesso>()
                .RuleFor(ca => ca.Id, counter++)
                .RuleFor(ca => ca.Login, usuario.Email)
                .RuleFor(ca => ca.Senha, "!12345")
                .RuleFor(ca => ca.UsuarioId, usuario.Id)
                .RuleFor(ca => ca.Usuario, usuario);

            return controleAcessoFaker.Generate();
        }

        public static ControleAcessoVM GetNewFakerVM(UsuarioVM usuarioVM)
        {
            var controleAcessoVMFaker = new Faker<ControleAcessoVM>()
                .RuleFor(ca => ca.Nome, usuarioVM.Nome)
                .RuleFor(ca => ca.SobreNome, usuarioVM.SobreNome)
                .RuleFor(ca => ca.Email, usuarioVM.Email)
                .RuleFor(ca => ca.Telefone, usuarioVM.Telefone)
                .RuleFor(ca => ca.Senha, "!12345")
                .RuleFor(ca => ca.ConfirmaSenha, "!12345");               
                

            return controleAcessoVMFaker.Generate();
        }

        public static List<ControleAcessoVM> ControleAcessoVMs()
        {   
            
            var listControleAcessoVM = new List<ControleAcessoVM>();                        
            for (int i = 0; i < 10; i++)
            {
                var usuarioVM = UsuarioFaker.GetNewFakerVM(i);
                var controleAcessoVM = GetNewFakerVM(usuarioVM);
                listControleAcessoVM.Add(controleAcessoVM);                
            }

            return listControleAcessoVM;
        }
        public static List<ControleAcesso> ControleAcessos()
        {

            var listControleAcesso = new List<ControleAcesso>();
            for (int i = 0; i < 10; i++)
            {
                var usuario = UsuarioFaker.GetNewFaker(i);
                var controleAcesso = GetNewFaker(usuario);
                listControleAcesso.Add(controleAcesso);
            }
            return listControleAcesso;
        }
    }
}
