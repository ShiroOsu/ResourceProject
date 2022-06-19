namespace Backlog
{
    #region Important

    // TODO: UnitSpawnFlag, If spawning units while player is moving the corresponding structures SpawnFlag move units 
    // TODO: the last position SpawnFlag was placed at
    // TODO: if spawning without placing a flag, spawn at unit-spawn-pos

    // TODO: Resources, Wood, Food(for amount of units you can have), etc.
    // TODO: Environment, Trees, Mountains, etc. 

    // TODO: Options UI.

    // TODO: Separate "AllTextures" into respective texture SO.

    // TODO: Increase resource yield by having specialized workers and further increase their yield by upgrades ?
    // Amount from harvest could be fixed and instead,
    // have the time until harvest be lowered by having more workers.

    #endregion Important

    #region Bugs

    // TODO: Units traveling in circles when assigning new position when its not arrived at last position.

    // TODO: UI Panels does not block mouse clicks (On structures etc..)

    // TODO: Horse Unit, Collider does not cover the whole GameObject making it hard to click on (Only clickable on horse atm).

    #endregion Bugs

    #region WIP

    // TODO: Main Menu, Missing: Options, Quit
    // TODO: Zoom, Stop from zooming into terrain
    // TODO: Be able to cancel unit crafting by clicking on the images in timers
    // Does not remove correct unit/image in queue

    #endregion

    #region Optional

    // TODO: Structures build animations
    // TODO: AutoCreate 
    // TODO: Cursor Texture
    // TODO: Game Name
    // TODO: Pause Game, Load in-game

    #endregion Optional

    #region ?

    // TODO?: Buildings does not need navmesh-obstacle on its GameObject if StructureLayer is not baked into Navmesh
    // When moving units close to the buildings without navmesh they move very slowly.

    //TODO: Creates a new Singleton of type T when exiting play mode.

    #endregion ?

    #region Completed

    // Camera, Move the camera if the mouse is at the edge of screen
    // Clamp zoom

    // Resources
    // Stone
    // Gold

    // Null ref on UI menu-buttons in savedGamesPanel
    // Multi selection of units

    #endregion Completed
}