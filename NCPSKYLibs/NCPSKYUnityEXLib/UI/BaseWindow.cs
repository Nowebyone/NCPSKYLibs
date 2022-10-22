#if UNITY_5_3_OR_NEWER
namespace NCPLib.BaseLib.UI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public abstract class BaseWindow : MonoBehaviour
    {
        public string path;
        public int windowCode { get; private set; }

        //通过里式转换原则 来存储所有的控件
        private Dictionary<string, List<UIBehaviour>> controlDic = new Dictionary<string, List<UIBehaviour>>();

        // Use this for initialization
        protected virtual void Awake()
        {
            FindChildrenControl<Button>();
            FindChildrenControl<Image>();
            FindChildrenControl<Text>();
            FindChildrenControl<Toggle>();
            FindChildrenControl<Slider>();
            FindChildrenControl<ScrollRect>();
            FindChildrenControl<InputField>();
        }

        /// <summary>
        /// 显示自己
        /// </summary>
        public virtual void ShowMe()
        {

        }

        /// <summary>
        /// 隐藏自己
        /// </summary>
        public virtual void HideMe()
        {

        }

        protected virtual void OnClick(string btnName)
        {

        }

        protected virtual void OnValueChanged(string toggleName, bool value)
        {

        }

        public virtual void SetWindowCode(int windowCode)
        {
            this.windowCode = windowCode;
        }

        /// <summary>
        /// 得到对应名字的对应控件脚本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controlName"></param>
        /// <returns></returns>
        protected T GetControl<T>(string controlName) where T : UIBehaviour
        {
            if (controlDic.ContainsKey(controlName))
            {
                for (int i = 0; i < controlDic[controlName].Count; ++i)
                {
                    if (controlDic[controlName][i] is T)
                        return controlDic[controlName][i] as T;
                }
            }

            return null;
        }

        /// <summary>
        /// 找到子对象的对应控件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private void FindChildrenControl<T>() where T : UIBehaviour
        {
            T[] controls = this.GetComponentsInChildren<T>();
            for (int i = 0; i < controls.Length; ++i)
            {
                string objName = controls[i].gameObject.name;
                if (controlDic.ContainsKey(objName))
                    controlDic[objName].Add(controls[i]);
                else
                    controlDic.Add(objName, new List<UIBehaviour>() { controls[i] });
                //如果是按钮控件
                if (controls[i] is Button)
                {
                    (controls[i] as Button).onClick.AddListener(() => { OnClick(objName); });
                }
                //如果是单选框或者多选框
                else if (controls[i] is Toggle)
                {
                    (controls[i] as Toggle).onValueChanged.AddListener((value) => { OnValueChanged(objName, value); });
                }
            }
        }
    }
}
#endif
