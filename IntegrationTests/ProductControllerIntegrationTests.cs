using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using FluentAssertions;

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
            
            products.Data.Should().HaveCount(5);
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
  
    }
}
