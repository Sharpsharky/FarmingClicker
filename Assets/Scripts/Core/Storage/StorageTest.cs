namespace Core.Storage
{
    using System.Net;
    using Audio.AudioManager;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Video;

    public class StorageTest : MonoBehaviour
    {
        [SerializeField] private VideoPlayer videoPlayer;
        [SerializeField] private Image image;
        [SerializeField] private RawImage rawImage;

        [Button]
        private async void GetVideo()
        {
            //StorageManager.ClearMediaPath(StorageFileType.Video);
            string testLink =
                "https://sample-videos.com/video123/mp4/720/big_buck_bunny_720p_1mb.mp4";
            string path = await StorageManager.GetVideoFile(testLink, DownloadProgressChangedB);
            videoPlayer.url = path;
            videoPlayer.Play();
        }

        [Button]
        private async void GetAudio()
        {
            string testLink =
                "https://file-examples-com.github.io/uploads/2017/11/file_example_MP3_700KB.mp3";
            var clip = await StorageManager.GetAudioClip(testLink, DownloadProgressChangedB);
            AudioManager.PlayClip(clip, AudioGroup.Effects);
        }


        [Button]
        private async void GetTexture()
        {
            string testLink = "https://live.staticflickr.com/3247/3042628421_cd4ce469e0_o_d.jpg";
            var texture = await StorageManager.GetTexture(testLink, DownloadProgressChangedB);
            image.overrideSprite =
                Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                              new Vector2());
            rawImage.texture = texture;
        }


        private void DownloadProgressChangedB(object sender,
                                              DownloadProgressChangedEventArgs progress)
        {
            Debug.Log(progress.ProgressPercentage);
        }

        private void DownloadProgressChanged(float percent)
        {
            Debug.Log(percent);
        }
    }
}