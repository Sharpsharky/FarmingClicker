namespace Core.Audio.AudioManager.UI
{
    using System.Collections;
    using System.Linq;
    using global::Utils;
    using Library;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using UnityEngine.UI;

    public class AudioUITrigger : MonoBehaviour
    {
        [SerializeField] private bool active;

        [SerializeField, ShowIf("@audioLibrary != null")]
        private Button button;

        [SerializeField, ShowIf("@audioLibrary != null")]
        private AudioGroup audioGroupTarget;

        [SerializeField, ValueDropdown("UIAudioClipData"), ShowIf("@audioLibrary != null")]
        private AudioClipData clipData;

        private AudioLibrary audioLibrary;

        private void Awake()
        {
            if(button == null)
            {
                return;
            }

            button.onClick.AddListener(OnClickHandler);
        }

        private void OnClickHandler()
        {
            if(!active)
            {
                return;
            }

            AudioManager.PlayClip(clipData.Clip, AudioGroup.UI);
        }

#if UNITY_EDITOR

        [Button, ShowIf("@audioLibrary == null")]
        private void Setup()
        {
            audioLibrary = AssetsUtils.FindFirstAsset<AudioLibrary>();
            button = GetComponent<Button>();
        }

        private IEnumerable UIAudioClipData()
        {
            if(audioLibrary == null)
            {
                return default;
            }

            var clips = audioLibrary.AudioClipEntries.Find(x => x.TargetGroup == audioGroupTarget);
            return clips == null
                       ? default(IEnumerable)
                       : clips.AudioClips.Select(x => new ValueDropdownItem(x.Clip.name, x));
        }
#endif
    }
}