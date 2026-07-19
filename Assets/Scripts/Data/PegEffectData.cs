using UnityEngine;

[CreateAssetMenu(fileName = "New Peg Effect Data", menuName = "Peg Effects/Peg Effect Data")]
public class PegEffectData : ScriptableObject
{
    public PegEffectType effectType;
    public float magnitude;
    public float duration;
    public float aoeRadius;
    public Sprite symbol;
    public bool isNegative;
}
