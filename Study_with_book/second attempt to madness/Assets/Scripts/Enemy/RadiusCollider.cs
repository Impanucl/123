using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Enemy
{

    public class RadiusCollider : MonoBehaviour
    {
            [SerializeField] public List<GameObject> playerTowerObjects;
        [SerializeField] public List<GameObject> playerBaseObjects;


        [SerializeField] public float minDistanceToTower = 1111110.0f;
        [SerializeField] public GameObject playerTowerCurrent;

        [SerializeField] public float minDistanceToBase = 11111110.0f;
        [SerializeField] public GameObject playerBaseCurrent;

        // Use this for initialization
        void Start()
        {
            gameObject.GetComponentInParent<EnemyController>().Objects.Add(FindPlayerObjects()) ;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public GameObject FindPlayerObjects()
        {
            if (gameObject.GetComponentInParent<EnemyController>().Objects.Count > 0 && gameObject.GetComponentInParent<EnemyController>().Objects[0] == null) {
                gameObject.GetComponentInParent<EnemyController>().Objects.RemoveAt(0);
            }

            gameObject.GetComponentInParent<EnemyController>().Objects.Clear();
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


        public float calcDistanceObject(GameObject Object)
        {
            Vector2 distanceVector = new Vector2(Object.transform.position.x - transform.position.x, Object.transform.position.y - transform.position.y);
            float distanceToNextObject = Mathf.Sqrt(Mathf.Pow(distanceVector.x, 2) + Mathf.Pow(distanceVector.y, 2));
            return distanceToNextObject;
        }

    }
}

        /*void OnTriggerEnter2D(Collider2D other)
		{
			string objectTag = other.tag;

			switch (objectTag) 
			{
			case "TowerPlayer": 
			case "PlayerBase": 
				gameObject.GetComponentInParent<EnemyController> ().Objects.Add (other.gameObject);
				break;
			default: ;
				break;
			}
		}

		void OnTriggerExit2D(Collider2D other)
		{
			string objectTag = other.tag;

			switch (objectTag) 
			{
			case "TowerPlayer": 
			case "PlayerBase": 
				gameObject.GetComponentInParent<EnemyController> ().Objects.Remove (other.gameObject);
				gameObject.GetComponentInParent<EnemyController> ().damageTargetObject = false;
				break;
			default: ;
				break;
			}
		}*/
