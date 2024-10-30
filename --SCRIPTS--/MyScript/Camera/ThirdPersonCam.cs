using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]

    public Transform orientaton;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;


    public float rotSpeed;

    public Transform combatLookAt;

    public CameraStyle currentStyle;

    public GameObject combatCam,normalCam;

    public enum CameraStyle
    {
        Basic,
        Combat
    }


    private void Start()
    {
        normalCam.SetActive(false); 
        combatCam.SetActive(true);
    }

    private void Update()
    {
        //rotate orientation
        Vector3 newDir = player.position - new Vector3 (transform.position.x, player.position.y, transform.position.z);
        orientaton.forward = newDir.normalized;

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            currentStyle = CameraStyle.Basic;
            combatCam.SetActive(false);
            normalCam.SetActive(true);
        }
      
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            currentStyle = CameraStyle.Combat;
        
        combatCam.SetActive(true);
        normalCam.SetActive(false);
        }

        if(currentStyle == CameraStyle.Basic)
        {
            //rotate Player Obj
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 inputDir = orientaton.forward * vertical + orientaton.right * horizontal;

            if (inputDir != Vector3.zero)
            {
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, rotSpeed * Time.deltaTime);
            }
        }else if (currentStyle == CameraStyle.Combat)
        {
            Vector3 dirToCombatLookAt = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
            orientaton.forward = dirToCombatLookAt.normalized;

            playerObj.forward = dirToCombatLookAt.normalized;

        }
    }


  
}
