using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    private GameObject bg;
    public float cameraSpeed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        bg = GameObject.Find("Background");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = target.position;
        newPosition.z = -18;
        transform.position = Vector3.Slerp(transform.position, newPosition, cameraSpeed * Time.deltaTime);
        
        bg.transform.position = 
            Vector3.Slerp(bg.transform.position, new Vector3(transform.position.x, transform.position.y, bg.transform.position.z), cameraSpeed);
    }
}
