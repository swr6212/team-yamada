
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MateriaDrag : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string materiaName = "マテリア";

    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private RectTransform dragImage;

    private bool isDragging;
    private bool equippedSuccessfully;

    private Transform originalParent;
    private int originalSiblingIndex;
    private Vector2 originalAnchoredPosition;
    private Vector2 originalSizeDelta;
    private Vector3 originalLocalScale;
    private Vector2 originalAnchorMin;
    private Vector2 originalAnchorMax;
    private Vector2 originalPivot;
    private Vector3 originalLocalPosition;
    private Quaternion originalLocalRotation;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();

        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        if (canvas == null)
        {
            Debug.LogError("MateriaDrag：親にCanvasが見つかりません。");
            enabled = false;
            return;
        }

        SaveOriginalPosition();
    }

    private void SaveOriginalPosition()
    {
        RectTransform rect = GetComponent<RectTransform>();

        originalParent = transform.parent;
        originalSiblingIndex = transform.GetSiblingIndex();

        if (rect == null)
            return;

        originalAnchoredPosition = rect.anchoredPosition;
        originalSizeDelta = rect.sizeDelta;
        originalLocalScale = rect.localScale;
        originalAnchorMin = rect.anchorMin;
        originalAnchorMax = rect.anchorMax;
        originalPivot = rect.pivot;
        originalLocalPosition = rect.localPosition;
        originalLocalRotation = rect.localRotation;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        equippedSuccessfully = false;

        Image sourceImage = GetComponent<Image>();

        if (sourceImage == null || sourceImage.sprite == null)
        {
            Debug.LogWarning("マテリア画像のSpriteが設定されていません。");
            return;
        }

        GameObject copy = new GameObject(
            "Dragging_" + materiaName,
            typeof(RectTransform),
            typeof(CanvasRenderer),
            typeof(Image)
        );

        copy.transform.SetParent(canvas.transform, false);
        copy.transform.SetAsLastSibling();

        Image image = copy.GetComponent<Image>();
        image.sprite = sourceImage.sprite;
        image.preserveAspect = true;
        image.raycastTarget = false;

        dragImage = copy.GetComponent<RectTransform>();

        RectTransform sourceRect = GetComponent<RectTransform>();
        dragImage.sizeDelta = sourceRect.rect.size;

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

        dragImage = null;

        if (canvasGroup != null)
            canvasGroup.blocksRaycasts = true;

        isDragging = false;

        if (equippedSuccessfully)
            gameObject.SetActive(false);
    }

    public void MarkEquipped()
    {
        equippedSuccessfully = true;
    }

    // MateriaSlotManagerから呼び出す復元処理
    public void ReturnToOriginalPosition()
    {
        if (originalParent == null)
        {
            Debug.LogWarning("マテリアの元の親が見つかりません。");
            gameObject.SetActive(true);
            return;
        }

        transform.SetParent(originalParent, false);

        RectTransform rect = GetComponent<RectTransform>();

        if (rect != null)
        {
            rect.anchorMin = originalAnchorMin;
            rect.anchorMax = originalAnchorMax;
            rect.pivot = originalPivot;
            rect.sizeDelta = originalSizeDelta;
            rect.anchoredPosition = originalAnchoredPosition;
            rect.localScale = originalLocalScale;
            rect.localRotation = originalLocalRotation;
            rect.localPosition = originalLocalPosition;
        }

        int lastIndex = originalParent.childCount - 1;

        transform.SetSiblingIndex(
            Mathf.Clamp(originalSiblingIndex, 0, lastIndex)
        );

        gameObject.SetActive(true);

        Debug.Log(materiaName + " を元の位置・サイズに戻しました！");
    }

    private void MoveDragImage(PointerEventData eventData)
    {
        if (canvas == null || dragImage == null)
            return;

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
