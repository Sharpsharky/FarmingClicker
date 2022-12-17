namespace Core.Storage
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using UnityEngine;

    public static class DownloadUtils
    {
        /// <summary>
        ///     Method downloads the file to the given savePath and returns its local path.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="savePath"></param>
        /// <param name="downloadProgressChanged"></param>
        public static async Task DownloadAndSave(string uri, string savePath,
                                                 Action<object, DownloadProgressChangedEventArgs>
                                                     downloadProgressChanged = null)
        {
            // return if we can't obtain directory.
            string directoryName = Path.GetDirectoryName(savePath);
            if(string.IsNullOrEmpty(directoryName))
            {
                return;
            }

            // In case we don't have the directory yet
            if(!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            byte[] existingBytes = new byte[0];

            if(File.Exists(savePath))
            {
                existingBytes = File.ReadAllBytes(savePath);
            }

            // Create directory if needed
            if(!Directory.Exists(Path.GetDirectoryName(savePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(savePath));
            }

            //get the filesize
            long filesize = await GetFilesize(uri);

            // case where file stored on the drive exceeds the filesize on server
            // we want to re-download everything
            if(existingBytes.Length > filesize)
            {
                existingBytes = new byte[0];
            }

            // case where the filesize on drive and server are even.
            // We want to abandon the download 
            if(existingBytes.Length == filesize)
            {
                return;
            }
            //otherwise we want to download or continue to download the file 

            using var client = new WebClientAddBuffer(existingBytes.Length);
            if(downloadProgressChanged != null)
            {
                client.DownloadProgressChanged +=
                    new DownloadProgressChangedEventHandler(downloadProgressChanged);
            }

            try
            {
                // if we do have already downloaded part of the file we need to combine downloaded bytes 
                // and save them manually.
                if(existingBytes.Length > 0)
                {
                    byte[] downloadedBytes = await client.DownloadDataTaskAsync(uri);
                    byte[] toWrite = existingBytes.Concat(downloadedBytes).ToArray();
                    File.WriteAllBytes(savePath, toWrite);
                }
                else
                {
                    await client.DownloadFileTaskAsync(uri, savePath);
                }
            }
            catch(WebException e)
            {
                // The file is fully downloaded
                if(!(e.Response is HttpWebResponse
                         {StatusCode: HttpStatusCode.RequestedRangeNotSatisfiable}))
                {
                    throw;
                }

                Debug.Log(e.Message);
            }
        }

        /// <summary>
        ///     Method returns the filesize of the file from server.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static async Task<long> GetFilesize(string uri)
        {
            using var wc = new WebClient();
            await wc.OpenReadTaskAsync(uri);
            long bytesTotal = Convert.ToInt64(wc.ResponseHeaders["Content-Length"]);
            wc.Dispose();
            return bytesTotal;
        }
    }
}