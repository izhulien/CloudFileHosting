namespace CFH.WebAPI.Models
{
    public class FileModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Size { get; set; }

        public string Type { get; set; }

        public string Link { get; set; }

        public int ApplicationUserId { get; set; }
    }
}