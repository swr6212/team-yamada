
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquippedWeaponSlot : MonoBehaviour, IPointerClickHandler
{
    public string WeaponName { get; private set; }
    public int MateriaSlotCount { get; private set; }
    public Sprite WeaponSprite { get; private set; }
    public bool HasWeapon { get; private set; }

    public void SetWeapon(
        string weaponName,
        int materiaSlotCount,
        Sprite weaponSprite)
    {
        WeaponName = weaponName;
        MateriaSlotCount = Mathf.Max(0, materiaSlotCount);
        WeaponSprite = weaponSprite;
        HasWeapon = weaponSprite != null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!HasWeapon)
            return;

        // 武器を外す
        Image image = GetComponent<Image>();

        if (image != null)
        {
            image.sprite = null;
            image.enabled = false;
        }

        Debug.Log(WeaponName + " を外しました！");

        WeaponName = "";
        WeaponSprite = null;
        MateriaSlotCount = 0;
        HasWeapon = false;

        // マテリア枠も消す
        MateriaSlotManager manager =
            FindFirstObjectByType<MateriaSlotManager>();

        if (manager != null)
            manager.ShowSlots(0);
    }
}
