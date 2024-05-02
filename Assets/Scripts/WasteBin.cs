using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasteBin : MonoBehaviour
{
    public string binType; // Type of bin (e.g., "Plastic", "Glass", "Paper")
    private int correctDisposals;
    private int targetCorrectDisposals = 12;
    
    
    private void Awake() 
    {
        correctDisposals = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is a waste object
        WasteObject wasteObject = other.GetComponent<WasteObject>();
        Debug.Log(wasteObject.wasteType);
        Debug.Log(binType);
        if (wasteObject != null)
        {
            AudioHandler.Instance.DropSfx();
            // Check if the waste object matches the bin type
            if (wasteObject.wasteType == binType)
            {
                // Increment the score by the points per correct disposal
                GameManager.Instance.UpdateScore();
            }

            correctDisposals++;
            // Destroy the waste object regardless of whether it's disposed correctly or not
            Destroy(wasteObject.gameObject);

            if (correctDisposals == targetCorrectDisposals)
            {
                GameManager.Instance.CheckGameResult();
            }
        }
    }
}
