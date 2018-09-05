using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy{

	public class EnemyBaseController : MonoBehaviour {

		[SerializeField] private GameObject enemy;
		[SerializeField] private Vector2 spawnPosition;
		[SerializeField] private GameObject EnemyPrefab;
		[SerializeField] public GameObject PlayerBase;
		[SerializeField] public float distance;
		[SerializeField] public float intervalSpawn = 1.0f;
		[SerializeField] private float _intervalSpawn;
		[SerializeField] public float intervalSpawnToPlayerBase = 1.0f;
		[SerializeField] private float _intervalSpawnToPlayerBase;
		[SerializeField] public int countEnemiesSpawn = 25;
		[SerializeField] public List<GameObject> enemyCount;
		[SerializeField] public bool walkToPlayerBase = false;

		[SerializeField] public GameObject GameOverMenu;

		// Use this for initialization
		void Start () {
			_intervalSpawn = intervalSpawn;
			_intervalSpawnToPlayerBase = intervalSpawnToPlayerBase;
		}
		
		// Update is called once per frame
		void Update () {
			_intervalSpawn -= Time.deltaTime;
			_intervalSpawnToPlayerBase -= Time.deltaTime;
			setSpawnPosition ();
			spawnEnemy ();
			spawnEnemyToPlayerBase ();
		}

		public void setSpawnPosition(){
			spawnPosition = Random.insideUnitCircle/2;  // переделать на учет базы
			spawnPosition = new Vector2 (transform.position.x + spawnPosition.x, transform.position.y + spawnPosition.y);
		}

		public void spawnEnemy(){
			if (enemyCount.Count <= countEnemiesSpawn && _intervalSpawn <= 0)
			{
				
				enemy = Instantiate (EnemyPrefab, spawnPosition, transform.rotation) as GameObject;
				enemy.GetComponent<EnemyController> ().setEnemyBase (gameObject);
				enemy.GetComponent<EnemyController> ().gameOverMenu = GameOverMenu;
				_intervalSpawn = intervalSpawn;
				enemyCount.Add (enemy);
			}
		}

		public void spawnEnemyToPlayerBase(){
			if (_intervalSpawnToPlayerBase <= 0 && walkToPlayerBase)
			{
				enemy = Instantiate (EnemyPrefab, spawnPosition, transform.rotation) as GameObject;
				enemy.GetComponent<EnemyController> ().setPlayerBase (PlayerBase);
				enemy.GetComponent<EnemyController> ().setWalkToPlayerBase (walkToPlayerBase);
				enemy.GetComponent<EnemyController> ().gameOverMenu = GameOverMenu;
				_intervalSpawnToPlayerBase = intervalSpawnToPlayerBase;
			}
		}

	}
}
