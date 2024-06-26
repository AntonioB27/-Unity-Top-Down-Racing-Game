using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipstreamHandler : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("Player") || collider.CompareTag("AI")){
            collider.GetComponent<CarController>().SetSpeedToSlipstreamSpeed();
        }
    }

    void OnTriggerExit2D(Collider2D collider){
        if(collider.CompareTag("Player") || collider.CompareTag("AI")){
            collider.GetComponent<CarController>().SetSpeedToInitialSpeed();
        }
    }
}
