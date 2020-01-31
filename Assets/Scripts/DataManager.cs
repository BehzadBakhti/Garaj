using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance = null;
    public int earthInitialHealth = 0;
    

    public List<DisasterInfo> Infos = null;
    public Dictionary<DisasterType, DisasterInfo> DisasterInfos = new Dictionary<DisasterType, DisasterInfo>();

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        for (int i = 0; i < Infos.Count; i++)
        {
            DisasterInfos.Add(Infos[i].type, Infos[i]);
        }
    }

    public DisasterInfo GetInfo(DisasterType type)
    {
        DisasterInfo info = null;
        if (DisasterInfos.TryGetValue(type, out info))
            return info;
        return null;
    }
}