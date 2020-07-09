using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonSpecial : MonoBehaviour
{    

    /* ------------------------------ FIELDS FOR TELEPORTERS ------------------------------  */
    [SerializeField] private bool teleporterEntrance;
    [SerializeField] private bool keepSpeedThroughTeleporter;
    [SerializeField] private int teleporterNumber;
    [SerializeField] private int teleporterConnectedWith;
    [SerializeField] private Vector3 teleporterOffset;
    [SerializeField] private bool normalizePhysics;


    /* ------------------------------ FIELDS FOR VELOCITY TILES ------------------------------  */
    [SerializeField] private float velocity;


    /* ------------------------------ FIELDS FOR JUMPAD ------------------------------  */
    [SerializeField] private Vector3 jumpDirection;


    /* ------------------------------ SPECIAL-TILE NUMBERS ------------------------------  */
    private int specialCase;
    private const int TELEPORTER = 0;
    private const int VELOCITY = 1;
    private const int JUMP_PAD = 2;
    private const int LOSING_TILE = 3;
    private const int NON_STANDARD_COUNTER = 4;



    /* ------------------------------ GENERAL INFORMATION FOR DIFFERENT OPERATIONS ------------------------------  */    
    private Dictionary<int, List<Hexagon>> specialTiles;
    private Tiles tiles;
    private List<Ball> players = new List<Ball>();
    private int getIndexNumberInList;
    private bool hasVFX = false;
    private Hexagon thisHexagon;


    /* ------------------------------ MAIN METHODS FOR SPECIAL TILES ------------------------------  */
    public void GetStarted(Dictionary<int, List<Hexagon>> specialTiles, Tiles tiles, Hexagon hexagon, bool inEditor)
    {
        thisHexagon = hexagon;
        specialCase = thisHexagon.GetSpecialNumber();
        this.specialTiles = specialTiles;
        this.tiles = tiles;
        TileNeedSetup(inEditor);  
    }

    private void TileNeedSetup(bool inEditor)
    {
        switch(specialCase)
        {
            case TELEPORTER:
            {
                thisHexagon.SetAudio(nameof(TELEPORTER).ToLower());
                if(teleporterEntrance) thisHexagon.SetVisualEffect("TeleporterVFX", inEditor);
                hasVFX = true;
                break;
            }

            case VELOCITY:
            {
                thisHexagon.SetAudio(nameof(VELOCITY).ToLower());
                thisHexagon.SetVisualEffect("VelocityVFX", inEditor);
                hasVFX = true;
                break;
            }

            case JUMP_PAD:
            {
                thisHexagon.SetAudio(nameof(JUMP_PAD).ToLower());
                thisHexagon.SetVisualEffect("JumpPadVFX", inEditor);
                hasVFX = true;
                break;
            }
        }
    }

    public void SpecialTileTouched(Ball player)
    {
        players.Add(player);
        getIndexNumberInList = GetIndexNumberInList();        

        if(getIndexNumberInList >= 0) // catch potential errors with the special tile list
        {
            switch(specialCase)
            {
                case TELEPORTER:
                {
                    if(teleporterEntrance)
                    {
                        if(!keepSpeedThroughTeleporter) player.StopMovement();
                        Hexagon teleporterExit = FindTeleporterExit();
                        if(teleporterExit) player.GoToSpawnPosition(teleporterExit, teleporterOffset, false);
                        if(jumpDirection.sqrMagnitude != 0) player.GetRigidbody().AddForce(jumpDirection);
                        thisHexagon.GetAudioSource().Play();
                    }
                    break;
                }

                case VELOCITY:
                {
                    Rigidbody rb = player.GetRigidbody();
                    Vector3 currentVelocity = rb.velocity;
                    currentVelocity *= velocity;
                    rb.velocity = currentVelocity;
                    thisHexagon.GetAudioSource().Play();
                    break;
                }

                case JUMP_PAD:
                {
                    player.GetRigidbody().AddForce(jumpDirection * 3.5f);
                    thisHexagon.GetAudioSource().Play();
                    break;
                }

                case LOSING_TILE:
                {
                    player.Lost();
                    break;
                }

                case NON_STANDARD_COUNTER:
                {
                    CheckIfPlayerTouchedAllNonSpecificHexagons(player);
                    break;
                }
            }
        }

        //If normalizePhysics, means that the gravity will be turned to the normal unity vanilla and drag of rigidbody turned to 0 
        if (normalizePhysics)
        {
            Physics.gravity = new Vector3(0,-9.81f);
            player.GetRigidbody().drag = 0;
            player.GetComponent<BallControls>().setMultiplier(1);
            //Debug.Log("Normalized physics to vanilla unity");
            //Debug.Log(player.GetComponent<BallControls>().getMultiplier());
        }
        //This else case is to changed back to the setting after physics control 
        else {
           Physics.gravity = new Vector3(0,-45);
           player.GetRigidbody().drag = 2;
           player.GetComponent<BallControls>().setMultiplier(2.5f);
           //Debug.Log("Changed back to new Physics setting");
           //Debug.Log(player.GetComponent<BallControls>().getMultiplier());
        }
    }

    /* ------------------------------ GENERAL METHODS FOR SPECIAL TILES ------------------------------  */
    public void SpecialTileLeft(Ball player)
    {
        players.Remove(player);

        if(specialCase == 0)
        {
            
        }
    }

    private int GetIndexNumberInList()
    {
        for(int i = 0; i < specialTiles[specialCase].Count; i++)
        {
            if(specialTiles[specialCase][i] == thisHexagon)
            {
                return i;
            }
        }
        return int.MinValue;
    }

    
    /* ------------------------------ SPECIFIC METHODS FOR TELEPORTERS ------------------------------  */

    private Hexagon FindTeleporterExit()
    {
        List<Hexagon> teleporterList = specialTiles[TELEPORTER];

        for(int i = 0; i < teleporterList.Count; i++)
        {
            HexagonSpecial teleporter = teleporterList[i].GetComponent<HexagonSpecial>();

            if(teleporter.GetTeleporterNumber() == teleporterConnectedWith)
            {
                return teleporter.GetComponent<Hexagon>();
            }
        }
        Debug.Log("This teleporter has not exit");
        return null;
    }

    public int GetTeleporterNumber()
    {
        return teleporterNumber;
    }


    /* ------------------------------ SPECIFIC METHOD FOR NON_STANDARD_COUNTER ------------------------------  */

    private void CheckIfPlayerTouchedAllNonSpecificHexagons(Ball player)
    {
        Dictionary<int, List<Hexagon>>[] nonStandardTiles = tiles.GetAllNonStandardTiles();        
        int nonStandardTilesCount = 0;

        for(int i = 0; i < nonStandardTiles.Length; i++)
        {
            Dictionary<int, List<Hexagon>> currentHexagonDictionary = nonStandardTiles[i];
            int listLength = currentHexagonDictionary.Count;

            for(int j = 0; j < listLength; j++)
            {
                if(currentHexagonDictionary.TryGetValue(j, out List<Hexagon> hexagonList))
                {
                    for(int h = 0; h < hexagonList.Count; h++)
                    {
                        if(!hexagonList[h].IsTouched())
                        {
                            nonStandardTilesCount++;
                        }
                    }
                }
                else
                {
                    listLength++;
                }
            } 
        }

        if(nonStandardTilesCount == 0)
        {
            player.Won();
        }
        else
        {
            string information = "Find the remaining " + nonStandardTilesCount;
            if(nonStandardTilesCount > 1)
            {
                information += " non-standard hexagons.";
            }
            else
            {
                information += " non-standard hexagon.";
            }
            
            player.GetComponentInChildren<TutorialManager>().SetInformationText(3, 0.3f, information);
        }         
    }



    /* ------------------------------ SETTERS FOR SPECIAL TILES ------------------------------  */

    public void SetVelocity(float velocity)
    {
        this.velocity = velocity;
    }

    public void SetJumpDirection(Vector3 jumpDirection)
    {
        this.jumpDirection = jumpDirection;
    }


    /* ------------------------------ GETTERS FOR SPECIAL TILES ------------------------------  */
    public bool HasVFX()
    {
        return hasVFX;
    }

    /* ------------------------------ EDITOR METHODS ------------------------------  */

    public string GetNameOfFunction()
    {
        string prefix = "-> ";
        
        switch(specialCase)
        {
            case TELEPORTER:    
                prefix += nameof(TELEPORTER).ToLower(); 
                if(teleporterEntrance)
                {
                    prefix += ", " + teleporterNumber + " to " + teleporterConnectedWith;
                }
                else
                {
                    prefix += "_exit " + teleporterNumber;
                }
                return prefix;
    
            case VELOCITY:
                return prefix + nameof(VELOCITY).ToLower() + " " + velocity;
    
            case JUMP_PAD:
                return prefix + nameof(JUMP_PAD).ToLower();

            case LOSING_TILE:
                return prefix + nameof(LOSING_TILE).ToLower();

            case NON_STANDARD_COUNTER:
                return prefix + nameof(NON_STANDARD_COUNTER).ToLower();
        }
        return "";
    }
}