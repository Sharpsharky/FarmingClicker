namespace Core.Audio.AudioManager.Library
{
    using System;
    using System.Collections.Generic;
    using global::Utils;
    using Sirenix.OdinInspector;
    using UnityEngine;

    [Serializable]
    public sealed class AudioClipEntry
    {
        #region Inspector

        [field: SerializeField] public AudioGroup TargetGroup { get; set; }
        [field: SerializeField, TableList] public List<AudioClipData> AudioClips { get; set; }

        #endregion
    }

    [Serializable]
    public sealed class AudioClipData
    {
        #region Inspector

        [field: SerializeField, TableColumnWidth(90)]
        public AudioClip Clip { get; set; }

        #endregion

#if UNITY_EDITOR
        [Button, LabelText("Play")]
        private void Play()
        {
            if(Clip != null)
            {
                AudioPreview.PlayClip(Clip);
            }
        }

        [Button, LabelText("Stop")]
        private void Stop()
        {
            AudioPreview.StopAllClips();
        }
#endif
    }
}