using Microsoft.AspNetCore.Mvc;
using PhotoService.Model;
using PhotoService.Requests;

namespace PhotoService.Interfaces
{
    public interface IPhotoService
    {
        Task<IActionResult> LoadPhoto(IFormFile file);
        Task<IActionResult> GetPhotos();
        Task<IActionResult> GetPhotosById(int id);
        Task<IActionResult> UpdatePhoto(int id, [FromQuery] Photos updatedPhoto);
        Task<IActionResult> DeletePhoto(int id);
    }
}
