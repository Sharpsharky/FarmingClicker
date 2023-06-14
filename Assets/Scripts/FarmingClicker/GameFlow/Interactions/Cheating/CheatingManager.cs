namespace FarmingClicker.GameFlow.Interactions.Cheating
{
    using Core.Message;
    using Data;
    using Messages.Commands.Currency;
    using InfiniteValue;
    using Sirenix.OdinInspector;
    using UnityEngine;
    public class CheatingManager : SerializedMonoBehaviour
    {
        [SerializeField] private InfVal cheatingCurrency = new InfVal(1000, InGameData.InfValPrecision);


        #if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                MessageDispatcher.Instance.Send(new ModifyCurrencyCommand(cheatingCurrency));
            }
        }
        #endif
    }
}