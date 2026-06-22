using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SegmentSlider : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [SerializeField] private int segmentCount = 10;
    [SerializeField] private Sprite activeSegment;
    [SerializeField] private Sprite inactiveSegment;
    [SerializeField] private Transform segmentsContainer;
    [SerializeField] private GameObject segmentPrefab;
    [SerializeField] private RectTransform sliderRect;
    [SerializeField] private RectTransform handleRect;

    public System.Action<float> onValueChanged;

    private List<Image> segments = new List<Image>();
    private float value = 1f;

    private void Awake()
    {
        for (int i = 0; i < segmentCount; i++)
        {
            GameObject seg = Instantiate(segmentPrefab, segmentsContainer);
            Image img = seg.GetComponent<Image>();
            segments.Add(img);
        }

        SetValue(value);
    }

    public void SetValue(float newValue)
    {
        value = Mathf.Clamp01(newValue);
        int activeCount = Mathf.RoundToInt(value * segmentCount);

        for (int i = 0; i < segments.Count; i++)
            segments[i].sprite = i < activeCount ? activeSegment : inactiveSegment;

        float handleX = Mathf.Lerp(sliderRect.rect.xMin, sliderRect.rect.xMax, value);
        handleRect.anchoredPosition = new Vector2(handleX, handleRect.anchoredPosition.y);
    }

    public float GetValue() => value;

    public void OnPointerDown(PointerEventData eventData)
    {
        UpdateValue(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateValue(eventData);
    }

    private void UpdateValue(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            sliderRect, eventData.position, eventData.pressEventCamera, out Vector2 localPoint);

        float newValue = Mathf.InverseLerp(
            sliderRect.rect.xMin,
            sliderRect.rect.xMax,
            localPoint.x);

        SetValue(newValue);
        onValueChanged?.Invoke(value);
    }
}