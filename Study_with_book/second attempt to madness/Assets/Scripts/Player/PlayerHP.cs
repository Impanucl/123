using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GUI;

namespace Player{

	public class PlayerHP : MonoBehaviour {

		[SerializeField] public float healPoint = 1231f;
		[SerializeField] public GameObject gameOverMenu;

		// Use this for initialization
		void Start () {
		}

		// Update is called once per frame
		void Update () {
			if (healPoint <= 0f) {
				destroyPlayerInstance ();
			}
		}

		public void TakeDamage(float takeDamage)
		{
			healPoint -= takeDamage;
		}

		public void destroyPlayerInstance(){
			gameOverMenu.GetComponent<PauseController>().SetTimeScale(true);

		}
			
	}
}
