using UnityEngine;
using UnityEditor;

public class SetupSceneUpdate
{
    public static void Execute()
    {
        // 1. Update Player Collider
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            CapsuleCollider collider = player.GetComponent<CapsuleCollider>();
            if (collider != null)
            {
                collider.radius = 0.20f;
                collider.height = 1.5f;
                collider.center = new Vector3(0, 0.75f, 0);
            }
        }

        // 2. Update Main Camera (FP)
        GameObject mainCameraObj = GameObject.Find("Player/Main Camera");
        if (mainCameraObj == null) mainCameraObj = GameObject.Find("Main Camera");
        
        if (mainCameraObj != null)
        {
            mainCameraObj.transform.SetParent(player.transform);
            // Move slightly forward (Z=0.15f) and adjust height (Y=1.4f) to avoid seeing inside the head
            mainCameraObj.transform.localPosition = new Vector3(0, 1.4f, 0.15f); 
            mainCameraObj.transform.localRotation = Quaternion.identity;

            Camera mainCam = mainCameraObj.GetComponent<Camera>();
            if (mainCam != null)
            {
                mainCam.rect = new Rect(0f, 0f, 0.5f, 1f);
            }
        }

        // 3. Create Top Down Camera
        GameObject topDownCameraObj = GameObject.Find("TopDownCamera");
        if (topDownCameraObj == null)
        {
            topDownCameraObj = new GameObject("TopDownCamera");
            Camera topCam = topDownCameraObj.AddComponent<Camera>();
            topCam.rect = new Rect(0.5f, 0f, 0.5f, 1f);
        }
        
        // Position it above the house
        topDownCameraObj.transform.position = new Vector3(0, 15f, 0);
        topDownCameraObj.transform.rotation = Quaternion.Euler(90f, 0f, 0f);

        // 4. Disable MeshColliders on Doors
        string[] doorPaths = new string[]
        {
            "House/House 2/Door",
            "House/House 2/Door 001",
            "House/House 2/Door 002",
            "House/House 2/Door 003"
        };

        foreach (string path in doorPaths)
        {
            GameObject door = GameObject.Find(path);
            if (door != null)
            {
                MeshCollider mc = door.GetComponent<MeshCollider>();
                if (mc != null)
                {
                    mc.enabled = false;
                }
            }
        }

        Debug.Log("Scene update completed successfully.");
    }
}
