using UnityEngine;

public class PulseText : MonoBehaviour
{
    [SerializeField] private float minScale = 0.9f;
    [SerializeField] private float maxScale = 1.1f;
    [SerializeField] private float speed = 2f;

    private void Update()
    {
        float scale = Mathf.Lerp(minScale, maxScale, (Mathf.Sin(Time.unscaledTime * speed) + 1f) / 2f);
        transform.localScale = Vector3.one * scale;
    }
}