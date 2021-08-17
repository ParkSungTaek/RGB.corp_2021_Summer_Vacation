﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPP_Cam : MonoBehaviour
{
    public float turnSpeed = 4.0f; // 마우스 회전 속도    
    private float xRotate = 0.0f;
    public GameObject SearchRay;
    public float moveSpeed = 4.0f;
    bool jump = false;

    float Vertical = 0f;
    float Horizontal = 0f;
    float jumpY = 20f;

    float V;
    float H;

    bool trg = false;

    float DTime = 0.0f;
    Vector3 Pos;
    // Start is called before the first frame update
    void Start()
    {
        SearchRay = GameObject.Find("SearchRay");
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obj")
        {
            V = Vertical;
            H = Horizontal;
            moveSpeed = 0f;
        }
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Obj")
        {
            moveSpeed = 0f;

            Vector3 move =
                    transform.forward * V +
                    transform.right * H;


            transform.position -= move * 0.01f;

        }

    }

    private void OnTriggerExit(Collider other)
    {
        moveSpeed = 4.0f;
    }
    // Update is called once per frame
    void Update()
    {

        if (Time.timeScale != 0f)
        {

            // 좌우로 움직인 마우스의 이동량 * 속도에 따라 카메라가 좌우로 회전할 양 계산
            float yRotateSize = Input.GetAxis("Mouse X") * turnSpeed;

            // 현재 y축 회전값에 더한 새로운 회전각도 계산
            float yRotate = transform.eulerAngles.y + yRotateSize;

            // 위아래로 움직인 마우스의 이동량 * 속도에 따라 카메라가 회전할 양 계산(하늘, 바닥을 바라보는 동작)
            float xRotateSize = -Input.GetAxis("Mouse Y") * turnSpeed;

            // 위아래 회전량을 더해주지만 -45도 ~ 80도로 제한 (-45:하늘방향, 80:바닥방향)
            xRotate = Mathf.Clamp(xRotate + xRotateSize, -45, 80);

            // 카메라 회전량을 카메라에 반영(X, Y축만 회전)
            transform.eulerAngles = new Vector3(xRotate, yRotate, 0);

            if (Input.GetKeyDown("space"))
            {
                jump = true;

                this.jumpY = 20f;
                DTime = 0f;

            }

            SearchRay.transform.position = this.transform.position;

            Vertical = Input.GetAxis("Vertical");
            Horizontal =  Input.GetAxis("Horizontal");



            Vector3 move =
                transform.forward * Vertical +
                transform.right * Horizontal;

                

            // 이동량을 좌표에 반영
            transform.position += move * moveSpeed * Time.deltaTime;
            
            


            if (!jump) {
                Pos = new Vector3(transform.position.x, 20, transform.position.z);
                transform.position = Pos;
            }
            else
            {
                DTime += Time.deltaTime;
                
                if (DTime < 0.4f)
                {
                    this.jumpY += 0.01f;
                    Pos = new Vector3(transform.position.x, this.jumpY, transform.position.z);
                }
                else if(DTime < 0.8f)
                {
                    this.jumpY -= 0.01f;
                    Pos = new Vector3(transform.position.x, this.jumpY, transform.position.z);

                }
                else
                {
                    jump = false;
                    this.jumpY = 20f;
                    DTime = 0f;
                }
                transform.position = Pos;


            }
             



        }
    }
    
}
