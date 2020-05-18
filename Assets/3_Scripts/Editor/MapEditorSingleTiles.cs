using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SingleTilesGenerator))]

public class MapEditorSingleTiles : Editor {

    // Thank you Brackeys: https://www.youtube.com/watch?v=RInUu1_8aGw
	public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SingleTilesGenerator singleTilesGenerator = (SingleTilesGenerator) target;
        
        if (GUILayout.Button("Generate Tile"))
        {
            singleTilesGenerator.GenerateTileWithEditor();
        }
    }
} // END OF CLASS