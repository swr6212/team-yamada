using UnityEngine;
using UnityEngine.UI;

public class WeaponUIController : MonoBehaviour
{
    [System.Serializable]
    public class WeaponSlot
    {
        public int weaponID;
        public Image weaponImage;
    }

    [SerializeField]
    private WeaponSlot[] weaponSlots;

    private void Start()
    {
        UpdateWeaponUI();
    }

    public void UpdateWeaponUI()
    {
        foreach (WeaponSlot slot in weaponSlots)
        {
            // ここでWeaponManagerから所持情報を取得する
            // bool hasWeapon = WeaponManager.Instance.HasWeapon(slot.weaponID);

            // slot.weaponImage.enabled = hasWeapon;
        }
    }
}