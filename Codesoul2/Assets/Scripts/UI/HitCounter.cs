using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HitCounter : MonoBehaviour
{
    public TMP_Text dmgCounter;
    public float damage;

    public Vector3 randomizeLocalPosition;

    private void Start()
    {
        SetCounterText(damage);
        GiveRandomPosition();
        Destroy(gameObject, .5f);
    }

    private void SetCounterText(float damage)
    {
        dmgCounter.text = damage.ToString("F0"); 
    }

    private void GiveRandomPosition()
    {
        randomizeLocalPosition.x = Random.Range(-0.5f, 0.5f);
        randomizeLocalPosition.y = Random.Range(1, 2);

        gameObject.transform.position += randomizeLocalPosition;

    }
}
