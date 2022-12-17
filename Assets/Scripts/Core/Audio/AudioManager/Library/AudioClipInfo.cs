namespace Core.Audio.AudioManager.Library
{
    using System;
    using UnityEngine;

    [Serializable]
    public class AudioClipInfo
    {
        [field: SerializeField] public AudioGroup TargetGroup { get; set; }
        [field: SerializeField] public string ClipName { get; set; }
    }
}