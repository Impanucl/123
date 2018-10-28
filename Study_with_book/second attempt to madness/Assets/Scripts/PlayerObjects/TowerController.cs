using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;


namespace PlayerObjects{

	public class TowerController : MonoBehaviour {

        [SerializeField] public List<GameObject> enemyTargetList;
        [SerializeField] public Sprite[] gunSprites = new Sprite[12];
		[SerializeField] public GameObject spawnBulletPosition;
		private GameObject bullet;
		[SerializeField] private GameObject bulletPrefab;
        [SerializeField] public float damage = 0.0f;
        [SerializeField] public float lifeTime = 3.0f;
        [SerializeField] public float speed = 10.0f;
        [SerializeField] public float intervalGun = 0.3f;

        [SerializeField] public int countGunFire = 3;
        [SerializeField] public float intervalGunAfterFire = 2f;

        [SerializeField] public Animator towerAnimator;

        [SerializeField] public float minDistanceEnemy = 11111110.0f;
        [SerializeField] public GameObject currentEnemy;

        private int   _countGunFire = 0;
         private float _intervalGunAfterFire;
         public int isFire = 0;
         private float _intervalGun;
		private float corner;

		public int playerSection = 4;

		void Start () 
		{
            towerAnimator = GetComponent<Animator>();
        }

		void FixedUpdate () 
		{
            _intervalGunAfterFire -= Time.deltaTime;
            _intervalGun -= Time.deltaTime;
			getTargetAndFire ();
        }

        //вычисление плоскости для поворота оружия джойстиком 
        public void CalcPositionJoystick(float positionX, float positionY)
		{
			if ((positionY > 0) && (positionX > -positionY / 3) && (positionX < positionY / 3))
			{
				playerSection = 0;
				SetPosition(-0.079F, 0.306F);
			}

			if ((positionY > 0) && (positionX > positionY / 3) && (positionX < positionY))
			{
				playerSection = 1;
				SetPosition(0.109F, 0.258F);
			}

			if ((positionY > 0) && (positionX > positionY) && (positionX < positionY * 3))
			{
				playerSection = 2;
				SetPosition(0.229F, 0.17F);
			}

			if ((positionX > 0) && (positionY > -positionX / 3) && (positionY < positionX / 3))
			{
				playerSection = 3;
				SetPosition(0.295F, 0.029F);
			}

			if ((positionX > 0) && (positionY > -positionX) && (positionY < -positionX / 3))
			{
				playerSection = 4;
				SetPosition(0.257F, -0.159F);
			}

			if ((positionX > 0) && (positionY > -positionX * 3) && (positionY < -positionX))
			{
				playerSection = 5;
				SetPosition(0.085F, -0.294F);
			}

			if ((positionY < 0) && (positionX > positionY / 3) && (positionX < -positionY / 3))
			{
				playerSection = 6;
				SetPosition(-0.091F, -0.349F);
			}

			if ((positionY < 0) && (positionX > positionY) && (positionX < positionY / 3))
			{
				playerSection = 7;
				SetPosition(-0.275F, -0.298F);
			}

			if ((positionY < 0) && (positionX > positionY * 3) && (positionX < positionY))
			{
				playerSection = 8;
				SetPosition(-0.437F, -0.158F);
			}

			if ((positionX < 0) && (positionY > positionX / 3) && (positionY < -positionX / 3))
			{
				playerSection = 9;
				SetPosition(-0.454F, 0.03F);
			}

			if ((positionX < 0) && (positionY > -positionX / 3) && (positionY < -positionX))
			{
				playerSection = 10;
				SetPosition(-0.375F, 0.182F);
			}

			if ((positionX < 0) && (positionY > -positionX) && (positionY < -positionX * 3))
			{
				playerSection = 11;
				SetPosition(-0.251F, 0.258F);
			}
            towerAnimator.SetInteger("sprite", playerSection);
            GetComponent<SpriteRenderer>().sprite = gunSprites[playerSection];
        }

		private void SetPosition(float posX, float posY)
		{
			//выставляю спаун позицию в зависимости от положения оружия
			spawnBulletPosition.transform.localPosition = new Vector3(posX, posY, transform.localPosition.z); 
		}

		void OnTriggerEnter2D(Collider2D other)
		{
			if (other.tag == "Enemy") 
			{
				enemyTargetList.Add (other.gameObject);
            }
		}

		void OnTriggerExit2D(Collider2D other)
		{
			if (other.tag == "Enemy") 
			{
				enemyTargetList.Remove(other.gameObject);
			}
		}


        public void getTargetAndFire()
        {
            getCurrentTarget(enemyTargetList);
            if (currentEnemy != null && _intervalGunAfterFire < 0)
            {
                if (_countGunFire <= countGunFire)
                {
                    CalcPositionJoystick(currentEnemy.transform.position.x - transform.position.x, currentEnemy.transform.position.y - transform.position.y);
                    corner = 90 - Mathf.Atan2(currentEnemy.transform.position.x - transform.position.x, currentEnemy.transform.position.y - transform.position.y) * Mathf.Rad2Deg;
                    if (_intervalGun < 0.0f)
                    {
                        towerAnimator.SetBool("isFire", true);
                        spawnBullet();
                    }
                    if (_countGunFire == countGunFire)
                    {
                        towerAnimator.SetBool("isFire", false);
                        _intervalGunAfterFire = intervalGunAfterFire;
                        _countGunFire = 0;
                    }
                }

            }

            if (currentEnemy == null)
            {
                towerAnimator.SetBool("isFire", false);
            }
        }

		//создание пули и добавление ей импулса
		private void spawnBullet()
		{
            bullet = Instantiate(bulletPrefab, spawnBulletPosition.transform.position, transform.rotation) as GameObject; 		 // создание пули
			bullet.transform.rotation = Quaternion.Euler(0,0,corner);													     // направление пули
			bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * speed * Time.deltaTime,ForceMode2D.Impulse);// ускорение пули физическим свойством
			_intervalGun = intervalGun;
            _countGunFire++;
			SetBulletParameters(damage, lifeTime);
        }

        private void SetBulletParameters(float damagePoint, float lifeTime)
		{
			bullet.GetComponent<BulletDamagePoint>().damage = damagePoint;
			bullet.GetComponent<BulletDamagePoint>().timeLife = lifeTime;
		}

        private void getCurrentTarget(List<GameObject> array)
        {
            minDistanceEnemy = 11111111111f;
            currentEnemy = null;
            foreach(GameObject currentTarget in array){
                if (currentTarget.GetComponent<Enemy.EnemyController>()._objectDistance < minDistanceEnemy){
                    minDistanceEnemy = currentTarget.GetComponent<Enemy.EnemyController>()._objectDistance;
                    currentEnemy = currentTarget;

                }
            }
        }

	}

}