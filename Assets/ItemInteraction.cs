using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    public Transform itemHolder;
    public float interactRange = 3f;
    private bool isHoldingItem = false;
    private GameObject heldItem;

    void Update()
    {
        // E Tuşu: Alma
        if (Input.GetKeyDown(KeyCode.E) && !isHoldingItem)
        {
            TryPickupItem();
        }
        // G Tuşu: Elindekini Yere Atma (YENİ EKLENDİ)
        else if (Input.GetKeyDown(KeyCode.G) && isHoldingItem)
        {
            DropItem();
        }
    }

    void TryPickupItem()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactRange))
        {
            if (hit.collider.CompareTag("Extinguisher"))
            {
                PickupItem(hit.collider.gameObject);
            }
        }
    }

    void PickupItem(GameObject item)
    {
        isHoldingItem = true;
        heldItem = item;

        Rigidbody rb = item.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        Collider col = item.GetComponent<Collider>();
        if (col != null) col.enabled = false;

        item.transform.SetParent(itemHolder);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;

        FireExtinguisher fe = item.GetComponent<FireExtinguisher>();
        if (fe != null) fe.enabled = true;

        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.HideKitchenInstruction();
        }
    }

    // 🎯 YENİ EKLENDİ: Tüpü elinden güvenle yere bırakma fonksiyonu
    void DropItem()
    {
        if (heldItem != null)
        {
            // 1. Tüpün söndürme scriptini kapat
            FireExtinguisher fe = heldItem.GetComponent<FireExtinguisher>();
            if (fe != null) fe.enabled = false;

            // 2. Tüpü oyuncunun elinden (Parent) kopar
            heldItem.transform.SetParent(null);

            // 3. Tüpün kendi fiziğini geri aç (Düşebilsin)
            Rigidbody rb = heldItem.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = false;

            // 4. Tüpün kendi çarpışmasını geri aç (Yerin içine girmesin)
            Collider col = heldItem.GetComponent<Collider>();
            if (col != null) col.enabled = true;

            // 5. Elimiz boşaldı
            heldItem = null;
            isHoldingItem = false;
        }
    }
}