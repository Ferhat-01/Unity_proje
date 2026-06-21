
using UnityEngine;
using System.Collections;

public class DisasterController : MonoBehaviour
{
    public static DisasterController Instance;

    [Header("Deprem Sarsýntý Ayarlarý")]
    public float depremSuresi = 8f;
    public float depremSiddeti = 0.12f;
    public float depremSikligi = 20f;

    public Transform cameraTransform;
    private Vector3 originalCamPos;
    private Coroutine shakeCoroutine;

    [Header("Sel (Su Baskýný) Ayarlarý")]
    public Transform waterPlane;
    private bool isFlooding = false;
    public float floodSpeed = 0.03f;

    // >>> YENÝ: Suyla birlikte yükselecek objelerin referanslarý <<<
    public Transform playerTransform;
    public Transform pokemonTransform;

    [Header("Yangýn Ayarlarý")]
    public ParticleSystem fireParticles;
    public Light fireLight;
    // >>> YENÝ: Kapatýlacak 3D yangýn çýtýrtý sesi <<<
    public AudioSource yanginSesi;

    // YENÝ EKLENENLER: Yangýn caný ve kontrolü
    private float fireHealth = 100f;
    private bool isFireOut = false;

    [Header("Kapý Objeleri")]
    public GameObject bathroomDoor;
    public GameObject kitchenDoor;
    public GameObject exitDoor;

    void Awake() { Instance = this; }

    void Start()
    {
        // Oyun baþýnda kapýlarý kilitliyoruz
        LockDoor(bathroomDoor);
        LockDoor(kitchenDoor);
        LockDoor(exitDoor);
    }

    void LockDoor(GameObject door)
    {
        if (door != null)
        {
            Collider col = door.GetComponent<Collider>();
            if (col != null)
            {
                col.enabled = true;
                col.isTrigger = false; // Ýçinden geçilmez yap
            }
            RoomTeleporter rt = door.GetComponent<RoomTeleporter>();
            if (rt != null) rt.enabled = false; // Iþýnlanmayý kapat
        }
    }

    void UnlockDoor(GameObject door)
    {
        if (door != null)
        {
            Collider col = door.GetComponent<Collider>();
            if (col != null)
            {
                col.enabled = true;
                col.isTrigger = true; // Ýçinden geçilebilir yap
            }
            RoomTeleporter rt = door.GetComponent<RoomTeleporter>();
            if (rt != null) rt.enabled = true; // Iþýnlanmayý aktif et
            Debug.Log(door.name + " isimli kapýnýn kilidi açýldý!");
        }
    }

    void Update()
    {
        if (isFlooding)
        {
            // Yükselme miktarýný bu kare için bir kez hesapla
            float riseAmount = floodSpeed * Time.deltaTime;

            // Space.World kullanýyoruz ki objelerin kendi rotasyonlarý yukarý çýkýþlarýný bozmasýn
            if (waterPlane != null) waterPlane.Translate(Vector3.up * riseAmount, Space.World);
            if (playerTransform != null) playerTransform.Translate(Vector3.up * riseAmount, Space.World);
            if (pokemonTransform != null) pokemonTransform.Translate(Vector3.up * riseAmount, Space.World);
        }
    }

    // 1. ODA: YATAK ODASI (DEPREM)
    public void StartEarthquake()
    {
        if (cameraTransform != null)
        {
            originalCamPos = cameraTransform.localPosition;
            if (shakeCoroutine != null) StopCoroutine(shakeCoroutine);
            shakeCoroutine = StartCoroutine(GercekciSarsintiRoutine());
        }

        if (AudioManager.Instance != null) AudioManager.Instance.PlayEarthquake();

        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.OnWrongAnswer = null;
            DialogueManager.Instance.OnRoomComplete = () =>
            {
                if (shakeCoroutine != null) StopCoroutine(shakeCoroutine);
                if (cameraTransform != null) cameraTransform.localPosition = originalCamPos;
                if (AudioManager.Instance != null) AudioManager.Instance.StopLoop();

                // KAMERA VE MOUSE DÜZELTME EKLENTÝSÝ
                FixCameraAndMouse();

                // Yatak odasý bitince Mutfak kapýsý baðlantýsýný aç
                UnlockDoor(kitchenDoor);
            };
            DialogueManager.Instance.StartRoomDialogue(0);
        }
    }

    private IEnumerator GercekciSarsintiRoutine()
    {
        float gecenSure = 0f;
        float seedX = Random.Range(0f, 100f);
        float seedY = Random.Range(0f, 100f);

        while (gecenSure < depremSuresi)
        {
            gecenSure += Time.deltaTime;
            float x = (Mathf.PerlinNoise(seedX + gecenSure * depremSikligi, 0f) * 2f - 1f) * depremSiddeti;
            float y = (Mathf.PerlinNoise(0f, seedY + gecenSure * depremSikligi) * 2f - 1f) * depremSiddeti;

            float damper = 1.0f - (gecenSure / depremSuresi);
            x *= damper;
            y *= damper;

            if (cameraTransform != null)
                cameraTransform.localPosition = originalCamPos + new Vector3(x, y, 0f);

            yield return null;
        }

        if (cameraTransform != null)
            cameraTransform.localPosition = originalCamPos;
    }

    // 2. ODA: MUTFAK (YANGIN)
    public void StartFire()
    {
        if (fireParticles != null) fireParticles.Play();
        if (fireLight != null) fireLight.enabled = true;
        if (AudioManager.Instance != null) AudioManager.Instance.PlayFire();

        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.OnWrongAnswer = () =>
            {
                if (fireParticles != null) { var main = fireParticles.main; main.startSizeMultiplier *= 1.5f; }
                if (fireLight != null) fireLight.intensity *= 1.5f;
                if (AudioManager.Instance != null && AudioManager.Instance.loopSource != null) AudioManager.Instance.loopSource.volume += 0.2f;
            };

            DialogueManager.Instance.OnRoomComplete = () =>
            {
                // Sorular bittiðinde oyuncuya yönlendirme yapýyoruz
                Debug.Log("Sorular bitti! Þimdi yerdeki yangýn tüpünü (E) al ve ateþi söndür!");

                // KAMERA VE MOUSE DÜZELTME EKLENTÝSÝ
                FixCameraAndMouse();
            };

            DialogueManager.Instance.StartRoomDialogue(2);
        }
    }

    public void ExtinguishFire(float amount)
    {
        if (isFireOut) return;

        // Köpük sýktýkça yangýnýn caný azalýr
        fireHealth -= amount * Time.deltaTime;

        // Yangýn caný azaldýkça alevler görsel olarak küçülür
        if (fireParticles != null)
        {
            var main = fireParticles.main;
            main.startSizeMultiplier = Mathf.Clamp(fireHealth / 100f, 0.1f, 1f);
        }

        // Yangýn tamamen söndüðünde çalýþacaklar
        if (fireHealth <= 0)
        {
            fireHealth = 0;
            isFireOut = true;

            if (fireParticles != null) fireParticles.Stop();
            if (fireLight != null) fireLight.enabled = false;
            if (AudioManager.Instance != null) AudioManager.Instance.StopLoop();

            // Yangýn tamamen bittiðinde 3D çýtýrtý sesini býçak gibi keser
            if (yanginSesi != null)
            {
                yanginSesi.Stop();
            }

            // Kapýlar yangýn söndürülünce açýlýyor!
            UnlockDoor(kitchenDoor);
            UnlockDoor(bathroomDoor);

            Debug.Log("Tebrikler! Yangýn baþarýyla söndürüldü ve kapýlar açýldý.");
        }
    }

    // 3. ODA: BANYO (SEL)
    public void StartFlood()
    {
        isFlooding = true;
        if (AudioManager.Instance != null) AudioManager.Instance.PlayWater();

        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.OnWrongAnswer = () =>
            {
                floodSpeed += 0.1f;
                if (AudioManager.Instance != null && AudioManager.Instance.loopSource != null) AudioManager.Instance.loopSource.volume += 0.2f;
            };
            DialogueManager.Instance.OnRoomComplete = () =>
            {
                isFlooding = false;
                if (AudioManager.Instance != null) AudioManager.Instance.StopLoop();

                // KAMERA VE MOUSE DÜZELTME EKLENTÝSÝ
                FixCameraAndMouse();

                UnlockDoor(bathroomDoor);
                UnlockDoor(exitDoor);
            };
            DialogueManager.Instance.StartRoomDialogue(1);
        }
    }

    // HER ODA BÝTÝMÝNDE ÇAÐRILAN ORTAK KAMERA/MOUSE DÜZELTME FONKSÝYONU
    private void FixCameraAndMouse()
    {
        // 1. Zaman akýþýný ve fare kilitlerini aç
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // 2. Kamera üzerindeki MouseLook'u aktif et
        if (cameraTransform != null)
        {
            MouseLook mouseLook = cameraTransform.GetComponent<MouseLook>();
            if (mouseLook != null) mouseLook.enabled = true;

            // Eðer MouseLook ana karakter nesnesindeyse onu da bul ve aç
            if (cameraTransform.parent != null)
            {
                MouseLook parentMouse = cameraTransform.parent.GetComponent<MouseLook>();
                if (parentMouse != null) parentMouse.enabled = true;

                // --- KARAKTER HAREKET SCRIPTLERINI GERÝ AÇMA BÖLÜMÜ ---
                Behaviour playerCtrl = cameraTransform.parent.GetComponent("PlayerController") as Behaviour;
                if (playerCtrl != null)
                {
                    playerCtrl.enabled = true;
                    Debug.Log("PlayerController baþarýyla geri açýldý!");
                }

                Behaviour karakterKon = cameraTransform.parent.GetComponent("KarakterKontrol") as Behaviour;
                if (karakterKon != null)
                {
                    karakterKon.enabled = true;
                    Debug.Log("KarakterKontrol baþarýyla geri açýldý!");
                }
            }
        }

        // 3. Genel Kontrol: Sahnede gözden kaçan baþka MouseLook varsa onu da uyandýr
        MouseLook backupMouse = Object.FindFirstObjectByType<MouseLook>();
        if (backupMouse != null) backupMouse.enabled = true;

        Debug.Log("Kamera ve Karakter hareket kilitleri tamamen açýldý!");
    }
}