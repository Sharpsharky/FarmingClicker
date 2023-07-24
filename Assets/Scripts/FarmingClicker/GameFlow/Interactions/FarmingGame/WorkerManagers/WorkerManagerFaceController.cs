using System.Collections.Generic;
using Core.Message;
using FarmingClicker.GameFlow.Interactions.FarmingGame.GlobalData;
using FarmingClicker.GameFlow.Messages.Notifications.FarmingGame.Managers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.WorkerManagers
{
    public class WorkerManagerFaceController : SerializedMonoBehaviour
    {
        [SerializeField, BoxGroup("Components")] private Image iconOfBoostImage;
        [SerializeField, BoxGroup("Components")] private Image faceImage;
        [SerializeField, BoxGroup("Components")] private Image flagImage;
        [SerializeField, BoxGroup("Components")] private GameObject raysEffect;
        [SerializeField, BoxGroup("Components")] private int startingLevelOfRays = 3;
        [SerializeField, BoxGroup("Components")] private Button faceButton;
        
        [SerializeField, BoxGroup("Settings")] private List<Color> colorsOfManagersIcons = new List<Color>();
        [SerializeField, BoxGroup("Settings")] private List<Sprite> spritesForManagersIconBackgrounds = new List<Sprite>();        
        [SerializeField, BoxGroup("Settings")] private Sprite noFaceSprite;        

        private WorkerManagerStatistics workerManagerStatistics;

        public Button FaceButton => faceButton;
        
        public void Initialize(WorkerManagerStatistics workerManagerStatistics)
        {
            raysEffect.SetActive(false);
            
            if (workerManagerStatistics.isEmpty)
            {
                faceImage.sprite = GlobalDataHolder.instance.noFaceManagerSprite;
                flagImage.gameObject.SetActive(false);
                return;
            }
            
            this.workerManagerStatistics = workerManagerStatistics;
            faceImage.sprite = GlobalDataHolder.instance.spritesForManagersFaces[workerManagerStatistics.FaceImageId];
            iconOfBoostImage.sprite = GlobalDataHolder.instance.GetStatisticType(workerManagerStatistics.StatisticsType).StatisticIcon;
            iconOfBoostImage.color = colorsOfManagersIcons[workerManagerStatistics.LevelOfManager];
            flagImage.sprite = spritesForManagersIconBackgrounds[workerManagerStatistics.LevelOfManager];
            
            flagImage.gameObject.SetActive(true);
            
            if(workerManagerStatistics.LevelOfManager >= startingLevelOfRays) raysEffect.SetActive(true);
            
            gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            faceButton.onClick.RemoveAllListeners();
        }
    }
}