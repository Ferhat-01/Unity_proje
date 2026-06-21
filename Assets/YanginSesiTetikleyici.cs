using UnityEngine;

public class YanginSesiTetikleyici : MonoBehaviour
{
    [Header("Ateþ Sesini Buraya Sürükle")]
    public AudioSource yanginSesi;

    private bool sesCaldi = false; // Sesin birden fazla kez üst üste çalmasýný engeller

    private void OnTriggerEnter(Collider other)
    {
        // Eðer içinden geçen kiþi Oyuncu ise ve ses daha önce çalmadýysa
        if (other.CompareTag("Player") && !sesCaldi)
        {
            if (yanginSesi != null)
            {
                yanginSesi.Play(); // Sesi baþlat
                sesCaldi = true;   // Tekrar çalmasýný kilitle
                Debug.Log("Oyuncu mutfaða girdi, yangýn sesi baþladý!");
            }
        }
    }
}