using FluentFTP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage
{
    public class FTPHelper
    {
        private string _ftpIpAddress;
        private string _username;
        private string _password;
        private int _port;

        public FTPHelper(string ftpIpAddress, string username, string password, int port)
        {
            _ftpIpAddress = ftpIpAddress;
            _username = username;
            _password = password;
            _port = port;
        }
        public async Task<List<string>> GetFileListAsnyc(string path)
        {
            FtpClient client = new FtpClient(_ftpIpAddress, new NetworkCredential(_username, _password));
            client.Port = _port;
            await client.ConnectAsync();
            var fileNames = new List<string>();
            foreach (FtpListItem item in client.GetListing(path))
            {
                // if this is a file
                if (item.Type == FtpFileSystemObjectType.File)
                {
                    fileNames.Add(item.FullName);
                }
            }
            await client.DisconnectAsync();
            return fileNames;
        }

        public async Task MoveFileAsync(string sourcePath, string sourceName, string destPath, string destName)
        {
            FtpClient client = new FtpClient(_ftpIpAddress, new NetworkCredential(_username, _password));
            client.Port = _port;
            await client.ConnectAsync();
            bool directoryExisted = await client.DirectoryExistsAsync(destPath);
            if (!directoryExisted)
            {
                await client.CreateDirectoryAsync(destPath);
            }
            await client.MoveFileAsync(sourcePath + sourceName, destPath + destName);
            await client.DisconnectAsync();
        }

        public async Task UploadFileFromStreamAsync(Stream fileStream, string filePath, string fileName)
        {
            FtpClient client = new FtpClient(_ftpIpAddress, new NetworkCredential(_username, _password));
            client.Port = _port;
            await client.ConnectAsync();
            bool directoryExisted = await client.DirectoryExistsAsync(filePath);
            if (!directoryExisted)
            {
                await client.CreateDirectoryAsync(filePath);
            }
            await client.UploadAsync(fileStream, filePath + fileName);

            await client.DisconnectAsync();
        }

        public async Task<Stream> GetStreamFromFileAsync(string filePath, string filename)
        {
            FtpClient client = new FtpClient(_ftpIpAddress, new NetworkCredential(_username, _password));
            client.Port = _port;
            await client.ConnectAsync();
            var stream = await client.OpenReadAsync(filePath + filename);
            await client.DisconnectAsync();
            return stream;
        }

        public async Task<string> DownLoadToTempFileAsync(string filePath, string filename)
        {
            try
            {
                FtpClient client = new FtpClient(_ftpIpAddress, new NetworkCredential(_username, _password));
                string pathTempFile = Path.GetTempPath();
                var tempFileName = Guid.NewGuid() + ".txt";
                client.Port = _port;
                await client.ConnectAsync();
                await client.DownloadFileAsync(pathTempFile + tempFileName, "./" + filePath + filename);
                await client.DisconnectAsync();
                return pathTempFile + tempFileName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
