using UnityEngine;
using UnityEditor;
using System;

namespace Scripts.CustomTool.EditorTools
{
    [CustomPropertyDrawer(typeof(MultiSelector))]
    public class MultiSelectorEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Enum targetObject = fieldInfo.GetValue(property.serializedObject.targetObject) as Enum;
            targetObject = EditorGUI.EnumFlagsField(position, label, targetObject);
            fieldInfo.SetValue(property.serializedObject.targetObject, targetObject);
            if(GUI.changed)
            {
                EditorUtility.SetDirty(property.serializedObject.targetObject);
            }
        }
    }
}