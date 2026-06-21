using UnityEngine;

public class AssemblyArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.name == "Player")
        {
            if (GameManager.Instance != null && !GameManager.Instance.isGameOver)
            {
                GameManager.Instance.GameOver(true);
            }
        }
    }
}
