using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Shop : MonoBehaviour
{

    [Header("CSVファイルの設定（Resourcesフォルダ内の名前）")]
    [SerializeField] private string csvFileName = "Items";

    [Header("UIパネルの紐づけ")]
    [SerializeField] private GameObject ShopPanel;
    [SerializeField] private GameObject YesNoPanel;

    [Header("購入確認ウィンドウのテキスト紐づけ")]
    [SerializeField] private Text confirmNameText;
    [SerializeField] private Text confirmPriceText;

    [Header("アイテムランダム配置野設定")]
    [SerializeField] private Transform itemContainer; //「Item」オブジェクトを紐づける
    [SerializeField] private int displayCount = 6; //画面に表示するアイテム数

    

    private Dictionary<int, ItemData> itemDatabase = new Dictionary<int, ItemData>();
    private ShopItem selectedItem;

    // 元のCSV（6列）に合わせたデータ構造
    private class ItemData
    {
        public int id;
        public int attack;
        public int burn;
        public int bonus;
        public int speedDown;
        public float duration;
    }

    private void Start()
    {
        LoadCSV();
        ApplyDataToItems();
    }

    // 元の6列のCSVを読み込む
    private void LoadCSV()
    {
        TextAsset csvFile = Resources.Load<TextAsset>(csvFileName);
        if (csvFile == null)
        {
            Debug.LogError($"CSVファイル '{csvFileName}' が Resources フォルダに見つかりません！");
            return;
        }

        string[] lines = csvFile.text.Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(',');
            if (values.Length < 6) continue;
            ItemData data = new ItemData();
            int.TryParse(values[0], out data.id);
            int.TryParse(values[1], out data.attack);
            int.TryParse(values[2], out data.burn);
            int.TryParse(values[3], out data.bonus);
            int.TryParse(values[4].Replace("%", ""), out data.speedDown);
            float.TryParse(values[5], out data.duration);


            itemDatabase[data.id] = data;
        }
    }

    // 各アイテムボタンにCSVの数値をセットする
    private void ApplyDataToItems()
    {
        if (itemContainer == null) return;

        ShopItem[] items = itemContainer.GetComponentsInChildren<ShopItem>(true);
        foreach (ShopItem item in items)
        {
            if (itemDatabase.TryGetValue(item.itemId, out ItemData data))
            {
                item.attackPower = data.attack;
                item.burnDamage = data.burn;
                item.bonusDamage = data.bonus;
                item.speedDownPercent = data.speedDown;
                item.duration = data.duration;
            }
        }
    }

    public void OpenShop()
    {
        RandomizeShopItems();
        ShopPanel.SetActive(true);
    }

    public void CloseShop()
    {
        ShopPanel.SetActive(false);
    }

    public void OnClickResetButton()
    {
        RandomizeShopItems();
    }

    private void RandomizeShopItems()
    {
        if (itemContainer == null) return;

        List<Transform> allItems = new List<Transform>();
        for (int i = 0; i < itemContainer.childCount; i++)
        {
            Transform child = itemContainer.GetChild(i);
            child.gameObject.SetActive(false);
            allItems.Add(child);
        }

        for (int i = allItems.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Transform temp = allItems[i];
            allItems[i] = allItems[randomIndex];
            allItems[randomIndex] = temp;
        }

        int count = Mathf.Min(displayCount, allItems.Count);
        for (int i = 0; i < count; i++)
        {
            allItems[i].gameObject.SetActive(true);
            allItems[i].SetSiblingIndex(i);
        }
    }

    public void OnClickItemButton(ShopItem item)
    {
        if (item == null) return;
        selectedItem = item;

        if (confirmNameText != null)
        {
            confirmNameText.text = $"{item.itemName} を購入しますか？";
        }
        if (confirmPriceText != null)
        {
            confirmPriceText.text = "価格未定";
        }

        OpenYesNo();
    }

    public void OpenYesNo() { YesNoPanel.SetActive(true); }
    public void CloseYesNo() { YesNoPanel.SetActive(false); }

    public void OnClickNext()
    {
        if (selectedItem != null)
        {
            ApplyItemEffect(selectedItem);
        }
        CloseYesNo();
    }

    private void ApplyItemEffect(ShopItem item)
    {
        PlayerPrefs.SetInt("Equipped_ElementID", item.itemId);
        PlayerPrefs.SetInt("Element_Attack", item.attackPower);
        PlayerPrefs.SetInt("Element_BurnDamage", item.burnDamage);
        PlayerPrefs.SetInt("Element_BonusDamage", item.bonusDamage);
        PlayerPrefs.SetInt("Element_SpeedDown", item.speedDownPercent);
        PlayerPrefs.SetFloat("Element_Duration", item.duration);
        PlayerPrefs.Save();

        Debug.Log($"属性「{item.itemName}」(ID:{item.itemId}) を選択！ 攻撃:{item.attackPower}, 燃焼:{item.burnDamage}, 追加:{item.bonusDamage}, 減速:{item.speedDownPercent}%, 時間:{item.duration}秒");
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("SentakuScene");
    }
}
