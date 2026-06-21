using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ToplanmaAlani : MonoBehaviour
{
    private bool gecisBasladi = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && TelefonGorevi.aramaYapildi == true && !gecisBasladi)
        {
            StartCoroutine(AmbulansiBekleVeBitir());
        }
    }

    IEnumerator AmbulansiBekleVeBitir()
    {
        gecisBasladi = true;

        // 1. SÜREYİ KAYDET
        if (TimerManager.Instance != null)
        {
            PlayerPrefs.SetFloat("SonSure", TimerManager.Instance.GecenSure);
            PlayerPrefs.Save();
        }

        // Puan artık soru bilindiği an DialogueManager tarafından otomatik kaydediliyor.
        // Bu yüzden burada hata verebilecek arama işlemlerini tamamen temizledik.

        yield return new WaitForSeconds(11f);

        Time.timeScale = 1f;
        SceneManager.LoadScene("BitisEkrani");
    }
}