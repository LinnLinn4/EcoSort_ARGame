using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class BinSpawner2 : MonoBehaviour
{
    public GameObject[] binPrefabs; // Prefabs of the bins to spawn
    private bool hasSpawned = false;

    void Update()
    {
        if (!hasSpawned)
        {
            // Check if the user has touched the screen
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                // Spawn bins at the position of the touch
                Vector3 touchPosition = GetTouchPosition();
                SpawnBins(touchPosition);

                // Mark as spawned to prevent continuous spawning
                hasSpawned = true;
            }
        }
        // else
        // {
        //     // Check if the user has touched the screen to move the bins
        //     if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        //     {
        //         // Move the bins to the position of the touch
        //         Vector3 touchPosition = GetTouchPosition();
        //         MoveBins(touchPosition);
        //     }
        // }
    }

    Vector3 GetTouchPosition()
    {
        // Raycast from the touch position to detect a plane
        Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // Return the position of the hit point
            return hit.point;
        }
        return Vector3.zero;
    }

    void SpawnBins(Vector3 position)
    {
        // Define the offset between each bin
        float offset = 1.5f;

        // Iterate through the bin prefabs
        for (int i = 0; i < binPrefabs.Length; i++)
        {
            // Calculate the position for this bin
            Vector3 binPosition = position + Vector3.right * (i * offset);

            // Spawn the bin prefab at the calculated position
            Instantiate(binPrefabs[i], binPosition, Quaternion.identity);
        }
    }
}
