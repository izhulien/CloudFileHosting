namespace CFH.ConsoleClient
{
    using CFH.Data;
    using CFH.DropboxApi;
    using System;
    using Spring.IO;

    public class CFHConsoleClient
    {
        private static readonly CFHData CFHData = new CFHData();

        internal static void Main()
        {
            // Console.WriteLine(CFHData.Files.All());

            // Testing Dropbox
            var dropbox = new DropBoxCloudConnector();

            string testFile = @"../../Resources/1.png";
            var file = new FileResource(testFile);
            var uploadedGirl = dropbox.UploadFileToCloud(file);

            var fileLink = dropbox.GetFileLink("/File/1.png");
            Console.WriteLine(fileLink.Url);
        }
    }
}
