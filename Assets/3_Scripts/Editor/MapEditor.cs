using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Map))]

public class MapEditor : Editor {
    
    // Thank you Brackeys: https://www.youtube.com/watch?v=RInUu1_8aGw
	public override void OnInspectorGUI()
    {
        Map map = (Map) target;
        base.OnInspectorGUI();


    }
} // END OF CLASS