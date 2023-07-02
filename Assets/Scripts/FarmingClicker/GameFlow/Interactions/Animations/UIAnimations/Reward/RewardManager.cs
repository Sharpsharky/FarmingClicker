using System;
using System.Collections.Generic;
using Core.Message;
using Core.Message.Interfaces;
using DG.Tweening;
using FarmingClicker.GameFlow.Messages.Commands.Currency;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FarmingClicker.GameFlow.Interactions.Animations.UIAnimations.Reward
{
    public class RewardManager : SerializedMonoBehaviour, IMessageReceiver
    {
        [SerializeField] private Transform parentOfCoins;
        [SerializeField] private Transform targetOfCoins;
        public List<Type> ListenedTypes { get; } = new List<Type>();


        public void Initialize()
        {
            ListenedTypes.Add(typeof(GiveRewardCoinsAnimationCommand));
            MessageDispatcher.Instance.RegisterReceiver(this);   
        }

        private void Start()
        {
            //AnimateCoins();
        }

        public void AnimateCoins()
        {
            SetInitialPosition();
            parentOfCoins.gameObject.SetActive(true);
            float delay = 0f;
            parentOfCoins.gameObject.SetActive(true);

            for (int i = 0; i < parentOfCoins.childCount; i++)
            {
                //https://www.youtube.com/watch?v=TRUOqUGAfLM&t=392s
                parentOfCoins.GetChild(i).DOScale(1f, 0.3f).SetDelay(0f).SetEase(Ease.OutBack);
                parentOfCoins.GetComponent<RectTransform>().DOAnchorPos(new Vector2(444f, 800f), 1f).SetDelay(delay)
                    .SetEase(Ease.OutBack);
                delay += 0.2f;
            }
            
        }

        private void SetInitialPosition()
        {
            for (int i = 0; i < parentOfCoins.childCount; i++)
            {
                float randomXPos = Random.Range(-5, 5);
                float randomYPos = Random.Range(-5, 5);
                
                float randomXRot = Random.Range(0, 360);
                float randomYRot = Random.Range(0, 360);
                
                parentOfCoins.GetChild(i).position = new Vector2(randomXPos,randomYPos);
                parentOfCoins.GetChild(i).eulerAngles = new Vector2(randomXRot,randomYRot);
            }
        }


        public void OnMessageReceived(object message)
        {
            if (!ListenedTypes.Contains(message.GetType())) return;

            Debug.Log($"Navigation manager received load scene command.");

            switch (message)
            {
                case GiveRewardCoinsAnimationCommand giveRewardCoinsAnimationCommand:
                {

                    break;
                }
                
            }        
        }
    }
}