using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 0, -10f);

    private Vector3 shakeOffset = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 basePosition = target.position + offset;
        transform.position = basePosition + shakeOffset;
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        float timer = 0f;

        while (timer < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            shakeOffset = new Vector3(x, y, 0);

            timer += Time.deltaTime;
            yield return null;
        }

        shakeOffset = Vector3.zero;
    }
}