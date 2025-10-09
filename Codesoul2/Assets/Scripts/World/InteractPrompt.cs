using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class InteractPrompt : MonoBehaviour
{
    [SerializeField] UnityEvent interact;
    [SerializeField] InteractUI interactUI;
    public string interactText;
    public bool inRange;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && inRange)
        {
            interact.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactUI.ShowPrompt(interactText);
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactUI.HidePrompt();
            inRange = false;
        }
    }
}
