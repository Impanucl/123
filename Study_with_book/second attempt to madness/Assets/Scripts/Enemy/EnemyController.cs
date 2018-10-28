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

        [SerializeField] public Animator enemyAnimator;

        [SerializeField] public List<GameObject> playerTowerObjects;
        [SerializeField] public List<GameObject> playerBaseObjects;
        [SerializeField] public float minDistanceToTower = 1111110.0f;
        [SerializeField] public float minDistanceToBase = 11111110.0f;
        [SerializeField] public GameObject playerTowerCurrent;
        [SerializeField] public GameObject playerBaseCurrent;

        // Use this for initialization
        void Start () {
            Objects.Add(FindPlayerObjects());
            enemyAnimator = this.GetComponent<Animator>();
            moveEnemy(findPlayer, walkToPlayerBase, damageTargetObject);
			PlayerObject = GameObject.FindWithTag ("MyPlayer");
			_intervalToDamage = intervalToDamage;
		}

		// Update is called once per frame
		void Update () {
            //enemyAnimator = this.GetComponent<Animator>();
            SetPlayerPos ();
			setPlayerDistance ();
			calcFindPlayer (realDistance);
			moveEnemy(findPlayer, walkToPlayerBase, damageTargetObject);
			findPlayerObjectToDamage ();
		}

        public void findPlayerObjectToDamage()
        {
            if (Objects.Count > 0 && Objects[0] != null)
            {
                _intervalToDamage -= Time.deltaTime;
                _objectDistance = calcDistanceObject(Objects[0]);

                if (_objectDistance <= objectDistance)
                {
                    damageTargetObject = true;

                    if (_intervalToDamage < 0.0f)
                    {
                        enemyAnimator.SetBool("isDamage", true);
                        damagePlayerObjects(Objects[0], damage, false);
                        _intervalToDamage = intervalToDamage;
                    }
                }
            } else {
                damageTargetObject = false;
                Objects.RemoveAt(0);
                Objects.Add(FindPlayerObjects());
            }

            if ((realDistance <= objectDistance) && findPlayer == true)
            {
                _intervalToDamage -= Time.deltaTime;
                damageTargetObject = true;

                if (_intervalToDamage < 0.0f)
                {
                    enemyAnimator.SetBool("isDamage", true);
                    damagePlayerObjects(PlayerObject, damage, true);
                    _intervalToDamage = intervalToDamage;
                }
            }

            if (realDistance >= objectDistance && findPlayer == true)
            {
                damageTargetObject = false;
                enemyAnimator.SetBool("isDamage", false);
            }

        }

		public void damagePlayerObjects(GameObject damageTarget, float damagePoint, bool isDamagePlayer)
		{

            if (!isDamagePlayer)
                {
                enemyAnimator.SetBool("isDamage", true);
                damageTarget.GetComponent<PlayerObjectHP>().TakeDamage(damagePoint);
                }
                else
                {
                enemyAnimator.SetBool("isDamage", true);
                damageTarget.GetComponent<PlayerHP>().TakeDamage(damagePoint);
                }
		}

		public void GetEnemyBasePos()
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
							GetEnemyBasePos ();
                            newPosEnemy = randOnCircle(radiusEnemy);
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
                            if (Objects[0] == null)
                            {
                                damageTargetObject = false;
                                Objects.RemoveAt(0);
                                Objects.Add(FindPlayerObjects());
                                } else {
                                enemyAnimator.SetBool("isDamage", false);
                                Vector2 objectPos = new Vector2(Objects[0].transform.position.x, Objects[0].transform.position.y);
                                    transfortDirection(objectPos);
                                }
                            }

                    } else if (isPlayer) {
						transfortDirection (PlayerPos);
					}
				}
			}
		}

		public void transfortDirection(Vector2 targetPos)
		{
            enemyAnimator.SetBool("isRun", true);
            this.transform.TransformDirection (targetPos);
			this.transform.position = Vector2.MoveTowards (transform.position, targetPos, speed * Time.deltaTime);
			CalcPositionJoystick (targetPos.x - transform.position.x, targetPos.y - transform.position.y);
            if (new Vector2(this.transform.position.x, this.transform.position.y) == targetPos){
                enemyAnimator.SetBool("isRun", false);
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
            enemyAnimator.SetInteger("sprite", enemySection);

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

        public Vector2 randOnCircle(float maxRadius, float minRadius = 1.9303124F)
        {
            float randAng = Random.Range(0, Mathf.PI * 2);
            Vector2 minCircle = new Vector2(Mathf.Cos(randAng) * minRadius, Mathf.Sin(randAng) * minRadius);
            Vector2 maxCircle = new Vector2(Mathf.Cos(randAng) * maxRadius, Mathf.Sin(randAng) * maxRadius);

            float realXPos = Random.Range(minCircle.x, maxCircle.x);
            float realYPos = Random.Range(minCircle.y, maxCircle.y);

            return new Vector2(realXPos, realYPos);
        }


        public GameObject FindPlayerObjects()
        {
            if (Objects.Count > 0 && Objects[0] == null)
            {
                Objects.RemoveAt(0);
            }

            Objects.Clear();
            playerTowerObjects.Clear();
            playerBaseObjects.Clear();
            playerBaseCurrent = null;
            playerTowerCurrent = null;
            minDistanceToBase = 11111110.0f;
            minDistanceToTower = 1111110.0f;

            playerTowerObjects.AddRange(GameObject.FindGameObjectsWithTag("TowerPlayer"));
            playerBaseObjects.AddRange(GameObject.FindGameObjectsWithTag("PlayerBase"));

            foreach (GameObject objectTower in playerTowerObjects)
            {
                float distanceTower = calcDistanceObject(objectTower);
                if (minDistanceToTower > distanceTower)
                {
                    minDistanceToTower = distanceTower;
                    playerTowerCurrent = objectTower;
                }
            }

            foreach (GameObject objectBase in playerBaseObjects)
            {
                float distanceBase = calcDistanceObject(objectBase);
                if (minDistanceToBase > distanceBase)
                {
                    minDistanceToBase = distanceBase;
                    playerBaseCurrent = objectBase;
                }
            }

            if (playerBaseObjects.Count > 0 && playerTowerObjects.Count > 0)
            {
                if (minDistanceToBase > minDistanceToTower)
                {
                    return playerTowerCurrent;
                }
                else
                {
                    return playerBaseCurrent;
                }

            }

            if (playerBaseObjects.Count == 0 && playerTowerObjects.Count == 0)
            {
                gameOverMenu.GetComponent<PauseController>().SetTimeScale(true);
            }

            if (playerBaseObjects.Count == 0 && playerTowerObjects.Count > 0)
            {
                return playerTowerCurrent;
            }

            if (playerTowerObjects.Count == 0 && playerBaseObjects.Count > 0)
            {
                return playerBaseCurrent;
            }
            return null;

        }

    }
}
                                                                                                                                                                                 