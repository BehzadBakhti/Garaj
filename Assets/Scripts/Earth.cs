using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
    [SerializeField] private ConcernPointSpawner _Spawner;

    private List<Disaster> _disasters;
    private List<ResourcePoint> _resourcePoints;
    private Disaster _activeDisaster;
    [SerializeField] private int _date;
    [SerializeField] private int _golds;
    [SerializeField] private int _health;


    private void Awake()
    {
        _disasters = new List<Disaster>();
        _resourcePoints = new List<ResourcePoint>();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnResource();
            SpawnDisaster();
        }
    }

    private void SpawnDisaster()
    {
        var c = _Spawner.SpawnDisaster();
        c.Selected += R_Selected;
        _disasters.Add(c);
    }

    private void SpawnResource()
    {
        var c = _Spawner.SpawnResource();
        c.Collected += C_Collected;
        _resourcePoints.Add(c);
    }

    private void R_Selected(Disaster d)
    {
        _activeDisaster = d;

        /// Open UI for selected
    }

    private void C_Collected(ResourcePoint r)
    {

        CollectResource(r);

    }

    private void CollectResource(ResourcePoint r)
    {
        _golds += r.GetAmount();
        _resourcePoints.Remove(r);
        Destroy(r.gameObject);
        //// Update Amount on UI
    }


}