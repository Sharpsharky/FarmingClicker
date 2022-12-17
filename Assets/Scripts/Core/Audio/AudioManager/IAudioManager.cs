namespace Core.Audio.AudioManager
{
    using UnityEngine;

    public interface IAudioManager
    {
        void PlayClip(AudioClip clip, AudioGroup audioGroup, bool playOneShot = false,
                      bool loop = false, float delay = 0, bool startDetectionOnThisGroup = false
                      , bool fadeIn = false, float targetClipVolume = .75f);

        void StopPlaying(AudioGroup targetGroup, AudioClip clip = null,
                         bool stopDetectionOnThisGroup = false,
                         bool fadeOut = false);

        void TogglePause(bool shouldPause, AudioGroup targetGroup, AudioClip clip = null,
                         bool stopDetectionOnThisGroup = false,
                         bool fadeOut = false);

        void StopPlayingAll();
        void SetVolume(float normalizedVolume, AudioGroup audioGroup = AudioGroup.Master);
        bool IsPlayingGroup(AudioGroup audioGroup = AudioGroup.Master);

        AudioSource GetSourcePlayingClip(AudioGroup group, AudioClip clip);
        bool IsPlayingClip(AudioGroup audioGroup, AudioClip clip);

        float GetVolume(AudioGroup audioGroup);
    }
}