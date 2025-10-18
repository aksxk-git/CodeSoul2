using UnityEngine;
using UnityEngine.UI;

public class SpinUIEffect : MonoBehaviour
{
    private void Update()
    {
        gameObject.transform.Rotate(0, 0, 5 * Time.deltaTime);
    }
}
