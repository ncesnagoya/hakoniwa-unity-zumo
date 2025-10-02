using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransformPrint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("CameraTransformPrint: " + this.transform.position.x + "," + this.transform.position.y + "," + this.transform.position.z + "," + this.transform.rotation.x + "," + this.transform.rotation.y + "," + this.transform.rotation.z + "," + this.transform.rotation.w);
        
    }
}
