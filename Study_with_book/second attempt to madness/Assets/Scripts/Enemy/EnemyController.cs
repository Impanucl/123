using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerObjects;

namespace Enemy{

	public class EnemyController : MonoBehaviour {
		[SerializeField] public GameObject enemyBase;
		[SerializeField] public Vector2 enemyBasePos;
		[SerializeField] public float speed = 3.0f;
		[SerializeField] public float radiusEnemy = 3.0f;
		[SerializeField] public float waitTimeToNewPosition = 9.0f;
		[SerializeField] private float _waitTimeToNewPosition;
		[SerializeField] public Vector2 newPosEnemy;
		[SerializeField] public bool findPlayer = false;
		[SerializeField] private GameObject PlayerPosition;
		[SerializeField] public float distance = 4.0f;
		[SerializeField] public float realDistance;
		[SerializeField] public GameObject playerBase;
		[SerializeField] public Vector2 playerBasePos;
		[SerializeField] public bool walkToPlayerBase = false;

		private Vector2 PlayerPos;
		[SerializeField] public int enemySection = 1;
		[SerializeField] public Sprite[] enemySprites = new Sprite[8];


		// Use this for initialization
		void Start () {
			moveEnemy(findPlayer, walkToPlayerBase);
			PlayerPosition = GameObject.FindWithTag ("MyPlayer");
		}

		// Update is called once per frame
		void Update () {
			SetPlayerPos ();
			setPlayerDistance ();
			calcFindPlayer (realDistance);
			moveEnemy(findPlayer, walkToPlayerBase);
		}

		public void GetBasePos(){
			enemyBasePos = new Vector2 (enemyBase.transform.position.x, enemyBase.transform.position.y);
		}

		public void moveEnemy(bool isPlayer, bool isWalkToPlayerBase)
		{
			if (!isWalkToPlayerBase) {
				if (!isPlayer) {
					_waitTimeToNewPosition -= Time.deltaTime;
					if (_waitTimeToNewPosition <= 0) {
						GetBasePos ();
						newPosEnemy = (Random.insideUnitCircle) * radiusEnemy; // переделать на учет базы
						newPosEnemy = new Vector2 (enemyBasePos.x + newPosEnemy.x, enemyBasePos.y + newPosEnemy.y);
						_waitTimeToNewPosition = waitTimeToNewPosition;
					}
					this.transform.TransformDirection (newPosEnemy);
					this.transform.position = Vector2.MoveTowards (transform.position, newPosEnemy, speed * Time.deltaTime);
					CalcPositionJoystick (newPosEnemy.x - transform.position.x, newPosEnemy.y - transform.position.y);
				} else if (isPlayer) {
					this.transform.TransformDirection (PlayerPos);
					this.transform.position = Vector2.MoveTowards (transform.position, PlayerPos, speed * Time.deltaTime);
					CalcPositionJoystick (PlayerPos.x - transform.position.x, PlayerPos.y - transform.position.y);
				} 
			}
			if (isWalkToPlayerBase) {
				
				if (!isPlayer) {
					getPlayerBasePos ();
					this.transform.TransformDirection (playerBasePos);
					this.transform.position = Vector2.MoveTowards (transform.position, playerBasePos, speed*2 * Time.deltaTime);
					CalcPositionJoystick (playerBasePos.x - transform.position.x, playerBasePos.y - transform.position.y);
				} else if (isPlayer) {
					this.transform.TransformDirection (PlayerPos);
					this.transform.position = Vector2.MoveTowards (transform.position, PlayerPos, speed * Time.deltaTime);
					CalcPositionJoystick (PlayerPos.x - transform.position.x, PlayerPos.y - transform.position.y);
				}
			}

		}

		//вычисление плоскости для поворота персонажа джойстиком
		public void CalcPositionJoystick(float positionX, float positionY)
		{

			if ((positionX > 0) && (positionY > -positionX / 2) && (positionY < positionX / 2))
			{
				enemySection = 0;
			}

			if ((positionX > 0) && (positionY > positionX / 2) && (positionY < positionX * 2))
			{
				enemySection = 1;
			}

			if ((positionY > 0) && (positionX > -positionY / 2) && (positionX < positionY / 2))
			{
				enemySection = 2;
			}

			if ((positionY > 0) && (positionX > -positionY * 2) && (positionX < -positionY / 2))
			{
				enemySection = 3;
			}

			if ((positionX < 0) && (positionY > positionX / 2) && (positionY < -positionX / 2))
			{
				enemySection = 4;
			}

			if ((positionX < 0) && (positionY > positionX * 2) && (positionY < positionX / 2))
			{
				enemySection = 5;
			}

			if ((positionY < 0) && (positionX > positionY / 2) && (positionX < -positionY / 2))
			{
				enemySection = 6;
			}

			if ((positionY < 0) && (positionX > -positionY / 2) && (positionX < -positionY * 2))
			{
				enemySection = 7;
			}

			GetComponent<SpriteRenderer>().sprite = enemySprites[enemySection];
		}

		public void setBaseSpawn (float xPos, float yPos)
		{
			enemyBasePos = new Vector2 (xPos, yPos);
		}

		public void setEnemyBase(GameObject EnemyBaseGameObject)
		{
			enemyBase = EnemyBaseGameObject;
		}

		public void setPlayerBase(GameObject playerBaseGameObject)
		{
			playerBase = playerBaseGameObject;
		}

		public void setWalkToPlayerBase(bool spawnToPlayerBase)
		{
			walkToPlayerBase = spawnToPlayerBase;
		}

		public void setPlayerDistance()	
		{
			Vector2 distanceVector = new Vector2 (PlayerPosition.transform.position.x - transform.position.x, PlayerPosition.transform.position.y - transform.position.y); 
			realDistance = Mathf.Sqrt (Mathf.Pow(distanceVector.x,2) + Mathf.Pow(distanceVector.y,2));
		}

		public void calcFindPlayer(float _distance)	
		{
			if (_distance <= distance) 
			{
				findPlayer = true;
			}
		}

		public void SetPlayerPos()
		{
			PlayerPos = new Vector2 (PlayerPosition.transform.position.x, PlayerPosition.transform.position.y);
		}

		public void getPlayerBasePos(){
			playerBasePos = new Vector2 (playerBase.transform.position.x, playerBase.transform.position.y);
		}

		public void updateCountEnemyToSpawn(){
			if (!walkToPlayerBase) {
				enemyBase.GetComponent<EnemyBaseController> ().updateCountEnemySpawn ();
			}
		}

			
	}
}
