using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

public class SetupPlayer
{
    public static void Execute()
    {
        // 1. Delete existing Player
        GameObject existingPlayer = GameObject.Find("Player");
        if (existingPlayer != null)
        {
            GameObject.DestroyImmediate(existingPlayer);
        }

        // 2. Instantiate prefab
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Supercyan Character Pack Free Sample/Prefabs/Base/High Quality/FreeSample_male_1.prefab");
        if (prefab == null)
        {
            Debug.LogError("Prefab not found!");
            return;
        }

        GameObject player = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
        player.name = "Player";
        player.transform.position = new Vector3(0, 0, 0);

        // 3. Add CapsuleCollider
        CapsuleCollider collider = player.AddComponent<CapsuleCollider>();
        collider.center = new Vector3(0, 1f, 0);
        collider.height = 2f;
        collider.radius = 0.3f;

        // 4. Add Rigidbody
        Rigidbody rb = player.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        // 5. Add PlayerController
        player.AddComponent<PlayerController>();

        // 6. Create Animator Controller
        string controllerPath = "Assets/PlayerAnimatorController.controller";
        AnimatorController controller = AnimatorController.CreateAnimatorControllerAtPath(controllerPath);

        // Load Animation Clips
        AnimationClip idleClip = AssetDatabase.LoadAssetAtPath<AnimationClip>("Assets/Supercyan Character Pack Free Sample/Animations/common_people@idle.FBX");
        AnimationClip walkClip = AssetDatabase.LoadAssetAtPath<AnimationClip>("Assets/Supercyan Character Pack Free Sample/Animations/common_people@walk.FBX");
        AnimationClip jumpClip = AssetDatabase.LoadAssetAtPath<AnimationClip>("Assets/Supercyan Character Pack Free Sample/Animations/common_people@jump-up.FBX");

        if (idleClip == null || walkClip == null || jumpClip == null)
        {
            Debug.LogError("One or more animation clips not found!");
        }

        // Add states
        AnimatorStateMachine rootStateMachine = controller.layers[0].stateMachine;
        
        AnimatorState idleState = rootStateMachine.AddState("Idle");
        idleState.motion = idleClip;

        AnimatorState walkState = rootStateMachine.AddState("Walk");
        walkState.motion = walkClip;

        AnimatorState jumpState = rootStateMachine.AddState("Jump");
        jumpState.motion = jumpClip;

        rootStateMachine.defaultState = idleState;

        // 7. Assign Animator Controller
        Animator animator = player.GetComponentInChildren<Animator>();
        if (animator != null)
        {
            animator.runtimeAnimatorController = controller;
        }
        else
        {
            Debug.LogError("Animator not found on player!");
        }

        Debug.Log("Player setup completed successfully.");
    }
}
