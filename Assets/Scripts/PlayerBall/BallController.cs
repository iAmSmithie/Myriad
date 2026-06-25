using UnityEngine;

public class BallController : MonoBehaviour
{
    public PegColour ballColour;
    public int bounceCount = 0;
    public float aoeRadiusSmall;
    public float aoeRadiusLarge;
    public bool isDetonateMode = true;
    private bool hasSlottedIn = false;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasSlottedIn)
        {
            return;
        }
        Peg hitPeg = collision.gameObject.GetComponent<Peg>();

        if (hitPeg == null)
        {
            return;
        }
        bounceCount++;

        //Debug.Log($"HIT: {hitPeg.gameObject.name} | BallColour: {ballColour} | PegColour: {hitPeg.Colour} | Bounce: {bounceCount} | DetonateMode: {isDetonateMode} | Match: {hitPeg.Colour == ballColour}");
        if (!BallManager.Instance.isDetonateMode)
        {
            SlotIn(hitPeg);
            return;
        }


        if (bounceCount >= 3)
        {
            SlotIn(hitPeg);
            return;
        }
        if (BallManager.Instance.isDetonateMode && hitPeg.Colour == ballColour)
        {
            Detonate();
        }

    }

    public void SetColour(PegColour colour)
    {
        ballColour = colour;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        switch (colour)
        {
            case PegColour.Red: sr.color = Color.red; break;
            case PegColour.Blue: sr.color = Color.blue; break;
            case PegColour.Green: sr.color = Color.green; break;
            case PegColour.Yellow: sr.color = Color.yellow; break;
            case PegColour.Purple: sr.color = Color.purple; break;
            case PegColour.Orange: sr.color = Color.orange; break;
        }
    }
    void Detonate()
    {
        //Debug.Log("Detonate() called!");
        float aoeRadius = (bounceCount == 1) ? aoeRadiusSmall : aoeRadiusLarge;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, aoeRadius);
        foreach (Collider2D collider in colliders)
        {
            Peg peg = collider.GetComponent<Peg>();
            if (peg != null && peg.Colour == ballColour)
            {
                peg.TakeHit();
            }
        }
        ChainManager.Instance.RecalculateChainPositions();
        Destroy(gameObject);
    }
    void SlotIn(Peg hitPeg)
    {
        if (hasSlottedIn)
        {
            return;
        }
        hasSlottedIn = true;
        //Debug.Log($"SlotIn called. Final bounce count: {bounceCount}");
        rb.bodyType = RigidbodyType2D.Static;

        Peg newPeg = gameObject.AddComponent<Peg>();
        newPeg.Colour = ballColour;
        Destroy(this);
        ChainManager.Instance.InsertPeg(newPeg, hitPeg);
    }
}
