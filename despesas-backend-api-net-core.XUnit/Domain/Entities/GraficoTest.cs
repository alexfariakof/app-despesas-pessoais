using System.Collections.Generic;

namespace Test.XUnit.Domain.Entities
{
    public class GraficoTest
    {
        [Fact]
        public void Grafico_Should_Set_Properties_Correctly()
        {
            var somatorioDespesasPorAno = new Mock<Dictionary<String, decimal>>().Object;

            var somatorioReceitasPorAno =  new Mock<Dictionary<String, decimal>>().Object;

            var grafico = new Grafico
            {
                SomatorioDespesasPorAno = somatorioDespesasPorAno,
                SomatorioReceitasPorAno = somatorioReceitasPorAno
            };

            Assert.Equal(grafico.SomatorioDespesasPorAno, somatorioDespesasPorAno);
            Assert.Equal(grafico.SomatorioReceitasPorAno, somatorioReceitasPorAno);            
        }
    }
}