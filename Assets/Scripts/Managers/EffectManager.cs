using UnityEngine;
using System.Collections.Generic;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance;

    public List<ActiveEffect> activeEffects = new List<ActiveEffect>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ApplyEffect(PegEffectData effectData, float durationMultiplier)
    {
        ActiveEffect existing = activeEffects.Find(e => e.effectType == effectData.effectType);
        if (existing != null)
        {
            existing.magnitude += effectData.magnitude;
            existing.remainingDuration = effectData.duration * durationMultiplier;
            Debug.Log($"Effect stacked: {existing.effectType} | New magnitude: {existing.magnitude} | Duration reset to: {existing.remainingDuration}");
        }
        else
        {
            ActiveEffect newEffect = new ActiveEffect
            {
                effectType = effectData.effectType,
                magnitude = effectData.magnitude,
                remainingDuration = effectData.duration * durationMultiplier
            };
            activeEffects.Add(newEffect);
            Debug.Log($"New effect applied: {newEffect.effectType} | Magnitude: {newEffect.magnitude} | Duration: {newEffect.remainingDuration}");
        }
    }

    [System.Serializable]
    public class ActiveEffect
    {
        public PegEffectType effectType;
        public float magnitude;
        public float remainingDuration;
    }
}
