using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class PointsUI : MonoBehaviour
{
    [SerializeField] GameManager gm;
    public TMP_Text pointsText;

    private void Update()
    {
        pointsText.text = gm.playerScore.ToString();
    }
}
