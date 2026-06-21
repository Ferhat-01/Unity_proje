using UnityEngine;

public class FixDestinations
{
    public static void Execute()
    {
        UpdateDest("Dest_Bedroom", new Vector3(5.0f, 1.5f, -2.0f));
        UpdateDest("Dest_Bathroom", new Vector3(-4.0f, 1.5f, -1.0f));
        UpdateDest("Dest_Kitchen", new Vector3(-4.0f, 1.5f, 3.0f));
        UpdateDest("Dest_Exit", new Vector3(0f, 1.5f, 15.0f));
        
        // Also make sure the bedroom door is a trigger and enabled
        GameObject door = GameObject.Find("House/House 2/Door");
        if (door != null)
        {
            BoxCollider bc = door.GetComponent<BoxCollider>();
            if (bc != null)
            {
                bc.isTrigger = true;
                bc.enabled = true;
            }
            RoomTeleporter rt = door.GetComponent<RoomTeleporter>();
            if (rt != null)
            {
                rt.enabled = true;
            }
        }

        Debug.Log("Destinations updated.");
    }

    static void UpdateDest(string name, Vector3 pos)
    {
        GameObject dest = GameObject.Find(name);
        if (dest != null)
        {
            dest.transform.position = pos;
        }
    }
}
