using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoService.DatabaseContext;
using PhotoService.Interfaces;
using PhotoService.Model;
using PhotoService.Requests;

namespace PhotoService.Services
{
    public class PhotosService : IPhotoService
    {
        readonly TestApiDB _context;
        private readonly string _loadPath;

        public PhotosService(TestApiDB context)
        {
            _context = context;
            _loadPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedPhotos");
            if (!Directory.Exists(_loadPath))
            {
                Directory.CreateDirectory(_loadPath);
            }
        }
        public async Task<IActionResult> DeletePhoto(int id)
        {
            if (id <= 0)
            {
                return new BadRequestObjectResult("Некорректный идентификатор фото.");
            }

            try
            {
                var photos = await _context.Photos.FindAsync(id);
                if (photos == null)
                {
                    return new NotFoundObjectResult("Фото с указанным идентификатором не найдено.");
                }

                _context.Photos.Remove(photos);
                await _context.SaveChangesAsync();

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Внутренняя ошибка сервера: {ex.Message}") { StatusCode = 500 };
            }
        }
        public async Task<IActionResult> GetPhotos()
        {
            try
            {
                var photos = await _context.Photos.ToListAsync();
                var photosDto = photos.Select(p => new GetAllPhoto
                {
                    Id = p.Id,
                    Name = p.Name,
                    File = p.File
                });
                return new OkObjectResult(photosDto);
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Внутренняя ошибка сервера: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<IActionResult> GetPhotosById(int id)
        {
            if (id <= 0)
            {
                return new BadRequestObjectResult("Некорректный идентификатор фото.");
            }
            try
            {
                var photos = await _context.Photos.FindAsync(id);
                if (photos == null)
                {
                    return new NotFoundObjectResult("Фото с указанным идентификатором не найдено.");
                }

                var photosDto = new GetAllPhoto
                {
                    Id = photos.Id,
                    Name = photos.Name,
                    File = photos.File
                };
                return new OkObjectResult(photosDto);
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Внутренняя ошибка сервера: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<IActionResult> LoadPhoto(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return new OkObjectResult(new { Message = "error" });
            }

            var Name = Path.GetFileName(file.FileName);
            var File = Path.Combine(_loadPath, Name);

            using (var stream = new FileStream(File, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var photo = new Photos { Name = Name, File = File };
            _context.Photos.Add(photo);
            await _context.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<IActionResult> UpdatePhoto(int id, [FromQuery] Photos updatedPhoto)
        {
            if (id != updatedPhoto.Id)
            {
                return new BadRequestObjectResult("Некорректные данные для обновления фото.");
            }

            _context.Entry(updatedPhoto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Внутренняя ошибка сервера: {ex.Message}") { StatusCode = 500 };
            }
        }
    }
}
