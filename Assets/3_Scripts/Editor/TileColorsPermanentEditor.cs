using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileColorsPermanent))]

public class TileColorsPermanentEditor : Editor {
    

    // Thank you Brackeys: https://www.youtube.com/watch?v=RInUu1_8aGw
	public override void OnInspectorGUI()
    {
        TileColorsPermanent tileColors = (TileColorsPermanent) target;
        Tiles tiles = tileColors.GetComponent<Tiles>();
        base.OnInspectorGUI();


        if (GUILayout.Button("Change Color of CrackedTiles"))
        {
            tiles.CollectTiles(true);
            tileColors.ChangeCrackedTilesColor();
        }

        if (GUILayout.Button("Change Color of PathTiles"))
        {           
            tiles.CollectTiles(true);
            tileColors.ChangePathTilesColor();
        }

        if (GUILayout.Button("Change Color of DistractionTiles"))
        {            
            tiles.CollectTiles(true);
            tileColors.ChangeDistractionTilesColor();
        }

        if (GUILayout.Button("Change Color of CheckpointTiles"))
        {            
            tiles.CollectTiles(true);
            tileColors.ChangeCheckpointTilesColor();
        }

        if (GUILayout.Button("Change Color of SpecialTiles"))
        {            
            tiles.CollectTiles(true);
            tileColors.ChangeSpecialTilesColor();
        }

        if (GUILayout.Button("Change Color of MovingTiles"))
        {            
            tiles.CollectTiles(true);
            tileColors.ChangeMovingTilesColor();
        }
        
        if (GUILayout.Button("Change Color of StartingTiles"))
        {            
            tiles.CollectTiles(true);
            tileColors.ChangeStartingTilesColor();
        }

        if (GUILayout.Button("Change Color of WinningTiles"))
        {            
            tiles.CollectTiles(true);
            tileColors.ChangeWinningTilesColor();
        }

        if (GUILayout.Button("Change Color of standardTiles"))
        {            
            tiles.CollectTiles(true);
            tileColors.ChangeColorOfStandardTiles();
        }        
        
        if (GUILayout.Button("Change Color of every Tile"))
        {            
            tiles.CollectTiles(true);
            tileColors.ChangeColorOfAllTiles();
        }

    }
} // END OF CLASS