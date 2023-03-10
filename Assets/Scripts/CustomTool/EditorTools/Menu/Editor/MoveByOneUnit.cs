using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Scripts.CustomTool.EditorTools
{
    public class MoveByOneUnit
    {
        [MenuItem("CustomTool/MoveLeftByOneUnit #&%a")]
        public static void MoveLeftMenuItem()
        {
            var objects = Selection.GetTransforms(SelectionMode.TopLevel);
            Undo.RecordObjects(objects, "Move Left");
            foreach (var t in objects)
            {
                t.transform.AddLocalPositionX(-1);
            }
        }
        [MenuItem("CustomTool/MoveRightByOneUnit #&%d")]
        public static void MoveRightMenuItem()
        {
            var objects = Selection.GetTransforms(SelectionMode.TopLevel);
            Undo.RecordObjects(objects, "Move Right");
            foreach (var t in objects)
            {
                t.transform.AddLocalPositionX(1);
            }
        }
        [MenuItem("CustomTool/MoveForwardByOneUnit #&%w")]
        public static void MoveForwardMenuItem()
        {
            var objects = Selection.GetTransforms(SelectionMode.TopLevel);
            Undo.RecordObjects(objects, "Move Forward");
            foreach (var t in objects)
            {
                t.transform.AddLocalPositionZ(1);
            }
        }
        [MenuItem("CustomTool/MoveBackwardByOneUnit #&%s")]
        public static void MoveBackwardMenuItem()
        {
            var objects = Selection.GetTransforms(SelectionMode.TopLevel);
            Undo.RecordObjects(objects, "Move Backward");
            foreach (var t in objects)
            {
                t.transform.AddLocalPositionZ(-1);
            }
        }
        [MenuItem("CustomTool/MoveUpByOneUnit #&%q")]
        public static void MoveUpMenuItem()
        {
            var objects = Selection.GetTransforms(SelectionMode.TopLevel);
            Undo.RecordObjects(objects, "Move Up");
            foreach (var t in objects)
            {
                t.transform.AddLocalPositionY(-1);
            }
        }
        [MenuItem("CustomTool/MoveDownByOneUnit #&%e")]
        public static void MoveDownMenuItem()
        {
            var objects = Selection.GetTransforms(SelectionMode.TopLevel);
            Undo.RecordObjects(objects, "Move Down");
            foreach (var t in objects)
            {
                t.transform.AddLocalPositionY(1);
            }
        }
    }
}