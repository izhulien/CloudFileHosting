namespace CFH.WebAPI.Controllers
{
    using System.Web.Http;
    using CFH.Data;
    using CFH.Models;
    using CFH.WebAPI.Models;

    //[Authorize]
    public class ConditionsController : ApiController
    {
        private ConditionData data;

        public ConditionsController()
        {
            this.data = new ConditionData();
        }

        [HttpPost]
        public IHttpActionResult Create([FromBody]ConditionModel condition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Created vote is not valid!");
            }

            // Check if file exists
            var uploadFile = this.data.Files
                .Find(condition.UploadFileId);
            var downloadFile = this.data.Files
                .Find(condition.DownloadFileId);

            if (uploadFile == null || downloadFile == null)
            {
                return BadRequest("Cannot find file in database!");
            }

            // Check if ids are equal
            if (condition.UploadFileId == condition.DownloadFileId)
            {
                return BadRequest("Cannot compare file that are the same!");
            }

            // Check if ids belongs to a same person
            if (uploadFile.ApplicationUserId == downloadFile.ApplicationUserId)
            {
                return BadRequest("Cannot compare files that belongs to a same person!");
            }

            // Check if pictures are from different categories
            if (uploadFile.DirectoryId != downloadFile.DirectoryId)
            {
                return BadRequest("Cannot compare files from different directories!");
            }

            var newCondition = new Condition
            {
                ApplicationUserId = condition.UserId,
                DowanloadFile = downloadFile,
                UploadFile = uploadFile,
            };
            this.data.Conditions.Add(newCondition);

            // ???
            //downloadFile.Count++;
            //uploadFile.Count--;

            this.data.SaveChanges();

            return Ok(newCondition.ConditionId);
        }
    }
}
