using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy{

	public class EnemyHP : MonoBehaviour {

		[SerializeField] public float healPoint = 1231f;
		[SerializeField] public bool isBase = false;
        [SerializeField] public float timeToRegeneration = 0.0f;
        [SerializeField] public float healthPerSecond = 0.0f;


        [SerializeField] public float _timeToRegeneration = 0.0f;
        [SerializeField] public float _healPoint = 1231f;

        // Use this for initialization
        void Start () {
            _healPoint = healPoint;
            _timeToRegeneration = timeToRegeneration;
		}
		
		// Update is called once per frame
		void Update () {
            if (_timeToRegeneration >= 0 && isBase)
            {
                _timeToRegeneration -= Time.deltaTime;
            }
            if (_timeToRegeneration <= 0 && isBase)
            {
                regenerationHP();
            }

			if (_healPoint <= 0f) {
				destroyEnemyInstance ();
			}

		}

		public void TakeDamage(float takeDamage)
		{
            _healPoint -= takeDamage;
            _timeToRegeneration = timeToRegeneration;

        }

		public void destroyEnemyInstance(){
			if (!isBase && this.gameObject.GetComponent<EnemyController>().enemyBase != null) {
                this.gameObject.GetComponent<EnemyController>().enemyBase.GetComponent<EnemyBaseController>().enemyCount.Remove(this.gameObject);
            }
			Destroy (this.gameObject);
		}

        public void regenerationHP(){
            if (_healPoint < healPoint && healthPerSecond > 0.0f) {
                _healPoint += healthPerSecond * Time.deltaTime;
            }
        }
	}
}
