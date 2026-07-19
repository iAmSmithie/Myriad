using UnityEngine;

public class EffectPeg : MonoBehaviour
{
    public PegEffectData effectData;

    void Start()
    {
        if (effectData != null)
        {
            Debug.Log($"Effect Peg active: {effectData.effectType}, magnitude: {effectData.magnitude}, duration: {effectData.duration}, AOE radius: {effectData.aoeRadius}, isNegative: {effectData.isNegative}");
        }
        else
        {
            Debug.Log("Effect Peg has no effect data assigned.");
        }
    }

}
