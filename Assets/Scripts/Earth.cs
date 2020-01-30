using UnityEngine;

public class Earth : MonoBehaviour
{
    [SerializeField] private ConcernPointSpawner _disasterSpawner, _resourceSpawner;

    public void Spawn()
    {
        print("Spawn");
        _disasterSpawner.Spawn();
        _resourceSpawner.Spawn();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Spawn();
            print("Space");
        }
    }

}