using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSFXHandler : MonoBehaviour
{
    [SerializeField] public AudioSource carEngineSound;
    [SerializeField] public AudioSource tyreScreechSound;


    float desiredEnginePitch = 0.2f;

    float tyreScreetchPitch = 0.5f;

    CarController carController;

    private void Awake()
    {
        carController = GetComponent<CarController>();
    }

    private void Update()
    {
        UpdateEngineSFX();
        UpdateTyreScreechSFX();
    }

    void UpdateEngineSFX()
    {
        float velocityMagnitude = carController.GetVelocityMagnitude();

        float desiredEngineVolume = velocityMagnitude * 0.05f;
        desiredEngineVolume = Mathf.Clamp(desiredEngineVolume, 0.2f, 0.4f);
        carEngineSound.volume = Mathf.Lerp(carEngineSound.volume, desiredEngineVolume, Time.deltaTime * 10);

        desiredEnginePitch = velocityMagnitude * 0.2f;
        desiredEnginePitch = Mathf.Clamp(desiredEnginePitch, 0.5f, 1f);
        carEngineSound.pitch = Mathf.Lerp(carEngineSound.pitch, desiredEnginePitch, Time.deltaTime * 1.5f);
    }

    void UpdateTyreScreechSFX()
    {
        if (carController.isTireScreeching(out float lateralVelocity, out bool isBraking))
        {
            if (isBraking)
            {
                tyreScreechSound.volume = Mathf.Lerp(tyreScreechSound.volume, 1f, Time.deltaTime * 10);
                tyreScreetchPitch = Mathf.Lerp(tyreScreetchPitch, 0.5f, Time.deltaTime * 10);
            }
            else
            {
                tyreScreechSound.volume = Mathf.Abs(lateralVelocity) * 0.05f;
                tyreScreetchPitch = Mathf.Abs(lateralVelocity) * 0.1f;
            }
        }
        else{
            tyreScreechSound.volume = Mathf.Lerp(tyreScreechSound.volume, 0, Time.deltaTime * 10);
        }
    }
}
