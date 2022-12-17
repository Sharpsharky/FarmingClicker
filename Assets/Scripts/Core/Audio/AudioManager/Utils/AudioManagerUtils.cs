namespace Core.Audio.AudioManager.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Audio;
    using Object = UnityEngine.Object;

    public static class AudioManagerUtils
    {
        private const string MASTER_GROUP = "Master";
        private const string EXPOSED_PARAMETERS = "exposedParameters";

        public static List<AudioGroupEntry> Setup(Transform transform, AudioMixer mixer,
                                                  int sourcesCount)
        {
            var audioGroupEntries = new List<AudioGroupEntry>();

            for(int i = transform.childCount - 1; i >= 0; i--)
            {
                if(Application.isPlaying)
                {
                    Object.Destroy(transform.GetChild(i).gameObject);
                }
                else
                {
                    Object.DestroyImmediate(transform.GetChild(i).gameObject);
                }
            }

            // Create new list of holders
            var groups = mixer.FindMatchingGroups(MASTER_GROUP);
            var audioGroups = Enum.GetValues(typeof(AudioGroup)) as AudioGroup[];

            foreach(var item in audioGroups)
            {
                var group =
                    groups.FirstOrDefault(x => x.name.ToLower() == item.ToString().ToLower());
                var entry = CreateGroup(transform, group, item, sourcesCount);
                entry.GroupVolumeParam = GetExposedParam(mixer, item);
                entry.TargetGroup = item;
                audioGroupEntries.Add(entry);
            }

            return audioGroupEntries;
        }

        private static string GetExposedParam(AudioMixer mixer, AudioGroup audioGroup)
        {
            var parameters = (Array) mixer.GetType().GetProperty(EXPOSED_PARAMETERS)
                                          ?.GetValue(mixer, null);

            if(parameters != null)
            {
                for(int i = 0; i < parameters.Length; i++)
                {
                    var o = parameters.GetValue(i);
                    string param = (string) o.GetType().GetField("name").GetValue(o);
                    if(param.ToLower().Contains(audioGroup.ToString().ToLower()))
                    {
                        return param;
                    }
                }
            }
            else
            {
                Debug.LogError("There are no exposed parameters on AudioMixer");
            }

            return string.Empty;
        }

        private static AudioGroupEntry CreateGroup(Transform parent, AudioMixerGroup mixerGroup,
                                                   AudioGroup audioGroup, int numberOfAudioSources)
        {
            var audioGroupEntry = new AudioGroupEntry {AudioSources = new List<AudioSource>()};

            var audioSourceHolder = new GameObject {name = $"AudioHolder{audioGroup.ToString()}"};
            audioSourceHolder.transform.parent = parent;
            audioSourceHolder.transform.position = Vector3.zero;
            audioSourceHolder.transform.rotation = Quaternion.identity;

            for(int i = 0; i < numberOfAudioSources; i++)
            {
                var source = audioSourceHolder.AddComponent<AudioSource>();
                source.outputAudioMixerGroup = mixerGroup;
                source.playOnAwake = false;
                audioGroupEntry.AudioSources.Add(source);
            }

            return audioGroupEntry;
        }
    }
}