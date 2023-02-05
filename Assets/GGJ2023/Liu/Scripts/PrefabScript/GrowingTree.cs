using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using UnityEngine;

public class GrowingTree : MonoBehaviour
{
    public float growthDuration = 5.0f;
    public Vector3 finalScale = new Vector3(5, 5, 5);

    private Vector3 startScale;
    private Vector3 startPosition;
    private float startTime;

    void Start()
    {
        startScale = transform.localScale;
        startPosition = transform.position;
        startTime = Time.time;
        finalScale = startScale * 5;
        StartCoroutine(GrowTree());
    }

    IEnumerator GrowTree()
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < growthDuration)
        {
            elapsedTime = Time.time - startTime;
            float growthFactor = elapsedTime / growthDuration;

            Vector3 newScale = Vector3.Lerp(startScale, finalScale, growthFactor);
            Vector3 newPosition = startPosition + new Vector3(0, newScale.y / 4, 0) - new Vector3(0, startScale.y / 4, 0);

            transform.localScale = newScale;
            transform.position = newPosition;

            yield return null;
        }
    }
}
