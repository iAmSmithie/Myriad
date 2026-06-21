using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject ballPrefab;
    public float launchForce;
    public float fireRate;
    private float nextFireTime;
    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
    }
    void Update()
    {
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mouseWorldPos.y - transform.position.y, mouseWorldPos.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + 1f / fireRate;
            fire();
        }
    }
    void fire()
    {
        GameObject ball = Instantiate(ballPrefab, transform.position, transform.rotation);
        PegColour nextColour = BallManager.Instance.GetNextBallColour();
        BallController ballController = ball.GetComponent<BallController>();
        ballController.SetColour(nextColour);
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        Vector2 direction = (mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        rb.AddForce(direction * launchForce, ForceMode2D.Impulse);
    }
}
