﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy{

	public class EnemyHP : MonoBehaviour {

		[SerializeField] public float healPoint = 1231f;
		[SerializeField] public bool isBase = false;

		// Use this for initialization
		void Start () {
			if (this.gameObject.GetComponent<EnemyController> ().enemyBase != null) 
			{
				isBase = true;
			}
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
			if (isBase) {
				this.gameObject.GetComponent<EnemyController> ().enemyBase.GetComponent<EnemyBaseController> ().enemyCount.Remove (this.gameObject);
			}
			Destroy (this.gameObject);
		}
	}
}