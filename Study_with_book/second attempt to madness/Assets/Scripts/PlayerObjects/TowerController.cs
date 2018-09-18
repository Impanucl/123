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

        [SerializeField] private int   _countGunFire = 0;
        [SerializeField] private float _intervalGunAfterFire;
        private float _intervalGun;
		private float corner;

		public int playerSection = 4;

		void Start () 
		{

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
				SetPosition(-0.024F , 0.341F);
			}

			if ((positionY > 0) && (positionX > positionY / 3) && (positionX < positionY))
			{
				playerSection = 1;
				SetPosition(0.189F, 0.228F);
			}

			if ((positionY > 0) && (positionX > positionY) && (positionX < positionY * 3))
			{
				playerSection = 2;
				SetPosition(0.258F, 0.146F);
			}

			if ((positionX > 0) && (positionY > -positionX / 3) && (positionY < positionX / 3))
			{
				playerSection = 3;
				SetPosition(0.384F, -0.0025F);
			}

			if ((positionX > 0) && (positionY > -positionX) && (positionY < -positionX / 3))
			{
				playerSection = 4;
				SetPosition(0.357F, -0.249F);
			}

			if ((positionX > 0) && (positionY > -positionX * 3) && (positionY < -positionX))
			{
				playerSection = 5;
				SetPosition(0.247F, -0.362F);
			}

			if ((positionY < 0) && (positionX > positionY / 3) && (positionX < -positionY / 3))
			{
				playerSection = 6;
				SetPosition(-0.003F, -0.462F);
			}

			if ((positionY < 0) && (positionX > positionY) && (positionX < positionY / 3))
			{
				playerSection = 7;
				SetPosition(-0.185F, -0.436F);
			}

			if ((positionY < 0) && (positionX > positionY * 3) && (positionX < positionY))
			{
				playerSection = 8;
				SetPosition(-0.329F, -0.33F);
			}

			if ((positionX < 0) && (positionY > positionX / 3) && (positionY < -positionX / 3))
			{
				playerSection = 9;
				SetPosition(-0.452F, -0.064F);
			}

			if ((positionX < 0) && (positionY > -positionX / 3) && (positionY < -positionX))
			{
				playerSection = 10;
				SetPosition(-0.367F, 0.167F);
			}

			if ((positionX < 0) && (positionY > -positionX) && (positionY < -positionX * 3))
			{
				playerSection = 11;
				SetPosition(-0.184F, 0.291F);
			}

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
            if (enemyTargetList.Count > 0 && _intervalGunAfterFire < 0)
            {
                if (_countGunFire <= countGunFire)
                {
                    CalcPositionJoystick(enemyTargetList[0].transform.position.x - transform.position.x, enemyTargetList[0].transform.position.y - transform.position.y);
                    corner = 90 - Mathf.Atan2(enemyTargetList[0].transform.position.x - transform.position.x, enemyTargetList[0].transform.position.y - transform.position.y) * Mathf.Rad2Deg;
                    if (_intervalGun < 0.0f)
                    {
                        spawnBullet();
                    }
                    if (_countGunFire == countGunFire)
                    {
                        _intervalGunAfterFire = intervalGunAfterFire;
                        _countGunFire = 0;
                    }
                }

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

	}

}