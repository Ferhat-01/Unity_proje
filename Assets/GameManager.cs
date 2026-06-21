using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int health = 3;
    // 🎯 1. DÜZELTME: Başlangıç puanını 100'den 0'a çektik.
    public int score = 0;
    public float timer = 0f;
    public bool isGameOver = false;

    public Text hudText;
    public GameObject gameOverPanel;
    public Text gameOverText;

    void Awake() { Instance = this; }

    void Update()
    {
        if (!isGameOver)
        {
            timer += Time.deltaTime;
            UpdateHUD();
        }
    }

    public void WrongAnswer()
    {
        health--;
        score -= 20; // Yanlışta 20 puan düşer
        UpdateHUD(); // Ekranda güncellenmesi için eklendi

        if (AudioManager.Instance != null) AudioManager.Instance.PlayBuzzer();

        if (health == 1 && AudioManager.Instance != null) AudioManager.Instance.PlayHeartbeat();
        if (health <= 0) GameOver(false);
    }

    // 🎯 2. KRİTİK DÜZELTME: Artık soruları doğru bilince GameManager'ın puanı da artacak!
    public void RightAnswer()
    {
        score += 100; // Puanı 100 artır
        UpdateHUD();  // Ekrandaki ana arayüzü güncelle

        if (AudioManager.Instance != null) AudioManager.Instance.PlayDing();
    }

    void UpdateHUD()
    {
        if (hudText != null)
            hudText.text = $"Can: {health} | Puan: {score} | Süre: {Mathf.FloorToInt(timer)}s";
    }

    public void GameOver(bool success)
    {
        isGameOver = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Verileri hafızaya kalıcı olarak yaz (Artık güncel puan gidecek!)
        PlayerPrefs.SetInt("SonSkor", score);
        PlayerPrefs.SetFloat("SonSure", timer);
        PlayerPrefs.Save();

        if (success)
        {
            if (AudioManager.Instance != null) AudioManager.Instance.PlaySuccess();
            SceneManager.LoadScene("BitisEkrani");
        }
        else
        {
            if (gameOverPanel != null) gameOverPanel.SetActive(true);
            if (gameOverText != null) gameOverText.text = "Oyun Bitti!\nCanınız kalmadı.";
        }
    }

    public void RestartGame() { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
}