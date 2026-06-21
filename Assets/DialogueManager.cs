
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("Skor ve Can Ayarları")]
    public int puan = 0;
    private int can = 3;

    [Header("Arayüz Bağlantıları")]
    public GameObject dialoguePanel;
    public Text questionText;
    public Button optionAButton;
    public Button optionBButton;
    public Text optionAText;
    public Text optionBText;
    public Text puanText;

    [Header("Kalp Can Sistemi")]
    public GameObject[] canKalpleri;

    private int currentQuestionIndex = 0;
    private int currentRoom = -1;

    private string[][] questions = new string[][]
    {
        new string[] { "Deprem başladığı an ilk ne yapmalısın?", "Çök-Kapan-Tutun yaparken hangisi doğrudur?", "Sarsıntı bitti, odadan çıkarken hangisini almalısın?" },
        new string[] { "Banyoyu su basmaya başladı, elektrik çarpmasını önlemek için ilk ne yapılmalı?", "Sel sırasında ev içinde en tehkileli bölge neresidir?", "Sel suları hızla yükselirse ne yapılmalı?" },
        new string[] { "Ocaktaki tava alev aldı, ilk ne yaparsın?", "Mutfak dumanla dolmaya başladı, odadan nasıl çıkmalısın?", "Yangın tüpünü kullanırken ilk adım ne olmalıdır?" }
    };

    private string[][][] options = new string[][][]
    {
        new string[][] { new string[] { "A) Balkon veya merdivene koşmak", "B) Güvenli bir yerde Çök-Kapan-Tutun yapmak" }, new string[] { "A) Sırtı pencereye dönük şekilde başı korumak", "B) Pencereden dışarıyı izlemek" }, new string[] { "A) Afet Çantasını", "B) Değerli Eşyaları" } },
        new string[][] { new string[] { "A) Suyu kovalarla atmak", "B) Evin ana şalterini kapatmak" }, new string[] { "A) Bodrum katı ve alt zemin odaları", "B) Üst katlar" }, new string[] { "A) Sel sularına karşı yüzmek", "B) Vanaları kapatıp yüksek noktaya çıkmak" } },
        new string[][] { new string[] { "A) Üzerine hemen su dökmek", "B) Tavanın kapağını kapatıp hava ile temasını kesmek" }, new string[] { "A) Dik bir şekilde koşarak", "B) Eğilerek veya emekleyerek dumanın altından" }, new string[] { "A) Doğrudan ateşe sıkmak", "B) Emniyet pimini çekmek" } }
    };

    private int[][] correctAnswers = new int[][] { new int[] { 1, 0, 0 }, new int[] { 1, 0, 1 }, new int[] { 1, 1, 1 } };

    public delegate void RoomAction();
    public RoomAction OnWrongAnswer;
    public RoomAction OnRoomComplete;

    public bool waitingForCrouch = false;

    void Awake() { Instance = this; }

    void Start()
    {
        // 🎯 YENİ: Oyun her sıfırdan başladığında eski skoru temizle ki sıfırdan başlasın
        PlayerPrefs.SetInt("SonSkor", 0);
        PlayerPrefs.Save();

        if (puanText != null) puanText.text = "Puan: " + puan;
        can = canKalpleri.Length;
        for (int i = 0; i < canKalpleri.Length; i++) if (canKalpleri[i] != null) canKalpleri[i].SetActive(true);
        if (optionAButton != null) optionAButton.onClick.AddListener(() => AnswerSelected(0));
        if (optionBButton != null) optionBButton.onClick.AddListener(() => AnswerSelected(1));
    }

    public void StartRoomDialogue(int roomIndex)
    {
        currentRoom = roomIndex;
        currentQuestionIndex = 0;
        if (dialoguePanel != null) dialoguePanel.SetActive(true);

        if (PlayerController.Instance != null) PlayerController.Instance.OyuncuyuKilitle(true);
        ShowQuestion();
    }

    void ShowQuestion()
    {
        if (currentQuestionIndex < 3)
        {
            questionText.text = questions[currentRoom][currentQuestionIndex];
            optionAText.text = options[currentRoom][currentQuestionIndex][0];
            optionBText.text = options[currentRoom][currentQuestionIndex][1];
            optionAButton.gameObject.SetActive(true);
            optionBButton.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            // 🎯 BANYO SORULARI BİTTİĞİNDE
            if (currentRoom == 1)
            {
                questionText.text = "GÜZEL! ŞİMDİ ŞALTERİ 'F' TUŞUNA BASARAK KAPAT!";
                optionAButton.gameObject.SetActive(false);
                optionBButton.gameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                if (PlayerController.Instance != null) PlayerController.Instance.OyuncuyuKilitle(false);
                return;
            }

            // 🎯 MUTFAK SORULARI BİTTİĞİNDE
            if (currentRoom == 2)
            {
                questionText.text = "Yangın tüpünü 'E' ile al ve yangını söndür!";
                optionAButton.gameObject.SetActive(false); optionBButton.gameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false;
                if (PlayerController.Instance != null) PlayerController.Instance.OyuncuyuKilitle(false);
                return;
            }

            // 🎯 YATAK ODASI (ROOM 0) BİTTİĞİNDE
            if (currentRoom == 0)
            {
                GameObject bariyer = GameObject.Find("YatakOdasiBariyer");
                if (bariyer != null)
                {
                    bariyer.SetActive(false);
                    Debug.Log("Yatak odası bitti, bariyer kaldırıldı!");
                }
                else
                {
                    Debug.LogWarning("Sahne içinde 'YatakOdasiBariyer' adında bir obje bulunamadı!");
                }
            }

            dialoguePanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false;
            if (PlayerController.Instance != null) PlayerController.Instance.OyuncuyuKilitle(false);
            OnRoomComplete?.Invoke();
        }
    }

    void AnswerSelected(int optionIndex)
    {
        bool isCorrect = (optionIndex == correctAnswers[currentRoom][currentQuestionIndex]);
        if (isCorrect)
        {
            puan += 100;
            puanText.text = "Puan: " + puan;

            // 🎯 KRİTİK GÜNCELLEME: Puan kazanıldığı salise PlayerPrefs'e kaydediliyor!
            // Böylece paneller kapansa bile puanımız diske güvenle yazılmış oluyor.
            PlayerPrefs.SetInt("SonSkor", puan);
            PlayerPrefs.Save();

            if (GameManager.Instance != null) GameManager.Instance.RightAnswer();
        }
        else
        {
            can -= 1;
            if (can >= 0 && can < canKalpleri.Length) canKalpleri[can].SetActive(false);

            if (can <= 0)
            {
                if (!(currentRoom == 1 && currentQuestionIndex == 2))
                {
                    SceneManager.LoadScene(0);
                    return;
                }
            }
        }

        if (currentRoom == 1 && currentQuestionIndex == 2)
        {
            FindObjectOfType<BathroomZone>()?.SorularBittiOyunuKazan(puan);
        }

        if (currentRoom == 0 && currentQuestionIndex == 0 && isCorrect)
        {
            waitingForCrouch = true;
            questionText.text = "Yatağın yanına git ve 'E'ye basarak çömel!";
            optionAButton.gameObject.SetActive(false); optionBButton.gameObject.SetActive(false);
            if (PlayerController.Instance != null) PlayerController.Instance.OyuncuyuKilitle(false);
        }
        else
        {
            currentQuestionIndex++;
            ShowQuestion();
        }
    }

    public void PlayerCrouched()
    {
        if (waitingForCrouch)
        {
            waitingForCrouch = false;
            StartCoroutine(WaitAndShowNextQuestionRoutine());
        }
    }

    private IEnumerator WaitAndShowNextQuestionRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        currentQuestionIndex++;
        ShowQuestion();
    }

    public void HideKitchenInstruction()
    {
        dialoguePanel.SetActive(false);
        if (PlayerController.Instance != null) PlayerController.Instance.OyuncuyuKilitle(false);
    }
}