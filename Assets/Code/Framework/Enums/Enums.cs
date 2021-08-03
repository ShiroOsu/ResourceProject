using System;

namespace Code.Framework.Enums
{
    public enum UnitType
    {
        Builder,
        Solider,
    }

    public enum StructureType
    {
        None,
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