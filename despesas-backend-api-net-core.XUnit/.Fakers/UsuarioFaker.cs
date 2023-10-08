using Bogus;

namespace despesas_backend_api_net_core.XUnit.Fakers
{
    public static class UsuarioFaker    
    {
        public static Usuario GetNewFaker()
        {
            var usuarioFaker = new Faker<Usuario>()
                .RuleFor(u => u.Id, f => f.Random.Number(1, 100))
                .RuleFor(u => u.Nome, f => f.Name.FullName())
                .RuleFor(u => u.SobreNome, f => f.Name.LastName())
                .RuleFor(u => u.Telefone, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.StatusUsuario, f => f.PickRandom<StatusUsuario>())
                .RuleFor(u => u.PerfilUsuario, f => f.PickRandom<PerfilUsuario>());
            return usuarioFaker.Generate();
        }
    }
}
