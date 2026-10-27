
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WeaponDrag : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string weaponName = "ïêäÌ";
    public int materiaSlotCount = 3;

    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private RectTransform dragImage;
    private bool isDragging;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();

        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        if (canvas == null)
        {
            Debug.LogError("WeaponDragÅFCanvasÇ™å©Ç¬Ç©ÇËÇ‹ÇπÇÒÅB");
            enabled = false;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Image source = GetComponent<Image>();

        if (source == null || source.sprite == null)
        {
            Debug.LogWarning("ïêäÌâÊëúÇÃSpriteÇ™ê›íËÇ≥ÇÍÇƒÇ¢Ç‹ÇπÇÒÅB");
            return;
        }

        GameObject copy = new GameObject(
            "Dragging_" + weaponName,
            typeof(RectTransform),
            typeof(CanvasRenderer),
            typeof(Image)
        );

        copy.transform.SetParent(canvas.transform, false);
        copy.transform.SetAsLastSibling();

        Image image = copy.GetComponent<Image>();
        image.sprite = source.sprite;
        image.preserveAspect = true;
        image.raycastTarget = false;

        dragImage = copy.GetComponent<RectTransform>();
        dragImage.sizeDelta = GetComponent<RectTransform>().rect.size;

        canvasGroup.blocksRaycasts = false;
        isDragging = true;

        MoveDragImage(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging || dragImage == null)
            return;

        MoveDragImage(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragImage != null)
            Destroy(dragImage.gameObject);

        if (canvasGroup != null)
            canvasGroup.blocksRaycasts = true;

        isDragging = false;
    }

    private void MoveDragImage(PointerEventData eventData)
    {
        RectTransform canvasRect = canvas.transform as RectTransform;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint))
        {
            dragImage.localPosition = localPoint;
        }
    }
}
