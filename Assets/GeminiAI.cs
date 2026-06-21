using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;

public class GeminiAI : MonoBehaviour
{
    [Header("Gemini API Key")]
    public string apiKey = "AIzaSyDSOxPLp3cXFE2ogQrVCWkdFLF_smukHDw";

    private string url =
        "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key=";

    public void SendMessageToAI(string message)
    {
        StartCoroutine(SendRequest(message));
    }

    IEnumerator SendRequest(string message)
    {
        string fullUrl = url + apiKey;

        string json = @"
        {
            ""contents"": [{
                ""parts"": [{
                    ""text"": """ + message + @"""
                }]
            }]
        }";

        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        UnityWebRequest request = new UnityWebRequest(fullUrl, "POST");

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("AI Response: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Error: " + request.error);
            Debug.LogError(request.downloadHandler.text);
        }
    }
}