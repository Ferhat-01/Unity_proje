using UnityEngine;
using TMPro;

public class PhoneDialer : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    public GameObject numpadPanel;

    private string input = "";

    // Rakamları eklemeye devam edecek
    public void AddNumber(string number)
    {
        if (input.Length < 3)
        {
            input += number;
            displayText.text = input;
        }
    }

    // 🎯 YENİ: Sadece ARA butonuna basıldığında çalışacak
    public void CallNumber()
    {
        if (input == "112")
        {
            Debug.Log("Ambulans çağrıldı!");

            // Görevi bitiren scripti tetikle
            TelefonGorevi gorevScript = Object.FindFirstObjectByType<TelefonGorevi>();
            if (gorevScript != null)
            {
                gorevScript.GorevTamamlandi();
            }

            input = ""; // Temizle
            displayText.text = "";
        }
        else
        {
            // Yanlış numara durumu
            displayText.text = "Hata!";
            input = "";
        }
    }
}