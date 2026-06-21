using UnityEngine;
using System.Collections.Generic;

public class AmbulanceController : MonoBehaviour
{
    public List<Transform> waypoints;
    public float speed = 5f;
    private int currentWP = 0;
    private bool isMoving = false;

    void Update()
    {
        if (isMoving && currentWP < waypoints.Count)
        {
            Transform target = waypoints[currentWP];

            // Hareket et
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            // Sadece Y ekseninde dönmesini engellemek için düzgün LookAt
            Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
            transform.LookAt(targetPosition);

            // Hedefe ulaþtý mý?
            if (Vector3.Distance(transform.position, target.position) < 0.5f)
            {
                currentWP++;
            }
        }
    }

    public void StartAmbulance()
    {
        isMoving = true;
    }
}