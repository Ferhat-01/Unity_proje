using UnityEngine;

public class RoomTeleporter : MonoBehaviour
{
    public Transform destinationPoint;

    [Header("Kilit Ayarları")]
    public bool isLocked = false;

    // 1. Durum: Obje "Is Trigger" (Hayalet) ise burası çalışır
    private void OnTriggerEnter(Collider other)
    {
        Teleport(other);
    }

    private void OnTriggerStay(Collider other)
    {
        Teleport(other);
    }

    // 🎯 YENİ EKLENEN: 2. Durum: Obje sert bir duvar/kapı (Is Trigger KAPALI) ise burası çalışır
    private void OnCollisionEnter(Collision collision)
    {
        Teleport(collision.collider);
    }

    private void Teleport(Collider other)
    {
        // Kapı kilitliyse ışınlama yapma
        if (isLocked) return;

        if (other.CompareTag("Player") || other.name == "Player")
        {
            if (destinationPoint != null)
            {
                other.transform.position = destinationPoint.position;

                Rigidbody rb = other.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.position = destinationPoint.position;
                    rb.linearVelocity = Vector3.zero;
                }
            }
            else
            {
                Debug.LogWarning("Destination point not set for " + gameObject.name + ". Please assign it in the Inspector.");
            }
        }
    }
}