using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Hexagon))]

public class HexagonEditor : Editor {

    // Thank you Brackeys: https://www.youtube.com/watch?v=RInUu1_8aGw
	public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Hexagon hexagon = (Hexagon) target;
        
        if (GUILayout.Button("Delete Hexagon"))
        {            
            // In order to delete the hexagon, we have to tell the platform about it, ergo we have to remove it first from its platform-list
            Platform platform = hexagon.transform.GetComponentInParent<Platform>(); // get the platform where the hexagon is inside 
            platform.RemoveHexagon(hexagon, true);
        }
    }
} // END OF CLASS