using TMPro;
using UnityEngine;

public class InteractUI : MonoBehaviour
{
    public GameObject prompt;
    public TMP_Text promptText;

    public void ShowPrompt(string text)
    {
        prompt.SetActive(true);
        promptText.text = text;
    }

    public void HidePrompt()
    {
        prompt.SetActive(false);
    }
}
