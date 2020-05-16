using UnityEngine;

public class SingleTilesGenerator : MonoBehaviour
{
    [SerializeField] string hexagonName;
    [SerializeField] string platformName ="IndividualTiles";

        public void GenerateTileWithEditor()
    {
        MapGenerator MG = GetComponent<MapGenerator>();
        MG.GenerateTileWithEditor(hexagonName, platformName);
    }

}
