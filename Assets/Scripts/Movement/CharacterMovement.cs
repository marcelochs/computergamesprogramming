using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    private InputVector _input;

    [SerializeField]
    private GameObject _playerModel;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float rotateSpeed;


    private void Awake()
    {
        
         _input = GetComponent<InputVector>();
        
       
    }

    // Update is called once per frame
    void Update()
    {
        var targetVector = new Vector3(_input._inputVector.x, 0, _input._inputVector.y);

        

        //Move Direction
        MoveTowardVector(targetVector);

        //Rotate Direction
        RotateTowardVector(targetVector);
    }

    public void RotateTowardVector(Vector3 targetVector)
    {
        if(targetVector.magnitude == 0)
        {
            return;
        }
        var rotation = Quaternion.LookRotation(targetVector);
        _playerModel.transform.rotation = Quaternion.RotateTowards(_playerModel.transform.rotation, rotation, rotateSpeed);
    }

    public void MoveTowardVector(Vector3 targetVector)
    {
        var speed = moveSpeed * Time.deltaTime;
        transform.Translate(targetVector * speed);

    }

    public float getMoveSpeed()
    {
        return moveSpeed;
    }
}
