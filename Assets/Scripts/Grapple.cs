using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LineRenderer webLineRenderer;
    [SerializeField] private DistanceJoint2D webJoint;
    [SerializeField] private float velocityBoostFactor;
    private Vector2 previousAnchorPosition;
    private Rigidbody2D rb;
    private bool forceApplied = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        webJoint.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("Grappable"))
            {
                Vector2 mousePos = hit.point;
                webLineRenderer.SetPosition(0, mousePos);
                webLineRenderer.SetPosition(1, transform.position);
                webJoint.connectedAnchor = mousePos;
                webJoint.enabled = true;
                webLineRenderer.enabled = true;

                // Save the current anchor position for velocity calculation
                previousAnchorPosition = webJoint.connectedAnchor;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (webJoint.enabled)
            {
                Vector2 currentAnchorPosition = webJoint.connectedAnchor;
                Vector2 velocityBoost = (currentAnchorPosition - previousAnchorPosition) * velocityBoostFactor;
                rb.velocity += velocityBoost;
            }

            webJoint.enabled = false;
            webLineRenderer.enabled = false;
        }

        if (webJoint.enabled)
        {
            webLineRenderer.SetPosition(1, transform.position);
            previousAnchorPosition = webJoint.connectedAnchor;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !forceApplied)
        {
            rb.AddForce(new Vector2(1, 0.35f) * 40f, ForceMode2D.Impulse);
            forceApplied = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
            webJoint.enabled = false;
            webLineRenderer.enabled = false;
        }
    }
}
