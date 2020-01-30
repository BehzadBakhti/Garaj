using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcernPointSpawner : MonoBehaviour
{

    [SerializeField] private Collider _surface;

    [SerializeField] private List<ConcernPoint> _concernTypes;


    public void Spawn()
    {
        RaycastHit hit = new RaycastHit();
        var center = transform.position;

        var outPoint = GenerateEndPoint();
        Ray ray = new Ray(outPoint,center-outPoint);
        var lr = gameObject.AddComponent<LineRenderer>();
        lr.SetPosition(0, center);
        lr.SetPosition(1, center + ray.direction * -5);


        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.GetComponent<Collider>() == _surface)
                print(hit.point);
        }
    }

    private Vector3 GenerateEndPoint()
    {
     
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 10;

    }







}