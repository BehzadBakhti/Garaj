using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcernPointSpawner : MonoBehaviour
{


    [SerializeField] private ResourcePoint _resourcePrefab;
    [SerializeField] private List<Disaster> _disasterTypePrefabs;
    [SerializeField] private List<ConcernPoint> _concernPoints;
    

    public Disaster SpawnDisaster()
    {

        int index = Random.Range(0, _disasterTypePrefabs.Count);
        GameObject prefab = _disasterTypePrefabs[index].gameObject;

        ConcernPoint d = null;

        while (d == null)
        {
            d = SpawnConcernPoint(prefab, "disaster");
        }

        return (Disaster)d;
    }

    public ResourcePoint SpawnResource()
    {
        ConcernPoint r = null;

        while (r == null)
        {
            r = SpawnConcernPoint(_resourcePrefab.gameObject, "resource");
        }

        return (ResourcePoint)r;

    }

    private ConcernPoint SpawnConcernPoint(GameObject prefab, string type)
    {
        var center = transform.position;

        var outPoint = GenerateEndPoint();
        Ray ray = new Ray(outPoint, center - outPoint);

        if (Physics.Raycast(ray, out var hit))
        {
            if (!IsAreaClear(hit.point)) return null;

            if (type == "disaster")
            {
                if (hit.collider.CompareTag("Land"))
                {
                    if (prefab.GetComponent<Disaster>().DisasterArea == DisasterArea.Water) return null;

                }
                else if (hit.collider.CompareTag("Water"))
                {
                    if (prefab.GetComponent<Disaster>().DisasterArea == DisasterArea.Land) return null;
                }
            }

            var go = Instantiate(prefab, hit.point, Quaternion.identity);
            go.transform.rotation = Quaternion.FromToRotation(Vector3.up, -ray.direction);
            var cp = go.GetComponent<ConcernPoint>();
            cp.RayOut();
            _concernPoints.Add(cp);

            return cp;

        }

        return null;

    }

    public void RemoveConcernPoint(ConcernPoint cp)
    {
        _concernPoints.Remove(cp);
    }
    private bool IsAreaClear(Vector3 newPoint)
    {
        foreach (var concernPoint in _concernPoints)
        {
            if (Vector3.Distance(newPoint, concernPoint.gameObject.transform.position) < concernPoint.Radius)
                return false;
        }
        return true;
    }

    private Vector3 GenerateEndPoint()
    {

        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 10;

    }


}