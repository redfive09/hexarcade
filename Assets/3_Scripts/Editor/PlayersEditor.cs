using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Players))]

public class PlayersEditor : Editor {    

	public override void OnInspectorGUI()
    {
        Players players = (Players) target;
        base.OnInspectorGUI();

        if (GUILayout.Button("Instantiate Players"))
        {            
            players.InstantiatePlayers(true);
        }

    }
} // END OF CLASS