using UnityEngine;
[CreateAssetMenu]
public class DisasterInfo:ScriptableObject
{
    public DisasterType type;
    public Sprite icon = null;
    public string desc = "";
    public int damagePerSecond;
    public int finalDamage;
    public int LifeTime;
    public int Frequency;

    public float minigame_velocity = 2;
    public float minigame_changeRate = 1.5f;
    public float minigame_changeSpeed = 3;
}