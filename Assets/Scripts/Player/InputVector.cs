using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputVector : MonoBehaviour
{
    public Vector2 _inputVector { get; private set; }

    

    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        _inputVector = new Vector2(horizontal, vertical);
        
    }
}
