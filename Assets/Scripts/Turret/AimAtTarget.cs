using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AimAtTarget : MonoBehaviour
{
    [SerializeField] private List<GameObject> guns;
    [SerializeField] private List<GameObject> targets;

    [SerializeField] private float torque;

    [SerializeField] private bool PID = false;
    
    //PID controller variables
    [SerializeField] private float Kp = 2;
    [SerializeField] private float Kd = 2;
    [SerializeField] private float Ki = 0.1f;
    private float Integral = 0;
    private float lastAngleDiff = 0;
    
    [SerializeField]
    private GameObject currentTarget;
    private Vector3 orientationToOther;
    private Rigidbody rb;

    [Range(0, 3)]
    [SerializeField] private int targetIndex;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentTarget = targets[targetIndex];
        var thisPos = transform.position;
        var otherPos = currentTarget.transform.position;
        
        
        orientationToOther = Vector3.Normalize(otherPos - thisPos);
        
        float angleDiff = Vector3.Angle(transform.right, orientationToOther);
        Vector3 cross = Vector3.Cross(transform.right, orientationToOther);
        
        
        
        if (PID)
        {
            // Attempt at PID controller. Does not work for this.
            float Deriv = (Kd*(angleDiff - lastAngleDiff)) / Time.fixedDeltaTime;
            Integral = Integral + Ki * angleDiff * Time.fixedDeltaTime;
            float diffStrength = angleDiff*Kp;
            
            lastAngleDiff = angleDiff;
            rb.AddTorque(cross * (torque * (diffStrength + Integral + Deriv))); 
        }
        else
        {
            // Simplified PID... I guess
            float diffStrength = angleDiff / 45;
            diffStrength = Mathf.Clamp(diffStrength, 0, 1);
    
            rb.AddTorque(cross * (angleDiff * torque * (diffStrength)));
        }
        
    }
    
    protected void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
}
