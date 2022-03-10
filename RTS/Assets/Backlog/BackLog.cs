namespace Backlog
{
    #region Sadge
    // TODO: (Should have started this project with Networking... But since I did not it is a bit late) 
    #endregion Sadge
    
    #region Important
    
    // TODO: Horse Unit, Collider does not cover the whole GameObject making it hard to click on
    
    // TODO: Main Menu, Loading Level, ? 
    
    // TODO: Multi-Selection, Rework into MouseInputs Update to "skip" EventSystem.IsPointerOverGameObject
    // TODO: Update multi-selection image anchored position so it wont start from last position 
    
    // TODO: UnitSpawnFlag, If spawning units while player is moving the corresponding structures SpawnFlag move units 
    // TODO: the last position SpawnFlag was placed at
    
    // TODO: Clamp Zoom function (Smooth zoom) 
    
    // TODO: Structures build animations
    // TODO: Structures in scene hierarchy, naming, why is "barracksUIMiddle" called info in inspector, why is CastleUIMiddle gameObject called castleStats
    
    // TODO: Resources, Wood, Gold, Stone, etc.
    // TODO: Environment, Trees, Mountains, etc. 
    // TODO: Procedural generated trees
    
    // TODO: Pause Game

    #endregion Important
    
    #region Bugs
    
    //TODO: Creates a new Singleton of type T when editor exits play mode.
    
    #endregion Bugs
    
    #region Optional
    
    // TODO: AutoCreate, Transfer string blocks to file
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
}