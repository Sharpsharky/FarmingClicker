using Core.Message.Interfaces;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.WorkerManagers
{
    using Sirenix.OdinInspector;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public class WorkerManagerController : SerializedMonoBehaviour
    {
        [SerializeField] private Dictionary<ManagerAbilityCooldown, int> ManagerAbilityCooldownTimes =
            new Dictionary<ManagerAbilityCooldown, int>()
            {
                {ManagerAbilityCooldown.ACTIVE_ABILITY_COOLDOWN, 300},
                {ManagerAbilityCooldown.WAITING_FOR_ABILITY_COOLDOWN, 300}
            };

        private IEnumerator currentCountDown;
        public Action<int, int> OnSecondOfManagerCooldownLasted;

        public void InitializeNewTimer(ManagerAbilityCooldown managerAbilityCooldown)
        {
            if(currentCountDown != null) StopCoroutine(currentCountDown);

            int startingTime = ManagerAbilityCooldownTimes[managerAbilityCooldown];
            int maxTime = ManagerAbilityCooldownTimes[managerAbilityCooldown];
            
            
            currentCountDown = CountDownTimeOfCooldown(startingTime, maxTime);
            StartCoroutine(currentCountDown);
        }

        public void InitializePreExistingTimer(ManagerAbilityCooldown managerAbilityCooldown, int startingTime)
        {
            if(currentCountDown == null) StopCoroutine(currentCountDown);
            
            int maxTime = ManagerAbilityCooldownTimes[managerAbilityCooldown];
            
            currentCountDown = CountDownTimeOfCooldown(startingTime, maxTime);
            
            StartCoroutine(currentCountDown);
        }

        private IEnumerator CountDownTimeOfCooldown(int startingTime, int maxTime)
        {
            Debug.Log($"Start CountDown: {startingTime}, {maxTime}");
            int currentTime = startingTime;
            
            while (currentTime >= 0)
            {
                Debug.Log($"Start CountDown: {currentTime}, {maxTime}");
                OnSecondOfManagerCooldownLasted?.Invoke(currentTime, maxTime);
                yield return new WaitForSeconds(1);
                currentTime--;
            }

            yield return null;
        }

    }
}