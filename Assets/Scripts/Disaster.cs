using System;
using UnityEngine;

public class Disaster : ConcernPoint
{
    public event Action<Disaster> Selected;
    public event Action<int> DamageEarth;
    public event Action<Disaster> FinalDamageEarth;
    public int DamagePerSecond;
    public int FinalDamage;
    public int LifeTime;
    public DisasterArea DisasterArea;
    public DisasterType DisasterType;


    protected override void Select()
    {
        Selected?.Invoke(this);
    }

    private void Start()
    {
     
        InvokeRepeating("Damage", 0, 1);
    }

    private void Damage()
    {
        LifeTime--;
        if (LifeTime <= 0)
        {
            FinalDamageEarth?.Invoke(this);
            return;
        }
        DamageEarth?.Invoke(DamagePerSecond);
    }

    private void OnDestroy()
    {
        CancelInvoke();
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