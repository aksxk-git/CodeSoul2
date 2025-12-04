using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class InteractPrompt : MonoBehaviour
{
    [Header("")]
    [SerializeField] UnityEvent interact;

    InteractUI interactUI;
    public string interactText;
    public bool inRange;

    private void Awake()
    {
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<InteractUI>();
    }

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
