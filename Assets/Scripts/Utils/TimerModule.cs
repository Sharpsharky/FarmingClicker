namespace Utils
{
    using System;
    using System.Collections.Generic;
    using _3rd_Party.Trinary_Software;
    using UnityEngine;

    public class TimerModule
    {
        #region Public Variables

        public TimeSpan CurrentTime { get; private set; }

        #endregion

        #region Private Methods

        private IEnumerator<float> TickCoroutine()
        {
            while(true)
            {
                yield return Timing.WaitForSeconds(tickPrecision);
                CurrentTime += TimeSpan.FromSeconds(tickPrecision);
                onTick?.Invoke(CurrentTime);
            }
        }

        #endregion

        #region Private Variables

        private Action<TimeSpan> onTick;
        private CoroutineHandle timerHandle;
        private readonly float tickPrecision;

        #endregion

        #region Public API

        public TimerModule(float tickPrecisionInSeconds = 1f)
        {
            tickPrecision = tickPrecisionInSeconds;
        }

        public void Start(Action<TimeSpan> onTickCallback = null)
        {
            if(onTick != null)
            {
                Debug.Log("Couldn't start timer - already started.");
                return;
            }

            onTick = onTickCallback;
            CurrentTime = TimeSpan.Zero;

            timerHandle = Timing.RunCoroutine(TickCoroutine());
        }

        public void Pause()
        {
            Timing.PauseCoroutines(timerHandle);
        }

        public void Resume()
        {
            Timing.ResumeCoroutines(timerHandle);
        }

        public void Stop()
        {
            onTick = null;
            Timing.KillCoroutines(timerHandle);
        }

        public void Dispose()
        {
            Stop();
            CurrentTime = TimeSpan.Zero;
        }

        #endregion
    }
}