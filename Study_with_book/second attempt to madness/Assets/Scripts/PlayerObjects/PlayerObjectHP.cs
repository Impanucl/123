using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerObjects{

	public class PlayerObjectHP : MonoBehaviour {

        [SerializeField] public float healPoint = 1231f;
        [SerializeField] public float timeToRegeneration = 0.0f;
        [SerializeField] public float healthPerSecond = 0.0f;


        [SerializeField] public float _timeToRegeneration = 0.0f;
        [SerializeField] public float _healPoint = 1231f;

        // Use this for initialization
        void Start()
        {
            _healPoint = healPoint;
            _timeToRegeneration = timeToRegeneration;
        }

        // Update is called once per frame
        void Update()
        {
            if (_timeToRegeneration >= 0)
            {
                _timeToRegeneration -= Time.deltaTime;
            }
            if (_timeToRegeneration <= 0)
            {
                regenerationHP();
            }

            if (_healPoint <= 0f)
            {
                destroyEnemyInstance();
            }

        }

        public void TakeDamage(float takeDamage)
        {
            _healPoint -= takeDamage;
            _timeToRegeneration = timeToRegeneration;

        }

        public void destroyEnemyInstance()
        {
            Destroy(this.gameObject);
        }

        public void regenerationHP()
        {
            if (_healPoint < healPoint && healthPerSecond > 0.0f)
            {
                _healPoint += healthPerSecond * Time.deltaTime;
            }
        }
    }
}
