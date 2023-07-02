using System;
using System.Collections;
using System.Collections.Generic;
using Core.Message;
using Core.Message.Interfaces;
using DG.Tweening;
using FarmingClicker.GameFlow.Messages.Commands.Currency;
using InfiniteValue;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FarmingClicker.GameFlow.Interactions.Animations.UIAnimations.Reward
{
    public class RewardManager : SerializedMonoBehaviour, IMessageReceiver
    {
        [SerializeField, BoxGroup("Properties")] private float coinDelay = 0.2f;
        [SerializeField, BoxGroup("Properties")] private float randomInitialScatter = 50f;
        
        [SerializeField, BoxGroup("Properties First Stage")] private float coinScalingTime = 0.5f;
        [SerializeField, BoxGroup("Properties First Stage")] private float coinFinalSize = 1.1f;
        
        [SerializeField, BoxGroup("Properties Second Stage")] private float timeOfMovingToFirstPoint = 0.5f;
        [SerializeField, BoxGroup("Properties Second Stage")] private float randomFirstPointScatterX = 250f;
        [SerializeField, BoxGroup("Properties Second Stage")] private float randomFirstPointScatterY = 50f;
        
        [SerializeField, BoxGroup("Properties Third Stage")] private float timeOfMovingToFinalPoint = 0.3f;
        
        [SerializeField, BoxGroup("Transforms")] private Transform coinsCanvas;
        [SerializeField, BoxGroup("Transforms")] private Transform parentOfCoins;
        [SerializeField, BoxGroup("Transforms")] private Transform firstPathPoint;
        [SerializeField, BoxGroup("Transforms")] private Transform finalPathPoint;
        
        public List<Type> ListenedTypes { get; } = new List<Type>();


        public void Awake()
        {
            ListenedTypes.Add(typeof(GiveRewardCoinsAnimationCommand));
            MessageDispatcher.Instance.RegisterReceiver(this);   
        }

        public void AnimateCoins(InfVal rewardValue)
        {
            InfVal singleCoinVal = rewardValue / parentOfCoins.childCount;
            
            SetInitialPosition();
            StartCoroutine(GiveCoins(singleCoinVal));
            float delay = 0f;

            for (int i = 0; i < parentOfCoins.childCount; i++)
            {
                parentOfCoins.GetChild(i).DOScale(coinFinalSize, coinScalingTime).SetDelay(delay).SetEase(Ease.OutBack);
                StartMovingCoin(parentOfCoins.GetChild(i), delay);

                parentOfCoins.GetChild(i).DOScale(0, coinScalingTime).SetDelay
                    (delay+ timeOfMovingToFirstPoint + coinScalingTime + (timeOfMovingToFinalPoint/2)).SetEase(Ease.OutBack);

                delay += coinDelay;
            }
            
        }

        private void StartMovingCoin(Transform coin, float delay)
        {
            float randomX = Random.Range(-randomFirstPointScatterX, randomFirstPointScatterX);
            float randomY = Random.Range(-randomFirstPointScatterY, randomFirstPointScatterY);
            
            var firstPathPointLocalPosition = firstPathPoint.localPosition;
            Vector2 targetFirstPos =
                new Vector2(firstPathPointLocalPosition.x + randomX, firstPathPointLocalPosition.y + randomY);
            
            coin.GetComponent<RectTransform>().DOAnchorPos(targetFirstPos, timeOfMovingToFirstPoint).
                SetDelay(delay+coinScalingTime).SetEase(Ease.Flash);
            
            coin.GetComponent<RectTransform>().DOAnchorPos(finalPathPoint.localPosition, timeOfMovingToFinalPoint).
                SetDelay(delay+ timeOfMovingToFirstPoint + coinScalingTime).SetEase(Ease.OutBack);
        }
        

        private void SetInitialPosition()
        {
            for (int i = 0; i < parentOfCoins.childCount; i++)
            {
                float randomXPos = Random.Range(-randomInitialScatter, randomInitialScatter);
                float randomYPos = Random.Range(-randomInitialScatter, randomInitialScatter);
                
                float randomRot = Random.Range(0, 360);
                
                parentOfCoins.GetChild(i).localPosition = new Vector3(randomXPos,randomYPos);
                parentOfCoins.GetChild(i).eulerAngles = new Vector3(0,0,randomRot);
                
                parentOfCoins.GetChild(i).localScale = Vector3.zero;
            }
        }

        private IEnumerator GiveCoins(InfVal singleCoinVal)
        {
            float finalWaitingTime = coinDelay + timeOfMovingToFirstPoint + coinScalingTime + timeOfMovingToFinalPoint;
            
            yield return new WaitForSeconds(finalWaitingTime);

                
            for (int i = 0; i < parentOfCoins.childCount; i++)
            {
                MessageDispatcher.Instance.Send(new AddCoinValueFromRewardCommand(singleCoinVal));
                yield return new WaitForSeconds(coinDelay);
            }

            yield return null;
        }
        
        public void OnMessageReceived(object message)
        {
            if (!ListenedTypes.Contains(message.GetType())) return;

            Debug.Log($"Navigation manager received load scene command.");

            switch (message)
            {
                case GiveRewardCoinsAnimationCommand giveRewardCoinsAnimationCommand:
                {
                    coinsCanvas.gameObject.SetActive(true);
                    AnimateCoins(giveRewardCoinsAnimationCommand.Amount);
                    
                    break;
                }
                
            }        
        }
    }
}