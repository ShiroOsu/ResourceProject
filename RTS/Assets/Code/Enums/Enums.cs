namespace Code.Enums
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
    }
    
    // Unity layers
    public enum UnityLayer
	{
        UI = 5,
	}

    public enum TextureAssetType
    {
        Builder,
        Soldier,
        Horse,
        Castle,
        Barracks,
        Flag,
        Build,
        Back, // No texture for this yet
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