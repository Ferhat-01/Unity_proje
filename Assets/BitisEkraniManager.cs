using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BitisEkraniManager : MonoBehaviour
{
    [Header("Skor Yazýlarý")]
    public Text txtGuncelSkor;
    public Text txtRekorSkor;

    [Header("Süre Yazýlarý")]
    public Text txtSure;
    public Text txtEnIyiSure;

    void Start()
    {
        // --- KRÝTÝK DÜZELTMELER ---
        // 1. Zamanýn donuk kalmamasýný garantile (Butonlarýn basmasý için þart)
        Time.timeScale = 1f;

        // 2. Fare imlecini görünür ve serbest yap (Ekranda týklama yapabilmek için þart)
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // --------------------------

        // 1. Verileri Hafýzadan Çekme
        int guncelSkor = PlayerPrefs.GetInt("SonSkor", 0);
        int rekorSkor = PlayerPrefs.GetInt("EnYuksekSkor", 0);
        float guncelSure = PlayerPrefs.GetFloat("SonSure", 0f);

        // REKOR ÝÇÝN GÜNCELLEME: Baþlangýç deðerini çok yüksek tut (1 saat = 3600 saniye)
        float enIyiSure = PlayerPrefs.GetFloat("EnIyiSure", 3600f);

        // 2. REKOR MANTIÐI
        if (guncelSkor > rekorSkor)
        {
            rekorSkor = guncelSkor;
            PlayerPrefs.SetInt("EnYuksekSkor", rekorSkor);
        }

        if (guncelSure > 0f && guncelSure < enIyiSure)
        {
            enIyiSure = guncelSure;
            PlayerPrefs.SetFloat("EnIyiSure", enIyiSure);
        }

        PlayerPrefs.Save();

        // 3. EKRANA YAZDIRMA (Null kontrolleri eklendi ki boþ referans varsa oyun çökmesin)
        if (txtGuncelSkor != null) txtGuncelSkor.text = guncelSkor.ToString();
        if (txtRekorSkor != null) txtRekorSkor.text = rekorSkor.ToString();
        if (txtSure != null) txtSure.text = SureyiFormataCevir(guncelSure);

        if (txtEnIyiSure != null)
        {
            if (enIyiSure >= 3600f)
            {
                txtEnIyiSure.text = "--:--";
            }
            else
            {
                txtEnIyiSure.text = SureyiFormataCevir(enIyiSure);
            }
        }
    }

    string SureyiFormataCevir(float sure)
    {
        int dakika = Mathf.FloorToInt(sure / 60);
        int saniye = Mathf.FloorToInt(sure % 60);
        return string.Format("{0:00}:{1:00}", dakika, saniye);
    }

    public void YenidenOynaButonu()
    {
        SceneManager.LoadScene("House Interior"); // Oyun sahnesinin tam adý olduðundan emin ol
    }

    public void AnaMenuButonu()
    {
        SceneManager.LoadScene("AnaMenu"); // Ana menü sahnesinin tam adý olduðundan emin ol
    }
}