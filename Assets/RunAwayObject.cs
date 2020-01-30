using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAwayObject : MonoBehaviour
{
    public Vector3 velocity = new Vector3(0, 0);
    public Vector3 targetVelocity = new Vector3(0, 0);
    public float velocityAmount = 1;
    private Vector3 lastMousePosition = new Vector3(0, 0, 0);
    private float changeRate = 3;
    private float lastChangeTime = 0;

    private void Start()
    {
        Init();
    }
    public void Init()
    {
        targetVelocity = velocity;
        lastMousePosition = Input.mousePosition;
        lastChangeTime = Time.time;
    }

    private void Update()
    {
        velocity = Vector2.Lerp(velocity, targetVelocity, Time.deltaTime);

        transform.localPosition += velocity;
        transform.localPosition += (Input.mousePosition - lastMousePosition)/20;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
        //lastMousePosition = Input.mousePosition;

        CheckVelocity();
    }

    public void CheckVelocity()
    {
        if (Time.time - lastChangeTime >= changeRate)
        {
            lastChangeTime = Time.time;
            targetVelocity = Random.onUnitSphere * velocityAmount;
        }
    }
}
