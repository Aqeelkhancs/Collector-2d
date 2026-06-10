using UnityEngine;

public class CoinCollectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance == null)
            {
                Debug.LogError("GameManager.instance is NULL! Score won't update.");
            }
            else
            {
                GameManager.instance.AddScore(1);
                Debug.Log("Score should be updated");
            }
            Destroy(gameObject);
        }
    }
}