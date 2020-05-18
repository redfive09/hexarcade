using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Map))]

public class MapEditor : Editor {
    

    // Thank you Brackeys: https://www.youtube.com/watch?v=RInUu1_8aGw
	public override void OnInspectorGUI()
    {
        Map map = (Map) target;
        base.OnInspectorGUI();

        if (GUILayout.Button("Activate Editor Access"))
        {
            Tiles tiles = map.transform.GetComponentInChildren<Tiles>();
            tiles.CollectTiles();
        }
    }
} // END OF CLASS


