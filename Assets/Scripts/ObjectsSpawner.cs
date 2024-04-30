using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ObjectsSpawner : MonoBehaviour
{
   public GameObject[] objectPrefabs; // Array of prefabs to spawn
    public float spawnInterval = 2f; // Time between each spawn
    private ARRaycastManager raycastManager;
    private float spawnTimer = 0f;

    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;

        // Check if it's time to spawn a new object
        if (spawnTimer >= spawnInterval)
        {
            // Raycast from the center of the screen to detect a plane
            Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            if (raycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon))
            {
                // Randomly select a hit point on the detected plane
                var hitPose = hits[Random.Range(0, hits.Count)].pose;

                // Randomly select one of the object prefabs to spawn
                GameObject objectToSpawn = objectPrefabs[Random.Range(0, objectPrefabs.Length)];

                // Spawn the selected object prefab at the hit pose
                Instantiate(objectToSpawn, hitPose.position, hitPose.rotation);
            }

            // Reset the spawn timer
            spawnTimer = 0f;
        }
    }
}
