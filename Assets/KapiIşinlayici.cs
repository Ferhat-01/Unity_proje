using UnityEngine;

public class KapiIsinlayici : MonoBehaviour
{
    [Header("Işınlanılacak Nokta (KoridorHedef objesini sürükle)")]
    public Transform hedefNokta;

    private void OnTriggerEnter(Collider other)
    {
        // Kapıya çarpan obje ana karakterimiz mi?
        if (other.CompareTag("Player"))
        {
            // Unity'de karakter kontrolcüleri bazen ışınlanmayı engeller.
            // Bu yüzden önce karakterin fiziğini 1 saliseliğine kapatıp, ışınlayıp geri açıyoruz.
            CharacterController cc = other.GetComponent<CharacterController>();

            if (cc != null) cc.enabled = false; // Varsa kapat

            // Karakteri varış noktasına fırlat
            other.transform.position = hedefNokta.position;
            other.transform.rotation = hedefNokta.rotation; // Karakter hedefin baktığı yöne baksın

            if (cc != null) cc.enabled = true; // Varsa geri aç

            Debug.Log("Karakter başarıyla koridora ışınlandı!");
        }
    }
}