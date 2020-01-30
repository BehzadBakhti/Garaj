using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunAwayCircle : MonoBehaviour
{
    public Image bgImage = null;
    public Color notInsideColor = Color.white;
    public Color isInsideColor = Color.white;
    public RunAwayObject Object = null;
    public float radius = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if (PointInsideSphere(Object.transform.position, transform.position, radius))
        {
            bgImage.color = isInsideColor;
        }
        else
        {
            bgImage.color = notInsideColor;
        }
    }


    public bool PointInsideSphere(Vector3 point, Vector3 center, float radius)
    {
        return Vector3.Distance(point, center) < radius;
    }
}
