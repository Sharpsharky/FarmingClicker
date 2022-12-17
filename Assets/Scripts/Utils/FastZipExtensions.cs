namespace Utils
{
    using Cysharp.Threading.Tasks;
    using ICSharpCode.SharpZipLib.Zip;
    using UnityEngine;

    public static class FastZipExtensions
    {
        public static async UniTask CreateZipAsync(this FastZip fastZip, string zipFileName,
                                                   string sourceDirectory, bool recurse,
                                                   string fileFilter = null)
        {
            await UniTask.RunOnThreadPool(() =>
                                          {
                                              Debug
                                                  .Log($"Zipping file '{zipFileName}' with contents '{sourceDirectory}'. {{ recurse: {recurse}, fileFilter: {fileFilter}}}...");
                                              fastZip.CreateZip(zipFileName, sourceDirectory,
                                                                recurse, fileFilter);
                                              Debug
                                                  .Log($"Zipping competed! File '{zipFileName}' with contents '{sourceDirectory}'. {{ recurse: {recurse}, fileFilter: {fileFilter}}}");
                                          });
        }

        public static async UniTask ExtractZipAsync(this FastZip fastZip, string zipFilePath,
                                                    string extractedFilesDirectoryPath,
                                                    string fileFilter = null)
        {
            await UniTask.RunOnThreadPool(() =>
                                          {
                                              Debug
                                                  .Log($"Unzipping file '{zipFilePath}' to '{extractedFilesDirectoryPath}'. {{ fileFilter: {fileFilter}}}...");
                                              fastZip.ExtractZip(zipFilePath,
                                                                 extractedFilesDirectoryPath,
                                                                 fileFilter);
                                              Debug
                                                  .Log($"Unzipped completed! File '{zipFilePath}' to '{extractedFilesDirectoryPath}'. {{ fileFilter: {fileFilter}}}...");
                                          });
        }
    }
}