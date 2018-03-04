using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtainCamera : MonoBehaviour {
    [SerializeField] private Transform target;
    public Vector3 offsetPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = target.position + offsetPosition;
	}
}
