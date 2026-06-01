using UnityEngine;

[CreateAssetMenu(fileName = "Collectible", menuName = "Scriptable Objects/Collectible")]
public class PickupData : ScriptableObject
{
    public new string name;
    public PickupType type = PickupType.None;
    public int points;
}
