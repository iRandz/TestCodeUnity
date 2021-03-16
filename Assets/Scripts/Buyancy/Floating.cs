using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    [SerializeField] private Transform fluid;
    [SerializeField] private float fluidDensity;
    [SerializeField] private float objectMass;
    [SerializeField] private float frictionCoefficient;

    private Transform thisTransform;
    private Rigidbody rb;
    private Collider _collider;
    private Bounds colBounds;
    private float fluidLevel;

    private float radius;
    private Vector3 friction;
    private Vector3 v;


    private float fluidMass;
    private float submergedHeight;
    private float submergedVolume;
    private Vector3 buyancy;
    
    private float gravity = 9.81f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        thisTransform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        colBounds = _collider.bounds;
        v = rb.velocity;


    }

    // Update is called once per frame
    void Update()
    {
        fluidLevel = fluid.position.y;
        radius = colBounds.extents.y;

        submergedHeight = Mathf.Clamp(fluidLevel - (thisTransform.position.y - radius),0,2*radius);
        submergedVolume = ((Mathf.PI * submergedHeight * submergedHeight) / 3) * (3 * radius - submergedHeight);

        fluidMass = submergedVolume * fluidDensity;
        buyancy = gravity * fluidMass * Vector3.up;

        rb.AddForce(buyancy+gravity*objectMass*Vector3.down, ForceMode.Acceleration);
    }
}
