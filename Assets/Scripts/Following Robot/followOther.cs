using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class followOther : MonoBehaviour
{
    public GameObject other;
    public float acceleration = 1f;
    public float TargetDistance = 10;
    public float maxSpeed = 2;

    private float distanceToOther;
    private Vector3 orientationToOther;
    private Vector3 velocity;
    private float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var position = transform.position;
        var position1 = other.transform.position;
        distanceToOther = Vector3.Distance(position, position1);
        orientationToOther = Vector3.Normalize(position1 - position);

        if (distanceToOther > TargetDistance + 2)
        {
            velocity += orientationToOther * (acceleration * Time.deltaTime);
        } else if (distanceToOther < TargetDistance - 2)
        {
            velocity += orientationToOther * (-acceleration * Time.deltaTime);
        }
        else
        {
            velocity -= Vector3.Normalize(velocity) * (acceleration * Time.deltaTime);
        }
        
        //velocity -= Vector3.Normalize(velocity) * (acceleration * Time.deltaTime);
        //velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        transform.position += velocity * Time.deltaTime;
    }
}
