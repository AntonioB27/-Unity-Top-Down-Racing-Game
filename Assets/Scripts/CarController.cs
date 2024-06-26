using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    CarPreformacneHandler preformacneHandler;
    private float df = 0;
    private float ms = 0;
    float initalSpeed = 0;
    float slipstreamSpeed = 0;
    private float tf = 0;
    float accelerationInput = 0;
    float steeringInput = 0;
    float rotationAngle = 0;

    float velocityVsUp = 0;
    private bool freshRubber = true;
    private bool newRubber = false;
    private bool oldRubber = false;
    private bool noRubber = false;
    Rigidbody2D carRigidbody2D;
    TyreWearHandler tyreWearHandler;
    public float distanceToNewRubber;
    public float distanceToOldRubber;
    public float distanceToNoRubber;
    private TrackTimeHandler fastestLapTimer;
    private LapCounter lapCounter;
    CheckpointHandler checkpointHandler;
    private bool isInPitStop = false;
    bool isRaceStarted = false;

    private void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
        tyreWearHandler = GetComponent<TyreWearHandler>();
        checkpointHandler = GetComponent<CheckpointHandler>();
        preformacneHandler = GetComponent<CarPreformacneHandler>();
        lapCounter = GetComponent<LapCounter>();

        fastestLapTimer = GameObject.FindGameObjectWithTag("HUD").GetComponent<TrackTimeHandler>();
    }

    private void Start()
    {
        preformacneHandler.GetPreformacneParameters();
        df = preformacneHandler.driftFactor;
        ms = preformacneHandler.maxSpeed;
        tf = preformacneHandler.turnFactor;
        initalSpeed = ms;
        slipstreamSpeed = initalSpeed + 0.2f;
    }

    private void FixedUpdate()
    {
        SendLogs();

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

        if (!isRaceStarted)
        {
            KillEngineForce();
        }
    }

    void SendLogs(){
        Debug.Log("Message to console window");
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
        if (tyreWearHandler.getDistanceTraveled() > distanceToNewRubber && !newRubber)
        {
            preformacneHandler.driftFactor += 0.05f;
            preformacneHandler.turnFactor -= 0.5f;
            newRubber = true;
            freshRubber = false;
        }

        if (tyreWearHandler.getDistanceTraveled() > distanceToOldRubber && !oldRubber)
        {
            preformacneHandler.driftFactor += 0.1f;
            preformacneHandler.turnFactor -= 0.5f;
            oldRubber = true;
        }

        if (tyreWearHandler.getDistanceTraveled() > distanceToNoRubber)
        {
            preformacneHandler.driftFactor = 0.95f;
            noRubber = true;
        }
    }

    public void resetTyreWear()
    {
        preformacneHandler.driftFactor = df;
        preformacneHandler.turnFactor = tf;
        freshRubber = true;
        newRubber = false;
        oldRubber = false;
        noRubber = false;
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
                fastestLapTimer.setFastestLap();
                checkpointHandler.resetAllHitCheckpoints();
            }
        }

        if (collision.gameObject.tag.Equals("Sand"))
        {
            preformacneHandler.driftFactor = df;
            preformacneHandler.maxSpeed = ms;
        }
    }

    public void SetSpeedToSlipstreamSpeed()
    {
        preformacneHandler.maxSpeed = slipstreamSpeed;
    }

    public void SetSpeedToInitialSpeed()
    {
        preformacneHandler.maxSpeed = initalSpeed;
    }

    public bool getIsFreshRubber()
    {
        return freshRubber;
    }

    public bool getIsNewRubber()
    {
        return newRubber;
    }

    public bool getIsOldRubber()
    {
        return oldRubber;
    }

    public bool getIsNoRubber()
    {
        return noRubber;
    }

    public void setIsInPitStop(bool isInPitStop_)
    {
        isInPitStop = isInPitStop_;
    }

    public void SetRaceStarted()
    {
        isRaceStarted = true;
    }

    private void RaceFinishedMode()
    {
        if (tag.Equals("Player") && lapCounter.isRaceFinished)
        {
            this.AddComponent<CarAIHandler>();
            GetComponent<CarInputHandler>().enabled = false;
            StartCoroutine(ReturnToHome(5));
        }
    }

    IEnumerator ReturnToHome(int seconds)
    {
        int count = seconds;

        while (count >= 0)
        {
            Debug.Log(count);
            yield return new WaitForSeconds(1);
            count--;
        }

        SceneManager.LoadScene("GroupSelection");
    }
}
