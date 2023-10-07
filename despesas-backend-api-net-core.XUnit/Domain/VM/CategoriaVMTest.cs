namespace Test.XUnit.Domain.VM
{
    public class CategoriaVMTest
    {

        [Theory]
        [InlineData(1, "Test Categoria Description 1", 1, TipoCategoria.Todas)]
        [InlineData(2, "Test Categoria Description 2", 2, TipoCategoria.Despesa)]
        [InlineData(3, "Test Categoria Description 3", 3, TipoCategoria.Receita)]
        public void CategoriaVM_ShouldSetPropertiesCorrectly(int id, string descricao, int idUsuario, TipoCategoria tipoCategoria)
        {
            // Arrange
            var categoriaVM = new CategoriaVM();

            // Act
            categoriaVM.Id = id;
            categoriaVM.Descricao = descricao;
            categoriaVM.IdUsuario = idUsuario;
            categoriaVM.IdTipoCategoria = (int)tipoCategoria;

            // Assert
            Assert.Equal(id, categoriaVM.Id);
            Assert.Equal(descricao, categoriaVM.Descricao);
            Assert.Equal(idUsuario, categoriaVM.IdUsuario);
            Assert.Equal((int)tipoCategoria, categoriaVM.IdTipoCategoria);
        }
    }
}