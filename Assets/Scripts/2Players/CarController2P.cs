using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarController2P : MonoBehaviour
{
    CarPreformacneHandler2P preformacneHandler;
    private float df = 0;
    private float ms = 0;
    float initalSpeed = 0;
    private float tf = 0;
    float accelerationInput = 0;
    float steeringInput = 0;
    float rotationAngle = 0;

    float velocityVsUp = 0;
    private bool freshRubber = true;
    Rigidbody2D carRigidbody2D;
    private LapCounter lapCounter;
    CheckpointHandler checkpointHandler;
    private bool isInPitStop = false;
    bool isRaceStarted = false;

    private void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
        checkpointHandler = GetComponent<CheckpointHandler>();
        preformacneHandler = GetComponent<CarPreformacneHandler2P>();
        lapCounter = GetComponent<LapCounter>();
    }

    private void Start()
    {
        preformacneHandler.GetPreformacneParameters();
        df = preformacneHandler.driftFactor;
        ms = preformacneHandler.maxSpeed;
        tf = preformacneHandler.turnFactor;
        initalSpeed = ms;
    }

    private void FixedUpdate()
    {
        if (!isInPitStop)
        {
            ApplyEngineForce();
            KillOrthogonalVelocity();
            ApplySteeringForce();
            CheckTyreWear();
            if (lapCounter.isRaceFinished)
                RaceFinishedMode();
        }
        else
        {
            KillEngineForce();
        }

        if(!isRaceStarted){
            KillEngineForce();
        }
    }

    private void KillEngineForce()
    {
        carRigidbody2D.velocity = Vector3.zero;
    }

    public float GetVelocityMagnitude()
    {
        return carRigidbody2D.velocity.magnitude;
    }

    private void ApplyEngineForce()
    {
        velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

        if (velocityVsUp > preformacneHandler.maxSpeed && accelerationInput > 0)
        {
            return;
        }

        if (velocityVsUp < -preformacneHandler.maxSpeed * 0.3 && accelerationInput < 0)
        {
            return;
        }

        if (carRigidbody2D.velocity.sqrMagnitude > preformacneHandler.maxSpeed * preformacneHandler.maxSpeed && accelerationInput > 0)
        {
            return;
        }

        if (accelerationInput == 0)
        {
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 1.5f, Time.fixedDeltaTime * 1.5f);
        }
        else carRigidbody2D.drag = 0;

        Vector2 engineForceVector = transform.up * accelerationInput * preformacneHandler.acceleration;

        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteeringForce()
    {
        float minSpeedBeforeTurning = Mathf.Clamp01(carRigidbody2D.velocity.magnitude / 8);

        float velocity = carRigidbody2D.velocity.magnitude;

        float velocityClamped = velocity / preformacneHandler.maxSpeed;

        float calculatedTurningFactor = Mathf.Lerp(10, preformacneHandler.turnFactor, velocityClamped);

        rotationAngle -= calculatedTurningFactor * steeringInput * minSpeedBeforeTurning;

        carRigidbody2D.MoveRotation(rotationAngle);
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }

    public void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);

        carRigidbody2D.velocity = forwardVelocity + rightVelocity * preformacneHandler.driftFactor;
    }

    float GetLateralVelocity()
    {
        return Vector2.Dot(transform.right, carRigidbody2D.velocity);
    }

    public bool isTireScreeching(out float lateralVelocity, out bool isBraking)
    {
        lateralVelocity = GetLateralVelocity();
        isBraking = false;

        if (accelerationInput < 0 && velocityVsUp > 0)
        {
            isBraking = true;
            return true;
        }

        if (Mathf.Abs(GetLateralVelocity()) > 1.0f)
        {
            return true;
        }

        return false;
    }

    void CheckTyreWear()
    {
        freshRubber = true;
    }

    public void resetTyreWear()
    {
        preformacneHandler.driftFactor = df;
        preformacneHandler.turnFactor = tf;
        freshRubber = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Sand"))
        {
            if (preformacneHandler.driftFactor < 0.9f)
            {
                df = preformacneHandler.driftFactor;
                ms = preformacneHandler.maxSpeed;
                preformacneHandler.driftFactor = 0.9f;
                preformacneHandler.maxSpeed = 3;
            }
            else
            {
                preformacneHandler.driftFactor = 1f;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Finish"))
        {
            if (tag != "AI" && checkpointHandler.didHitAllCheckpoints())
            {
                checkpointHandler.resetAllHitCheckpoints();
            }
        }

        if (collision.gameObject.tag.Equals("Sand"))
        {
            preformacneHandler.driftFactor = df;
            preformacneHandler.maxSpeed = ms;
        }
    }

    public void SetSpeedToInitialSpeed()
    {
        preformacneHandler.maxSpeed = initalSpeed;
    }

    public bool getIsFreshRubber()
    {
        return freshRubber;
    }

    public void setIsInPitStop(bool isInPitStop_)
    {
        isInPitStop = isInPitStop_;
    }

    public void SetRaceStarted(){
        isRaceStarted = true;
    }

    private void RaceFinishedMode()
    {
        if (tag.Equals("Player") && lapCounter.isRaceFinished)
        {
            this.AddComponent<CarAIHandler>();
            GetComponent<CarInputHandler>().enabled = false;
            ReturnToHome(10);
        }
    }

    IEnumerator ReturnToHome(int seconds)
    {
        int count = seconds;

        while (count >= 0)
        {
            yield return new WaitForSeconds(1);
            count--;
        }

        SceneManager.LoadScene("SelectionGroup");
    }
}
