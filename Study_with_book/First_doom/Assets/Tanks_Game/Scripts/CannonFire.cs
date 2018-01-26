using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFire : MonoBehaviour {
    public float reloadTime = 2.0f;

    [SerializeField] private GameObject CannonPrefab;
    private GameObject _cannonPref;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        reloadTime -=Time.deltaTime;
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Input.GetButtonDown("Jump") && reloadTime <=0)
        {
            _cannonPref = Instantiate(CannonPrefab) as GameObject;
            _cannonPref.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
            _cannonPref.transform.rotation = transform.rotation;
            reloadTime += 0.0f;
        }
	}
}
