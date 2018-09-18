using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MYGUI;

namespace Player{

	public class PlayerHP : MonoBehaviour {

		[SerializeField] public float healPoint = 1231f;
        [SerializeField] public float _healPoint;
        [SerializeField] public GameObject gameOverMenu;

        public Texture HealsTexture;

        private float BarWidth;
        private float TextureWidth;

        // Use this for initialization
        void Start () {
            _healPoint = healPoint;
            BarWidth = Screen.width / 4;
            TextureWidth = BarWidth;
        }

		// Update is called once per frame
		void Update () {
            TextureWidth = BarWidth * (_healPoint / healPoint);

            if (_healPoint <= 0f) {
                TextureWidth = 0.2f;
                destroyPlayerInstance ();
			}
		}

		public void TakeDamage(float takeDamage)
		{
            _healPoint -= takeDamage;
		}

		public void destroyPlayerInstance(){
			gameOverMenu.GetComponent<PauseController>().SetTimeScale(true);

		}


        void OnGUI()
        {
            GUI.Box(new Rect(10, 10, BarWidth, 40), _healPoint + " / " + healPoint);

            if (HealsTexture != null && TextureWidth > 0)
            {
                GUI.DrawTexture(new Rect(10, 30, TextureWidth, 15), HealsTexture, ScaleMode.ScaleAndCrop, true, 10.0f);
            }
        }

    }
}
