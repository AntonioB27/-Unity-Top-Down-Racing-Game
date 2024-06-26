using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputHandler2P : MonoBehaviour
{
    CarController2P carController;

    public int player;

    private void Awake()
    {
        carController = GetComponent<CarController2P>();
    }

    void Update()
    {
        SetInput();
    }

    void SetInput()
    {
        Vector2 inputVector = Vector2.zero;

        switch (player)
        {
            case 1:
                inputVector.x = Input.GetAxis("Horizontal_P1");
                inputVector.y = Input.GetAxis("Vertical_P1");
                break;
            case 2:
                inputVector.x = Input.GetAxis("Horizontal_P2");
                inputVector.y = Input.GetAxis("Vertical_P2");
                break;
        }

        carController.SetInputVector(inputVector);
    }
}
