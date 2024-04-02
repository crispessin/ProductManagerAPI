using Application.DTOs;
using Application.Services;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace IntegrationTests
{
    public class ProductControllerIntegrationTests: IntegrationTest
    {

        [Test]
        public async Task Deve_Retornar_Produtos()
        {
            string inputUrl = "https://localhost:44395/api/products";

            var response = await _client.GetAsync(inputUrl);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));            

            var products = JsonConvert.DeserializeObject<ResultService<ICollection<ProductDTO>>> (
                await response.Content.ReadAsStringAsync()
            );            
            
            products.Data.Should().HaveCountGreaterThan(5);
        }

        [Test]
        public async Task Deve_Pesquisar_Produtos_Por_Nome()
        {
            string inputUrl = "https://localhost:44395/api/products?name=Teclado";

            var response = await _client.GetAsync(inputUrl);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var products = JsonConvert.DeserializeObject<ResultService<ICollection<ProductDTO>>>(
                await response.Content.ReadAsStringAsync()
            );

            products.Data.Should().OnlyContain(item => item.Name.StartsWith("Teclado"));
        }

        [Test]
        public async Task Deve_Retornar_Produto()
        {
            string inputUrl = "https://localhost:44395/api/products/1";

            var response = await _client.GetAsync(inputUrl);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var product = JsonConvert.DeserializeObject<ResultService<ProductDTO>>(
                await response.Content.ReadAsStringAsync()
            );

            product.Data.Name.Should().Be("Teclado");
        }
        
        [Test]
        public async Task Deve_Criar_Produto()
        {
            string inputUrl = "https://localhost:44395/api/products";

            var productDTO = new ProductDTO();
            productDTO.Name = "Carregador";
            productDTO.Stock = 1;
            productDTO.Price = 10M;

            var response = await _client.PostAsJsonAsync(inputUrl, productDTO);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            
            var product = JsonConvert.DeserializeObject<ResultService<ProductDTO>>(
                await response.Content.ReadAsStringAsync()
            );

            product.Data.Name.Should().Be("Carregador");
        }

        [Test]
        public async Task Deve_Atualizar_Produto()
        {
            // Arrange
            string inputUrl = "https://localhost:44395/api/products";

            var productDTO = new ProductDTO();
            productDTO.Name = "Mochila";
            productDTO.Stock = 1;
            productDTO.Price = 10M;

            var response = await _client.PostAsJsonAsync(inputUrl, productDTO);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var product = JsonConvert.DeserializeObject<ResultService<ProductDTO>>(
                await response.Content.ReadAsStringAsync()
            );

            product.Data.Name.Should().Be("Mochila");

            // Assert Update            
            var productDTOUpdate = new ProductDTO
            {
                ProductId = product.Data.ProductId,
                Name = "Mochila Alterada",
                Stock = 10,
                Price = 1M
            };

            var responseUpdated = await _client.PutAsJsonAsync(inputUrl, productDTOUpdate);

            Assert.That(responseUpdated.StatusCode, Is.EqualTo(HttpStatusCode.OK));            
        }

        //ToDo deletar
        [Test]
        public async Task Deve_Deletar_Produto()
        {
            // Arrange
            string inputUrl = "https://localhost:44395/api/products";

            var productDTO = new ProductDTO();
            productDTO.Name = "Mochila";
            productDTO.Stock = 1;
            productDTO.Price = 10M;

            var response = await _client.PostAsJsonAsync(inputUrl, productDTO);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var product = JsonConvert.DeserializeObject<ResultService<ProductDTO>>(
                await response.Content.ReadAsStringAsync()
            );
            
            //Delete
            string inputUrlDelete = $"https://localhost:44395/api/products/{product.Data.ProductId}";

            var responseDelete = await _client.DeleteAsync(inputUrlDelete);
            Assert.That(responseDelete.StatusCode, Is.EqualTo(HttpStatusCode.OK));


            // Assert Find Product Not Found
            string inputUrlFind = $"https://localhost:44395/api/products/{product.Data.ProductId}";

            var responseFind = await _client.GetAsync(inputUrlFind);
            Assert.That(responseFind.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                        
        }       
    }
}
