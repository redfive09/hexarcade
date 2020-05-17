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
            // Platform platform = hexagon.transform.parent;
            // Debug.Log(hexagon);
            GameObject hexagonObject = hexagon.transform.gameObject;
            // Debug.Log();
            // Debug.Log(hexagonObject.GetInstanceID());
            // Debug.Log(hexagonObject.GetInstanceID());
            Platform platform = hexagon.transform.GetComponentInParent<Platform>();
            platform.RemoveHexagonInEditor(hexagon);




            /*  1. Go to parent
                2. Look for the selected hexagon
                3. Destroy() it (and it should disapper)
            */
        }
    }
} // END OF CLASS