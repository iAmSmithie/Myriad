using UnityEngine;


public class Peg : MonoBehaviour
{
    public PegColour Colour;
    private SpriteRenderer spriteRenderer;
    public int damageValue;
    public int pointValue = 100;

    void Start()
    {
        SetColour(Colour);
    }
    public void SetColour(PegColour colour)
    {
        Colour = colour;
        spriteRenderer = GetComponent<SpriteRenderer>();
        switch (Colour)
        {
            case PegColour.Red:
                spriteRenderer.color = Color.red;
                break;
            case PegColour.Blue:
                spriteRenderer.color = Color.blue;
                break;
            case PegColour.Green:
                spriteRenderer.color = Color.green;
                break;
            case PegColour.Yellow:
                spriteRenderer.color = Color.yellow;
                break;
            case PegColour.Purple:
                spriteRenderer.color = Color.purple;
                break;
            case PegColour.Orange:
                spriteRenderer.color = Color.orange;
                break;
        }
    }

    public void TakeHit(bool wasDetonated = false)
    {
        EffectPeg effectPeg = GetComponent<EffectPeg>();
        if (effectPeg != null && effectPeg.effectData != null)
        {
            float durationMultiplier = wasDetonated ? 2f : 1f; // Double the duration if detonated
            EffectManager.Instance.ApplyEffect(effectPeg.effectData, durationMultiplier);
            //Debug.Log($"Effect triggered: {effectPeg.effectData.effectType} | Duration multiplier: {durationMultiplier}");
        }
        ChainManager.Instance.RemovePeg(this);
        ScoreManager.Instance.AddScore(pointValue);
        Destroy(gameObject);
    }

}
