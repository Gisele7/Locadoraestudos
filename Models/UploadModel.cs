namespace Filme_Locadora.Models
{
    public class UploadModel
    {
        public string Name { get; set; }

        public IFormFile File { get; set; }
    }
}
