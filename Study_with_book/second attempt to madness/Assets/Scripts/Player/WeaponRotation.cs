using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GUI;

namespace Player{

public class WeaponRotation : MonoBehaviour {
    	public Sprite[] sprites = new Sprite[12];

    	[SerializeField] public Joystick rotationJoystick;
		[SerializeField] public GameObject spawnBulletPosition;
   	 	private Transform gunPosition;

    	public int playerSection = 4;

		public float shiftX = 0.0F;
		public float shiftY = 0.0F;


    	void Start () 
		{
     	 gunPosition = GetComponent<Transform>();
    	}
	

		void Update () 
		{
		RotationJoystick handlePosition = rotationJoystick.GetComponent<RotationJoystick>();
		CalcPositionJoystick(handlePosition.Xpos, handlePosition.Ypos);
   		}

    public void CalcPositionJoystick(float positionX, float positionY)
    {
        //вычисление плоскости для поворота оружия джойстиком
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

        GetComponent<SpriteRenderer>().sprite = sprites[playerSection];
    }

    	private void SetPosition(float posX, float posY)
    	{
			spawnBulletPosition.transform.localPosition = new Vector3(posX, posY, gunPosition.localPosition.z);
   		}

}
}
