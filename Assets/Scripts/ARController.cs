using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARController : MonoBehaviour
{
    public string markerToListen = "hiro";
    public GameObject target;
    public float updateSpeed = 10;
    public float positionThreshold = 0;
    public Text TXT_Tip;

    bool firstTime = true;
    void Start()
    {
        ARWT.Marker.DetectionManager.onMarkerVisible = null;
        ARWT.Marker.DetectionManager.onMarkerVisible += onMarkerVisible;
        ARWT.Marker.DetectionManager.onMarkerLost = null;
        ARWT.Marker.DetectionManager.onMarkerLost += onMarkerLost;
    }

    void onMarkerVisible(ARWT.Marker.MarkerInfo m)
    {
        if (m.name == markerToListen)
        {
            target?.SetActive(true);
            TXT_Tip?.gameObject.SetActive(false);

            if (!firstTime)
            {
                if (Vector3.Distance(m.position, transform.position) > positionThreshold)
                {
                    transform.position = Vector3.Lerp(transform.position, m.position, Time.deltaTime * updateSpeed);
                }
            }
            else
            {
                transform.position = m.position;
                firstTime = false;
            }

            transform.rotation = m.rotation;

            Vector3 absScale = new Vector3(
                Mathf.Abs(m.scale.x),
                Mathf.Abs(m.scale.y),
                Mathf.Abs(m.scale.z)
            );

            transform.localScale = absScale;
        }
    }

    void onMarkerLost(ARWT.Marker.MarkerInfo m)
    {
        if (m.name == markerToListen)
        {
            target?.SetActive(false);
            TXT_Tip?.gameObject.SetActive(true);
            firstTime = true;
        }
    }
}
