using UnityEngine;

public class DropMechanic : MonoBehaviour
{
    [Header("Tüpü Tuttuðun Obje (ItemHolder)")]
    // Karakterinin elini veya tüpü tuttuðu o görünmez noktayý buraya baðlayacaðýz.
    public Transform itemHolder;

    void Update()
    {
        // Klavyeden 'G' tuþuna basýldýysa VE itemHolder'ýn içinde bir obje varsa çalýþýr.
        if (Input.GetKeyDown(KeyCode.G) && itemHolder.childCount > 0)
        {
            DropObject();
        }
    }

    void DropObject()
    {
        // 1. Eldeki objeyi tespit et: itemHolder'ýn altýndaki 0. (ilk) objeyi alýr.
        Transform heldItem = itemHolder.GetChild(0);

        // 2. Baðlantýyý kopar: Objenin karakterle olan ebeveyn (parent) baðýný siler.
        // Artýk karakter nereye giderse tüp oraya gitmeyecek, serbest kalacak.
        heldItem.SetParent(null);

        // 3. Fizik motorunu bul: Tüpün üzerindeki Rigidbody bileþenine ulaþýyoruz.
        Rigidbody rb = heldItem.GetComponent<Rigidbody>();

        // Eðer tüpün üzerinde Rigidbody varsa (ki fotoðraflarýnda var)...
        if (rb != null)
        {
            // isKinematic = false yaparak fizik hesaplamalarýný (çarpýþmalarý) aktif ediyoruz.
            rb.isKinematic = false;

            // Yerçekimini açýyoruz ki obje havada asýlý kalmasýn, yere düþsün.
            rb.useGravity = true;

            // Tüp yere düþerken karakterin ayaklarýna çarpýp takýlmasýn diye,
            // itemHolder'ýn baktýðý yöne (forward) doðru küçük bir kuvvetle (3f) fýrlatýyoruz.
            rb.AddForce(itemHolder.forward * 3f, ForceMode.Impulse);
        }

        Debug.Log("Obje yere býrakýldý!");
    }
}