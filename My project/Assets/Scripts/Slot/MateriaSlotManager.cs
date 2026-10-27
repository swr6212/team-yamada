
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class MateriaSlotManager : MonoBehaviour
{
    [Header("マテリア装着エリア")]
    [SerializeField] private Transform slotParent;

    [Header("マテリア一覧")]
    [SerializeField] private Transform materiaListParent;

    [Header("スロット設定")]
    [SerializeField] private Vector2 slotSize = new Vector2(70f, 70f);
    [SerializeField] private float slotSpacing = 10f;

    [Header("スロット背景色")]
    [SerializeField]
    private Color slotColor =
        new Color(0.2f, 0.2f, 0.2f, 0.9f);

    private readonly List<GameObject> slots = new List<GameObject>();

    public void ShowSlots(int slotCount)
    {
        // 既存スロット内のマテリアを一覧に戻してから削除
        foreach (GameObject slotObject in slots)
        {
            if (slotObject == null)
                continue;

            MateriaDropSlot dropSlot =
                slotObject.GetComponent<MateriaDropSlot>();

            if (dropSlot != null && dropSlot.HasMateria)
            {
                dropSlot.ReturnEquippedMateria();
            }

            Destroy(slotObject);
        }

        slots.Clear();

        if (slotParent == null)
        {
            Debug.LogError(
                "MateriaSlotManager：Slot Parentが未設定です。"
            );
            return;
        }

        slotCount = Mathf.Max(0, slotCount);

        for (int i = 0; i < slotCount; i++)
        {
            GameObject slotObject = new GameObject(
                "MateriaSlot" + (i + 1),
                typeof(RectTransform),
                typeof(CanvasRenderer),
                typeof(Image),
                typeof(MateriaDropSlot)
            );

            slotObject.transform.SetParent(slotParent, false);

            RectTransform rect =
                slotObject.GetComponent<RectTransform>();

            rect.sizeDelta = slotSize;
            rect.anchorMin = new Vector2(0f, 0.5f);
            rect.anchorMax = new Vector2(0f, 0.5f);
            rect.pivot = new Vector2(0f, 0.5f);

            rect.anchoredPosition = new Vector2(
                i * (slotSize.x + slotSpacing),
                0f
            );

            Image background = slotObject.GetComponent<Image>();
            background.color = slotColor;
            background.raycastTarget = true;

            GameObject iconObject = new GameObject(
                "MateriaIcon",
                typeof(RectTransform),
                typeof(CanvasRenderer),
                typeof(Image)
            );

            iconObject.transform.SetParent(slotObject.transform, false);

            RectTransform iconRect =
                iconObject.GetComponent<RectTransform>();

            iconRect.anchorMin = Vector2.zero;
            iconRect.anchorMax = Vector2.one;
            iconRect.offsetMin = new Vector2(5f, 5f);
            iconRect.offsetMax = new Vector2(-5f, -5f);

            Image icon = iconObject.GetComponent<Image>();
            icon.preserveAspect = true;
            icon.raycastTarget = false;
            icon.enabled = false;

            slotObject.GetComponent<MateriaDropSlot>()
                .Initialize(icon, this);

            slots.Add(slotObject);
        }

        Debug.Log(
            "マテリアスロットを " + slotCount + " 個表示しました。"
        );
    }

    // マテリアを元のオブジェクトのまま一覧に戻す
    public void ReturnMateria(MateriaDrag materia)
    {
        if (materia == null)
        {
            Debug.LogWarning(
                "戻すマテリアのオブジェクトが見つかりません。"
            );
            return;
        }

        // 保存済みの親・座標・サイズを使って復元
        materia.ReturnToOriginalPosition();

        Debug.Log(
            materia.materiaName + " を一覧に戻しました！"
        );
    }
}


// マテリア装着スロット
public class MateriaDropSlot : MonoBehaviour,
    IDropHandler, IPointerClickHandler
{
    private Image icon;
    private MateriaSlotManager manager;
    private MateriaDrag equippedMateria;

    public bool HasMateria
    {
        get { return equippedMateria != null; }
    }

    public void Initialize(
        Image targetIcon,
        MateriaSlotManager slotManager)
    {
        icon = targetIcon;
        manager = slotManager;
    }

    // マテリアをスロットにドロップしたとき
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        MateriaDrag materia =
            eventData.pointerDrag.GetComponent<MateriaDrag>();

        if (materia == null || icon == null || manager == null)
            return;

        Image sourceImage = materia.GetComponent<Image>();

        if (sourceImage == null || sourceImage.sprite == null)
        {
            Debug.LogWarning(
                "マテリア一覧のImageにSpriteが設定されていません。"
            );
            return;
        }

        // 同じスロットにドロップした場合は何もしない
        if (equippedMateria == materia)
            return;

        // 以前のマテリアを元の一覧に戻す
        if (equippedMateria != null)
        {
            ReturnEquippedMateria();
        }

        // 新しいマテリアを装着
        icon.sprite = sourceImage.sprite;
        icon.preserveAspect = true;
        icon.enabled = true;

        equippedMateria = materia;

        materia.MarkEquipped();

        Debug.Log(materia.materiaName + " を装着しました！");
    }

    // スロットをクリックしてマテリアを外す
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!HasMateria)
            return;

        ReturnEquippedMateria();
    }

    // 装着中のマテリアを元の一覧に戻す
    public void ReturnEquippedMateria()
    {
        if (equippedMateria == null)
            return;

        MateriaDrag materiaToReturn = equippedMateria;

        // スロットから先に切り離す
        equippedMateria = null;

        if (icon != null)
        {
            icon.sprite = null;
            icon.enabled = false;
        }

        if (manager != null)
        {
            manager.ReturnMateria(materiaToReturn);
        }
        else
        {
            materiaToReturn.ReturnToOriginalPosition();
        }

        Debug.Log(materiaToReturn.materiaName + " を外しました！");
    }
}
