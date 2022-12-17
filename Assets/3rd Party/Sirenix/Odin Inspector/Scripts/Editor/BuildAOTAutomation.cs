//-----------------------------------------------------------------------
// <copyright file="BuildAOTAutomation.cs" company="Sirenix IVS">
// Copyright (c) Sirenix IVS. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

#if UNITY_EDITOR && UNITY_5_6_OR_NEWER

namespace _3rd_Party.Sirenix.Odin_Inspector.Scripts.Editor
{
    using System.IO;
    using global::Sirenix.Serialization;
    using UnityEditor;
    using UnityEditor.Build;
    using UnityEditor.Build.Reporting;
#if UNITY_2018_1_OR_NEWER

#endif

#if UNITY_2018_1_OR_NEWER
    public class PreBuildAOTAutomation : IPreprocessBuildWithReport
#else
    public class PreBuildAOTAutomation : IPreprocessBuild
#endif
    {
        public int callbackOrder => -1000;

        public void OnPreprocessBuild(BuildTarget target, string path)
        {
            if(AOTGenerationConfig.Instance.ShouldAutomationGeneration(target))
            {
                AOTGenerationConfig.Instance.ScanProject();
                AOTGenerationConfig.Instance.GenerateDLL();
            }
        }

#if UNITY_2018_1_OR_NEWER

        public void OnPreprocessBuild(BuildReport report)
        {
            OnPreprocessBuild(report.summary.platform, report.summary.outputPath);
        }

#endif
    }

#if UNITY_2018_1_OR_NEWER
    public class PostBuildAOTAutomation : IPostprocessBuildWithReport
#else
    public class PostBuildAOTAutomation : IPostprocessBuild
#endif
    {
        public int callbackOrder => -1000;

        public void OnPostprocessBuild(BuildTarget target, string path)
        {
            if(AOTGenerationConfig.Instance.DeleteDllAfterBuilds &&
               AOTGenerationConfig.Instance.ShouldAutomationGeneration(target))
            {
                Directory.Delete(AOTGenerationConfig.Instance.AOTFolderPath, true);
                File.Delete(AOTGenerationConfig.Instance.AOTFolderPath.TrimEnd('/', '\\') +
                            ".meta");
                AssetDatabase.Refresh();
            }
        }

#if UNITY_2018_1_OR_NEWER

        public void OnPostprocessBuild(BuildReport report)
        {
            OnPostprocessBuild(report.summary.platform, report.summary.outputPath);
        }

#endif
    }
}

#endif // UNITY_EDITOR && UNITY_5_6_OR_NEWER