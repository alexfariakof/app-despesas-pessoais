<<<<<<<< HEAD:XunitTests/Business/Dtos/Parsers/ChangePasswordDtoTest.cs
﻿namespace Business.Dtos;
========
﻿using Business.Dtos.v1;

namespace Business.Dtos;
>>>>>>>> feature/Create-Migrations-AZURE_SQL_SERVER:XunitTests/Business/Dtos/ChangePasswordDtoTest.cs
public class ChangePasswordDtoTest
{
    [Theory]
    [InlineData("userTeste1", "userTeste1")]
    [InlineData("userTeste2", "userTeste2")]
    [InlineData("userTeste3", "userTeste3")]
    public void ChangePasswordVM_Should_Set_Properties_Correctly(string senha, string confirmaSenha)
    {
        // Arrange and Act
        var changePasswordVM = new ChangePasswordDto
        {
            Senha = senha,
            ConfirmaSenha = confirmaSenha
        };

        // Assert
        Assert.Equal(senha, changePasswordVM.Senha);
        Assert.Equal(confirmaSenha, changePasswordVM.ConfirmaSenha);
    }
}