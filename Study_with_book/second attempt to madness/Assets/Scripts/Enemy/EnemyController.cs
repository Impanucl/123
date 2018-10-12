using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerObjects;
using Player;
using MYGUI;

namespace Enemy{

	public class EnemyController : MonoBehaviour {
		[SerializeField] public GameObject enemyBase;
		[SerializeField] public Vector2 enemyBasePos;
		[SerializeField] public float speed = 3.0f;
		[SerializeField] public float radiusEnemy = 3.0f;
		[SerializeField] public float waitTimeToNewPosition = 9.0f;
		[SerializeField] private float _waitTimeToNewPosition;
		[SerializeField] public Vector2 newPosEnemy;
		[SerializeField] private GameObject PlayerObject;
		[SerializeField] public float distance = 4.0f;
		[SerializeField] public float realDistance;
		[SerializeField] public GameObject playerBase;
		[SerializeField] public Vector2 playerBasePos;
		[SerializeField] public bool walkToPlayerBase = false;
		[SerializeField] public bool findPlayer = false;
		[SerializeField] public bool damageTargetObject = false;

		[SerializeField] public float damage = 10f;
		[SerializeField] public float intervalToDamage = 1.0f;
		[SerializeField] public float _intervalToDamage;

		[SerializeField] public float objectDistance = 0.5f;
		[SerializeField] public float _objectDistance;

		[SerializeField] public List<GameObject> Objects;

		private Vector2 PlayerPos;
		[SerializeField] public int enemySection = 1;
		[SerializeField] public Sprite[] enemySprites = new Sprite[8];

		[SerializeField] public GameObject gameOverMenu;


		[SerializeField] public List<GameObject> playerBaseObjects;
		[SerializeField] public GameObject newBasePlayerObject;

		[SerializeField] public float newDistance = 0.0f;
		[SerializeField] public float minNewDistance = 0.0f;

		// Use this for initialization
		void Start () {
			moveEnemy(findPlayer, walkToPlayerBase, damageTargetObject);
			PlayerObject = GameObject.FindWithTag ("MyPlayer");
			_intervalToDamage = intervalToDamage;
		}

		// Update is called once per frame
		void Update () {
			SetPlayerPos ();
			setPlayerDistance ();
			calcFindPlayer (realDistance);
			moveEnemy(findPlayer, walkToPlayerBase, damageTargetObject);
			findPlayerObjectToDamage ();
		}

		public void findPlayerObjectToDamage()
		{
			
			if (Objects.Count > 0) {

				_intervalToDamage -= Time.deltaTime;
				_objectDistance = calcDistanceObject (Objects [0]);

				if (_objectDistance <= objectDistance) {
					damageTargetObject = true;

					if (_intervalToDamage < 0.0f) {
						damagePlayerObjects (Objects [0], damage, false);
						_intervalToDamage = intervalToDamage;
					}
				} 
			}

            if ((realDistance <= objectDistance) && findPlayer == true) {
				_intervalToDamage -= Time.deltaTime;
				damageTargetObject = true;
	
				if (_intervalToDamage < 0.0f) {
					damagePlayerObjects (PlayerObject, damage, true);
					_intervalToDamage = intervalToDamage;
				}
            } else 

            if (realDistance >= objectDistance && findPlayer == true)
            {
                damageTargetObject = false;
            }
		}

		public void damagePlayerObjects(GameObject damageTarget, float damagePoint, bool isDamagePlayer)
		{
           // if (!findPlayer)
           // {
                if (!isDamagePlayer)
                {
                    damageTarget.GetComponent<PlayerObjectHP>().TakeDamage(damagePoint);
                }
                else
                {
                    damageTarget.GetComponent<PlayerHP>().TakeDamage(damagePoint);
                }
            //}

		}

		public void GetBasePos()
		{
            if (enemyBase != null)
            {
                enemyBasePos = new Vector2(enemyBase.transform.position.x, enemyBase.transform.position.y);
            }
		}

		public void moveEnemy(bool isPlayer, bool isWalkToPlayerBase, bool isDamageTargetObject)
		{
			if (!isDamageTargetObject) {
				if (!isWalkToPlayerBase) {
					if (!isPlayer) {
						_waitTimeToNewPosition -= Time.deltaTime;
						if (_waitTimeToNewPosition <= 0) {
							GetBasePos ();
							newPosEnemy = (Random.insideUnitCircle) * radiusEnemy; // переделать на учет базы
							newPosEnemy = new Vector2 (enemyBasePos.x + newPosEnemy.x, enemyBasePos.y + newPosEnemy.y);
							_waitTimeToNewPosition = waitTimeToNewPosition;
						}
						transfortDirection (newPosEnemy);
					} else if (isPlayer) {
						transfortDirection (PlayerPos);
					} 
				}
				if (isWalkToPlayerBase) {
				
					if (!isPlayer) {
						if (Objects.Count > 0) {
							Vector2 TowerPos = new Vector2 (Objects [0].transform.position.x, Objects [0].transform.position.y);
							transfortDirection (TowerPos);
						} else {
							getPlayerBasePos ();
							transfortDirection (playerBasePos);
						}
					} else if (isPlayer) {
						transfortDirection (PlayerPos);
					}
				}
			}

		}

		public void transfortDirection(Vector2 targetPos)
		{
			this.transform.TransformDirection (targetPos);
			this.transform.position = Vector2.MoveTowards (transform.position, targetPos, speed * Time.deltaTime);
			CalcPositionJoystick (targetPos.x - transform.position.x, targetPos.y - transform.position.y);
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
			Vector2 distanceVector = new Vector2 (PlayerObject.transform.position.x - transform.position.x, PlayerObject.transform.position.y - transform.position.y); 
			realDistance = Mathf.Sqrt (Mathf.Pow(distanceVector.x,2) + Mathf.Pow(distanceVector.y,2));
		}

		public void calcFindPlayer(float _distance)	
		{
			if (_distance <= distance) 
			{
				findPlayer = true;
			}
		}
			
		public float calcDistanceObject(GameObject firstObject) 
		{	
			Vector2 distanceVector = new Vector2 (firstObject.transform.position.x - transform.position.x, firstObject.transform.position.y - transform.position.y); 
			float distanceToNextObject = Mathf.Sqrt (Mathf.Pow(distanceVector.x,2) + Mathf.Pow(distanceVector.y,2));

			return distanceToNextObject;
		}

		public void SetPlayerPos()
		{ 
			PlayerPos = new Vector2 (PlayerObject.transform.position.x, PlayerObject.transform.position.y);
		}

		public void getPlayerBasePos()
		{
			if (playerBase == null) {
				FindNewPlayerBase ();
			} else {
				playerBasePos = new Vector2 (playerBase.transform.position.x, playerBase.transform.position.y);
			}
		}
			
		public void FindNewPlayerBase()
		{
			if (GameObject.FindGameObjectsWithTag ("PlayerBase").Length == 0) 
			{
				gameOverMenu.GetComponent<PauseController> ().SetTimeScale (true);
				enabled = false;
			} 
			else 
			{
				playerBaseObjects.AddRange (GameObject.FindGameObjectsWithTag ("PlayerBase"));

				newDistance = 0.0f;

				minNewDistance = calcDistanceObject (playerBaseObjects [0]);

				foreach (GameObject value in playerBaseObjects) {
					newDistance = calcDistanceObject (value);
					if (minNewDistance >= newDistance) {
						newBasePlayerObject = value;
					}
				}

				if (playerBaseObjects [0] == null) {
					playerBaseObjects.RemoveAt (0);
				}

				playerBaseObjects.Clear ();
				playerBase = newBasePlayerObject;
			}
		}

	}
}
                                                                                                                                                                                 