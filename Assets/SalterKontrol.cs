using System.Collections;
using UnityEngine;

public class SalterKontrol : MonoBehaviour
{
    private bool oyuncuIceride = false;
    private bool islemYapildi = false;
    private bool gorevTamamlandi = false;

    void Update()
    {
        if (oyuncuIceride && Input.GetKeyDown(KeyCode.F) && !islemYapildi)
        {
            SalteriKapatSistemi();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) { oyuncuIceride = true; }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) { oyuncuIceride = false; }
    }

    void SalteriKapatSistemi()
    {
        islemYapildi = true;

        AudioSource[] tumSesler = FindObjectsOfType<AudioSource>();
        foreach (AudioSource s in tumSesler) { s.Stop(); }

        AudioSource kendiSesimiz = GetComponent<AudioSource>();
        if (kendiSesimiz != null) { kendiSesimiz.Play(); }

        GameObject kapi = GameObject.Find("Dest_Exit");
        if (kapi != null) { kapi.SetActive(false); }

        GameObject[] tumObjeler = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject obje in tumObjeler)
        {
            // Sadece diyalog panellerini kapatıyoruz
            if (obje.name == "DialoguePanel" || obje.name == "DisasterDialoguePanel")
            {
                obje.SetActive(false);
            }

            // 🎯 DEĞİŞİKLİK BURADA: TimerManager ve HUDText'i kapatan kodları sildim.
            // Artık süre ve arayüz şalterden etkilenmeyecek.
        }
    }

    public void GoreviTamamla()
    {
        gorevTamamlandi = true;
        islemYapildi = false;
    }

    public void GoreviBitirVeYaziyiKapat()
    {
        islemYapildi = false;
    }

    void OnGUI()
    {
        if (islemYapildi && !gorevTamamlandi)
        {
            GUIStyle stil = new GUIStyle();
            stil.fontSize = 25;
            stil.normal.textColor = Color.green;
            stil.fontStyle = FontStyle.Bold;

            GUI.Label(new Rect(20, Screen.height - 50, 1000, 100), "GÖREV BAŞARILI: Elektrik kesildi! ŞİMDİ DIŞARI ÇIK TELEFON KULUBESİNE GİT F TUŞUNA BAS VE 112'Yİ ARA!", stil);
        }
    }
}