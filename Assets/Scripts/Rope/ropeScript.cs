using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ropeScript : MonoBehaviour
{
    [SerializeField] private GameObject start;
    [SerializeField] private GameObject end;
    private List<Rigidbody> _rigidbodyList = new List<Rigidbody>();
    private List<Transform> _transformList = new List<Transform>();
    
    [SerializeField] private int childCount = 0;
    [SerializeField] private float springConstant = 1;
    [SerializeField] private float particleMass = 1;
    [SerializeField] private float particleDrag = 1;
    [SerializeField] private float springLength = 1;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbodyList.Add(start.GetComponent<Rigidbody>());
        _transformList.Add(start.GetComponent<Transform>());
        
        int i = 0;
        foreach (var child in GetComponentsInChildren<Rigidbody>())
        {
            _transformList.Add(child.GetComponent<Transform>());
            _rigidbodyList.Add(child);
        }

        _rigidbodyList.Add(end.GetComponent<Rigidbody>());
        _transformList.Add(end.GetComponent<Transform>());

        childCount = _transformList.Count - 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 1; i < _rigidbodyList.Count-1; i++)
        {
            _rigidbodyList[i].mass = particleMass;
            _rigidbodyList[i].drag = particleDrag;
            
            var vectorP = _transformList[i - 1].position - _transformList[i].position;
            var lengthP = Vector3.Magnitude(vectorP);
            var previous = springConstant*(lengthP-springLength) * Vector3.Normalize(vectorP);
            
            var vectorN = _transformList[i + 1].position - _transformList[i].position;
            var lengthN = Vector3.Magnitude(vectorN);
            var next = springConstant*(lengthN-springLength) * Vector3.Normalize(vectorN);
             
            _rigidbodyList[i].AddForce(previous+next);
        }
    }
}
