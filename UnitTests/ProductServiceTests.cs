using Application.DTOs;
using Application.Services;
using AutoMapper;
using Domain.Entites;
using Domain.Repositories;
using Moq;

namespace UnitTests
{
    public class ProductServiceTests
    {
        [Fact(DisplayName = "Deve criar o produto")]
        public async Task Deve_Criar_O_Produto()
        {
            var productRepository = new Mock<IProductRepository>();
            var mapper = new Mock<IMapper>();
            var productService = new ProductService(productRepository.Object, mapper.Object);

            //Arrange
            var product = new Product("mouse", 1, 10M);

            var productDTO = new ProductDTO()
            {
                Name = "mouse",
                Stock = 1,
                Price = 10M
            };

            productRepository
                .Setup(repo => repo.CreateAsync(product))
                .ReturnsAsync(product);

            mapper
                .Setup(m => m.Map<Product>(productDTO))
                .Returns(product);

            //Act
            var result = await productService.CreateAsync(productDTO);

            //Assert
            productRepository.Verify(p => p.CreateAsync(product), Times.AtLeastOnce);
            mapper.Verify(m => m.Map<ProductDTO>(product), Times.AtLeastOnce);
        }

        [Fact(DisplayName = "Deve retornar erro se o produto for nulo")]
        public async Task Deve_Retornar_Erro_Quando_Produto_Nulo()
        {
            var productRepository = new Mock<IProductRepository>(MockBehavior.Strict);
            var mapper = new Mock<IMapper>(MockBehavior.Strict);
            var productService = new ProductService(productRepository.Object, mapper.Object);

            //Arrange
            var expectedMessage = "Objeto deve ser informado!";
  
            //Act
            var result = await productService.CreateAsync(null);

            //Assert
            Assert.False(result.IsSucess);
            Assert.Equal(expectedMessage, result.Message);
            
            productRepository.VerifyNoOtherCalls();
            mapper.VerifyNoOtherCalls();
        }

        [Fact(DisplayName = "Deve retornar erro se o estoque vazio")]
        public async Task Deve_Retornar_Erro_Quando_Estoque_Vazio()
        {
            var productRepository = new Mock<IProductRepository>(MockBehavior.Strict);
            var mapper = new Mock<IMapper>(MockBehavior.Strict);
            var productService = new ProductService(productRepository.Object, mapper.Object);

            //Arrange
            var expectedMessage = "Problemas na validação";
            var expectedMessageError = "Campo estoque deve ser informado.";

            var productDTO = new ProductDTO()
            {
                Name = "mouse",
                Price = 10M
            };

            //Act
            var result = await productService.CreateAsync(productDTO);

            //Assert
            Assert.False(result.IsSucess);
            Assert.Equal(expectedMessage, result.Message);
            Assert.Equal(expectedMessageError, result.Errors.First().Message);

            productRepository.VerifyNoOtherCalls();
            mapper.VerifyNoOtherCalls();
        }

        [Fact(DisplayName = "Deve retornar erro se o nome vazio")]
        public async Task Deve_Retornar_Erro_Quando_Nome_Vazio()
        {
            var productRepository = new Mock<IProductRepository>(MockBehavior.Strict);
            var mapper = new Mock<IMapper>(MockBehavior.Strict);
            var productService = new ProductService(productRepository.Object, mapper.Object);

            //Arrange
            var expectedMessage = "Problemas na validação";
            var expectedMessageError = "Campo nome deve ser informado.";

            var productDTO = new ProductDTO()
            {
                Name = "",
                Price = 10M,
                Stock = 1
            };

            //Act
            var result = await productService.CreateAsync(productDTO);

            //Assert
            Assert.False(result.IsSucess);
            Assert.Equal(expectedMessage, result.Message);
            Assert.Equal(expectedMessageError, result.Errors.First().Message);

            productRepository.VerifyNoOtherCalls();
            mapper.VerifyNoOtherCalls();
        }

        [Fact(DisplayName = "Deve retornar erro se o preço vazio")]
        public async Task Deve_Retornar_Erro_Quando_Preco_Vazio()
        {
            var productRepository = new Mock<IProductRepository>(MockBehavior.Strict);
            var mapper = new Mock<IMapper>(MockBehavior.Strict);
            var productService = new ProductService(productRepository.Object, mapper.Object);

            //Arrange
            var expectedMessage = "Problemas na validação";
            var expectedMessageError = "Campo preço deve ser maior que zero.";

            var productDTO = new ProductDTO()
            {
                Name = "mouse",
                Price = 0M,
                Stock = 1
            };

            //Act
            var result = await productService.CreateAsync(productDTO);

            //Assert
            Assert.False(result.IsSucess);
            Assert.Equal(expectedMessage, result.Message);
            Assert.Equal(expectedMessageError, result.Errors.First().Message);

            productRepository.VerifyNoOtherCalls();
            mapper.VerifyNoOtherCalls();
        }

        [Fact(DisplayName = "Deve deletar o produto")]
        public async Task Deve_Deletar_O_Produto()
        {
            var productRepository = new Mock<IProductRepository>();
            var mapper = new Mock<IMapper>(MockBehavior.Strict);
            var productService = new ProductService(productRepository.Object, mapper.Object);

            //Arrange
            var product = new Product("mouse", 1, 10M);

            productRepository
                .Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(product);

            var expectedMessage = "Produto do id: 1 foi deletado.";

            //Act
            var result = await productService.DeleteAsync(1);

            //Assert
            Assert.True(result.IsSucess);
            Assert.Equal(expectedMessage, result.Message);

            productRepository.Verify(p => p.GetByIdAsync(1), Times.AtLeastOnce);
            productRepository.Verify(p => p.DeleteAsync(product), Times.AtLeastOnce);
        }

        [Fact(DisplayName = "Deve dar erro ao deletar o produto sem ID encontrado")]
        public async Task Deve_Retornar_Erro_Ao_Deleter_Produto_Nao_Encontrado()
        {
            var productRepository = new Mock<IProductRepository>();
            var mapper = new Mock<IMapper>(MockBehavior.Strict);
            var productService = new ProductService(productRepository.Object, mapper.Object);

            //Arrange
            var product = new Product("mouse", 1, 10M);

            productRepository
                .Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync((Product) null);

            var expectedMessage = "Produto não encontrado.";

            //Act
            var result = await productService.DeleteAsync(1);

            //Assert
            Assert.False(result.IsSucess);
            Assert.Equal(expectedMessage, result.Message);

            productRepository.Verify(p => p.GetByIdAsync(1), Times.AtLeastOnce);
            productRepository.VerifyNoOtherCalls();
        }

        [Fact(DisplayName = "Deve listar os produtos")]
        public async Task Deve_Listar_Os_Produtos()
        {
            var productRepository = new Mock<IProductRepository>();
            var mapper = new Mock<IMapper>();
            var productService = new ProductService(productRepository.Object, mapper.Object);

            //Arrange
            var product = new Product("mouse", 1, 10M);

            var products = new List<Product> { product };

            var productDTO = new ProductDTO()
            {
                Name = "mouse",
                Stock = 1,
                Price = 10M
            };

            var productsDTO = new List<ProductDTO> { productDTO };

            productRepository
                .Setup(repo => repo.GetProductsAsync(null))
                .ReturnsAsync(products);

            mapper
                .Setup(m => m.Map<ICollection<ProductDTO>>(products))
                .Returns(productsDTO);

            //Act
            var result = await productService.GetAsync(null);

            //Assert
            productRepository.Verify(p => p.GetProductsAsync(null), Times.AtLeastOnce);
            mapper.Verify(m => m.Map<ICollection<ProductDTO>>(products), Times.AtLeastOnce);

            var productFirst = result.Data.First();
            Assert.Equal("mouse", productFirst.Name);
            Assert.Equal(1, productFirst.Stock);
            Assert.Equal(10M, productFirst.Price);
        }

        [Fact(DisplayName = "Deve retornar o produto por ID")]
        public async Task Deve_Retornar_O_Produto_Por_ID()
        {
            var productRepository = new Mock<IProductRepository>();
            var mapper = new Mock<IMapper>();
            var productService = new ProductService(productRepository.Object, mapper.Object);

            //Arrange
            var product = new Product("mouse", 1, 10M);

            var productDTO = new ProductDTO()
            {
                Name = "mouse",
                Stock = 1,
                Price = 10M
            };

            productRepository
                .Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(product);

            mapper
                .Setup(m => m.Map<ProductDTO>(product))
                .Returns(productDTO);

            //Act
            var result = await productService.GetByIdAsync(1);

            //Assert
            productRepository.Verify(p => p.GetByIdAsync(1), Times.AtLeastOnce);
            mapper.Verify(m => m.Map<ProductDTO>(product), Times.AtLeastOnce);

            var productResult = result.Data;
            Assert.Equal("mouse", productResult.Name);
            Assert.Equal(1, productResult.Stock);
            Assert.Equal(10M, productResult.Price);
        }

        [Fact(DisplayName = "Deve retornar erro ao buscar o produto inexistente")]
        public async Task Deve_Retornar_Erro_Ao_Buscar_um_ID_Inesxistente()
        {
            var productRepository = new Mock<IProductRepository>();
            var mapper = new Mock<IMapper>(MockBehavior.Strict);
            var productService = new ProductService(productRepository.Object, mapper.Object);

            //Arrange
            productRepository
                .Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync((Product)null);

            var expectedMessage = "Produto não encontrado.";

            //Act
            var result = await productService.GetByIdAsync(1);

            //Assert
            Assert.False(result.IsSucess);
            Assert.Equal(expectedMessage, result.Message);

            productRepository.Verify(p => p.GetByIdAsync(1), Times.AtLeastOnce);
            productRepository.VerifyNoOtherCalls();
        }

        [Fact(DisplayName = "Deve permitir a busca por produtos por campo informado")]
        public async Task Deve_Permitir_A_Busca_Por_Produtos()
        {
            var productRepository = new Mock<IProductRepository>();
            var mapper = new Mock<IMapper>();
            var productService = new ProductService(productRepository.Object, mapper.Object);

            //Arrange
            var product = new Product("mouse", 1, 10M);

            var products = new List<Product> { product };

            var productDTO = new ProductDTO()
            {
                Name = "mouse",
                Stock = 1,
                Price = 10M
            };

            var productsDTO = new List<ProductDTO> { productDTO };

            productRepository
                .Setup(repo => repo.SearchByNameAsync("mouse", null))
                .ReturnsAsync(products);

            mapper
                .Setup(m => m.Map<ICollection<ProductDTO>>(products))
                .Returns(productsDTO);

            //Act
            var result = await productService.SearchByNameAsync("mouse", null);

            //Assert
            productRepository.Verify(p => p.SearchByNameAsync("mouse", null), Times.AtLeastOnce);
            mapper.Verify(m => m.Map<ICollection<ProductDTO>>(products), Times.AtLeastOnce);

            var productFirst = result.Data.First();
            Assert.Equal("mouse", productFirst.Name);
            Assert.Equal(1, productFirst.Stock);
            Assert.Equal(10M, productFirst.Price);
        }

        [Fact(DisplayName = "Deve atualizar o produto")]
        public async Task Deve_Atualizar_O_Produto()
        {
            var productRepository = new Mock<IProductRepository>();
            var mapper = new Mock<IMapper>();
            var productService = new ProductService(productRepository.Object, mapper.Object);

            //Arrange
            var product = new Product(1, "mouse", 1, 10M);

            var productDTO = new ProductDTO()
            {
                ProductId = 1,
                Name = "mouse",
                Stock = 1,
                Price = 10M
            };

            productRepository
               .Setup(repo => repo.GetByIdAsync(1))
               .ReturnsAsync(product);

            mapper
                .Setup(m => m.Map<ProductDTO, Product>(productDTO, product))
                .Returns(product);

            //Act
            var result = await productService.UpdateAsync(productDTO);

            //Assert
            productRepository.Verify(p => p.EditAsync(product), Times.AtLeastOnce);
            mapper.Verify(m => m.Map<ProductDTO, Product>(productDTO, product), Times.AtLeastOnce);
        }
    }
}
