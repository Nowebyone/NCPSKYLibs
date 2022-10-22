#if UNITY_5_3_OR_NEWER
using System.Collections.Generic;
using NCPLib.BaseLib;
using UnityEngine.Events;


namespace NCPLib.Event
{
    public class EventInfo<T> : IEventInfo
    {
        public UnityAction<T> actions;

        public EventInfo(UnityAction<T> action)
        {
            actions += action;
        }
    }

    public class EventInfo : IEventInfo
    {
        public UnityAction actions;

        public EventInfo(UnityAction action)
        {
            actions += action;
        }
    }


    /// <summary>
    /// EventCenter UnityAction Ver
    /// </summary>
    public class EventCenter : BaseManager<EventCenter>
    {
        //key —— EventKey
        //value —— EventActionList
        private Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();

        /// <summary>
        /// AddEventListener
        /// </summary>
        /// <param name="name">EventName</param>
        /// <param name="action">EventCall</param>
        /// <typeparam name="T">Type</typeparam>
        public void AddEventListener<T>(string name, UnityAction<T> action)
        {
            if (eventDic.ContainsKey(name))
            {
                (eventDic[name] as EventInfo<T>).actions += action;
            }
            else
            {
                eventDic.Add(name, new EventInfo<T>(action));
            }
        }

        /// <summary>
        /// AddEventListener
        /// </summary>
        /// <param name="name">EventName</param>
        /// <param name="action">EventCall</param>
        public void AddEventListener(string name, UnityAction action)
        {
            if (eventDic.ContainsKey(name))
            {
                (eventDic[name] as EventInfo).actions += action;
            }
            else
            {
                eventDic.Add(name, new EventInfo(action));
            }
        }


        /// <summary>
        /// RemoveEventListener
        /// </summary>
        /// <param name="name">EventName</param>
        /// <param name="action">EventCall</param>
        /// <typeparam name="T">Type</typeparam>
        public void RemoveEventListener<T>(string name, UnityAction<T> action)
        {
            if (eventDic.ContainsKey(name))
                (eventDic[name] as EventInfo<T>).actions -= action;
        }

        /// <summary>
        /// RemoveEventListener
        /// </summary>
        /// <param name="name">EventName</param>
        /// <param name="action">EventCall</param>
        public void RemoveEventListener(string name, UnityAction action)
        {
            if (eventDic.ContainsKey(name))
                (eventDic[name] as EventInfo).actions -= action;
        }

        /// <summary>
        /// EventTrigger
        /// </summary>
        /// <param name="name">EventName</param>
        /// <param name="info">EventInfo</param>
        /// <typeparam name="T">Type</typeparam>
        public void EventTrigger<T>(string name, T info)
        {
            if (eventDic.ContainsKey(name))
            {
                if ((eventDic[name] as EventInfo<T>).actions != null)
                    (eventDic[name] as EventInfo<T>).actions.Invoke(info);
            }
        }

        /// <summary>
        /// EventTrigger
        /// </summary>
        /// <param name="name">EventName</param>
        public void EventTrigger(string name)
        {
            if (eventDic.ContainsKey(name))
            {
                if ((eventDic[name] as EventInfo).actions != null)
                    (eventDic[name] as EventInfo).actions.Invoke();
            }
        }

        /// <summary>
        /// ClearEventCenter
        /// </summary>
        public void Clear()
        {
            eventDic.Clear();
        }
    }

}
#endif
