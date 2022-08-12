namespace Code.Tools.Enums
{
    public enum StructureType
    {
        Null, // default
        Castle,
        Barracks,
	}

    public enum UnitType
    {
        Builder,
        Soldier,
        Horse,
        Worker,
    }

    public enum ResourceType
    {
        Gold,
        Stone,
        Wood,
        Food,
        Units,
    }
    
    // Unity layers
    public enum UnityLayer
	{
        UI = 5,
	}

    // Because this is used for getting unit costs,
    // make sure the unit order is the same as in UnitType. 
    // Changing this will cause the scriptableObject 'AllTextures' to change.
    public enum TextureAssetType
    {
        Builder,
        Soldier,
        Horse,
        Worker,
        Castle,
        Barracks,
        Flag,
        Build,
        Back, // No texture for this yet
        Gold,
        Wood,
        Food,
	}

    public enum LogMaskThreshold
    {
        Undefined = -1, // 0xFFFFFFFF
        Debugging = 0,
        Normal = 1,
        Critical = 2,
        Nothing = 3,
    }
  
    public enum LogMask
    {
        Debugging,
        Normal,
        Critical,
    }
}