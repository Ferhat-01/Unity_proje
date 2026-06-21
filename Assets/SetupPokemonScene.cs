using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class SetupPokemonScene
{
    public static void Execute()
    {
        // 1. Create UI Canvas
        GameObject canvasObj = new GameObject("PokemonCanvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObj.AddComponent<CanvasScaler>();
        canvasObj.AddComponent<GraphicRaycaster>();

        // Create UI Panel (Background)
        GameObject panelObj = new GameObject("DialoguePanel");
        panelObj.transform.SetParent(canvasObj.transform, false);
        Image panelImage = panelObj.AddComponent<Image>();
        panelImage.color = new Color(0, 0, 0, 0.8f);
        RectTransform panelRect = panelObj.GetComponent<RectTransform>();
        panelRect.anchorMin = new Vector2(0.2f, 0.1f);
        panelRect.anchorMax = new Vector2(0.8f, 0.3f);
        panelRect.offsetMin = Vector2.zero;
        panelRect.offsetMax = Vector2.zero;

        // Create UI Text
        GameObject textObj = new GameObject("DialogueText");
        textObj.transform.SetParent(panelObj.transform, false);
        Text uiText = textObj.AddComponent<Text>();
        uiText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        uiText.color = Color.white;
        uiText.fontSize = 24;
        uiText.alignment = TextAnchor.MiddleCenter;
        RectTransform textRect = textObj.GetComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.offsetMin = new Vector2(10, 10);
        textRect.offsetMax = new Vector2(-10, -10);

        panelObj.SetActive(false);

        // 2. Place Pokemons
        PlacePokemon("Assets/Pokemon_Red.glb", "Pokemon_Red", new Vector3(-4.0f, 0.5f, 5.0f), "Çok açım, mutfakta yiyecek bir şeyler var mı?", panelObj, uiText);
        PlacePokemon("Assets/Pokemon_Brown.glb", "Pokemon_Brown", new Vector3(6.0f, 0.5f, -2.0f), "Çok uykum var, lütfen ışığı kapat...", panelObj, uiText);
        PlacePokemon("Assets/Pokemon_Blue.glb", "Pokemon_Blue", new Vector3(-4.0f, 0.5f, -3.0f), "Su çok güzel, sen de gelsene!", panelObj, uiText);

        Debug.Log("Pokemon setup completed.");
    }

    private static void PlacePokemon(string assetPath, string name, Vector3 position, string dialogue, GameObject uiPanel, Text uiText)
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
        if (prefab == null)
        {
            Debug.LogError("Could not load " + assetPath);
            return;
        }

        GameObject pokemon = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
        pokemon.name = name;
        pokemon.transform.position = position;
        pokemon.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); // Scale down a bit

        // Add Box Collider
        BoxCollider boxCol = pokemon.AddComponent<BoxCollider>();
        boxCol.center = new Vector3(0, 0.5f, 0);
        boxCol.size = new Vector3(1, 1, 1);

        // Add Sphere Collider (Trigger)
        SphereCollider sphereCol = pokemon.AddComponent<SphereCollider>();
        sphereCol.isTrigger = true;
        sphereCol.radius = 3f;

        // Add Dialogue Script
        PokemonDialogue dialogueScript = pokemon.AddComponent<PokemonDialogue>();
        dialogueScript.dialogueText = dialogue;
        dialogueScript.uiPanel = uiPanel;
        dialogueScript.uiText = uiText;
    }
}
