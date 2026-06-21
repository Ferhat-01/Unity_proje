using UnityEngine;

public class PokemonCanlandirici : MonoBehaviour
{
    [Header("Oyuncu Etkileþimi")]
    public Transform oyuncu; // Senin karakterinin (Player) referansý
    public float farkEtmeMesafesi = 6f; // Hangi mesafede sana bakmaya baþlayacaðý
    public float donmeHizi = 5f;

    [Header("Nefes Alma (Canlýlýk)")]
    public float nefesHizi = 2f;
    public float nefesMiktari = 0.03f;
    private Vector3 baslangicBoyutu;

    [Header("Zýplama Efekti")]
    public float ziplamaAraligiMin = 3f;
    public float ziplamaAraligiMax = 8f;
    private float sonrakiZiplamaZamani;
    private bool zipliyorMu = false;

    void Start()
    {
        // Oyun baþladýðýndaki orijinal boyutunu kaydediyoruz
        baslangicBoyutu = transform.localScale;
        YeniZiplamaZamaniBelirle();
    }

    void Update()
    {
        // 1. NEFES ALMA EFEKTÝ (Sürekli hafifçe esneme)
        if (!zipliyorMu)
        {
            float nefes = Mathf.Sin(Time.time * nefesHizi) * nefesMiktari;
            transform.localScale = baslangicBoyutu + new Vector3(nefes, nefes, nefes);
        }

        // 2. OYUNCUYA BAKMA (Etkileþim)
        if (oyuncu != null && Vector3.Distance(transform.position, oyuncu.position) <= farkEtmeMesafesi)
        {
            // Oyuncunun yönünü bul
            Vector3 bakisYonu = oyuncu.position - transform.position;
            bakisYonu.y = 0; // Sadece kendi etrafýnda dönsün, yukarý-aþaðý yamulmasýn

            if (bakisYonu != Vector3.zero)
            {
                Quaternion hedefRotasyon = Quaternion.LookRotation(bakisYonu);
                transform.rotation = Quaternion.Slerp(transform.rotation, hedefRotasyon, Time.deltaTime * donmeHizi);
            }
        }

        // 3. ARADA BÝR ZIPLAMA (Zamaný gelince Coroutine tetikler)
        if (Time.time >= sonrakiZiplamaZamani && !zipliyorMu)
        {
            StartCoroutine(ZiplamaAnimasyonu());
            YeniZiplamaZamaniBelirle();
        }
    }

    // Fizik kullanmadan, su yükselmesiyle çakýþmayan güvenli zýplama
    System.Collections.IEnumerator ZiplamaAnimasyonu()
    {
        zipliyorMu = true;
        float gecenSure = 0f;
        float ziplamaSuresi = 0.4f;
        Vector3 baslangicPoz = transform.position;

        // Yukarý çýkýþ (Esneyerek)
        while (gecenSure < ziplamaSuresi / 2)
        {
            transform.Translate(Vector3.up * (2f * Time.deltaTime), Space.World);
            transform.localScale = baslangicBoyutu + new Vector3(-0.1f, 0.2f, -0.1f); // Ýnce uzun
            gecenSure += Time.deltaTime;
            yield return null;
        }

        // Aþaðý iniþ (Baský yiyerek)
        while (gecenSure < ziplamaSuresi)
        {
            transform.Translate(Vector3.down * (2f * Time.deltaTime), Space.World);
            transform.localScale = baslangicBoyutu + new Vector3(0.2f, -0.1f, 0.2f); // Þiþman kýsa
            gecenSure += Time.deltaTime;
            yield return null;
        }

        // Normale dön
        transform.localScale = baslangicBoyutu;
        zipliyorMu = false;
    }

    void YeniZiplamaZamaniBelirle()
    {
        sonrakiZiplamaZamani = Time.time + Random.Range(ziplamaAraligiMin, ziplamaAraligiMax);
    }
}