using Firebase;
using Firebase.Analytics;

namespace FarmingClicker.Data
{
    using UnityEngine;

    public class FirebaseManager : MonoBehaviour
    {
        private void Awake()
        {
            FirebaseApp.CheckDependenciesAsync().ContinueWith(task =>
            {
                FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);

            });

        }
    }
}
