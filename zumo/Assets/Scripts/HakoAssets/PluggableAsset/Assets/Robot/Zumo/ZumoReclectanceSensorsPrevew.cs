using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hakoniwa.PluggableAsset.Assets.Robot.Zumo
{

public class ZumoReclectanceSensorsPrevew : MonoBehaviour
{
    [SerializeField] private GameObject rawImagePrefab;
    private List<UnityEngine.UI.RawImage> rawImages = new List<UnityEngine.UI.RawImage>();
    private ZumoReflectanceSensor[] sensors;
    private List<Camera> cameras = new List<Camera>();
    // Start is called before the first frame update
    void Start()
    {
        sensors = FindObjectsOfType<ZumoReflectanceSensor>();
        foreach (ZumoReflectanceSensor sensor in sensors)
        {
            cameras.Add(sensor.gameObject.GetComponent<Camera>());
            GameObject rawImage = Instantiate(rawImagePrefab, transform);
            rawImages.Add(rawImage.GetComponent<UnityEngine.UI.RawImage>());
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        foreach (UnityEngine.UI.RawImage rawImage in rawImages)
        {
            rawImage.texture = cameras[i].targetTexture;
            i++;
        }
    }
}

}