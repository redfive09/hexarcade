using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(TilesApplyForAll))]

public class TilesApplyForAllEditor : Editor {

    // Thank you Brackeys: https://www.youtube.com/watch?v=RInUu1_8aGw
	public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TilesApplyForAll tilesApplyForAll = (TilesApplyForAll) target;        
        
        if (GUILayout.Button("Set All Crackable Tiles"))
        {
            CollectTiles();
            tilesApplyForAll.SetTiles(true, false);
        }

        if (GUILayout.Button("Set All Velocity Tiles"))
        {
            CollectTiles();
            tilesApplyForAll.SetTiles(false, true);
        }
    }
    
    private void CollectTiles()
    {
        GameObject.Find("Map/Tiles").GetComponent<Tiles>().CollectTiles(true);
    }
}
