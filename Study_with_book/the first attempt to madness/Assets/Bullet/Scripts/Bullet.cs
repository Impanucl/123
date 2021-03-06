﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float speed = 10.0f;
    public float temperaturePerShoot = 1.0f;

	// Use this for initialization
	void Start () {
        Messenger<float>.Broadcast(GameEvent.SPAWN_BULLET, temperaturePerShoot);
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, 0, speed * Time.deltaTime);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.name != "Tower")
        {
            Destroy(gameObject);
        }
    }

}
