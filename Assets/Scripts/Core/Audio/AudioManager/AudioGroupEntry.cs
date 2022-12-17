namespace Core.Audio.AudioManager
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Rendering.UI;

    [Serializable]
    public sealed class AudioGroupEntry
    {
        #region Inspector
        

        [field: SerializeField] public AudioGroup TargetGroup { get; set; }
        [field: SerializeField] public List<AudioSource> AudioSources { get; set; }
        [field: SerializeField] public string GroupVolumeParam { get; set; }

        #endregion
    }
}