using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NCPLib.BaseLib;
using UnityEngine;

namespace NCPLib.BaseLib.Random
{
    public class AccessibleRandomNum_Int
    {
        public string KeyName;

        private List<int> isGeneratedNum;
        private int maxNum, minNum;
        private bool limit;
        private System.Random _random = new System.Random(DateTime.Today.Millisecond);


        #region Constructor
        public AccessibleRandomNum_Int(string _KeyName,int min,int max)
        {
            limit = true;
            maxNum = max;
            minNum = min;
            KeyName = _KeyName;
        }
        public AccessibleRandomNum_Int(string _KeyName)
        {
            limit = false;
            KeyName = _KeyName;
        }
        #endregion

        
        public int GetRandomNum()
        {
            int _randomNum = 0;
            bool isNewNum =false;
            while (!isNewNum)
            {
                if (limit)
                {
                    _randomNum = _random.Next(minNum,maxNum);
                }
                else
                {
                    _randomNum = _random.Next();
                }

                if (! isGeneratedNum.Contains(_randomNum))
                {
                    isNewNum = true;
                }
            }
            isGeneratedNum.Add(_randomNum);
            return _randomNum;
        }
        
        public void GiveBackRandomNum(int _num)
        {
            if (isGeneratedNum.Contains(_num))
            {
                isGeneratedNum.Remove(_num);
            }
        }

        public void Remove()
        {
            RandomMgr.GetInstance().RemoveAccessibleRandomCollection(KeyName);
        }
    }


    public enum NumType
    {
        _int,
        _float
    }


    public class RandomMgr : BaseManager<RandomMgr>
    {
        public Dictionary<string, AccessibleRandomNum_Int> AccessibleInts { get; private set; } =
            new Dictionary<string, AccessibleRandomNum_Int>();


        #region FunReload
        public bool CreateAccessibleRandomCollection(string KeyName,NumType type = NumType._int)
        {
            switch (type)
            {
                case NumType._int:
                    return _CreateAccessibleRandomCollection(KeyName,0,0, false);
            }
            return false;
        }
        public bool CreateAccessibleRandomCollection(string KeyName,int maxNum,int minNum)
        {
            return _CreateAccessibleRandomCollection(KeyName, maxNum, minNum, true);
        }
        #endregion


        public void RemoveAccessibleRandomCollection(string KeyName,NumType type = NumType._int)
        {
            switch (type)
            {
                case NumType._int:
                    _CreateAccessibleRandomCollection(KeyName,0,0, false);
                    break;
                case NumType._float:
                    break;
            }
        }




        private bool _CreateAccessibleRandomCollection(string KeyName,int maxNum,int minNum,bool limit)
        {
            if (!AccessibleInts.ContainsKey(KeyName))
            {
                if (limit)
                {
                    AccessibleInts.Add(KeyName,new AccessibleRandomNum_Int(KeyName,minNum,maxNum));
                    return true;
                }
                AccessibleInts.Add(KeyName,new AccessibleRandomNum_Int(KeyName));
                return true;
            }
            return false;
        }
    }
}
