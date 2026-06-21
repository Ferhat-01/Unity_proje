using UnityEngine;

public class ListCanvasChildren
{
    public static void Execute()
    {
        GameObject canvas = GameObject.Find("PokemonCanvas");
        if (canvas != null)
        {
            foreach (Transform child in canvas.transform)
            {
                Debug.Log("Child: " + child.name + " Active: " + child.gameObject.activeSelf);
            }
        }
    }
}
