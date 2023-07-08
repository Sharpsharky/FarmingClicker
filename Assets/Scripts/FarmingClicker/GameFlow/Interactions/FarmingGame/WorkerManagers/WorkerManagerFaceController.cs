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
        [SerializeField] private Image iconOfBoostImage;
        [SerializeField] private Image faceImage;
        [SerializeField] private Image flagImage;
        [SerializeField] private GameObject raysEffect;
        [SerializeField] private int startingLevelOfRays = 3;

        [SerializeField] private Button faceButton;
        
        private WorkerManagerStatistics workerManagerStatistics;

        public Button FaceButton => faceButton;
        
        public void Initialize(WorkerManagerStatistics workerManagerStatistics)
        {
            this.workerManagerStatistics = workerManagerStatistics;
            raysEffect.SetActive(false);
            
            faceImage.sprite = GlobalDataHolder.instance.spritesForManagersFaces[workerManagerStatistics.FaceImageId];
            iconOfBoostImage.sprite = GlobalDataHolder.instance.GetStatisticType(workerManagerStatistics.StatisticsType).StatisticIcon;
            iconOfBoostImage.color = GlobalDataHolder.instance.colorsOfManagersIcons[workerManagerStatistics.LevelOfManager];
            flagImage.sprite = GlobalDataHolder.instance.spritesForManagersIconBackgrounds[workerManagerStatistics.LevelOfManager];
            
            if(workerManagerStatistics.LevelOfManager >= startingLevelOfRays) raysEffect.SetActive(true);
            
            gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            faceButton.onClick.RemoveAllListeners();
        }
    }
}