using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float distance;
    bool moveUp = true;
    float maxDistance;
    float minDistance;
    [SerializeField] MovementType movementType;
    

    private void Awake()
    {
        if (movementType == MovementType.walking)
        {
            maxDistance = distance + transform.position.x;
            minDistance = transform.position.x;

        }
            
        
    }


    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {

        if (movementType == MovementType.walking)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
            if (moveUp)
            {
                if (transform.position.x >= maxDistance)
                {
                    moveUp = false;
                    transform.rotation = Quaternion.LookRotation(-transform.forward, Vector3.up);
                }
            }
            else 
            {           
                if (transform.position.x <= minDistance)
                {
                    moveUp = true;
                    transform.rotation = Quaternion.LookRotation(-transform.forward, Vector3.up);
                }
            }
            

        }
        if (movementType == MovementType.rotating)
        {

        }
    }

    enum MovementType{
        standing,
        walking,
        rotating
    }
}
