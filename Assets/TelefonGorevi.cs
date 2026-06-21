using UnityEngine;
using TMPro;

public class TelefonGorevi : MonoBehaviour
{
    public static bool aramaYapildi = false;
    private bool oyuncuIceride = false;
    private bool arandimi = false;

    [Header("UI Bağlantıları")]
    public GameObject numpadPanel;
    public GameObject gorevYazisiPaneli;

    // 🎯 YENİ: Toplanma görevini duyurmak için bu paneli ekledik
    public GameObject yeniGorevYazisi;

    public AmbulanceController ambulance;

    void Start()
    {
        aramaYapildi = false;
        if (numpadPanel != null) numpadPanel.SetActive(false);
        if (yeniGorevYazisi != null) yeniGorevYazisi.SetActive(false); // Başta gizli
    }

    void Update()
    {
        if (oyuncuIceride && Input.GetKeyDown(KeyCode.F) && !arandimi)
        {
            if (numpadPanel != null)
            {
                numpadPanel.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !arandimi)
        {
            oyuncuIceride = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            oyuncuIceride = false;
            if (numpadPanel != null) numpadPanel.SetActive(false);
        }
    }

    public void GorevTamamlandi()
    {
        // 1. Ambulansı hareket ettir
        if (ambulance != null) ambulance.StartAmbulance();

        // 2. Yeni görevi başlat
        if (yeniGorevYazisi != null) yeniGorevYazisi.SetActive(true);

        arandimi = true;
        aramaYapildi = true;

        if (numpadPanel != null) numpadPanel.SetActive(false);

        // 3. Şalterdeki inatçı yeşil yazıyı kapat
        SalterKontrol salter = Object.FindFirstObjectByType<SalterKontrol>();
        if (salter != null)
        {
            salter.GoreviBitirVeYaziyiKapat();
        }

        // 4. Panel varsa kapat
        if (gorevYazisiPaneli != null)
        {
            gorevYazisiPaneli.SetActive(false);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Debug.Log("GÖREV BAŞARILI: Ambulans çağrıldı, toplanma alanına git!");
    }
}