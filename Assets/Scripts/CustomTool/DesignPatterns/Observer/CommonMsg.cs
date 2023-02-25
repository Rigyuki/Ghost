using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.CustomTool.DesignPatterns.ObserverPattern
{
    public struct CommonMsg
    {
        public int MsgId; // 区分这条消息到底是什么样的消息
        public object Content; // 放置内容  
        public int intParam; 
    }
}
