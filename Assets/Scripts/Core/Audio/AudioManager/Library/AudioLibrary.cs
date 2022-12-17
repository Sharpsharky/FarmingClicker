namespace Core.Audio.AudioManager.Library
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Sirenix.OdinInspector;
    using UnityEngine;

    [CreateAssetMenu(menuName = "FarmingClicker/Audio/AudioLibraryData",
                     fileName = "AudioLibraryData")]
    public class AudioLibrary : ScriptableObject
    {
        [field: SerializeField] public List<AudioClipEntry> AudioClipEntries { get; private set; }

        public AudioClip GetAudioClip(AudioClipInfo clipInfo)
        {
            var entry = AudioClipEntries.Find(x => x.TargetGroup == clipInfo.TargetGroup);
            if(entry == null)
            {
                Debug.LogError($"Couldn't find AudioClipEntry for group: {clipInfo.TargetGroup}");
                return null;
            }

            return entry.AudioClips.Find(x => x.Clip.name == clipInfo.ClipName).Clip;
        }


        public AudioClipInfo GetClipByName(string clipName, AudioGroup targetGroup)
        {
            var entry = AudioClipEntries.Find(x => x.TargetGroup == targetGroup);
            
            if(entry == null)
            {
                Debug.Log($"Couldn't find AudioClipEntry for group {targetGroup}");
                return null;
            }

            var type = Type.GetType($"AudioLibraryData.{targetGroup}");

            if(type == null)
            {
                Debug.Log($"Couldn't find group {targetGroup}");
                return null;
            }
            var field = type.GetField(clipName);

            if(field == null)
            {
                Debug.Log($"Couldn't find entry {clipName} in group {targetGroup}");
                return null;
            }
            
            var value = field.GetValue(null);

            if(value == null)
            {
                Debug.Log($"Couldn't find correct data in entry {clipName} in group {targetGroup}");
                return null;
            }
            
            var audioClipInfo = (AudioClipInfo)value;
            return audioClipInfo;
        }

#if UNITY_EDITOR

        [Button]
        private void RegenerateAudioClipEntries()
        {
            var values = Enum.GetValues(typeof(AudioGroup)) as AudioGroup[];
            foreach(var t in values)
            {
                var entry = AudioClipEntries.Find(x => x.TargetGroup == t);
                if(entry == null)
                {
                    AudioClipEntries.Add(new AudioClipEntry
                                         {TargetGroup = t, AudioClips = new List<AudioClipData>()});
                }
            }
        }

        [Button]
        private void RegenerateLibraryClass()
        {
            AudioLibraryClassGenerator.Generate(AudioClipEntries);
        }
#endif
    }
}