using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [Header("Hareket Ayarlari")]
    public float moveSpeed = 3f;
    public float jumpForce = 4f;
    public float groundCheckDistance = 0.4f;
    public LayerMask groundMask;

    public bool canMove = true;

    [Header("1. Şahıs Kamera Çömelme Ayarları")]
    public Transform playerCamera;
    public float standCameraHeight = 1.3f;
    public float crouchCameraHeight = 0.6f;
    public float cameraSmoothSpeed = 10f;

    [Header("Hile Modu (Hayalet Modu)")]
    public bool hayaletModuAktif = false;

    private Rigidbody rb;
    private Animator animator;
    private CapsuleCollider capsuleCollider;
    private bool isGrounded;
    private string currentState;

    private float originalHeight = 1.5f;
    private Vector3 originalCenter = new Vector3(0, 0.75f, 0);
    private float crouchHeight = 0.8f;
    private float originalSpeed;
    private bool isCrouching = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        rb.useGravity = true;

        capsuleCollider.height = originalHeight;
        capsuleCollider.center = originalCenter;
        capsuleCollider.radius = 0.20f;

        originalSpeed = moveSpeed;

        if (playerCamera == null)
        {
            playerCamera = GetComponentInChildren<Camera>().transform;
        }
    }

    void Update()
    {
        if (!canMove) return;

        // 🎯 GÜNCELLEME: Çakışmayı önlemek için hile tuşu 'G' yerine 'H' yapıldı.
        // Böylece tüpü atarken/bırakırken yanlışlıkla hayalet moduna girmeyeceksin!
        if (Input.GetKeyDown(KeyCode.H))
        {
            hayaletModuAktif = !hayaletModuAktif;
            rb.useGravity = !hayaletModuAktif;
            capsuleCollider.isTrigger = hayaletModuAktif;
            if (hayaletModuAktif) rb.linearVelocity = Vector3.zero;
        }

        if (hayaletModuAktif)
        {
            HandleGhostMovement();
        }
        else
        {
            CheckGrounded();
            HandleCrouch();
            HandleMovement();
            HandleJump();
            UpdateAnimator();
            HandleCameraHeight();
        }
    }

    public void OyuncuyuKilitle(bool kilitle)
    {
        canMove = !kilitle;
        if (kilitle)
        {
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
        else
        {
            if (capsuleCollider != null) capsuleCollider.isTrigger = false;
            if (rb != null) rb.useGravity = true;
        }
    }

    void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ComelmeDurumunuAyarla(!isCrouching);
        }
    }

    public void ComelmeDurumunuAyarla(bool comeldiMi)
    {
        isCrouching = comeldiMi;
        capsuleCollider.height = isCrouching ? crouchHeight : originalHeight;
        capsuleCollider.center = isCrouching ? new Vector3(originalCenter.x, crouchHeight / 2f, originalCenter.z) : originalCenter;
        moveSpeed = isCrouching ? originalSpeed / 2f : originalSpeed;

        if (isCrouching && DialogueManager.Instance != null)
        {
            DialogueManager.Instance.PlayerCrouched();
        }
    }

    void HandleCameraHeight()
    {
        if (playerCamera == null) return;
        float targetY = isCrouching ? crouchCameraHeight : standCameraHeight;
        Vector3 localPos = playerCamera.localPosition;

        localPos.y = Mathf.Lerp(localPos.y, targetY, Time.unscaledDeltaTime * cameraSmoothSpeed);
        playerCamera.localPosition = localPos;
    }

    void HandleGhostMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        Vector3 moveDir = transform.right * moveX + transform.forward * moveZ;
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space)) transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftControl)) transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }

    void CheckGrounded()
    {
        Vector3 origin = transform.position + Vector3.up * 0.1f;
        isGrounded = Physics.Raycast(origin, Vector3.down, groundCheckDistance, groundMask, QueryTriggerInteraction.Ignore);
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        Vector3 move = (transform.right * moveX + transform.forward * moveZ).normalized;

        Vector3 targetVelocity = move * moveSpeed;
        targetVelocity.y = rb.linearVelocity.y;
        rb.linearVelocity = targetVelocity;
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isCrouching)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void UpdateAnimator()
    {
        if (animator == null) return;
        float speed = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z).magnitude;
        string newState = !isGrounded ? "Jump" : (isCrouching ? "Crouch" : (speed > 0.1f ? "Walk" : "Idle"));

        if (newState != currentState)
        {
            animator.CrossFade(newState, 0.1f);
            currentState = newState;
        }
    }
}