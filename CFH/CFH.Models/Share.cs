namespace CFH.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Share
    {
        [Key]
        public int ShareId { get; set; }

        public string ShareLink { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public int FileId { get; set; }

        public virtual File File { get; set; }
    }
}
