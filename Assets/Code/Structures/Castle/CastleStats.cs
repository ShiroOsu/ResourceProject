using UnityEngine;

[CreateAssetMenu(fileName = "CastleData", menuName = "ScriptableObjects/Structures/CastleData")]
public class CastleStats : ScriptableObject
{
    public float maxHealth = 100f;
    public float defense = 10f;
    public float attack = 5f;

}