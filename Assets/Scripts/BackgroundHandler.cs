
using UnityEngine;
using UnityEngine.UI;

public class BackgroundHandler : MonoBehaviour
{
    public Image austria;
    public Image bahrein;
    public Image belgium;

    public void OnHoverSetBackgroundAustria()
    {
        austria.enabled = true;
        bahrein.enabled = false;
        belgium.enabled = false;
    }

    public void OnHoverSetBackgroundBahrein()
    {
        austria.enabled = false;
        bahrein.enabled = true;
        belgium.enabled = false;
    }

    public void OnHoverSetBackgroundBelgium()
    {
        austria.enabled = false;
        bahrein.enabled = false;
        belgium.enabled = true;
    }
}
