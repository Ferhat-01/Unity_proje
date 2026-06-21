using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    [Header("Zorunlu Kamera Pozisyonu")]
    [Tooltip("Başka scriptlerin kamerayı bozmasını engeller. X:0, Y:1.5, Z:0.3 göz hizası için idealdir.")]
    public Vector3 cameraOffset = new Vector3(0f, 1.5f, 0.3f);

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Başlangıçta kameranın aşağı/yukarı açısını al ki oyun başlar başlamaz kamera aniden yere bakmasın
        xRotation = transform.localEulerAngles.x;
        if (xRotation > 180f) xRotation -= 360f; // Unity'nin 360 derece bug'ını engelle
    }

    void LateUpdate()
    {
        bool etrafaBakabilirMi = false;

        // >>> GÜNCELLEME: Akıllı Kamera Pozisyon ve Dönüş Kontrolü <<<

        // DURUM 1: Normal oynanış modu (Fare zaten kilitliyse serbestçe dönebilir)
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            etrafaBakabilirMi = true;

            // 🎯 KESİN ÇÖZÜM: Pozisyon sabitlemeyi SADECE normal oyundayken yapıyoruz.
            // Böylece normalde kamerayı animasyonlar bozamaz.
            transform.localPosition = cameraOffset;
        }
        // DURUM 2: Soru/Diyalog ekranı açık (Fare serbest ama oyuncu SAĞ TIK'a basılı tutuyor)
        else if (Input.GetMouseButton(1))
        {
            etrafaBakabilirMi = true;
            Cursor.visible = false; // Sağ tıka basarken ok işaretini gizle ki rahat dönebilsin

            // 🚫 BURADA transform.localPosition DEĞİŞTİRMİYORUZ!
            // Sabitleme satırını burada çalıştırmadığımız için deprem scripti kamerayı özgürce sallayabilecek!
        }
        else
        {
            // Sağ tık bırakıldığında şıklara/butonlara tıklayabilmek için ok işareti geri gelsin
            Cursor.visible = true;
        }

        // Eğer yukarıdaki iki durumdan biri sağlanmıyorsa kamerayı döndürme, pas geç
        if (!etrafaBakabilirMi) return;


        // Zaman durdurulsa bile etkilenmeyen hassasiyet
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.unscaledDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.unscaledDeltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Kameranın dikey dönüşünü zorla uygula
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Gövdenin yatay dönüşünü zorla uygula
        if (playerBody != null)
        {
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }

    void OnDisable()
    {
        // Sağ tık sisteminin çalışabilmesi için bu scriptin asla kapanmaması gerekiyor.
        // Koruma kalkanını her durum için aktif ediyoruz.
        this.enabled = true;
        Debug.LogWarning("MouseLook Koruma Kalkanı: Sağ tık modu için scriptin kapanması engellendi!");
    }
}