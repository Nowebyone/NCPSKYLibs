using UnityEngine;

namespace NCPLib.BaseLib
{
    /// <summary>
    /// MonoBehaviour SingletonBase,Auto
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonMono_Auto<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        public static T GetInstance()
        {
            if (instance == null)
            {
                GameObject obj = new GameObject();
                obj.name = typeof(T).ToString();
                DontDestroyOnLoad(obj);
                instance = obj.AddComponent<T>();
            }

            return instance;
        }

    }

}
