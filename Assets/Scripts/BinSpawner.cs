using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class BinSpawner : MonoBehaviour
{
    public GameObject[] binPrefabs; // Prefabs of the bins to spawn
    public Transform[] binPositionsPlastic; // Positions for plastic bins
    public Transform[] binPositionsGlass; // Positions for glass bins
    public Transform[] binPositionsPaper; // Positions for paper bins
    public Transform[] binPositionsMetal;
    private bool hasSpawned = false;

    void Update()
    {
        // Check if bins have already been spawned
        if (!hasSpawned)
        {
            // Raycast from the center of the screen to detect a plane
            Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            if (GetComponent<ARRaycastManager>().Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon))
            {
                // Detected a plane, spawn bins at specified positions
                SpawnBins(binPrefabs[0], binPositionsPlastic); // Spawn plastic bins
                SpawnBins(binPrefabs[1], binPositionsGlass); // Spawn glass bins
                SpawnBins(binPrefabs[2], binPositionsPaper); // Spawn paper bins
                SpawnBins(binPrefabs[3], binPositionsMetal);

                // Mark as spawned to prevent continuous spawning
                hasSpawned = true;
            }
        }
    }

    // Function to spawn bins of a specific type at specified positions
    void SpawnBins(GameObject binPrefab, Transform[] binPositions)
    {
        // Get the detected plane
        ARPlane detectedPlane = GetDetectedPlane();
        if (detectedPlane == null)
        {
            Debug.LogError("No detected plane found.");
            return;
        }

        // Offset the bin positions based on the detected plane's position and orientation
        foreach (Transform binPosition in binPositions)
        {
            // Calculate the position offset relative to the detected plane
            Vector3 planeOffset = binPosition.localPosition;

            // Rotate the offset to match the plane's rotation
            Vector3 rotatedOffset = detectedPlane.transform.rotation * planeOffset;

            // Add the rotated offset to the plane's position
            Vector3 finalPosition = detectedPlane.transform.position + rotatedOffset;

            // Spawn the bin prefab at the adjusted position
            Instantiate(binPrefab, finalPosition, detectedPlane.transform.rotation);
        }
    }

    ARPlane GetDetectedPlane()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        if (GetComponent<ARRaycastManager>().Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.PlaneWithinPolygon))
        {
            foreach (ARRaycastHit hit in hits)
            {
                ARPlane plane = GetComponent<ARPlaneManager>().GetPlane(hit.trackableId);
                if (plane != null && plane.trackingState == TrackingState.Tracking)
                {
                    return plane;
                }
            }
        }
        return null;
    }
}
