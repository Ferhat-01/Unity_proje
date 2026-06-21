using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;

    [Header("UI Bağlantısı")]
    public Text sureText;

    private float gecenSure = 0f;
    private bool oyunBitti = false;

    // 🎯 HATA ÇÖZÜMÜ: Diğer scriptlerin (ToplanmaAlani gibi) süreyi 
    // "okumasına" izin veren ama değiştirmesine izin vermeyen güvenli kapı.
    public float GecenSure
    {
        get { return gecenSure; }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Eğer oyun sahneleri arası geçişte süre sıfırlanmasın istiyorsan 
            // alttaki satırı yorumdan çıkar (DontDestroyOnLoad).
            // DontDestroyOnLoad(gameObject); 
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (!oyunBitti)
        {
            // Eğer oyun duraklatılsa bile sürenin akmasını istersen
            // Time.deltaTime yerine Time.unscaledDeltaTime kullanabilirsin.
            gecenSure += Time.deltaTime;
            SureyiArayuzdeGoster();
        }
    }

    void SureyiArayuzdeGoster()
    {
        if (sureText == null) return;

        int dakika = Mathf.FloorToInt(gecenSure / 60);
        int saniye = Mathf.FloorToInt(gecenSure % 60);

        sureText.text = string.Format("Süre: {0:00}:{1:00}", dakika, saniye);
    }

    public void SureyiDurdur()
    {
        oyunBitti = true;
    }

    public void SureyiDevamEttir()
    {
        oyunBitti = false;
    }
    public int PuanHesapla()
    {
        // 1000 puandan başla, her saniye 10 puan düş (veya kendi formülünü kur)
        int temelPuan = 1000;
        int dusulecekPuan = Mathf.FloorToInt(gecenSure * 10);
        int finalPuan = Mathf.Clamp(temelPuan - dusulecekPuan, 0, 1000);
        return finalPuan;
    }
}