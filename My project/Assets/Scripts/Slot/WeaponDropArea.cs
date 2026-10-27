
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class WeaponDropArea : MonoBehaviour, IDropHandler
{
    [Header("装備スロットのImageを登録")]
    [SerializeField]
    private List<Image> equippedWeaponImages = new List<Image>();

    [Header("マテリアスロット管理")]
    [SerializeField]
    private MateriaSlotManager materiaSlotManager;

    private void Awake()
    {
        // Inspectorに登録されていなければ子から取得
        if (equippedWeaponImages.Count == 0)
        {
            Image[] images = GetComponentsInChildren<Image>(true);

            foreach (Image image in images)
            {
                if (image.transform != transform)
                {
                    equippedWeaponImages.Add(image);
                }
            }
        }

        // マテリアスロット管理を自動取得
        if (materiaSlotManager == null)
        {
            materiaSlotManager =
                FindFirstObjectByType<MateriaSlotManager>();
        }

        Debug.Log("装備スロット数: " + equippedWeaponImages.Count);

        if (materiaSlotManager == null)
        {
            Debug.LogWarning(
                "MateriaSlotManagerが見つかりません。"
                + " マテリア装着エリアに追加してください。"
            );
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        // ドラッグされた武器を取得
        WeaponDrag weapon =
            eventData.pointerDrag.GetComponent<WeaponDrag>();

        if (weapon == null)
            return;

        Image sourceImage = weapon.GetComponent<Image>();

        if (sourceImage == null || sourceImage.sprite == null)
        {
            Debug.LogWarning(
                "武器一覧のImageにSpriteが設定されていません。"
            );
            return;
        }

        // ドロップ位置にある装備スロットを探す
        foreach (Image target in equippedWeaponImages)
        {
            if (target == null)
                continue;

            RectTransform targetRect =
                target.GetComponent<RectTransform>();

            if (targetRect == null)
                continue;

            if (!RectTransformUtility.RectangleContainsScreenPoint(
                targetRect,
                eventData.position,
                eventData.pressEventCamera))
            {
                continue;
            }

            // 武器画像を設定・交換
            target.sprite = sourceImage.sprite;
            target.preserveAspect = true;
            target.enabled = true;

            // 装備した武器の情報を保存
            EquippedWeaponSlot slot =
                target.GetComponent<EquippedWeaponSlot>();

            if (slot == null)
            {
                slot = target.gameObject
                    .AddComponent<EquippedWeaponSlot>();
            }

            slot.SetWeapon(
                weapon.weaponName,
                weapon.materiaSlotCount,
                sourceImage.sprite
            );

            // 装備した武器に応じてマテリア枠を自動更新
            if (materiaSlotManager == null)
            {
                materiaSlotManager =
                    FindFirstObjectByType<MateriaSlotManager>();
            }

            if (materiaSlotManager != null)
            {
                materiaSlotManager.ShowSlots(
                    weapon.materiaSlotCount
                );
            }
            else
            {
                Debug.LogWarning(
                    "マテリア枠を表示できません。"
                    + " MateriaSlotManagerを確認してください。"
                );
            }

            Debug.Log(
                weapon.weaponName + " を装備しました！"
                + " マテリア枠数: "
                + weapon.materiaSlotCount
            );

            return;
        }

        Debug.Log("装備スロットの上にドロップしてください。");
    }
}
