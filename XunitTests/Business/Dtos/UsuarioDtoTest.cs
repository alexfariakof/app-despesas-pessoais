﻿using Business.Dtos.Core;
using Business.Dtos.v1;
using Domain.Entities.ValueObjects;

namespace Business.Dtos;
public sealed class UsuarioDtoTest
{
    public class UsuarioDtoBaseTest : UsuarioDtoBase { }

    [Theory]
    [InlineData(1, "Usuario 1", "Teste Usuario 1" ,"(21) 99999-9999", "user1@user.com")]
    [InlineData(2, "Usuario 2", "Teste Usuario 2", "(21) 99999-9999", "user2@user.com")]
    [InlineData(3, "Usuario 3", "Teste Usuario 3", "(21) 99999-9999", "user3@user.com")]
    public void UsuarioDto_Should_Set_Properties_Correctly(int id, string nome, string sobreNome, string telefone, string email)
    {
        // Arrange and Act

        var usuarioDto = new UsuarioDto
        {
            Id = id,
            Nome = nome,
            SobreNome = sobreNome,
            Telefone = telefone,
            Email = email,
            PerfilUsuario = new PerfilUsuario()
        };

        // Assert
        Assert.Equal(id, usuarioDto.Id);
        Assert.Equal(nome, usuarioDto.Nome);
        Assert.Equal(sobreNome, usuarioDto.SobreNome);
        Assert.Equal(telefone, usuarioDto.Telefone);
        Assert.Equal(email, usuarioDto.Email);
        Assert.NotNull(usuarioDto.PerfilUsuario);
    }

    [Theory]
    [InlineData(1, "Usuario 1", "Teste Usuario 1", "(21) 99999-9999", "user1@user.com")]
    [InlineData(2, "Usuario 2", "Teste Usuario 2", "(21) 99999-9999", "user2@user.com")]
    [InlineData(3, "Usuario 3", "Teste Usuario 3", "(21) 99999-9999", "user3@user.com")]
    public void UsuarioDtoBase_Should_Set_Properties_Correctly(int id, string nome, string sobreNome, string telefone, string email)
    {
        // Arrange and Act

        var usuarioDto = new UsuarioDtoBaseTest
        {
            Id = id,
            Nome = nome,
            SobreNome = sobreNome,
            Telefone = telefone,
            Email = email,
            PerfilUsuario = new PerfilUsuario()
        };

        // Assert
        Assert.Equal(id, usuarioDto.Id);
        Assert.Equal(nome, usuarioDto.Nome);
        Assert.Equal(sobreNome, usuarioDto.SobreNome);
        Assert.Equal(telefone, usuarioDto.Telefone);
        Assert.Equal(email, usuarioDto.Email);
        Assert.NotNull(usuarioDto.PerfilUsuario);
    }
}