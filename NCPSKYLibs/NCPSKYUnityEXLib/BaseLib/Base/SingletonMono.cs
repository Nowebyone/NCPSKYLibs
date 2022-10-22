using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NCPLib.BaseLib
{
    /// <summary>
    /// MonoBehaviour SingletonBase,Plz Manually ensure uniqueness
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        public static T GetInstance()
        {
            return instance;
        }
        protected virtual void Awake()
        {
            instance = this as T;
        }

    }

}
