using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Scriptable Objects/Ability")]
public class Ability : ScriptableObject
{
    public new string name;
    public float cooldownTime;
    public float activeTime;
    public int manaCost;

    public virtual void Activate(GameObject parent, bool charged) { }

    public virtual void BeginCooldown(GameObject parent) { }
}
