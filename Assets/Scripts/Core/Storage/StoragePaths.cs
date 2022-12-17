namespace Core.Storage
{
    using System.Collections.Generic;
    using System.IO;
    using UnityEngine;

    /// <summary>
    ///     Stores all the paths used in StorageManager.
    /// </summary>
    public static class StoragePaths
    {
        private const string VIDEO_PATH = "Videos/";
        private const string TEXTURE_PATH = "Textures/";
        private const string AUDIO_PATH = "Audio/";
        private const string DATA_PATH = "Data/";

        private static readonly Dictionary<StorageFileType, string> Paths = new()
            {
                {StorageFileType.Audio, AUDIO_PATH},
                {StorageFileType.Data, DATA_PATH},
                {StorageFileType.Texture, TEXTURE_PATH},
                {StorageFileType.Video, VIDEO_PATH},
            };

        /// <summary>
        ///     Method returns path based on StorageFileType.
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns></returns>
        public static string GetPath(StorageFileType fileType)
        {
            return Path.Combine(Application.persistentDataPath, Paths[fileType]);
        }
    }
}