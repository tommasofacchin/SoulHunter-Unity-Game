using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor.Rendering;

public class CameraScript : MonoBehaviour
{

    public CinemachineVirtualCamera cam;
    public float distance;


    private PlayerScript player;
    private bool isCharging;
    private bool isShifting;

    public PolygonCollider2D bound;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        isCharging = false;
        isShifting = false;
    }


    private void FixedUpdate()
    {
        /*
        Vector3 targetPos = player.position;
        //targetPos.x = Mathf.Clamp(targetPos.x, bound.bounds.min.x, bound.bounds.max.x);
        //targetPos.y = Mathf.Clamp(targetPos.y, bound.bounds.min.y, bound.bounds.max.y);
        transform.position =  Vector3.Lerp(transform.position, targetPos, Time.deltaTime);
        */
        
        
        /*
        if (Input.GetKey(KeyCode.LeftShift) && !isCharging)
        {
            isShifting = true;
            if(distance < 12)
            {
                distance += .4f;
            }
            else if (distance < 12.5)
            {
                distance += .1f;
            }
            else if (distance < 13)
            {
                distance += .05f;
            }
            else
            {
                distance = 13;
            }
        }
        else
        {
            if (distance > 10)
            {
                distance -= .3f;
            }
            if (distance < 10 && !isCharging)
            {
                isShifting = false;
                distance = 10;
            }
        }


        if (Input.GetKey(KeyCode.F) && player.lp < 100 && player.mediKitCounter > 0)
        {
                isCharging=true;
                distance -= .01f;
        }
        else
        {
            if (distance < 10) distance += .2f;
            if (distance > 10 && !isShifting)
            {
                distance = 10;
                isCharging = false;
            }
        }

        
        cam.GetComponent<CinemachineVirtualCamera>()
            .GetCinemachineComponent<CinemachineFramingTransposer>()
            .m_CameraDistance = distance;
        */

    }


        private void Start()
    {
        cam.GetComponent<CinemachineVirtualCamera>()
            .GetCinemachineComponent<CinemachineFramingTransposer>()
            .m_CameraDistance = distance;
    }
}
