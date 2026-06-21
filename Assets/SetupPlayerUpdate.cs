using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

public class SetupPlayerUpdate
{
    public static void Execute()
    {
        GameObject player = GameObject.Find("Player");
        if (player == null)
        {
            Debug.LogError("Player not found!");
            return;
        }

        // 1. Freeze Rotation X, Y, Z
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }

        // 2. Update Capsule Collider
        CapsuleCollider collider = player.GetComponent<CapsuleCollider>();
        if (collider != null)
        {
            collider.radius = 0.25f;
            collider.height = 1.8f;
            collider.center = new Vector3(0, 0.9f, 0);
        }

        // 3. Move Main Camera
        GameObject mainCamera = GameObject.Find("Main Camera");
        if (mainCamera != null)
        {
            mainCamera.transform.SetParent(player.transform);
            mainCamera.transform.localPosition = new Vector3(0, 1.6f, 0); // Eye level
            mainCamera.transform.localRotation = Quaternion.identity;

            // Add MouseLook script
            MouseLook mouseLook = mainCamera.GetComponent<MouseLook>();
            if (mouseLook == null)
            {
                mouseLook = mainCamera.AddComponent<MouseLook>();
            }
            mouseLook.playerBody = player.transform;
        }

        // 4. Update Animator Controller to add Crouch parameter
        string controllerPath = "Assets/PlayerAnimatorController.controller";
        AnimatorController controller = AssetDatabase.LoadAssetAtPath<AnimatorController>(controllerPath);
        if (controller != null)
        {
            bool hasCrouch = false;
            foreach (var param in controller.parameters)
            {
                if (param.name == "Crouch")
                {
                    hasCrouch = true;
                    break;
                }
            }

            if (!hasCrouch)
            {
                controller.AddParameter("Crouch", AnimatorControllerParameterType.Bool);
            }
        }

        Debug.Log("Player update completed successfully.");
    }
}
