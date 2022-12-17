namespace Core.Storage
{
    using System;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;
    using Cysharp.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.Networking;

    public static class StorageManager
    {
        /// <summary>
        ///     Method returns the video file local path. If the file is not present on the device, the storage manager will
        ///     download the file first.
        /// </summary>
        /// <param name="fileAddress"></param>
        /// <param name="onProgressEvent"></param>
        /// <returns></returns>
        public static async Task<string> GetVideoFile(string fileAddress,
                                                      Action<object,
                                                              DownloadProgressChangedEventArgs>
                                                          onProgressEvent = null)
        {
            return await CacheAndGetLocalPath(fileAddress, StorageFileType.Video, onProgressEvent);
        }

        /// <summary>
        ///     Method returns the AudioClip from storage. If the file is not present on the device, the storage manager will
        ///     download the file first.
        /// </summary>
        /// <param name="fileAddress"></param>
        /// <param name="onProgressEvent"></param>
        /// <returns></returns>
        public static async Task<AudioClip> GetAudioClip(string fileAddress,
                                                         Action<object,
                                                                 DownloadProgressChangedEventArgs>
                                                             onProgressEvent = null)
        {
            string localPath =
                await CacheAndGetLocalPath(fileAddress, StorageFileType.Audio, onProgressEvent);
            var fileInfo = new FileInfo(fileAddress);
            using var loader =
                UnityWebRequestMultimedia.GetAudioClip(localPath, fileInfo.ToAudioType());
            try
            {
                await loader.SendWebRequest();
            }
            catch(Exception e)
            {
                Debug.LogError($"Error loading AudioClip '{loader.uri}': {loader.error}, exception: {e.Message}");
                throw;
            }

            return DownloadHandlerAudioClip.GetContent(loader);
        }

        /// <summary>
        ///     Method returns the Texture2D from storage. If the file is not present on the device, the storage manager will
        ///     download the file first.
        /// </summary>
        /// <param name="fileAddress"></param>
        /// <param name="onProgressEvent"></param>
        /// <returns></returns>
        public static async Task<Texture2D> GetTexture(string fileAddress,
                                                       Action<object,
                                                               DownloadProgressChangedEventArgs>
                                                           onProgressEvent = null)
        {
            string localPath =
                await CacheAndGetLocalPath(fileAddress, StorageFileType.Texture, onProgressEvent);
            using var loader = UnityWebRequestTexture.GetTexture(localPath);
            try
            {
                await loader.SendWebRequest();
            }
            catch(Exception e)
            {
                Debug.LogError($"Error loading Texture '{loader.uri}': {loader.error}, exception: {e.Message}");
                throw;
            }

            return DownloadHandlerTexture.GetContent(loader);
        }

        /// <summary>
        ///     Method downloads the file and returns local path to the file.
        /// </summary>
        /// <param name="fileAddress"></param>
        /// <param name="storageFileType"></param>
        /// <param name="onProgressEvent"></param>
        /// <returns></returns>
        private static async Task<string> CacheAndGetLocalPath(string fileAddress,
                                                               StorageFileType storageFileType,
                                                               Action<object,
                                                                       DownloadProgressChangedEventArgs
                                                                   >
                                                                   onProgressEvent = null)
        {
            var fileInfo = new FileInfo(fileAddress);

            string storagePath = Path.Combine(StoragePaths.GetPath(storageFileType), fileInfo.Name);
            Debug.Log(storagePath);

            await DownloadUtils.DownloadAndSave(fileAddress, storagePath, onProgressEvent);
            return StorageUtilities.CreateLocalPath(storagePath);
        }

        /// <summary>
        ///     Method cleans the folder based on StorageFileType. i.e. "Audio" for StorageFileType.Audio.
        /// </summary>
        /// <param name="fileType"></param>
        public static void ClearMediaPath(StorageFileType fileType)
        {
            var directoryInfo = new DirectoryInfo(StoragePaths.GetPath(fileType));
            var fileInfos = directoryInfo.GetFiles("*", SearchOption.AllDirectories);
            foreach(var fileInfo in fileInfos)
            {
                File.Delete(fileInfo.FullName);
            }

            var directories = directoryInfo.GetDirectories("*", SearchOption.AllDirectories);
            foreach(var directory in directories)
            {
                Directory.Delete(directory.FullName);
            }
        }

        /// <summary>
        ///     Method returns FileInfos from the StorageFileType space
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns></returns>
        public static FileInfo[] GetFiles(StorageFileType fileType)
        {
            var directoryInfo = new DirectoryInfo(StoragePaths.GetPath(fileType));
            return directoryInfo.GetFiles("*", SearchOption.AllDirectories);
        }
    }
}