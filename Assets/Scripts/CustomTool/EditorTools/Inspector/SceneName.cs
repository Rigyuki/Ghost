using System;
using UnityEngine;

namespace Scripts.CustomTool.EditorTools
{
    /*
     * 标签，修饰string，在编辑器中实现下拉选择场景名的效果
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