using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flak : MonoBehaviour {
    public float speed = 10.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, -speed * Time.deltaTime, 0);
	}

    void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
