using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ObjectsSpawner : MonoBehaviour
{
   public GameObject[] objectPrefabs; 
    private ARRaycastManager raycastManager;
    private bool hasSpawned = false;

    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (!hasSpawned)
        {
            // Raycast from the center of the screen to detect a plane
            Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
            List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                // Perform a raycast from the screen touch point
                if (raycastManager.Raycast(Input.touches[0].position, raycastHits, TrackableType.PlaneWithinPolygon))
                {
                    // Check if the hit trackable is an ARPlane
                    ARPlane plane = GetDetectedPlane(raycastHits);
                    if (plane != null)
                    {
                        // Get the detected plane's position and size
                        Pose planePose = new Pose(plane.transform.position, plane.transform.rotation);
                        Vector2 planeSize = plane.size;

                        // Define the spawning area around the detected plane
                        float minX = planePose.position.x - planeSize.x / 2;
                        float maxX = planePose.position.x + planeSize.x / 2;
                        float minZ = planePose.position.z - planeSize.y / 2;
                        float maxZ = planePose.position.z + planeSize.y / 2;

                        // Detected a plane, spawn all prefabs
                        foreach (GameObject prefab in objectPrefabs)
                        {
                            // Generate a random point within the spawning area
                            Vector3 randomPoint = new Vector3(Random.Range(minX, maxX), planePose.position.y, Random.Range(minZ, maxZ));

                            // Spawn the object at the random point
                            Instantiate(prefab, randomPoint, Quaternion.identity);
                        }
                        hasSpawned = true;
                    }
                }
            }
        }
    }

    ARPlane GetDetectedPlane(List<ARRaycastHit> hits)
    {
        foreach (var hit in hits)
        {
            ARPlane plane = hit.trackable as ARPlane;
            if (plane != null)
            {
                return plane;
            }
        }
        return null;
    }
}
