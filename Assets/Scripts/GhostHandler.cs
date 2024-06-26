
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GhostHandler : MonoBehaviour
{
    private Transform carTransform;
    private Transform ghostCarTransform;
    private SpriteRenderer ghostCarSpriteRenderer;

    private SpriteRenderer carSpriteRenderer;

    public int savePositionEveryNFrames = 2;

    private int frameCounter = 0;

    private int i = 0;

    List<Vector3> carPositionArray = new List<Vector3>();
    List<Quaternion> carRotationArray = new List<Quaternion>();

    List<Vector3> carPositionArrayFastestLap = new List<Vector3>();
    List<Quaternion> carRotationArrayFastestLap = new List<Quaternion>();


    private void Start(){
        ghostCarSpriteRenderer = GameObject.FindGameObjectWithTag("GhostCar").GetComponent<SpriteRenderer>();
        ghostCarTransform = GameObject.FindGameObjectWithTag("GhostCar").GetComponent<Transform>();
        carSpriteRenderer = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SpriteRenderer>();
        carTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        ghostCarSpriteRenderer.color = new Color(1f,1f,1f,0.5f);
        ghostCarSpriteRenderer.sprite = carSpriteRenderer.sprite;
    }

    private void FixedUpdate() {
        SaveGhostPosition();
        DisplayGhostCar();
    }

    private void SaveGhostPosition(){
        if(frameCounter == savePositionEveryNFrames){
            carPositionArray.Add(carTransform.position);
            carRotationArray.Add(carTransform.rotation);
            frameCounter = 0;
        }

        frameCounter++;
    }

    public void SetFastestLapArray(){
        carPositionArrayFastestLap.Clear();
        carRotationArrayFastestLap.Clear();
        carPositionArrayFastestLap.AddRange(carPositionArray);
        carRotationArrayFastestLap.AddRange(carRotationArray);
        carPositionArray.Clear();
        carRotationArray.Clear();
        i=0;
    }

    private void DisplayGhostCar(){
        if(carPositionArrayFastestLap.Count > 0 && frameCounter == savePositionEveryNFrames && carPositionArrayFastestLap.Count > i){
            ghostCarTransform.position = carPositionArrayFastestLap.ElementAt(i);
            ghostCarTransform.rotation = carRotationArrayFastestLap.ElementAt(i);
            i++;
        }
    }

    public void ResetCounter(){
        i=0;
        carPositionArray.Clear();
        carRotationArray.Clear();
    }
}
