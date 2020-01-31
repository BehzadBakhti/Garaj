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
        c.DamageEarth += C_DamageEarth;
        c.FinalDamageEarth += C_FinalDamage;
        _disasters.Add(c);
    }

    private void C_FinalDamage(Disaster d)
    {
        ReduceHealth(d.FinalDamage);
        _disasters.Remove(d);
        _Spawner.RemoveConcernPoint(d);
        UnSubscribe(d);
        Destroy(d.gameObject);
        //// Update Amount on UI
    }

    private void UnSubscribe(Disaster d)
    {
       d.Selected -= R_Selected;
       d.DamageEarth -= C_DamageEarth;
       d.FinalDamageEarth -= C_FinalDamage;
    }

    private void C_DamageEarth(int damage)
    {
        ReduceHealth(damage);
    }

    private void ReduceHealth(int damage)
    {
        _health -= damage;
       
        
        if (_health < 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        throw new System.NotImplementedException();
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
    {    _golds += r.GetAmount();
        
        _resourcePoints.Remove(r);
        _Spawner.RemoveConcernPoint(r);
        Destroy(r.gameObject);
        

    }

}