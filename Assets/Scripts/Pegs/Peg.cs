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

    public void TakeHit()
    {
        ChainManager.Instance.RemovePeg(this);
        ScoreManager.Instance.AddScore(pointValue);
        Destroy(gameObject);
    }

}
