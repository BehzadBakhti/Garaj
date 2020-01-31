using System;
using UnityEngine;

public class Disaster : ConcernPoint
{
    public event Action<Disaster> Selected;
    public event Action<int> Damage;
    public event Action<int> FinalDamage;
    [SerializeField] private int _damagePerSecond;
    [SerializeField] private int _finalDamage;
    [SerializeField] private int _lifeTime;
    public DisasterArea DisasterArea;
    public DisasterType DisasterType;


    protected override void Select()
    {
        Selected?.Invoke(this);
    }
}

public enum DisasterArea
{
    Water,
    Land
}

public enum DisasterType
{
    OilLeakage,
    Fire,
    NuclearPowerPlant,
    NuclearTest
}