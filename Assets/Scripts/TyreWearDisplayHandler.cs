using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TyreWearDisplayHandler : MonoBehaviour
{
    UnityEngine.UI.Image image;
    CarController carController;

    private void Awake()
    {
        image = GetComponent<UnityEngine.UI.Image>();
        carController = GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>();
    }

    private void Start()
    {
    }

    private void Update()
    {
        CheckColor();
    }

    private void CheckColor()
    {
        if (carController.getIsFreshRubber())
        {
            
            image.color = Color.blue;
        }

        if (carController.getIsNewRubber() && !carController.getIsOldRubber() && !carController.getIsNoRubber())
        {
            image.color = Color.green;
        }

        if (carController.getIsNewRubber() && carController.getIsOldRubber() && !carController.getIsNoRubber())
        {
            image.color = Color.red;
        }

        if (carController.getIsNewRubber() && carController.getIsOldRubber() && carController.getIsNoRubber())
        {
            image.color = Color.black;
        }
    }
}
