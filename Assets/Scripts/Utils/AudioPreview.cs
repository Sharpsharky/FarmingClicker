namespace Utils
{
    using System;
    using UnityEngine;
#if UNITY_EDITOR
    using System.Reflection;
    using UnityEditor;

#endif

    public static class AudioPreview
    {
        public static void PlayClip(AudioClip clip, int startSample = 0, bool loop = false)
        {
#if UNITY_EDITOR
            var unityEditorAssembly = typeof(AudioImporter).Assembly;

            var audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
            var method = audioUtilClass.GetMethod(
                                                  "PlayPreviewClip",
                                                  BindingFlags.Static | BindingFlags.Public,
                                                  null,
                                                  new[]
                                                  {
                                                      typeof(AudioClip), typeof(int),
                                                      typeof(bool),
                                                  },
                                                  null
                                                 );

            method?.Invoke(null, new object[] {clip, startSample, loop});
#endif
        }

        public static void StopAllClips()
        {
#if UNITY_EDITOR
            var unityEditorAssembly = typeof(AudioImporter).Assembly;

            var audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
            var method = audioUtilClass.GetMethod(
                                                  "StopAllPreviewClips",
                                                  BindingFlags.Static | BindingFlags.Public,
                                                  null,
                                                  new Type[] { },
                                                  null
                                                 );

            method?.Invoke(null, new object[] { });
#endif
        }
    }
}