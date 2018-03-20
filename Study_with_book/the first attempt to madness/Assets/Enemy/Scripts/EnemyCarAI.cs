using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCarAI : MonoBehaviour {
    [SerializeField] public GameObject _spawnPosition;
    public float speed = 20.0f;
    public float spawnRangeRadius = 40.0f;
    private float distance;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        distance = Vector3.Distance(_spawnPosition.transform.position, transform.position);
        if (distance <= spawnRangeRadius)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        else
        {
            float angle = Random.Range(-110, 110);
            transform.Rotate(0, angle, 0);
        }
            Debug.Log(distance);
        
    }
}
