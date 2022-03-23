namespace Backlog
{
    #region Sadge
    // TODO: (Should have started this project with Networking... But since I did not it is a bit late) 
    #endregion Sadge
    
    #region Important
    
    // TODO: Horse Unit, Collider does not cover the whole GameObject making it hard to click on (Only clickable on horse atm)
    
    // TODO: Multi-Selection, Rework into MouseInputs Update to "skip" EventSystem.IsPointerOverGameObject
    // TODO: Update multi-selection image anchored position so it wont start from last position 
    
    // TODO: UnitSpawnFlag, If spawning units while player is moving the corresponding structures SpawnFlag move units 
    // TODO: the last position SpawnFlag was placed at
    // TODO: if spawning without placing a flag, spawn at unit-spawn-pos
    
    // TODO: Be able to cancel unit crafting by clicking on the images in timers
    
    // TODO: Resources, Wood, Gold, Stone, Food(for amount of units you can have), etc.
    // TODO: Environment, Trees, Mountains, etc. 
    // TODO: Procedural generated stuff

    // TODO: Clicking on menu-buttons over a structure will select the structure 
    
    // TODO: Options UI
    
    // TODO: Save & Load Game 

    #endregion Important
    
    #region Bugs
    
    // TODO: Units traveling in circles when assigning new position when its not arrived at last position.
    
    #endregion Bugs

    #region Working but not Completed

    // TODO: Pause Game, Missing: UI(return to game, exit, save, load)
    // TODO: Main Menu, Missing: Options, Load, Exit
    // TODO: Zoom, Missing: Clamp, Smooth
    
    #endregion
    
    #region Optional
    
    // TODO: Structures build animations
    // TODO: AutoCreate 
    // TODO: Cursor Texture
    // TODO: Game Name
    
    #endregion Optional

    #region ?
    
    // TODO?: Buildings does not need navmesh-obstacle on its GameObject if StructureLayer is not baked into Navmesh
    // When moving units close to the buildings without navmesh they move very slowly.
    
    #endregion ?
    
    #region Completed
    
    // Camera, Move the camera if the mouse is at the edge of screen : 2022-02-11

    
    #endregion Completed
    
    #region Bug fixed
    
    // Creates a new Singleton of type T when exiting play mode.

    #endregion Bug fixed
}