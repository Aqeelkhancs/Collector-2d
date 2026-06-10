using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f;
    private Transform player;
    private bool hasTriggered = false; // avoid multiple triggers

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void Update()
    {
        if (player == null || hasTriggered) return; // stop moving after game over

        Vector2 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            if (GameManager.instance != null)
                GameManager.instance.GameOver();
        }
    }
}