using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BathroomZone : MonoBehaviour
{
    [Header("UI Elemanları")]
    public Text sayacText;

    [Header("Ses Efekti")]
    public AudioSource suSesi;

    [Header("Süre Ayarları")]
    private float kalanSure = 30f;
    private bool sureBasladi = false;
    private bool oyuncuYandi = false;
    private bool isFinished = false;

    void Start()
    {
        if (sayacText != null) sayacText.gameObject.SetActive(false);
        if (suSesi != null) suSesi.Stop();
    }

    void Update()
    {
        // !isFinished kontrolü sayesinde şaltere basınca burası artık çalışmayacak
        if (sureBasladi && !oyuncuYandi && !isFinished)
        {
            kalanSure -= Time.deltaTime;

            if (sayacText != null)
            {
                sayacText.text = "Zaman az! " + Mathf.CeilToInt(kalanSure).ToString() + "s";
            }

            if (kalanSure <= 0)
            {
                kalanSure = 0;
                SüreBittiYandın();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !sureBasladi && !isFinished)
        {
            sureBasladi = true;
            if (sayacText != null) sayacText.gameObject.SetActive(true);
            if (suSesi != null) suSesi.Play();
        }
    }

    void SüreBittiYandın()
    {
        if (isFinished) return;
        if (suSesi != null) suSesi.Stop();

        oyuncuYandi = true;
        if (sayacText != null) sayacText.gameObject.SetActive(false); // Yazıyı kapat
        SceneManager.LoadScene(0);
    }

    public void SorularBittiOyunuKazan(int kazanilanPuan)
    {
        if (isFinished) return;

        // 🛑 KRİTİK: Su sesini kes, zamanlayıcıyı durdur ve yazıyı tamamen yok et
        if (suSesi != null) suSesi.Stop();

        isFinished = true;
        sureBasladi = false;

        // Yazıyı gizle VE objeyi tamamen pasif yap ki Update'te bir şey yazamasın
        if (sayacText != null)
        {
            sayacText.text = ""; // İçini boşalt
            sayacText.gameObject.SetActive(false); // Obje kapanınca Update artık çalışamaz
        }

        SalterKontrol salterScript = GameObject.FindObjectOfType<SalterKontrol>();
        if (salterScript != null)
        {
            salterScript.enabled = true;
        }
    }
}