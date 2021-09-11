namespace Code.Framework.Enums
{
    
    public enum UnitType
    {
        Null, // default
        Builder,
        Solider,
        Horse,
    }

    public enum StructureType
    {
        Null, // default
        Castle,
        Barracks,
    }
    
    // Unity layers
    public enum UnityLayer
    {
        UI = 5,
    }

    // Temp
    public enum ButtonName
    {
        SpawnBuilder,
        SpawnSoldier,
        SpawnFlag,
    }

    public enum TextureAssetType
    {
        Builder,
        Solider,
        Horse,
        Castle,
        Barracks,
    }
    
    public enum LogMaskThreshold
    {
        Undefined = -1, // 0xFFFFFFFF
        Debuging = 0,
        Normal = 1,
        Critical = 2,
        Nothing = 3,
    }
  
    public enum LogMask
    {
        Debuging,
        Normal,
        Critical,
    }
}