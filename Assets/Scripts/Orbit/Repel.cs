using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repel : MonoBehaviour
{
    [SerializeField] private float acceleration;
    [SerializeField][Range(1,100)]
    private float accelerationAttenuation = 5;
    
    private Vector3 _repelPoint;
    private Vector3 _direction;
    
    
    private List<Transform> _childTrans;
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
            foreach (var compareToSphere in _spheres)
            {
                if (compareToSphere._Transform == sphere._Transform)
                {
                    continue;
                }
                _repelPoint = compareToSphere._Transform.position;
                var diff = _repelPoint - sphere._Transform.position;
                var distance = Vector3.Magnitude(diff);
                _direction = -1*(diff/distance);
                var actualAccel = Mathf.Exp(-distance / accelerationAttenuation) * acceleration;
            
                sphere._rb.AddForce(_direction*actualAccel);
            }
        }
    }
}
