namespace Core.Storage
{
    using System.IO;
    using UnityEngine;

    public static class StorageUtilities
    {
        /// <summary>
        ///     Converts extension of the file to AudioType.
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        public static AudioType ToAudioType(this FileInfo fileInfo)
        {
            return fileInfo.Extension switch
                   {
                       ".mp3" => AudioType.MPEG,
                       ".ogg" => AudioType.OGGVORBIS,
                       _ => AudioType.UNKNOWN,
                   };
        }

        /// <summary>
        ///     Creates local path for files as for Desktop we need to add additional "file://" on the beginning of the path
        /// </summary>
        /// <param name="storagePath"></param>
        /// <returns></returns>
        public static string CreateLocalPath(string storagePath)
        {
            return Application.isMobilePlatform
                       ? storagePath
                       : Path.Combine("file://", storagePath);
        }
    }
}