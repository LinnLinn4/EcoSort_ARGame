using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class BinSpawner2 : MonoBehaviour
{
    public GameObject[] binPrefabs; // Prefabs of the bins to spawn
    private List<GameObject> spawnedBins = new List<GameObject>();
    private float tapDurationThreshold = 1.0f;

    void Update()
    {
        Touch touch = Input.GetTouch(0);
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            // Get the position of the touch
            Vector3 touchPosition = GetTouchPosition();

            // Check if bins have already been spawned
            if (spawnedBins.Count == 0)
            {
                // Spawn bins at the position of the touch
                SpawnBins(touchPosition);
            }
            else
            {
                if (touch.deltaTime > tapDurationThreshold)
                {
                    // Move the existing bins to the position of the touch
                    MoveBins(touchPosition);
                }
            }
        }
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
        float offset = 0.4f;

        // Iterate through the bin prefabs
        for (int i = 0; i < binPrefabs.Length; i++)
        {
            // Calculate the position for this bin
            Vector3 binPosition = position + Vector3.right * (i * offset);

            GameObject newBin = Instantiate(binPrefabs[i], binPosition, Quaternion.identity);
            newBin.transform.Rotate(0, 180, 0);
            spawnedBins.Add(newBin);
        }
    }

    void MoveBins(Vector3 position)
    {
        // Define the offset between each bin (should be the same as during spawning)
        float offset = 0.4f;

        // Iterate through the spawned bins and move them to the new position
        for (int i = 0; i < spawnedBins.Count; i++)
        {
            // Calculate the new position for this bin
            Vector3 binPosition = position + Vector3.right * (i * offset);

            // Move the bin to the calculated position
            spawnedBins[i].transform.position = binPosition;
        }
    }
}
