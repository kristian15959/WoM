using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
[RequireComponent(typeof(playerCharacterAndEquip))]
public class PlayerController : MonoBehaviour {
	
	PlayerStats _ps;
	public Transform cam;
	public Animator anim;
    public float offsetRotation;
    public Transform guideObject;
    public LayerMask layerMask;


    //Quaternions
    Quaternion FinalRotation;
    Quaternion BeforeMoveRotation;



    [Header("Results")] 
    public float groundSlopeAngle = 0f;            // Angle of the slope in degrees
    public Vector3 groundSlopeDir = Vector3.zero;  // The calculated slope as a vector

    [Header("Settings")]
    public bool showDebug = false;                  // Show debug gizmos and lines
    public LayerMask castingMask;                  // Layer mask for casts. You'll want to ignore the player.
    public Transform groundSlopeChecker;
    public float startDistanceFromBottom = 0.2f;   // Should probably be higher than skin width
    public float sphereCastRadius = 0.25f;
    public float sphereCastDistance = 0.75f;       // How far spherecast moves down from origin point

    public float raycastLength = 0.75f;
    public Vector3 rayOriginOffset1 = new Vector3(-0.2f, 0f, 0.16f);
    public Vector3 rayOriginOffset2 = new Vector3(0.2f, 0f, -0.16f);

    void Start()
    {
        _ps = this.GetComponent<PlayerStats>();
    }

    void Update()
    {
        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            Movement();
            MovementRotation();
            CameraFollow();
            
        }else
        {         
            anim.SetFloat("Speed", 0);            
        }
        UpdatePlayerHeightPosition();
        CheckGround(new Vector3(groundSlopeChecker.position.x, groundSlopeChecker.position.y + startDistanceFromBottom, groundSlopeChecker.position.z));                

    }

    private void FixedUpdate()
    {
        
    }

    void Movement()
    {
        if (groundSlopeAngle >= 20)
            return;
        transform.position += transform.forward * Time.deltaTime * (_ps.Speed / 50);
        anim.SetFloat("Speed", 1);
        anim.SetFloat("AnimationSpeed", (_ps.Speed / 50));
    }

    void CameraFollow()
    {
        cam.position = transform.position + new Vector3(0,1,0);
    }

    void MovementRotation()
    {
        guideObject.localPosition = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));        
        transform.LookAt(new Vector3(guideObject.transform.position.x, transform.position.y, guideObject.transform.position.z));
    }

    void UpdatePlayerHeightPosition()
    {

        RaycastHit hit;
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), transform.TransformDirection(Vector3.down) * hit.distance, Color.black);
            //Debug.Log("Did Hit");
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }
    }

    public void CheckGround(Vector3 origin)
    {
        RaycastHit hit;
        
        if (Physics.SphereCast(origin, sphereCastRadius, Vector3.down, out hit, sphereCastDistance, castingMask))
        {
            groundSlopeAngle = Vector3.Angle(hit.normal, Vector3.up);
            
            Vector3 temp = Vector3.Cross(hit.normal, Vector3.down);
            groundSlopeDir = Vector3.Cross(temp, hit.normal);
        }
        
        RaycastHit slopeHit1;
        RaycastHit slopeHit2;
        
        if (Physics.Raycast(origin + rayOriginOffset1, Vector3.down, out slopeHit1, raycastLength))
        {
            if (showDebug) { Debug.DrawLine(origin + rayOriginOffset1, slopeHit1.point, Color.red); }
            float angleOne = Vector3.Angle(slopeHit1.normal, Vector3.up);
            
            if (Physics.Raycast(origin + rayOriginOffset2, Vector3.down, out slopeHit2, raycastLength))
            {
                if (showDebug) { Debug.DrawLine(origin + rayOriginOffset2, slopeHit2.point, Color.red); }
                float angleTwo = Vector3.Angle(slopeHit2.normal, Vector3.up);
                float[] tempArray = new float[] { groundSlopeAngle, angleOne, angleTwo };
                Array.Sort(tempArray);
                groundSlopeAngle = tempArray[1];
            }
            else
            {
                // 2 collision points (sphere and first raycast): AVERAGE the two
                float average = (groundSlopeAngle + angleOne) / 2;
                groundSlopeAngle = average;
            }
        }
    }
}