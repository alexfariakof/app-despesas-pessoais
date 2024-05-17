namespace Domain.Entities.ValueObjects;
public record PerfilUsuario
{
    public static implicit operator PerfilType(PerfilUsuario pu) => (PerfilType)pu.Id;
    public static implicit operator PerfilUsuario(PerfilType perfilType) => new PerfilUsuario(perfilType);
    public static implicit operator PerfilUsuario(int perfilType) => new PerfilUsuario(perfilType);
    public static bool operator ==(PerfilUsuario perfilUsuario, PerfilType perfilType) => perfilUsuario?.Id == (int)perfilType;
    public static bool operator !=(PerfilUsuario perfilUsuario, PerfilType perfilType) => !(perfilUsuario == perfilType);

    public enum PerfilType : int
    {
        Administrador = 1,
        Usuario = 2
    }
    public int Id { get; set; }
    public string Name { get; set; }
    
    public PerfilUsuario() { }

    public PerfilUsuario(int id)
    {
        Id = id;
        Name = GetPerfilUsuarioName((PerfilType)id);
    }

    private string GetPerfilUsuarioName(PerfilType perfilUsuario = PerfilType.Usuario)
    {
        if (PerfilType.Administrador == perfilUsuario)
            return "Administrador";
        else if (PerfilType.Usuario == perfilUsuario)
            return "Usuario";

        throw new ArgumentException("Perfil de usuário inexistente!");
    }
}
