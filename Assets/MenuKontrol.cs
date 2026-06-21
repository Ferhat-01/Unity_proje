using UnityEngine;
using UnityEngine.SceneManagement; // Sahneler arasý geçiþ için bu kütüphane þart!

public class MenuKontrol : MonoBehaviour
{
    public void OyunaBasla()
    {
        // Týrnak içindeki kýsma, kendi oyun sahnelerinin tam ve birebir adýný yazmalýsýn!
        // Örneðin asýl oyununun olduðu sahnenin adý "SampleScene" veya "Level1" ise onu yaz.
        SceneManager.LoadScene("House Interior");
    }

    public void OyundanCik()
    {
        Application.Quit();
        Debug.Log("Oyundan Çýkýldý!"); // Bu mesaj sadece Unity editöründe görünür, oyun build edilince gerçekten kapanýr.
    }
}