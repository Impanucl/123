using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player{

public class OrbitCamera : MonoBehaviour {
    [SerializeField] private Transform target;
    [SerializeField] public Vector3 offsetPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = target.position + offsetPosition;
	}
}
}
