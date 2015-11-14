namespace CFH.DropboxApi
{
    using Spring.IO;
    using Spring.Social.Dropbox.Api;

    public class DropBoxCloudConnector
    {
        private const string FileColection = "File";

        private readonly DropBoxCloud dropBoxCloud;

        public DropBoxCloudConnector()
            : this(new DropBoxCloud())
        {
        }

        public DropBoxCloudConnector(DropBoxCloud dropBoxCloud)
        {
            this.dropBoxCloud = dropBoxCloud;
        }

        public Entry UploadFileToCloud(FileResource resource)
        {
            string collection = "/" + FileColection + "/" + resource.File.Name;
            var entry = this.dropBoxCloud.UploadToCloud(resource, collection);
            return entry;
        }

        public Entry GetAllFiless()
        {
            var files = this.dropBoxCloud.GetAllMediaFiles(FileColection);

            return files;
        }

        public DropboxLink GetFileLink(string path)
        {
            var fileLink = this.dropBoxCloud.GetMediaLink(path);

            return fileLink;
        }
    }
}
