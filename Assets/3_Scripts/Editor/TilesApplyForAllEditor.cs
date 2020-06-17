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
        
        if (GUILayout.Button("Set all Crackable Tiles"))
        {
            CollectTiles();
            tilesApplyForAll.SetTiles(0);
        }

        if (GUILayout.Button("Set all Velocity Tiles"))
        {
            CollectTiles();
            tilesApplyForAll.SetTiles(1);            
        }

        if (GUILayout.Button("Set all Standard Tiles to Loosing Tiles"))
        {
            CollectTiles();
            tilesApplyForAll.SetTiles(2);
            CollectTiles();
            UpdateNames();
        }

        if (GUILayout.Button("Remove all Loosing Tiles"))
        {
            CollectTiles();
            tilesApplyForAll.SetTiles(3);
            CollectTiles();
            UpdateNames();
        }
    }
    
    private void CollectTiles()
    {
        GameObject.Find("Map/Tiles").GetComponent<Tiles>().CollectTiles(true);
    }

    private void UpdateNames()
    {
        Tiles tiles = GameObject.Find("Map/Tiles").GetComponent<Tiles>();
        TilesEditor.UpdateNames(tiles.GetPlatforms());
    }
}
