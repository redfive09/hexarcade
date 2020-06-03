using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HexagonChild))]

public class HexagonChildEditor : Editor
{

    // Thx to the people here: https://forum.unity.com/threads/in-editor-select-the-parent-instead-of-an-object-in-the-messy-hierarchy-it-creates.543479/
    private void OnEnable()
    {
        GameObject targetGO = ((HexagonChild)target).gameObject;
        SceneView sceneView = EditorWindow.focusedWindow as SceneView;
        if (targetGO.transform.parent != null && sceneView != null) {
            GameObject[] currentSelection = Selection.gameObjects;
            int idx = -1;
            for (int i = 0; i < Selection.gameObjects.Length; i++) {
                if (Selection.gameObjects[i].GetInstanceID() == targetGO.GetInstanceID()) {
                    idx = i;
                }
            }
            if (idx != -1) {
                currentSelection[idx] = targetGO.transform.parent.gameObject;;
                Selection.objects = currentSelection;
            }
            return;
        }
    }
}