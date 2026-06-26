using UnityEngine;

public class TrajectoryPreview : MonoBehaviour
{
    LineRenderer lineRenderer;
    float fixedLength = 10f;
    float detectionLength = 10f;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        Vector3 startPoint = transform.position;
        Vector3 aimDirection = transform.up;
        if (Input.GetMouseButton(1))
        {
           lineRenderer.enabled = true; 
        }
        else
        {
            lineRenderer.enabled = false;
        }
        if (!BallManager.Instance.isDetonateMode)
        {
            lineRenderer.positionCount = 2;
            Vector3 endPoint = startPoint + aimDirection * fixedLength;
            lineRenderer.SetPosition(0, startPoint);
            lineRenderer.SetPosition(1, endPoint);
        }
        else
        {
            lineRenderer.positionCount = 3;
            RaycastHit hit = Physics.Raycast(startPoint, aimDirection, out hit, detectionLength);

            if (hit.collider != null)
            {
                Vector2 hitPoint = hit.point;
                Vector2 reflectDirection = Vector2.Reflect(aimDirection, hit.normal);
                Vector2 reflectEndPoint = hitPoint + reflectDirection * fixedLength;

                lineRenderer.SetPosition(0, startPoint);
                lineRenderer.SetPosition(1, hitPoint);
                lineRenderer.SetPosition(2, reflectEndPoint);
            }
            else
            {
                lineRenderer.positionCount = 2;
                Vector3 endPoint = startPoint + aimDirection * fixedLength;
                lineRenderer.SetPosition(0, startPoint);
                lineRenderer.SetPosition(1, endPoint);
                lineRenderer.SetPosition(2, endPoint);
            }
        }
    }
}
