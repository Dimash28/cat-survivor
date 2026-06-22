using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] private SoundSO hoverSound;
    [SerializeField] private SoundSO clickSound;

    public void OnPointerEnter(PointerEventData eventData)
    {
        G.audio.Play(hoverSound);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        G.audio.Play(clickSound);
    }
}