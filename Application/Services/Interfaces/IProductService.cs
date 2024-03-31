using Application.DTOs;

namespace Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<ResultService<ProductDTO>> CreateAsync(ProductDTO productDTO);
        Task<ResultService<ICollection<ProductDTO>>> GetAsync(string? orderBy);
        Task<ResultService<ProductDTO>> GetByIdAsync(int id);
        Task<ResultService<ICollection<ProductDTO>>> SearchByNameAsync(string name, string? orderBy);
        Task<ResultService> UpdateAsync(ProductDTO productDTO);
        Task<ResultService> DeleteAsync(int id);
    }
}
