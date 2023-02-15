using System;
using UnityEngine;

namespace Scripts.CustomTool.EditorTools
{
    [AttributeUsage(AttributeTargets.Field)]
    public class MultiSelector : PropertyAttribute
    {
        public MultiSelector()
        {

        }
    }
}