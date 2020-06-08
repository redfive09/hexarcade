<h3>Current task for all who instantiated players on their maps</h3>
--> Go to your maps and remove the "PlayerCanvas" (if you instantiated one before). Reason is: There are changes in the PlayerCanvas-prefab and they don't apply to existing ones.
<br />
<br />

<h3>What to do in a new scene for creating a simple map?</h3>
1. Add the prefab "MapGenerator" (Folder "4_Map")
2. Click on it and choose one of the buttons "Generate Platform" (or "Generate Tile")
3. Choose one of the tiles and make it to a starting tile (by entering a positive number)
4. For a winning condition, you need to make another tile to a winning Tile. 

That's already enough for having a simple game with the minimum requirements met.
<br />
<br />

<h3>------ Here's a list of the editor features ------</h3>

<b>GameObject "Map", Script "MapSettings":</b>
* Introduction Screen (bool) - not working yet!
* Number Of Checkpoints (int number) - How many can the player choose? None is fine.
* Stoptime For Checkpoints (float seconds) - How much time has the player for choosing the checkpoints? Zero means, no timelimit, so players can only continue by confirming their choices.
* Standard Tiles Means Losing (bool) - Standard Tiles are all hexagons, which doesn't have any function. If you checkmark the boolean, then touching the tile will lead to a loosing condition.
* Spawn Position Offset (Vector3, floats) - Make sure player touches the startTile at the begin, otherwise the timer won't appear. Standard is (0,1,0).
<br /><br />
<b>GameObject "Players", Script "Players":</b>
* Instantiate Players/Players go to spawn position (button) - Only necessary, if you want to make changes to the player (e. g. scale of the ball or changes in the PlayerCanvas)
