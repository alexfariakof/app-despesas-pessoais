namespace Test.XUnit.Domain.VM
{
    public class CategoriaVMTest
    {

        [Theory]
        [InlineData("Test Description 1", 1, TipoCategoria.Todas)]
        [InlineData("Test Description 2", 2, TipoCategoria.Despesa)]
        [InlineData("Test Description 3", 3, TipoCategoria.Receita)]
        public void CategoriaVM_ShouldSetPropertiesCorrectly(string descricao, int idUsuario, TipoCategoria tipoCategoria)
        {
            // Arrange
            var categoriaVM = new CategoriaVM();

            // Act
            categoriaVM.Descricao = descricao;
            categoriaVM.IdUsuario = idUsuario;
            categoriaVM.IdTipoCategoria = (int)tipoCategoria;

            // Assert
            Assert.Equal(descricao, categoriaVM.Descricao);
            Assert.Equal(idUsuario, categoriaVM.IdUsuario);
            Assert.Equal((int)tipoCategoria, categoriaVM.IdTipoCategoria);
        }
    }
}