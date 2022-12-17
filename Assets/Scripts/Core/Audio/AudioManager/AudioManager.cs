namespace Core.Audio.AudioManager
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using _3rd_Party.Demigiant.DOTween.Modules;
    using Cysharp.Threading.Tasks;
    using DG.Tweening;
    using global::Utils;
    using Library;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using UnityEngine.Audio;
    using UnityEngine.Networking;
    using Utils;
    using Random = UnityEngine.Random;

    // ReSharper disable UnusedAutoPropertyAccessor.Local

    /// <summary>
    ///     Dealing with playing/stopping clips on given channel/group, volume change etc.
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        private const float DEFAULT_AUDIO_LOUDNESS = 0.075f;
        #region Unity Methods

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else if(Instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        #endregion

#if UNITY_EDITOR

        #region EditorHelpers

        [Button]
        private void RebuildAudioGroupEntries()
        {
            AudioGroupEntries = AudioManagerUtils.Setup(transform, Mixer, poolAudioSourcesCount);
        }

        #endregion

#endif

        #region Inspector

        [BoxGroup("Settings"), SerializeField] 
        private float defaultFadeInTime = 5f;

        [BoxGroup("Settings"), SerializeField] 
        private int poolAudioSourcesCount = 5;

        [field: SerializeField] public AudioMixer Mixer { get; private set; }

        [field: SerializeField, InlineEditor]
        public AudioLibrary AudioLibraryHolder { get; private set; }

        #endregion

        #region Public Variables

        private static AudioManager Instance
        {
            get
            {
                if(Application.isPlaying)
                {
                    return instance;
                }

                Debug.LogError("Can't use AudioManager in EditorMode");
                return null;
            }
            set => instance = value;
        }

        private static AudioManager instance;

        [field: SerializeField, ReadOnly]
        public List<AudioGroupEntry> AudioGroupEntries { get; private set; }


        [SerializeField] private  AudioSource LecturesSource;
        #endregion

        #region Public Methods

        public static async void PlayFromURL(string url)
        {
            var clip = await FilesHelper.GetAudioClipFromURL(url);
            PlayClip(clip, AudioGroup.Effects);
        }
        
        public static async Task<float> GetAudioLengthFromURL(string url)
        {
            var clip = await FilesHelper.GetAudioClipFromURL(url);
            return clip.length;
        }
        
        public static async Task<float> PlayLectureFromURL(string url)
        {
            var clip = await FilesHelper.GetAudioClipFromURL(url);
            Debug.Log(clip);
            return PlayLecture(clip).Result;
        }

        public static void PauseMusicStopOthers()
        {
            Instance.AudioGroupEntries.PauseMusicStopOthers();
        }
        
        public static void UnpauseMusic()
        {
            Instance.AudioGroupEntries.UnpauseMusic();
        }
        
        public static async Task<float> PlayLecture(AudioClip lectureClip, float targetClipVolume = .8f)
        {
            Instance.LecturesSource.clip = lectureClip;
            Instance.LecturesSource.volume = targetClipVolume;
            Instance.LecturesSource.Play();
            return lectureClip.length;
        }
        
        public static void PlayClip(AudioClip clip, AudioGroup targetGroup,
                                    bool playOneShot = false, bool loop = false,
                                    float delay = 0, bool fadeIn = false,
                                    float targetClipVolume = .8f)
        {
            if(Instance == null)
            {
                return;
            }

            var source = Instance.AudioGroupEntries.ResolveFreeAudioSource(targetGroup);
            if(source == null)
            {
                Debug.Log($"Cannot find AudioSource for specified audio group: {targetGroup} ");
                return;
            }

            source.clip = clip;
            source.volume = targetClipVolume;
            source.loop = loop;

            if(playOneShot)
            {
                source.PlayOneShot(clip);
                return;
            }

            if(delay > 0f)
            {
                source.PlayDelayed(delay);
            }
            else
            {
                if(fadeIn)
                {
                    source.volume = 0f;
                    source.Play();
                    source.DOFade(targetClipVolume, Instance.defaultFadeInTime);
                }
                else
                {
                    source.Play();
                }
            }
        }

        public static void PlayRandomAudioFromGroup(Type audioClass, bool playOneShot = false, bool loop = false, float delay = 0, bool fadeIn = false, float targetClipVolume = DEFAULT_AUDIO_LOUDNESS)
        {
            var fields = audioClass.GetFields();
            
            int rnd = Random.Range(0, fields.Length);
            var clipInfo = (AudioClipInfo)fields[rnd].GetValue(null);
            
            PlayClip(clipInfo, playOneShot, loop, delay, fadeIn, targetClipVolume);
        }

        public static void PlayClip(AudioClipInfo clipInfo, bool playOneShot = false,
                                    bool loop = false,
                                    float delay = 0, bool fadeIn = false,
                                    float targetClipVolume = .8f)
        {
            if(Instance == null)
            {
                return;
            }

            var clip = Instance.AudioLibraryHolder.GetAudioClip(clipInfo);
            PlayClip(clip, clipInfo.TargetGroup, playOneShot, loop, delay, fadeIn,
                     targetClipVolume);
        }

        public static void PlayClipByName(string clipName, AudioGroup targetGroup, bool playOneShot = false,
                                    bool loop = false,
                                    float delay = 0, bool fadeIn = false,
                                    float targetClipVolume = .8f)
        {
            if(Instance == null)
            {
                return;
            }

            var clip = Instance.AudioLibraryHolder.GetClipByName(clipName, targetGroup);
            if(clip == null)
            {
                return;
            }
            PlayClip(clip, playOneShot, loop, delay, fadeIn, targetClipVolume);
        }

        public static async void StopLecture()
        {
            Instance.LecturesSource.Stop();
        }
        
        
        public static async void PlayFile(string filePath,
                                          AudioType audioType = AudioType.OGGVORBIS,
                                          AudioGroup targetGroup = AudioGroup.Effects,
                                          bool playOneShot = false, bool loop = false,
                                          float delay = 0, bool fadeIn = false,
                                          float targetClipVolume = .8f)
        {
            if(Instance == null)
            {
                return;
            }

            string path = Application.isMobilePlatform
                              ? filePath
                              : Path.Combine("file://", filePath);
            using var loader = UnityWebRequestMultimedia.GetAudioClip(path, audioType);

            try
            {
                Debug.Log($"Playing local file from url: {path}");
                await loader.SendWebRequest();
                PlayClip(DownloadHandlerAudioClip.GetContent(loader), AudioGroup.Effects);
            }
            catch(Exception e)
            {
                Debug.LogError($"Error loading AudioClip '{loader.uri}': {loader.error}, exception: {e.Message}");
                throw;
            }
        }

        public static void StopPlayingAll()
        {
            Debug.Log("StopPlayingAll");
            if(Instance == null)
            {
                return;
            }

            foreach(var audioGroupEntry in Instance.AudioGroupEntries)
            {
                StopPlayingGroup(audioGroupEntry.TargetGroup);
            }
        }

        public static void StopPlayingGroup(AudioGroup targetGroup, float fadeoutTime = -1)
        {
            Debug.Log($"StopPlayingGroup: {targetGroup}");

            if(Instance == null)
            {
                return;
            }

            Instance.AudioGroupEntries.StopPlayingGroup(targetGroup, fadeoutTime);
        }

        public static void StopPlayingClip(AudioClip clip, AudioGroup targetGroup,
                                           float fadeoutTime = -1)
        {
            if(Instance == null)
            {
                return;
            }

            Instance.AudioGroupEntries.StopPlayingClip(clip, targetGroup);
        }

        public static void StopPlayingClip(AudioClip clip, float fadeoutTime = -1)
        {
            if(Instance == null)
            {
                return;
            }

            Instance.AudioGroupEntries.StopPlayingClip(clip);
        }

        public static void StopPlayingClip(AudioClipInfo clipInfo, float fadeoutTime = -1)
        {
            
            Debug.Log($"StopPlayingClip: {clipInfo}");

            if(Instance == null)
            {
                return;
            }

            var clip = Instance.AudioLibraryHolder.GetAudioClip(clipInfo);
            Instance.AudioGroupEntries.StopPlayingClip(clip, clipInfo.TargetGroup);
        }

        public static void Pause(AudioClip clip, AudioGroup targetGroup)
        {
            if(Instance == null)
            {
                return;
            }

            var source = Instance.AudioGroupEntries.GetSourcePlayingClip(clip, targetGroup);
            source.Pause();
        }

        #endregion
    }
}