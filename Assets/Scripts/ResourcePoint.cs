using System;
using UnityEngine;

public class ResourcePoint : ConcernPoint
{
    public new event Action<ResourcePoint> Collected;
    [SerializeField] private int _amount;

    public int GetAmount()
    {
        return _amount;
    }


    protected override void Select()
    {
        Collected?.Invoke(this);
    }

}