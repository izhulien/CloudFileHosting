namespace CFH.WebAPI.Models
{
    public class ConditionModel
    {
        //[Required]
        //[MinLength(40)]
        //[MaxLength(40)]
        public int UserId { get; set; }

        public int UploadFileId { get; set; }

        public int DownloadFileId { get; set; }
    }
}