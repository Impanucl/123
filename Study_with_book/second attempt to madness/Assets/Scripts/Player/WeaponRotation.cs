using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour {
    public Sprite[] sprites = new Sprite[12];

    [SerializeField] public Joystick rotationJoystick;
    private Transform gunPosition;

    public int playerSection = 4;


    void Start () {
      gunPosition = GetComponent<Transform>();
    }
	

	void Update () {
        RotationJoystick position = rotationJoystick.GetComponent<RotationJoystick>();
        CalcPositionJoystick(position.Xposition, position.Yposition);
    }

    public void CalcPositionJoystick(float positionX, float positionY)
    {
        //вычисление плоскости для поворота оружия джойстиком
        if ((positionY > 0) && (positionX > -positionY / 3) && (positionX < positionY / 3))
        {
            playerSection = 0;
            SetPosition(0.07F , 0.33F);
        }

        if ((positionY > 0) && (positionX > positionY / 3) && (positionX < positionY))
        {
            playerSection = 1;
            SetPosition(0.07F, 0.28F);
        }

        if ((positionY > 0) && (positionX > positionY) && (positionX < positionY * 3))
        {
            playerSection = 2;
            SetPosition(0.09F, 0.28F);
        }

        if ((positionX > 0) && (positionY > -positionX / 3) && (positionY < positionX / 3))
        {
            playerSection = 3;
            SetPosition(0.10F, 0.20F);
        }

        if ((positionX > 0) && (positionY > -positionX) && (positionY < -positionX / 3))
        {
            playerSection = 4;
            SetPosition(0.09F, 0.18F);
        }

        if ((positionX > 0) && (positionY > -positionX * 3) && (positionY < -positionX))
        {
            playerSection = 5;
            SetPosition(0.12F, 0.15F);
        }

        if ((positionY < 0) && (positionX > positionY / 3) && (positionX < -positionY / 3))
        {
            playerSection = 6;
            SetPosition(0.02F, 0.10F);
        }

        if ((positionY < 0) && (positionX > positionY) && (positionX < positionY / 3))
        {
            playerSection = 7;
            SetPosition(0.04F, 0.13F);
        }

        if ((positionY < 0) && (positionX > positionY * 3) && (positionX < positionY))
        {
            playerSection = 8;
            SetPosition(0.04F, 0.20F);
        }

        if ((positionX < 0) && (positionY > positionX / 3) && (positionY < -positionX / 3))
        {
            playerSection = 9;
            SetPosition(-0.03F, 0.20F);
        }

        if ((positionX < 0) && (positionY > -positionX / 3) && (positionY < -positionX))
        {
            playerSection = 10;
            SetPosition(-0.01F, 0.30F);
        }

        if ((positionX < 0) && (positionY > -positionX) && (positionY < -positionX * 3))
        {
            playerSection = 11;
            SetPosition(0.04F, 0.28F);
        }

        GetComponent<SpriteRenderer>().sprite = sprites[playerSection];
    }

    private void SetPosition(float posX, float posY)
    {
            gunPosition.localPosition = new Vector3(posX, posY, gunPosition.localPosition.z);
    }
}
