namespace Core.Audio.AudioManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using _3rd_Party.Demigiant.DOTween.Modules;
    using DG.Tweening;
    using UnityEngine;

    public static class AudioManagerExtensions
    {
        private const float MIXER_LOG_VOLUME_FACTOR = 20f;
        private const float NORMALIZED_VOLUME_LOWER_BOUND = .00001f;
        private const float NORMALIZED_VOLUME_UPPER_BOUND = 1f;

        public static AudioSource ResolveFreeAudioSource(this List<AudioGroupEntry> entries,
                                                         AudioGroup targetGroup)
        {
            var targetEntry = GetGroupEntryByGroup(entries, targetGroup);

            if(targetEntry == null)
            {
                Debug.LogError($"Cannot find matching AudioGroupEntry for group: {targetGroup}");
                return null;
            }

            if(targetEntry.AudioSources == null || targetEntry.AudioSources.Count == 0)
            {
                Debug.LogError($"No AudioSources available for group: {targetGroup}");
                return null;
            }

            // first try to get a free slot
            foreach(var audioSource in targetEntry.AudioSources.Where(audioSource =>
                !audioSource.isPlaying))
            {
                return audioSource;
            }

            // if there is no one free, take the one that plays the sound for the longest time
            return targetEntry.AudioSources.OrderByDescending(x => x.time).First();
        }

        public static AudioSource ResolvePlayingAudioSource(this List<AudioGroupEntry> entries,
                                                            AudioGroup targetGroup)
        {
            var targetEntry = GetGroupEntryByGroup(entries, targetGroup);

            if(targetEntry == null)
            {
                Debug.LogError($"Cannot find matching AudioGroupEntry for group: {targetGroup}");
                return null;
            }

            foreach(var audioSource in targetEntry.AudioSources.Where(audioSource =>
                audioSource.isPlaying))
            {
                return audioSource;
            }

            Debug.Log($"No free AudioSource to pool for: {targetGroup}");
            return null;
        }

        public static AudioGroupEntry GetGroupEntryByGroup(this List<AudioGroupEntry> entries,
                                                           AudioGroup audioGroup)
        {
            return entries.FirstOrDefault(groupEntry => groupEntry.TargetGroup.Equals(audioGroup));
        }

        #region Playing

        public static AudioSource GetSourcePlayingClip(this List<AudioGroupEntry> entries,
                                                       AudioClip clip, AudioGroup audioGroup)
        {
            var targetGroupEntry = GetGroupEntryByGroup(entries, audioGroup);

            if(targetGroupEntry == null)
            {
                Debug.LogError($"Cannot find AudioEntryGroup for group: {audioGroup}. Cannot return AudioSource.");
                return null;
            }

            if(clip == null)
            {
                Debug.LogError("Cannot return AudioSource: clip is null. ");
                return null;
            }

            return targetGroupEntry.AudioSources.FirstOrDefault(source => source.clip == clip);
        }

        public static bool IsPlayingGroup(this List<AudioGroupEntry> entries,
                                          AudioGroup audioGroup = AudioGroup.Master)
        {
            var source = ResolvePlayingAudioSource(entries, audioGroup);
            // get source for audio group - any playing
            return source != null && source.isPlaying;
        }

        public static bool IsPlayingClip(this List<AudioGroupEntry> entries, AudioGroup audioGroup,
                                         AudioClip clip)
        {
            var source = GetSourcePlayingClip(entries, clip, audioGroup);
            return source != null && source.isPlaying;
        }

        public static void StopPlayingGroup(this List<AudioGroupEntry> entries,
                                            AudioGroup targetGroup, float fadeOutTime = -1f)
        {
            // we want to stop playing source on given group playing given clip:
            var entry = GetGroupEntryByGroup(entries, targetGroup);
            foreach(var source in entry.AudioSources)
            {
                if(fadeOutTime > 0)
                {
                    source.FadeOut(fadeOutTime);
                }
                else
                {
                    source.clip = null;
                    source.Stop();
                }
            }
        }

        public static void StopPlayingClip(this List<AudioGroupEntry> entries, AudioClip clip,
                                           AudioGroup targetGroup, float fadeOutTime = -1f)
        {
            // we want to stop playing source on given group playing given clip:
            var source = entries.GetSourcePlayingClip(clip, targetGroup);
            // no AudioSource playing the clip, get out of here
            if(source == null)
            {
                return;
            }

            if(fadeOutTime > 0)
            {
                source.FadeOut(fadeOutTime);
            }
            else
            {
                source.clip = null;
                source.Stop();
            }
        }

        public static void StopPlayingClip(this List<AudioGroupEntry> entries, AudioClip clip,
                                           float fadeOutTime = -1f)
        {
            var groups = Enum.GetValues(typeof(AudioGroup)) as AudioGroup[];
            foreach(var audioGroup in groups)
            {
                entries.StopPlayingClip(clip, audioGroup, fadeOutTime);
            }
        }

        public static void PauseMusicStopOthers(this List<AudioGroupEntry> entries)
        {
            foreach(var entry in entries )
            {
                if (entry.TargetGroup == AudioGroup.Music || entry.TargetGroup == AudioGroup.NavigationMusic)
                {
                    foreach(var source in entry.AudioSources)
                    {
                        Debug.Log($"Source pause: {source}");

                        source.Pause();
                    }
                }
                else
                {
                    foreach(var source in entry.AudioSources)
                    {
                        source.Stop();
                    }   
                }
            }
        }
        
        public static void UnpauseMusic(this List<AudioGroupEntry> entries)
        {
            foreach(var entry in entries )
            {
                if (entry.TargetGroup == AudioGroup.Music || entry.TargetGroup == AudioGroup.NavigationMusic)
                {
                    foreach(var source in entry.AudioSources)
                    {
                        Debug.Log($"Source unpause: {source}");
                        source.UnPause();
                    }
                }
            }
        }
        
        #endregion

        #region Volume

        /// <summary>
        ///     Set volume of specified audio group.
        /// </summary>
        /// <param name="audioManager"></param>
        /// <param name="normalizedVolume">Normalized volume value from 0f to 1f.</param>
        /// <param name="audioGroup">Optional parameter describing target audio group.</param>
        public static void SetVolume(this AudioManager audioManager, float normalizedVolume,
                                     AudioGroup audioGroup = AudioGroup.Master)
        {
            var groupEntry =
                audioManager.AudioGroupEntries.FirstOrDefault(entry =>
                                                                  entry.TargetGroup
                                                                      .Equals(audioGroup));
            if(groupEntry == null)
            {
                Debug.LogError("Cannot find group entry for specified audio group!");
                return;
            }

            normalizedVolume = normalizedVolume <= 0f ? NORMALIZED_VOLUME_LOWER_BOUND :
                               normalizedVolume > 1f ? NORMALIZED_VOLUME_UPPER_BOUND :
                               normalizedVolume;

            audioManager.Mixer.SetFloat(groupEntry.GroupVolumeParam,
                                        Mathf.Log10(normalizedVolume) * MIXER_LOG_VOLUME_FACTOR);
        }

        public static float GetVolume(this AudioManager audioManager, AudioGroup audioGroup)
        {
            var groupEntry =
                audioManager.AudioGroupEntries.FirstOrDefault(entry =>
                                                                  entry.TargetGroup
                                                                      .Equals(audioGroup));
            if(groupEntry == null)
            {
                Debug.LogError("Cannot find group entry for specified audio group!");
                return -1f;
            }

            audioManager.Mixer.GetFloat(groupEntry.GroupVolumeParam, out float resultVolume);
            return resultVolume;
        }

        private static void FadeOut(this AudioSource source, float duration)
        {
            source.DOFade(0f, duration).OnComplete(() =>
                                                   {
                                                       source.Stop();
                                                       source.clip = null;
                                                       source.volume = 1f;
                                                   }
                                                  );
        }

        #endregion
    }
}