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
    [SerializeField] private int _spawnInterval;
    [SerializeField] private int _initialSpawnChance;
    private int _spawnChance;


    private void Awake()
    {
        _disasters = new List<Disaster>();
        _resourcePoints = new List<ResourcePoint>();
        _spawnChance = _initialSpawnChance;
        InvokeRepeating("Spawn", 5, _spawnInterval);
        
    }

    private void Start()
    {
        UIManager.Instance.MiniGameFinished += Instance_MiniGameFinished;
    }

    private void Instance_MiniGameFinished(bool done)
    {
        if (done)
            ResolveDisaster();
    }

    private void Spawn()
    {
        int chance = Random.Range(0, 100);
        if (chance < _spawnChance)
        {

            //  SpawnResource();
            SpawnDisaster();
            _spawnChance = _initialSpawnChance;
        }
        else
        {
            _spawnChance += 20;
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
        if (_activeDisaster == d)
        {
            _activeDisaster = null;
        }
        ReduceHealth(d.FinalDamage);
        _disasters.Remove(d);
        _Spawner.RemoveConcernPoint(d);
        UnSubscribe(d);
        Destroy(d.gameObject);
        //// Update Amount on UI
    }

    private void ResolveDisaster()
    {
        _disasters.Remove(_activeDisaster);
        _Spawner.RemoveConcernPoint(_activeDisaster);
        UnSubscribe(_activeDisaster);
        Destroy(_activeDisaster.gameObject);
    }

    private void UnSubscribe(Disaster d)
    {
        if(d==null)return;
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

        UIManager.Instance.ShowDisaster(d.DisasterType);
    }

    private void C_Collected(ResourcePoint r)
    {
        _golds += r.GetAmount();

        _resourcePoints.Remove(r);
        _Spawner.RemoveConcernPoint(r);
        Destroy(r.gameObject);


    }

}