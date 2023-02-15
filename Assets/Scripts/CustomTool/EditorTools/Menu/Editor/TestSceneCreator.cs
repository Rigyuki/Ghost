using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Scripts.CustomTool.EditorTools
{
    public class TestSceneCreator : EditorWindow
    {
        const string defaultFilePath = "Scenes/TestScene/";
        static TestSceneCreator window;
        [MenuItem("CustomTool/CreateTestScene")]
        public static void CreateTestSceneMenuItem()
        {
            if (window == null)
            {
                window = GetWindow<TestSceneCreator>();
                window.minSize = new Vector2(300, 100);
                window.maxSize = new Vector2(600, 100);
            }
            window.Show();
        }
        static string fileName = "";
        private void OnGUI()
        {
            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label(defaultFilePath, GUILayout.ExpandWidth(false));
                fileName = GUILayout.TextField(fileName).Replace("/", "");
            }
            GUILayout.EndHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Create"))
                {
                    if (fileName == "")
                    {
                        Debug.LogError("Scene name cannot be null.");
                    }
                    else
                    {
                        CreateTestScene();
                        window.Close();
                    }
                }
                if (GUILayout.Button("Close"))
                {
                    window.Close();
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.FlexibleSpace();
        }
        private void OnDestroy()
        {
            fileName = "";
        }
        void CreateTestScene()
        {
            string dir = "/" + defaultFilePath + "/" + fileName;
            string temp = (Application.dataPath + dir).Replace("//", "/");
            if (!Directory.Exists(temp))
                Directory.CreateDirectory(temp);
            EditorSceneManager.SaveScene(EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Additive), ("Assets/" + dir + "/" + fileName).Replace("//", "/") + ".unity");
        }
    }
}