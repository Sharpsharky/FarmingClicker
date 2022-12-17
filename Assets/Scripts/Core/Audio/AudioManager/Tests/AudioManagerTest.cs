namespace Core.Audio.AudioManager.Tests
{
    using System.IO;
    using AudioLibraryData;
    using DG.Tweening;
    using Library;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public class AudioManagerTest : MonoBehaviour
    {
        [SerializeField] private string filePath;

        private void Start()
        {
            AudioManager.PlayClip(Music.MainTheme, false, true);
        }

        [Button]
        private void PlayFile(string relativePath)
        {
            string path = Path.Combine(Application.streamingAssetsPath, filePath);
            AudioManager.PlayFile(path);
        }

        [Button]
        private void PlayMusic()
        {
            AudioManager.PlayClip(Music.BackgroundMusicFinal);
        }

        [Button]
        private void StopMusicGroup()
        {
            AudioManager.StopPlayingGroup(AudioGroup.Music);
        }

        [Button]
        private void StopMusicClip()
        {
            AudioManager.StopPlayingClip(Music.BackgroundMusicFinal);
        }

        [Button]
        private void PlayMultiple()
        {
            DOVirtual.DelayedCall(0.1f, () => { PlayClip(Effects.RecordingStart); });
            DOVirtual.DelayedCall(0.15f, () => { PlayClip(Effects.ChangingTextNotification); });
            DOVirtual.DelayedCall(0.2f, () => { PlayClip(Effects.MenuButton); });
            DOVirtual.DelayedCall(0.25f, () => { PlayClip(Effects.PlopButton1); });
        }

        [Button]
        private void StopAll()
        {
            AudioManager.StopPlayingAll();
        }

        private void PlayClip(AudioClipInfo clipInfo)
        {
            AudioManager.PlayClip(clipInfo);
        }
    }
}