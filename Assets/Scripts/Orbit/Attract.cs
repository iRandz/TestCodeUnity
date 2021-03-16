using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Spheres
{
    public Transform _Transform;
    public Rigidbody _rb;

    public Spheres(Transform t, Rigidbody r)
    {
        this._Transform = t;
        this._rb = r;
    }
}
public class Attract : MonoBehaviour
{

    [SerializeField] private Transform attractionTransform;
    [SerializeField] private float acceleration;
    
    private Vector3 _attractionPoint;
    private List<Transform> _childTrans;
    private Vector3 _direction;

    private List<Spheres> _spheres = new List<Spheres>();
    
    // Start is called before the first frame update
    void Start()
    {

        _childTrans = new List<Transform>(GetComponentsInChildren<Transform>());
        _childTrans.RemoveAt(0);
        foreach (var childTransform in _childTrans)
        {
            _spheres.Add(new Spheres(childTransform,childTransform.gameObject.GetComponent<Rigidbody>()));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var sphere in _spheres)
        {
            _attractionPoint = attractionTransform.position;
            _direction = Vector3.Normalize(_attractionPoint - sphere._Transform.position);
            
            sphere._rb.AddForce(_direction*acceleration);
        }
        
    }
}
