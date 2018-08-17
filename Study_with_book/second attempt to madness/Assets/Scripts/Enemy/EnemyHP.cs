using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy{

	public class EnemyHP : MonoBehaviour {

		[SerializeField] public float healPoint = 1231f;

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			if (healPoint <= 0f) {
				destroyEnemyInstance ();
			}
		}

		public void TakeDamage(float takeDamage)
		{
			healPoint -= takeDamage;
		}

		public void destroyEnemyInstance(){
			this.gameObject.GetComponent<EnemyController> ().updateCountEnemyToSpawn ();
			Destroy (this.gameObject);
		}
	}
}
