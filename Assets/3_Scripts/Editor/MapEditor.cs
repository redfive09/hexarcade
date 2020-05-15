using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class MapEditor : Editor {
    MapGenerator mapGenerator;

	public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        mapGenerator = (MapGenerator) target;

        if (GUILayout.Button("Generate Platform"))
			{
				mapGenerator.GenerateMapWithEditor();
			}
    }


}