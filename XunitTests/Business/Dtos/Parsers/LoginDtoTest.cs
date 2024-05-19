<<<<<<<< HEAD:XunitTests/Business/Dtos/Parsers/LoginDtoTest.cs
﻿namespace Business.Dtos;
========
﻿using Business.Dtos.v1;

namespace Business.Dtos;
>>>>>>>> feature/Create-Migrations-AZURE_SQL_SERVER:XunitTests/Business/Dtos/LoginDtoTest.cs
public class LoginDtoTest
{
    [Theory]
    [InlineData("user1@user1.com", "userTeste1")]
    [InlineData("user2@user2.com", "userTeste2")]
    [InlineData("user3@user3.com", "userTeste3")]
    public void LoginVM_Should_Set_Properties_Correctly(string email, string senha)
    {
        // Arrange and Act
        var loginVM = new LoginDto
        {
            Email = email,
            Senha = senha
        };

        // Assert
        Assert.Equal(email, loginVM.Email);
        Assert.Equal(senha, loginVM.Senha);
    }
}