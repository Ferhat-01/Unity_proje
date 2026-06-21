using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

public class SetupSceneUpdate2
{
    public static void Execute()
    {
        // 1. Update Camera FOV
        GameObject mainCameraObj = GameObject.Find("Player/Main Camera");
        if (mainCameraObj != null)
        {
            Camera cam = mainCameraObj.GetComponent<Camera>();
            if (cam != null)
            {
                cam.fieldOfView = 80f;
            }
        }

        // 2. Disable Carpet Colliders
        string[] carpetNames = new string[] { "House/Bath Carpet", "House/Carpet1", "House/Carpet2", "House/Carpet 002" };
        foreach (string path in carpetNames)
        {
            GameObject carpet = GameObject.Find(path);
            if (carpet != null)
            {
                Collider[] colliders = carpet.GetComponents<Collider>();
                foreach (Collider col in colliders)
                {
                    col.enabled = false;
                }
            }
        }

        // 3. Setup Doors for Teleportation
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
                // Remove MeshCollider if it exists
                MeshCollider mc = door.GetComponent<MeshCollider>();
                if (mc != null)
                {
                    GameObject.DestroyImmediate(mc);
                }

                // Add BoxCollider and set as Trigger
                BoxCollider bc = door.GetComponent<BoxCollider>();
                if (bc == null)
                {
                    bc = door.AddComponent<BoxCollider>();
                }
                bc.isTrigger = true;

                // Add RoomTeleporter script
                RoomTeleporter rt = door.GetComponent<RoomTeleporter>();
                if (rt == null)
                {
                    rt = door.AddComponent<RoomTeleporter>();
                }
            }
        }

        // 4. Update Animator Controller
        string controllerPath = "Assets/PlayerAnimatorController.controller";
        AnimatorController controller = AssetDatabase.LoadAssetAtPath<AnimatorController>(controllerPath);
        if (controller != null)
        {
            // Add parameter if not exists
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

            // Add Crouch state
            AnimatorStateMachine rootStateMachine = controller.layers[0].stateMachine;
            
            AnimatorState crouchState = null;
            foreach (var childState in rootStateMachine.states)
            {
                if (childState.state.name == "Crouch")
                {
                    crouchState = childState.state;
                    break;
                }
            }

            if (crouchState == null)
            {
                crouchState = rootStateMachine.AddState("Crouch");
                // Assign idle animation to crouch state so it has a visual representation
                AnimationClip idleClip = AssetDatabase.LoadAssetAtPath<AnimationClip>("Assets/Supercyan Character Pack Free Sample/Animations/common_people@idle.FBX");
                crouchState.motion = idleClip;
                crouchState.speed = 0.5f; // Make it look slightly different from normal idle
            }
        }

        Debug.Log("Scene update 2 completed successfully.");
    }
}
