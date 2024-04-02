using Domain.Entites;
using Domain.Validations;

namespace UnitTests
{
    public class ProductTests
    {
        [Fact(DisplayName = "Deve retornar erro em produto sem ID")]
        public void Deve_Retornar_Erro_Em_Produto_Sem_Id()
        {
            //Arrange
            var expectedMessage = "Id do produto deve ser informado!";

            //Act
            var exception = Assert.Throws<DomainValidationException>(() => new Product(-1, "mouse", 1, 10M));            

            //Assert
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact(DisplayName = "Deve retornar erro em produto sem nome")]
        public void Deve_Retornar_Erro_Em_Produto_Sem_Nome()
        {
            //Arrange
            var expectedMessage = "Nome deve ser informado!";

            //Act
            var exception = Assert.Throws<DomainValidationException>(() => new Product(1, "", 1, 10M));

            //Assert
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact(DisplayName = "Deve retornar erro em produto sem estoque informado")]
        public void Deve_Retornar_Erro_Em_Produto_Sem_Estoque_Informado()
        {
            //Arrange
            var expectedMessage = "Estoque deve ser informado!";

            //Act
            var exception = Assert.Throws<DomainValidationException>(() => new Product(1, "mouse", -1, 10M));

            //Assert
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact(DisplayName = "Deve retornar erro em produto sem preço informado")]
        public void Deve_Retornar_Erro_Em_Produto_Sem_Preco_Informado()
        {
            //Arrange
            var expectedMessage = "Preço deve ser informado!";

            //Act
            var exception = Assert.Throws<DomainValidationException>(() => new Product(1, "mouse", 1, -1M));

            //Assert
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}