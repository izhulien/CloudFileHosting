namespace CFH.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// File uploaded by given user for given directory
    /// One user can upload one file for one directory 
    /// </summary>
    public class File
    {
        [Key]
        public int FileId { get; set; }

        public string Name { get; set; }

        public int Size { get; set; }

        public string Type { get; set; }

        public string Link { get; set; }

        public int Count { get; set; }

        [ForeignKey("ApplicationUser")]
        public int ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public int DirectoryId { get; set; }

        public virtual Directory Directory { get; set; }
    }
}
