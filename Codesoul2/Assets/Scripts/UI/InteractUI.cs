using TMPro;
using UnityEngine;

public class InteractUI : MonoBehaviour
{
    public GameObject prompt;
    public TMP_Text promptText;

    public void ShowPrompt(string text)
    {
        if (prompt != null)
        {
            prompt.SetActive(true);
            promptText.text = text;
        }
    }

    public void HidePrompt()
    {
        if (prompt != null)
        {
            prompt.SetActive(false);
        }
    }
}
