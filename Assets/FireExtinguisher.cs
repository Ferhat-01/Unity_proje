using UnityEngine; // 🎯 Hatanın sebebi bu satırın eksik olmasıydı, burası çok önemli!

public class FireExtinguisher : MonoBehaviour
{
    [Header("Gereken Obje Baglantilari")]
    public Transform foamOrigin;
    public GameObject foamParticleObject;

    [Header("Ayarlar")]
    public float extinguishPower = 100f;
    public float maxDistance = 25f;

    private bool isUsing = false;
    private ParticleSystem pSystem;
    private AudioSource extSound;

    // 🎯 Obje aktifleştiğinde (oyuncu tüpü eline aldığında) çalışır
    void OnEnable()
    {
        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.HideKitchenInstruction();
        }
    }

    void Awake()
    {
        if (foamParticleObject != null)
        {
            pSystem = foamParticleObject.GetComponent<ParticleSystem>();
            if (pSystem == null) Debug.LogError("HATA: foamParticleObject uzerinde Particle System bulunamadı!");
        }
        else
        {
            Debug.LogError("HATA: foamParticleObject Inspector uzerinde bos birakilmis!");
        }

        extSound = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (foamParticleObject != null)
        {
            foamParticleObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (!isUsing) StartExtinguishing();
            ShootExtinguishRay();
        }
        else
        {
            if (isUsing) StopExtinguishing();
        }
    }

    void ShootExtinguishRay()
    {
        if (foamOrigin == null) return;

        RaycastHit hit;

        if (Physics.Raycast(foamOrigin.position, foamOrigin.forward, out hit, maxDistance))
        {
            Debug.DrawLine(foamOrigin.position, hit.point, Color.red);

            if (hit.collider.CompareTag("Fire"))
            {
                if (DisasterController.Instance != null)
                {
                    DisasterController.Instance.ExtinguishFire(extinguishPower);
                    Debug.Log("HEDEF VURULDU! Ates basariyla sonduruluyor!");
                }
            }
        }
        else
        {
            Debug.DrawRay(foamOrigin.position, foamOrigin.forward * maxDistance, Color.yellow);
        }
    }

    void StartExtinguishing()
    {
        isUsing = true;
        Debug.Log("Sol Tik Basildi - Sondurme Baslatildi.");

        if (foamParticleObject != null)
        {
            foamParticleObject.SetActive(true);
            if (pSystem != null && !pSystem.isPlaying) pSystem.Play();
        }

        if (extSound != null && !extSound.isPlaying) extSound.Play();
    }

    void StopExtinguishing()
    {
        isUsing = false;
        Debug.Log("Sol Tik Birakildi - Sondurme Durduruldu.");

        if (foamParticleObject != null)
        {
            if (pSystem != null) pSystem.Stop();
            foamParticleObject.SetActive(false);
        }

        if (extSound != null) extSound.Stop();
    }
}