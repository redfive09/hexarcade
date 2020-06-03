using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraFollow))]

public class CameraFollowEditor : Editor {

    // Thank you Brackeys: https://www.youtube.com/watch?v=RInUu1_8aGw
	public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CameraFollow cameraFollow = (CameraFollow) target;

        if (GUILayout.Button("Go to current spawn position"))
        {
            Players players = GameObject.Find("Map/Players").GetComponent<Players>();
            Ball ball = players.GetComponentInChildren<Ball>();
            players.GetCamera().SetPosition(ball.transform);
        }
    }

} // END OF CLASS