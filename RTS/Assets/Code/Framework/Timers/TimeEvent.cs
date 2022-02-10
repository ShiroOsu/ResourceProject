using System;
using System.Collections;
using UnityEngine;

namespace Code.Framework.Timers
{
    [Serializable]
    public class TimeEvent
    {
        private Coroutine m_Coroutine;
        private MonoBehaviour m_MonoBehaviour;
        private float m_OverTime;
        private bool m_Done;

        public delegate void OnFinishAction();
        public event OnFinishAction OnFinish;
        
        public TimeEvent()
        {
            m_Done = true;
        }

        public TimeEvent(MonoBehaviour monoBehaviour)
        {
            m_Done = false;
            m_MonoBehaviour = monoBehaviour;
        }

        public TimeEvent(Coroutine coroutine, MonoBehaviour monoBehaviour)
        {
            m_Done = false;
            m_Coroutine = coroutine;
            m_MonoBehaviour = monoBehaviour;
        }

        /// <summary>
        /// Call expression for certain time, after certain delay
        /// </summary>
        /// <param name="action"> Example: ()=> { Debug.Log("Hello"); Debug.Log("World"); } </param>
        /// <param name="monoBehaviour"></param>
        /// <param name="delay"> Time delay before first action call </param>
        /// <param name="time"> How long the Event will run </param>
        /// <param name="timeBetweenAction"> Time of delay between each action call </param>
        /// <returns> TimeEvent </returns>
        public static TimeEvent EventCall(Action<float> action, MonoBehaviour monoBehaviour, float delay = 0f, 
            float time = 0f, float timeBetweenAction = 0f)
        {
            var timeEvent = new TimeEvent(monoBehaviour);
            timeEvent.m_Coroutine = 
                monoBehaviour.StartCoroutine(EventCoroutine(action, delay, time, timeBetweenAction, timeEvent));
            return timeEvent;
        }

        /// <summary>
        /// Call expression for certain time, after certain delay
        /// </summary>
        /// <param name="action"> Example: ()=> { Debug.Log("Hello"); Debug.Log("World"); } </param>
        /// <param name="monoBehaviour"></param>
        /// <param name="delay"> Time delay before first action call </param>
        /// <param name="time"> How long the Event will run </param>
        /// <param name="timeBetweenAction"> Time of delay between each action call </param>
        /// <returns> TimeEvent </returns>
        public static TimeEvent EventCall(Action action, MonoBehaviour monoBehaviour, float delay = 0f,
            float time = 0f, float timeBetweenAction = 0f)
        {
            var timeEvent = new TimeEvent(monoBehaviour);
            timeEvent.m_Coroutine =
                monoBehaviour.StartCoroutine(EventCoroutine(action, delay, time, timeBetweenAction, timeEvent));
            return timeEvent;
        }

        /// <summary>
        /// Calls expression for a certain amount of times, after certain delay
        /// </summary>
        /// <param name="action"> Example: ()=> { Debug.Log("Hello"); Debug.Log("World"); } </param>
        /// <param name="monoBehaviour"></param>
        /// <param name="delay"> Time delay before first action call </param>
        /// <param name="repeatCount"> Amount of times to repeat action </param>
        /// <param name="timeBetweenAction"> Time of delay between each action call </param>
        /// <returns> TimeEvent </returns>
        public static TimeEvent RepeatedEventCall(Action<int> action, MonoBehaviour monoBehaviour, float delay = 0f,
            int repeatCount = 0, float timeBetweenAction = 0f)
        {
            var timeEvent = new TimeEvent(monoBehaviour);
            timeEvent.m_Coroutine =
                monoBehaviour.StartCoroutine(
                    EventRepeatedCoroutine(action, delay, repeatCount, timeBetweenAction, timeEvent));
            return timeEvent;
        }
        
        /// <summary>
        /// Calls expression for a certain amount of times, after certain delay
        /// </summary>
        /// <param name="action"> Example: ()=> { Debug.Log("Hello"); Debug.Log("World"); } </param>
        /// <param name="monoBehaviour"></param>
        /// <param name="delay"> Time delay before first action call </param>
        /// <param name="repeatCount"> Amount of times to repeat action </param>
        /// <param name="timeBetweenAction"> Time of delay between each action call </param>
        /// <returns> TimeEvent </returns>
        public static TimeEvent RepeatedEventCall(Action action, MonoBehaviour monoBehaviour, float delay = 0f,
            int repeatCount = 0, float timeBetweenAction = 0f)
        {
            var timeEvent = new TimeEvent(monoBehaviour);
            timeEvent.m_Coroutine =
                monoBehaviour.StartCoroutine(
                    EventRepeatedCoroutine(action, delay, repeatCount, timeBetweenAction, timeEvent));
            return timeEvent;
        }

        private static IEnumerator EventCoroutine(Action<float> action, float delay, float time,
            float timeBetweenAction, TimeEvent timeEvent)
        {
            if (delay > 0f)
            {
                yield return new WaitForSeconds(delay);
            }

            if (time == 0f)
            {
                action(0f);
            }
            else
            {
                var endTime = Time.realtimeSinceStartup + time;

                if (timeBetweenAction > 0f)
                {
                    while (Time.realtimeSinceStartup < endTime + timeEvent.m_OverTime)
                    {
                        action(endTime + timeEvent.m_OverTime - Time.realtimeSinceStartup);
                        yield return new WaitForSeconds(timeBetweenAction);
                    }
                }
                else
                {
                    while (Time.realtimeSinceStartup < endTime + timeEvent.m_OverTime)
                    {
                        action(endTime + timeEvent.m_OverTime - Time.realtimeSinceStartup);
                        yield return null;
                    }
                }
            }
            timeEvent.SetDone();
        }
        
        private static IEnumerator EventCoroutine(Action action, float delay, float time,
            float timeBetweenAction, TimeEvent timeEvent)
        {
            if (delay > 0f)
            {
                yield return new WaitForSeconds(delay);
            }

            if (time == 0f)
            {
                action();
            }
            else
            {
                var endTime = Time.realtimeSinceStartup + time;

                if (timeBetweenAction > 0f)
                {
                    while (Time.realtimeSinceStartup < endTime + timeEvent.m_OverTime)
                    {
                        action();
                        yield return new WaitForSeconds(timeBetweenAction);
                    }
                }
                else
                {
                    while (Time.realtimeSinceStartup < endTime + timeEvent.m_OverTime)
                    {
                        action();
                        yield return null;
                    }
                }
            }
            timeEvent.SetDone();
        }

        private static IEnumerator EventRepeatedCoroutine(Action<int> action, float delay, int repeatCount,
            float timeBetweenAction, TimeEvent timeEvent)
        {
            if (delay > 0f)
            {
                yield return new WaitForSeconds(delay);
            }

            if (timeBetweenAction == 0f)
            {
                for (int i = 0; i < repeatCount; i++)
                {
                    action(i);
                    yield return null;
                }
            }
            else
            {
                for (int i = 0; i < repeatCount; i++)
                {
                    action(i);
                    yield return new WaitForSeconds(timeBetweenAction);
                }
            }
            timeEvent.SetDone();
        }
        
        private static IEnumerator EventRepeatedCoroutine(Action action, float delay, int repeatCount,
            float timeBetweenAction, TimeEvent timeEvent)
        {
            if (delay > 0f)
            {
                yield return new WaitForSeconds(delay);
            }

            if (timeBetweenAction == 0f)
            {
                for (int i = 0; i < repeatCount; i++)
                {
                    action();
                    yield return null;
                }
            }
            else
            {
                for (int i = 0; i < repeatCount; i++)
                {
                    action();
                    yield return new WaitForSeconds(timeBetweenAction);
                }
            }
            timeEvent.SetDone();
        }

        public void Stop(bool callOnFinish = false)
        {
            if (m_MonoBehaviour != null && m_Coroutine != null)
            {
                m_MonoBehaviour.StopCoroutine(m_Coroutine);
                m_Done = true;
            }

            if (callOnFinish)
            {
                OnFinish?.Invoke();
            }
        }

        public void SetDone()
        {
            m_Done = true;
            Stop(true);
        }

        public void AddTime(float time)
        {
            m_OverTime += time;
        }

        public bool IsEventDone()
        {
            return m_Done;
        }
    }
}