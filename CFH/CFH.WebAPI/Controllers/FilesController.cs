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

    public class FilesController : ApiController
    {
        private static readonly Random random = new Random();
        private readonly ConditionData data;

        public FilesController()
        {
            this.data = new ConditionData();
        }

        public IQueryable<FileModel> GetFiles()
        {
            return this.data.Files
                            .All()
                            .Select(x => new FileModel()
                            {
                                Id = x.FileId,
                                Link = x.Link,
                                DirectoryId = x.DirectoryId,
                                ApplicationUserId = x.ApplicationUserId
                            });
        }

        [HttpGet]
        public IQueryable<FileModel> GetTwoRandomFileFromDirectory(int directoryId)
        {

            var fileIds = this.data.Files.All();
            var skip = (int)(random.NextDouble() * fileIds.Count());
            var files = this.data.Files
                .All()
                .OrderBy(c => c.FileId)
                .Skip(skip)
                .Take(2);

            return files.Select(x => new FileModel()
            {
                Id = x.FileId,
                Link = x.Link,
                DirectoryId = x.DirectoryId,
                ApplicationUserId = x.ApplicationUserId
            })
            .Take(2);
        }

        [HttpGet]
        public IQueryable<FileModel> GetRandomFileFromRandomDirectory()
        {
            int randomDirectory = GetRandomDirectoryId();
            return GetTwoRandomFileFromDirectory(randomDirectory);
        }

        [HttpGet]
        public IHttpActionResult GetFile(int id)
        {
            FileModel file = data.Files
                .All()
                .Where(x => x.FileId == id)
                .Select(x =>
                    new FileModel()
                    {
                        Id = x.FileId,
                        Link = x.Link,
                        DirectoryId = x.DirectoryId,
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
            this.data.Files
                .Find(file.Id)
                .Link = file.Link;

            try
            {
                this.data.SaveChanges();
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

        [HttpPost]
        public IHttpActionResult PostPicture(FileModel file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            this.data.Files
                     .Add(new File()
                     {
                         DirectoryId = file.DirectoryId,
                         ApplicationUserId = file.ApplicationUserId,
                         Link = file.Link
                     });

            this.data.SaveChanges();
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

            if (file.DirectoryId == null ||
                !dbContext.Directories
                .Any(x => x.DirId == file.DirectoryId))
            {
                return BadRequest("DirectoryId NOT FOUND");
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
            File file = this.data.Files
                .Find(id);

            if (file == null)
            {
                return NotFound();
            }

            this.data.Files.Delete(file);
            this.data.SaveChanges();

            return Ok(string.Format("File {0} DELETED", id));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.data.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FileExists(int id)
        {
            return this.data.Files
                .All()
                .Count(e => e.FileId == id) > 0;
        }

        private int GetRandomDirectoryId()
        {
            var directoryIds = this.data.Directories
                .All()
                .Select(d => d.DirId);
            var skip = (int)(random.NextDouble() * directoryIds.Count());
            return this.data.Directories
                .All()
                .OrderBy(d => d.DirId)
                .Skip(skip)
                .Take(1)
                .First()
                .DirId;
        }
    }
}
