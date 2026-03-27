using UnityEngine;

public interface IPowerUpEffect
{
    void Apply(PlayerController player, PowerUpSO data);
    void Remove(PlayerController player, PowerUpSO data);
}
