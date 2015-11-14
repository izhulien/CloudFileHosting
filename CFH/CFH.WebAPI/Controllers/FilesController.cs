namespace CFH.WebAPI.Controllers
{
    using System;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Web.Http;
    using CFH.Data;
    using CFH.Models;
    using CFH.WebAPI.Models;
    using Microsoft.AspNet.Identity;
    using System.Web;

    public class FilesController : BaseController
    {
        private static readonly Random random = new Random();

        public FilesController()
            :base(new CFHData())
        {
        }

        public IQueryable<FileModel> GetFiles()
        {
            return this.Data.Files
                            .All()
                            .Select(x => new FileModel()
                            {
                                Id = x.FileId,
                                Link = x.Link,
                                ApplicationUserId = x.ApplicationUserId
                            });
        }

        [HttpGet]
        public IQueryable<FileModel> GetTwoRandomFileFromDirectory(int directoryId)
        {

            var fileIds = this.Data.Files.All();
            var skip = (int)(random.NextDouble() * fileIds.Count());
            var files = this.Data.Files
                .All()
                .OrderBy(c => c.FileId)
                .Skip(skip)
                .Take(2);

            return files.Select(x => new FileModel()
            {
                Id = x.FileId,
                Link = x.Link,
                ApplicationUserId = x.ApplicationUserId
            })
            .Take(2);
        }

        [HttpGet]
        public IHttpActionResult GetFile(int id)
        {
            FileModel file = Data.Files
                .All()
                .Where(x => x.FileId == id)
                .Select(x =>
                    new FileModel()
                    {
                        Id = x.FileId,
                        Link = x.Link,
                        ApplicationUserId = x.ApplicationUserId
                    })
                .FirstOrDefault();

            if (file == null)
            {
                return NotFound();
            }

            return Ok(file);
        }

        /// <summary>
        /// Modifies an already uplaoded files.
        /// </summary>
        /// <param name="file">new file model</param>
        /// <returns>Http Result</returns>
        [HttpPut]
        public IHttpActionResult PutFile(FileModel file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO Add validations
            this.Data.Files
                .Find(file.Id)
                .Link = file.Link;

            try
            {
                this.Data.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FileExists(file.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw new ArgumentException("The file Id wasn't valid");
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult PostFile(FileModel file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int userId = int.Parse(User.Identity.GetUserId());
            var user = Data.Users.Find(userId);

            string subPath = "ImagesPath"; // your code goes here

            bool exists = System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("Images"));

            //if (!exists)
            //    System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

            this.Data.Files
                     .Add(new File()
                     {
                         ApplicationUserId = file.ApplicationUserId,
                         Link = file.Link
                     });

            this.Data.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = file.Id }, file);
        }

        private IHttpActionResult ValidatePicture(FileModel file)
        {
            var dbContext = new Data.ApplicationDbContext();

            if (file.ApplicationUserId == null ||
                !dbContext.Users
                .Any(x => x.Id == file.ApplicationUserId.ToString()))
            {
                return BadRequest("ApplicationUserId NOT FOUND");
            }

            if (file.Link == null || !file.Link.StartsWith("https://"))
            {
                return BadRequest("Invalid Url");

            }

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteFile(int id)
        {
            File file = this.Data.Files
                .Find(id);

            if (file == null)
            {
                return NotFound();
            }

            this.Data.Files.Delete(file);
            this.Data.SaveChanges();

            return Ok(string.Format("File {0} DELETED", id));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Data.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FileExists(int id)
        {
            return this.Data.Files
                .All()
                .Count(e => e.FileId == id) > 0;
        }
    }
}
