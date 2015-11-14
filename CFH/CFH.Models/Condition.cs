namespace CFH.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Condition
    {
        [Key]
        public int ConditionId { get; set; }

        public int ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual File UploadFile { get; set; }

        public virtual File DowanloadFile { get; set; }
    }
}
