using UnityEngine;
using UnityEngine.UI;

public class PokemonDialogue : MonoBehaviour
{
    public int roomIndex; // 0: Bedroom, 1: Bathroom, 2: Kitchen
    private bool isPlayerInRange = false;
    private bool hasTalked = false;
    
    public string dialogueText;
    public Text uiText;
    public GameObject uiPanel;

    void Update()
    {
        if (isPlayerInRange && !hasTalked && Input.GetKeyDown(KeyCode.F))
        {
            hasTalked = true;
            if (uiPanel != null) uiPanel.SetActive(false);
            
            if (DisasterController.Instance != null)
            {
                if (roomIndex == 0) DisasterController.Instance.StartEarthquake();
                else if (roomIndex == 1) DisasterController.Instance.StartFlood();
                else if (roomIndex == 2) DisasterController.Instance.StartFire();
            }
            else
            {
                Debug.LogWarning("DisasterController.Instance is null!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTalked && (other.CompareTag("Player") || other.name == "Player"))
        {
            isPlayerInRange = true;
            if (uiPanel != null && uiText != null)
            {
                uiPanel.SetActive(true);
                uiText.text = "[F] Konuş";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.name == "Player")
        {
            isPlayerInRange = false;
            if (uiPanel != null)
            {
                uiPanel.SetActive(false);
            }
        }
    }
}
