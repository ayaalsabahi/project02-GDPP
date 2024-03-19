using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public static InventoryController S;

    //public Transform targetObject; // The object whose position the sprite will follow
    //public bool followMainCamera = true; // Flag to determine whether to follow the main camera or secondary camera

    //public GameObject possessCamera;
    //public GameObject dollCamera;

    ArrayList playerInventory = new ArrayList();
    public bool hasKey;

    // Start is called before the first frame update
    void Awake()
    {
        S = this; // singleton definition
    }

    private void Start()
    {
        //// Find all GameObjects with the tag "MainCamera"
        //GameObject[] mainCameras = GameObject.FindGameObjectsWithTag("MainCamera");

        //// Loop through all found cameras
        //foreach (GameObject camera in mainCameras)
        //{
        //    // Check the name of each camera and assign aliases accordingly
        //    switch (camera.name)
        //    {
        //        case "PossessCamera":
        //            possessCamera = camera;
        //            break;
        //        case "DollCamera":
        //            dollCamera = camera;
        //            break;
        //        default:
        //            Debug.LogWarning("Unknown camera found with name: " + camera.name);
        //            break;
        //    }
        //}

        //targetObject = this.gameObject.transform;
    }

    private void Update()
    {
        //// Check if we want to follow the main camera or secondary camera
        //Transform cameraToFollow = followMainCamera ? dollCamera.transform : possessCamera.transform;

        //// If the target object is set and camera to follow is not null, update the position
        //if (targetObject != null && cameraToFollow != null)
        //{
        //    // Set the position of the sprite to be aligned with the target object and follow the camera's position
        //    transform.position = new Vector3(targetObject.position.x, targetObject.position.y, cameraToFollow.position.z);
        //}
  
    }

    public void AddObject(string obj)
    {
        playerInventory.Add(obj);
        if(obj == "Key1")
        { 
            hasKey = true;
            Debug.Log(hasKey);
            this.GetComponent<SpriteRenderer>().enabled = true;
        }
    }


}
