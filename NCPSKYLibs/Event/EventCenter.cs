using System;
using System.Collections.Generic;
using NCPLib.BaseLib;

namespace NCPLib.Event
{
    public interface IEventInfo
    {

    }

    #if !UNITY_5_3_OR_NEWER
    public class EventInfo<T> : IEventInfo
    {
        public Action<T> actions;

        public EventInfo(Action<T> action)
        {
            actions += action;
        }
    }

    public class EventInfo : IEventInfo
    {
        public Action actions;

        public EventInfo(Action action)
        {
            actions += action;
        }
    }


    /// <summary>
    /// EventCenter
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
        public void AddEventListener<T>(string name, Action<T> action)
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
        public void AddEventListener(string name, Action action)
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
        public void RemoveEventListener<T>(string name, Action<T> action)
        {
            if (eventDic.ContainsKey(name))
                (eventDic[name] as EventInfo<T>).actions -= action;
        }

        /// <summary>
        /// RemoveEventListener
        /// </summary>
        /// <param name="name">EventName</param>
        /// <param name="action">EventCall</param>
        public void RemoveEventListener(string name, Action action)
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
    #endif
}
