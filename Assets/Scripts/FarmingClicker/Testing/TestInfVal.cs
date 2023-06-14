namespace FarmingClicker.Testing
{
    using InfiniteValue;
    using Sirenix.OdinInspector;
    using UnityEngine;
    public class TestInfVal : SerializedMonoBehaviour
    {
        public InfVal testVal = new InfVal(1).ToPrecision(255);

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                testVal *= 1000;
                Debug.Log($"CurrentlyTestVal = {testVal.ToString(4)}");
            }
        }
    }
}