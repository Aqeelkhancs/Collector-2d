using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 8f;
    private Rigidbody2D rb;
    private Vector2 screenMin, screenMax;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.bodyType = RigidbodyType2D.Kinematic;

        // Calculate screen bounds
        Camera cam = Camera.main;
        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;
        screenMin = new Vector2(-halfWidth, -halfHeight);
        screenMax = new Vector2(halfWidth, halfHeight);
    }

    void Update()
    {
        // --- WebGL‑friendly input: detect both arrow keys and WASD ---
        float horizontal = 0f;
        float vertical = 0f;

        // Arrow keys (reliable in WebGL)
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            horizontal = -1f;
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            horizontal = 1f;

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            vertical = -1f;
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            vertical = 1f;

        Vector2 movement = new Vector2(horizontal, vertical).normalized;

        // Move the player
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        // Clamp to screen bounds
        Vector3 clampedPos = transform.position;
        clampedPos.x = Mathf.Clamp(clampedPos.x, screenMin.x, screenMax.x);
        clampedPos.y = Mathf.Clamp(clampedPos.y, screenMin.y, screenMax.y);
        transform.position = clampedPos;

        if (rb != null)
            rb.position = transform.position;
    }
}