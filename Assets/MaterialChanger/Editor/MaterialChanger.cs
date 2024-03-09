#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Sample
{
    public class MaterialChanger : EditorWindow
    {
        static Material _materialQ;
        static Material _materialE;
        static Material _materialR;
        static Vector2 _mousePos;


        [MenuItem("Tools/MaterialChanger/MaterialChanger Window")]
        private static void Open()
        {
            MaterialChanger materialChanger = GetWindow<MaterialChanger>();
            materialChanger.Show();
        }

        private void OnGUI()
        {
            _materialQ = EditorGUILayout.ObjectField("Material Q", _materialQ, typeof(Material), false) as Material;
            _materialE = EditorGUILayout.ObjectField("Material E", _materialE, typeof(Material), false) as Material;
            _materialR = EditorGUILayout.ObjectField("Material R", _materialR, typeof(Material), false) as Material;
        }

        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            SceneView.duringSceneGui += SceneViewOnGUI;
        }

        static void SceneViewOnGUI(SceneView sceneView)
        {
            Event e = Event.current;
            _mousePos = e.mousePosition;

            if (e.keyCode == KeyCode.Q)
            {
                ChangingMaterialQ();
            }
        }

        static void ChangingMaterialQ()
        {
            var picked = HandleUtility.PickGameObject(_mousePos, false);

            if (picked && picked.TryGetComponent(out Renderer renderer))
            {
                renderer.sharedMaterial = _materialQ;
            }
        }

        [MenuItem("Tools/MaterialChanger/Change Material on E #e")]
        static void ChangingMaterialE()
        {
            var picked = HandleUtility.PickGameObject(_mousePos, false);

            if (picked && picked.TryGetComponent(out Renderer renderer))
            {
                renderer.sharedMaterial = _materialE;
            }
        }

        [MenuItem("Tools/MaterialChanger/Change Material on R #r")]
        static void ChangingMaterialR()
        {
            var picked = HandleUtility.PickGameObject(_mousePos, false);

            if (picked && picked.TryGetComponent(out Renderer renderer))
            {
                renderer.sharedMaterial = _materialR;
            }
        }
    }
}
#endif