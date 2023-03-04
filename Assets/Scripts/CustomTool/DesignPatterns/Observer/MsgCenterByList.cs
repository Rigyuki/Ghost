using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.CustomTool.DesignPatterns.ObserverPattern {

    public class MsgCenterByList
    {
       // public const int MSG_COLOR_BUTTON_CLICKED = 1;
        public const int COLLECTION_LU = 1;
        public const int COLLECTION_GHOST = 2;

        public const int ENEMY_AI_ATTACK = 3;
        public const int ENEMY_AI_CHASE = 4;
        public const int ENEMY_AI_PATROL = 5;

        public static bool Enabled = true;
        private static List<System.Action<CommonMsg>> _actions = new List<Action<CommonMsg>>(1024);

        public static void AddListener(System.Action<CommonMsg> action)
        {
            if (!_actions.Contains(action))
            {
                _actions.Add(action);
            }
        }

        public static void RemoveListener(System.Action<CommonMsg> action)
        {
            _actions.Remove(action);
        }

        public static void SendMessage(CommonMsg commonMsg)
        {
            if (!Enabled)
            {
                return;
            }
            for (int i = _actions.Count - 1; i >= 0; i--)
            {
                if (_actions[i] != null)
                {
                    _actions[i].Invoke(commonMsg);
                }
            }
        }
    }
}

