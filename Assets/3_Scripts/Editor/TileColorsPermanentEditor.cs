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
            tiles.ResetAllLists();
            tileColors.ChangeCrackedTilesColor();
        }

        if (GUILayout.Button("Change Color of PathTiles"))
        {           
            tiles.ResetAllLists();
            tileColors.ChangePathTilesColor();
        }

        if (GUILayout.Button("Change Color of DistractionTiles"))
        {            
            tiles.ResetAllLists();
            tileColors.ChangeDistractionTilesColor();
        }

        if (GUILayout.Button("Change Color of CheckpointTiles"))
        {            
            tiles.ResetAllLists();
            tileColors.ChangeCheckpointTilesColor();
        }

        if (GUILayout.Button("Change Color of SpecialTiles"))
        {            
            tiles.ResetAllLists();
            tileColors.ChangeSpecialTilesColor();
        }

        if (GUILayout.Button("Change Color of MovingTiles"))
        {            
            tiles.ResetAllLists();
            tileColors.ChangeMovingTilesColor();
        }
        
        if (GUILayout.Button("Change Color of StartingTiles"))
        {            
            tiles.ResetAllLists();
            tileColors.ChangeStartingTilesColor();
        }

        if (GUILayout.Button("Change Color of WinningTiles"))
        {            
            tiles.ResetAllLists();
            tileColors.ChangeWinningTilesColor();
        }

        if (GUILayout.Button("Change Color of standardTiles"))
        {            
            tiles.ResetAllLists();
            tileColors.ChangeColorOfStandardTiles();
        }        
        
        if (GUILayout.Button("Change Color of every Tile"))
        {            
            tiles.ResetAllLists();
            tileColors.ChangeColorOfAllTiles();
        }

    }
} // END OF CLASS