using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public bool isShake = false;
    public AnimationCurve curve;
    public float duration = 0.5f;

    private void Update()
    {
        if (isShake)
        {
            isShake = false;
            StartCoroutine(IE_shake());
        }
    }

    public void Shake()
    {
        isShake = true;
    }

    private IEnumerator IE_shake()
    {
        Vector3 startPos = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPos + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = new Vector3(0, 0, -10);
    }
}