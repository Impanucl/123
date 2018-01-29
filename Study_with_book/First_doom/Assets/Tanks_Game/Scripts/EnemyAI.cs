using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    private bool _alive;
    public float speed = 3.0f;
    public float StopRange = 5.0f;

	// Use this for initialization
	void Start () {
        _alive = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (_alive)
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);

            Ray ray = new Ray(transform.position, -transform.right);
            RaycastHit hit;

            if(Physics.SphereCast(ray, 0.75f, out hit))
            {
                if(hit.distance < StopRange)
                {
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, 0, angle);
                    GameObject hitObject = hit.transform.gameObject;
                        Debug.Log(hitObject);
                }
            }
        }
	}

    public void SetAlive(bool alive)
    {
        _alive = alive;
    }
}
