using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MYGUI;

namespace Player{

	public class PlayerHP : MonoBehaviour {

		[SerializeField] public float healPoint = 1231f;
        [SerializeField] public float _healPoint;
        [SerializeField] public GameObject gameOverMenu;



        // Use this for initialization
        void Start () {
            _healPoint = healPoint;
        }

		// Update is called once per frame
		void Update () {

            if (_healPoint <= 0f) {
                destroyPlayerInstance ();
			}
		}

		public void TakeDamage(float takeDamage)
		{
            _healPoint -= takeDamage;
            HealthBar.AdjustCurrentValue(-takeDamage);
        }

		public void destroyPlayerInstance(){
			gameOverMenu.GetComponent<PauseController>().SetTimeScale(true);

		}

        
    }
}
