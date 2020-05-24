using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Map))]

public class MapEditor : Editor {
    
    // Thank you Brackeys: https://www.youtube.com/watch?v=RInUu1_8aGw
	public override void OnInspectorGUI()
    {
        Map map = (Map) target;
        base.OnInspectorGUI();

        GUILayout.Label("");
        GUILayout.Label("Values in editor getting lost after entering the playmode (e. g. lists).");
        GUILayout.Label("So if something doesn't work (e. g. changing colour), maybe");
        GUILayout.Label("it's because of the missing values. To solve that, press this");
        GUILayout.Label("button and all the tiles are getting added to their lists again.");

        if (GUILayout.Button("Load Lists for Editor access"))
        {
            Tiles tiles = map.transform.GetComponentInChildren<Tiles>();
            tiles.CollectTiles();            
        }
    }
} // END OF CLASS