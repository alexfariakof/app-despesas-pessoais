namespace Domain.Entities.ValueObjects;
public sealed record PerfilUsuario
{
    public static implicit operator PerfilType(PerfilUsuario pu) => (PerfilType)pu.Id;
    public static implicit operator PerfilUsuario(int perfilType) => new PerfilUsuario((PerfilType)perfilType);
    public static bool operator ==(PerfilUsuario perfilUsuario, PerfilType perfilType) => perfilUsuario?.Id == (int)perfilType;
    public static bool operator !=(PerfilUsuario perfilUsuario, PerfilType perfilType) => !(perfilUsuario?.Id == (int)perfilType);

    public enum PerfilType : int
    {
        Administrador = 1,
        Usuario = 2
    }

    public int Id { get; set; }
    public string Name { get; set; }
    
    public PerfilUsuario() { }

    public PerfilUsuario(PerfilType itipoPerfil)
    {
        Id = (int)itipoPerfil;
        Name = GetPerfilUsuarioName(itipoPerfil);
    }

    private static string GetPerfilUsuarioName(PerfilType perfilUsuario = PerfilType.Usuario)
    {
        if (PerfilType.Administrador == perfilUsuario)
            return "Administrador";
        else if (PerfilType.Usuario == perfilUsuario)
            return "Usuario";

        throw new ArgumentException("Perfil de usuário inexistente!");
    }
}
