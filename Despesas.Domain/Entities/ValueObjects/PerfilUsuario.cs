namespace Domain.Entities.ValueObjects;
public sealed record PerfilUsuario
{
    public static implicit operator Perfil(PerfilUsuario pu) => (Perfil)pu.Id;
    public static implicit operator PerfilUsuario(int perfilType) => new PerfilUsuario((Perfil)perfilType);
    public static bool operator ==(PerfilUsuario perfilUsuario, Perfil perfilType) => perfilUsuario?.Id == (int)perfilType;
    public static bool operator !=(PerfilUsuario perfilUsuario, Perfil perfilType) => !(perfilUsuario?.Id == (int)perfilType);

    public enum Perfil : int
    {
        Admin = 1,
        User = 2
    }

    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;

    public PerfilUsuario() { }

    public PerfilUsuario(Perfil itipoPerfil)
    {
        Id = (int)itipoPerfil;
        Name = GetPerfilUsuarioName(itipoPerfil);
    }

    private static string GetPerfilUsuarioName(Perfil perfilUsuario = Perfil.User)
    {
        if (Perfil.Admin == perfilUsuario)
            return "Administrador";
        else if (Perfil.User == perfilUsuario)
            return "Usuario";

        throw new ArgumentException("Perfil de usuário inexistente!");
    }
}
