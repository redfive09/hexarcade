using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class MapEditor : Editor {
    MapGenerator mapGenerator;

    // Thank you Brackeys: https://www.youtube.com/watch?v=RInUu1_8aGw
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