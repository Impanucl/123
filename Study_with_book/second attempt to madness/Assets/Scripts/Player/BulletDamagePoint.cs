using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

namespace Player{

	public class BulletDamagePoint : MonoBehaviour {

		public float damage = 0.0f;
		public float timeLife = 0.0f;

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			timeLife -= Time.deltaTime;
			if (timeLife <= 0f) 
			{
				Destroy (this.gameObject);
			}
		}

		void OnTriggerEnter2D(Collider2D other) {

			if (other.tag == "Enemy") 
			{
				other.GetComponent<EnemyHP>().TakeDamage(damage);
				Destroy (this.gameObject);

			}

		}

}
}
