using System.ComponentModel.DataAnnotations;

namespace PhotoService.Model
{
    public class Photos
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string File { get; set; }

    }
}
