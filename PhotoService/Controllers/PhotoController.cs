using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoService.Interfaces;
using PhotoService.Model;

namespace PhotoService.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
    public class PhotoController : Controller
    {
            private readonly IPhotoService _photoService;
            public PhotoController(IPhotoService photoService)
            {
                _photoService = photoService;
            }

            [HttpPost("load")]
            public async Task<IActionResult> LoadPhoto(IFormFile file)
            {
                return await _photoService.LoadPhoto(file);
            }

            [HttpGet]
            public async Task<IActionResult> GetPhotos()
            {
                return await _photoService.GetPhotos();
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetPhotosById(int id)
            {
                return await _photoService.GetPhotosById(id);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdatePhoto(int id, [FromQuery] Photos updatedPhoto)
            {
                return await _photoService.UpdatePhoto(id, updatedPhoto);
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeletePhoto(int id)
            {
                return await _photoService.DeletePhoto(id);
            }
        }
    }
