using UnityEngine;

public class BasitIsinlayici : MonoBehaviour
{
    [Header("Işınlanacak Hedef Nokta")]
    public Transform hedefNokta;

    [Header("Güvenlik Kilidi")]
    public bool isLocked = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isLocked) return;

        if (other.GetComponent<PlayerController>() != null)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
            }

            // --- SÜREYİ ZORLA BAŞLATMA EKLEMESİ ---
            // Eğer oyun bir şekilde duraklatılmışsa, ışınlanma anında süreyi tekrar akışa geçir.
            Time.timeScale = 1f;
            // --------------------------------------

            other.transform.position = hedefNokta.position;
            Debug.Log("Karakter başarıyla ışınlandı: " + hedefNokta.name);
        }
    }
}