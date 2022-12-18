namespace FarmingClicker.GameFlow.Messages.Notifications.Scene
{
    public record SceneUnloadedNotification : GameFlowMessage
    {
        public string SceneName { get; }

        public SceneUnloadedNotification(string sceneName)
        {
            this.SceneName = sceneName;
        }
    }
}
