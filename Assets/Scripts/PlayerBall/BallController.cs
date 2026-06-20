using UnityEngine;

public class BallController : MonoBehaviour
{
    public PegColour ballColour;
    public int bounceCount = 0;
    public float aoeRadiusSmall;
    public float aoeRadiusLarge;
    public bool isDetonateMode = true;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Peg hitPeg = collision.gameObject.GetComponent<Peg>();

        if (hitPeg == null)
        {
            return; //no peg was hit, exit the function
        }
        bounceCount++;

        Debug.Log($"HIT: {hitPeg.gameObject.name} | BallColour: {ballColour} | PegColour: {hitPeg.Colour} | Bounce: {bounceCount} | DetonateMode: {isDetonateMode} | Match: {hitPeg.Colour == ballColour}");

        if (bounceCount >= 3)
        {
            SlotIn();
        }
        if (isDetonateMode && hitPeg.Colour == ballColour)
        {
            Detonate();
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
                Destroy(peg.gameObject);
                //peg.TakeHit();
            }
        }
        Destroy(gameObject);
    }
    void SlotIn()
    {
        Debug.Log($"SlotIn called. Final bounce count: {bounceCount}");
        rb.simulated = false;
    }
}
