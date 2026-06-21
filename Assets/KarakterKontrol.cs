
using UnityEngine;
using UnityEngine.InputSystem; // Yeni giriþ sistemini koda dahil ettik

public class KarakterKontrol : MonoBehaviour
{
    public CharacterController controller;
    public Transform kamera;
    public Animator oyuncuAnimator; // Supercyan animasyonlarýný tetiklemek için ekledik

    public float yurusHizi = 5f;
    public float fareHassasiyeti = 0.1f; // Yeni sistemde fare deðerleri daha büyük geldiði için hassasiyeti düþürdük

    private float xDonusu = 0f;
    private Vector3 yercekimiHizi;

    void Start()
    {
        // Fareyi ekrana kilitler ve gizler
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 1. FARE ÝLE ETRAFA BAKMA
        Vector2 fareHareketi = Mouse.current.delta.ReadValue() * fareHassasiyeti;
        float fareX = fareHareketi.x;
        float fareY = fareHareketi.y;

        xDonusu -= fareY;
        xDonusu = Mathf.Clamp(xDonusu, -90f, 90f); // Kafayý çok yukarý/aþaðý eðmeyi engeller

        kamera.localRotation = Quaternion.Euler(xDonusu, 0f, 0f);
        transform.Rotate(Vector3.up * fareX);

        // 2. KLAVYE ÝLE HAREKET (WASD / Yön Tuþlarý Hatalarý Düzeltildi)
        float x = 0f;
        float z = 0f;

        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) z = 1f;
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) z = -1f;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) x = 1f;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) x = -1f;

        Vector3 hareket = transform.right * x + transform.forward * z;
        controller.Move(hareket.normalized * yurusHizi * Time.deltaTime);

        // 3. ANÝMASYON KONTROLÜ (Supercyan MoveSpeed Parametresi)
        if (oyuncuAnimator != null)
        {
            // Eðer herhangi bir hareket tuþuna basýlýyorsa yürüme animasyonuna (1) geç
            if (x != 0 || z != 0)
            {
                oyuncuAnimator.SetFloat("MoveSpeed", 1f);
            }
            else
            {
                oyuncuAnimator.SetFloat("MoveSpeed", 0f); // Tuþ býrakýlýnca durma (Idle) animasyonuna dön
            }
        }

        // 4. BASÝT YERÇEKÝMÝ
        if (controller.isGrounded && yercekimiHizi.y < 0)
        {
            yercekimiHizi.y = -2f;
        }
        yercekimiHizi.y += -9.81f * Time.deltaTime;
        controller.Move(yercekimiHizi * Time.deltaTime);
    }
}
