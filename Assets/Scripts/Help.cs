using UnityEngine;

public class Help : MonoBehaviour
{
    public GameObject helpWindow;

    private static bool isOpen = false;


    public void ToggleHelp()
    {
        if (isOpen)
        {
            HideHelp();
            return;
        }

        helpWindow.SetActive(true);
        helpWindow.transform.localScale = new Vector3(1, 0, 1);
        LeanTween.scaleY(helpWindow, 1.0f, 0.25f)
            .setEaseOutCirc()
            .setIgnoreTimeScale(true);
        isOpen = true;
    }

    public void HideHelp()
    {
        if (!isOpen)
        {
            return;
        }

        LeanTween.scaleY(helpWindow, 0.0f, 0.25f)
            .setEaseOutCirc()
            .setIgnoreTimeScale(true)
            .setOnComplete(() => helpWindow.SetActive(false));
        isOpen = false;
    }
}
