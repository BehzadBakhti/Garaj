using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAwayObject : MonoBehaviour
{
    private Vector3 velocity = new Vector3(0, 0);
    private Vector3 targetVelocity = new Vector3(0, 0);
    private float velocityAmount = 1;
    private Vector3 lastMousePosition = new Vector3(0, 0, 0);
    private float changeRate = 3;
    private float lastChangeTime = 0;
    private bool isInited = false;
    private float velocityChangeSpeed = 1;
    public float maxRadius = 0;

    private void Start()
    {
    }

    public void Init(float velocityAmount, float changeRate = 3, float velocityChangeSpeed = 1)
    {
        this.velocityAmount = velocityAmount;
        this.changeRate = changeRate;
        this.velocityChangeSpeed = velocityChangeSpeed;
        targetVelocity = velocity;
        lastMousePosition = Input.mousePosition;
        CheckVelocity();
        isInited = true;
    }

    private void Update()
    {
        if (isInited)
        {
            velocity = Vector2.Lerp(velocity, targetVelocity, Time.deltaTime* 3);

            transform.localPosition += velocity;
            transform.localPosition += (Input.mousePosition - lastMousePosition) / 20;
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);

            Vector2 pos = transform.localPosition;
            ClampPositionToCircle(new Vector3(0, 0, 0), maxRadius, ref pos);
            transform.localPosition = pos;

            CheckVelocity();
        }
    }

    public void CheckVelocity()
    {
        if (Time.time - lastChangeTime >= changeRate)
        {
            lastChangeTime = Time.time;
            targetVelocity = Random.onUnitSphere;
            targetVelocity.x = Mathf.Sign(targetVelocity.x);
            targetVelocity.y = Mathf.Sign(targetVelocity.y);
            targetVelocity.z = Mathf.Sign(targetVelocity.z);
            targetVelocity *= velocityAmount;
        }
    }

    public void ClampPositionToCircle(Vector2 center, float radius, ref Vector2 position)
    {
        // Calculate the offset vector from the center of the circle to our position
        Vector2 offset = position;
        // Calculate the linear distance of this offset vector
        float distance = offset.magnitude;
        if (radius < distance)
        {
            // If the distance is more than our radius we need to clamp
            // Calculate the direction to our position
            Vector2 direction = offset / distance;
            // Calculate our new position using the direction to our old position and our radius
            position = center + direction * radius;
        }
    }
}
