using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpSO", menuName = "Scriptable Objects/PowerUpSO")]
public class PowerUpSO : ScriptableObject
{
    public string powerUpName;
    public Sprite icon;
    public float duration;
    public float multiplier = 1.5f;
    public PowerUpType type;

    public enum PowerUpType 
    {
        AdrenalineShot, // Increases Player Movement Speed
        MagneticHands,  // Increases the interaction distance
        PocketDimension,// Allows the player to hold two items at once
        TimeWarp,       // Adds additional time to the round clock
        GoldenPlate,    // The next delivery rewards double points
        TimeFreeze,     // The world stops but the player can still move
        LaserKnife,     // Instant cutting
        CoolingMist     // Slows down the "Burning" bar on the Stove Counter
    }
}
