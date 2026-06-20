using UnityEngine;


public class Peg : MonoBehaviour
{
    public PegColour Colour;
    private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        //ChainManagerGoesHere
        //ScoreGoesHere
        Destroy(gameObject);
    }

}
