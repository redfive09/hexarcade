What to do in a new scene for creating a simple map?
1. Add the prefab "MapGenerator" (Folder "4_Map")
2. Click on it and choose one of the buttons "Generate Platform" (or "Generate Tile")
3. Choose one of the tiles and make it to a starting tile (by entering a positive number)
4. For a winning condition, you need to make another tile to a winning Tile. 

That's already enough for having a simple game with the minimum requirements met.
 <br />
 <br />
 <br />
------ Here's a list of the editor features ------

1. GameObject "Map", Script "MapSettings":
* Introduction Screen (bool) - not working yet!
* Number Of Checkpoints (int number) - How many can the player choose? None is fine.
* Stoptime For Checkpoints (float seconds) - How much time has the player for choosing the checkpoints? Zero means, no timelimit, so players can only continue by confirming their choices.
* Standard Tiles Means Losing (bool) - Standard Tiles are all hexagons, which doesn't have any function. If you checkmark the boolean, then touching the tile will lead to a loosing condition.
* Spawn Position Offset (Vector3, floats) - Make sure player touches the startTile at the begin, otherwise the timer won't appear. Standard is (0,1,0).

