using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Players))]

public class PlayersEditor : Editor {    

	public override void OnInspectorGUI()
    {
        Players players = (Players) target;
        base.OnInspectorGUI();

        GUILayout.Label("");
        GUILayout.Label("This button will instantiate the players, when enough startingTiles are available.");
        GUILayout.Label("If you already have a player, you can click the button again and it will reset it,");
        GUILayout.Label("which means the player might be deleted and instantiated again, if the player");
        GUILayout.Label("has not the latest feature and/or will go to the current spawn position.");
        GUILayout.Label("It's not necessary to instantiate a player beforehand.");

        if (GUILayout.Button("Instantiate or Reset player"))
        {
            GameObject.Find("Map/Tiles").GetComponent<Tiles>().CollectTiles(true);
            players.InstantiatePlayers(true);
        }
    }
} // END OF CLASS