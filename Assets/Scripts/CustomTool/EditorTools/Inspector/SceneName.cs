using System;
using UnityEngine;

namespace Scripts.CustomTool.EditorTools
{
    /*
     * ��ǩ������string���ڱ༭����ʵ������ѡ�񳡾�����Ч��
     */
    [AttributeUsage(AttributeTargets.Field)]
    public class SceneName : PropertyAttribute
    {
        int _selected;
        string _name;
        public int selected { get { return _selected; } set { _selected = value; } }
        public string name { get { return _name; } set { _name = value; } }
        public SceneName()
        {

        }
    }
}