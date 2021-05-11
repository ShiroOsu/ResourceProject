using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager s_Instance = null;
    public static DataManager Instance => s_Instance ??= FindObjectOfType<DataManager>();

    //public BuilderStats builderData;
    //public SoldierStats soldierData;
    [Header("Units")]
    public UnitData unitData;

    [Header("Structures")]
    public CastleData castleData;

    [Header("Player")]
    public MouseData mouseData;
    public CameraData cameraData;
    public MouseInputs mouseInputs;
}
