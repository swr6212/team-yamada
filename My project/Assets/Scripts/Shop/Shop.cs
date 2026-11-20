using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject confirmPanel;

    [Header("UIパネルの紐づけ")]
    [SerializeField] private GameObject ShopPanel;
    [SerializeField] private GameObject YesNoPanel;

    [Header("アイテムランダム配置野設定")]
    [SerializeField] private Transform itemContainer; //「Item」オブジェクトを紐づける
    [SerializeField] private int displayCount = 6; //画面に表示するアイテム数

    //現在表示されているアイテムのデータを一時保存する
    private ShopItem selectedItem;


    // ショップ画面を開く処理
    public void OpenShop()
    {
        RandomizeShopItems(); // パネルを開くときにランダム配置
        ShopPanel.SetActive(true);
    }

    // ショップ画面を閉じる処理
    public void CloseShop()
    {
        ShopPanel.SetActive(false);
    }

    // リセットボタンを押したときの処理
    public void OnClickResetButton()
    {
        RandomizeShopItems(); // 配置をリセット（再抽選）する
    }

    // 全アイテム（9個）からランダムに6個選んで表示・並べ替えする処理
    private void RandomizeShopItems()
    {
        if (itemContainer == null) return;

        // 1. Itemの下にある全ボタン（非表示のものも含む）をリストに集める
        List<Transform> allItems = new List<Transform>();
        for (int i = 0; i < itemContainer.childCount; i++)
        {
            Transform child = itemContainer.GetChild(i);
            child.gameObject.SetActive(false); // 一旦すべて非表示にする
            allItems.Add(child);
        }

        // 2. リストの中身をランダムにシャッフルする（フィッシャー–イェーツのシャッフル）
        for (int i = allItems.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Transform temp = allItems[i];
            allItems[i] = allItems[randomIndex];
            allItems[randomIndex] = temp;
        }

        // 3. シャッフルした先頭から6個（displayCount分）だけ表示し、順番を確定する
        int count = Mathf.Min(displayCount, allItems.Count);
        for (int i = 0; i < count; i++)
        {
            allItems[i].gameObject.SetActive(true);
            allItems[i].SetSiblingIndex(i); // Grid Layout Groupの並び順を更新
        }
    }

    // YesNo画面を開く処理
    public void OpenYesNo()
    {
        YesNoPanel.SetActive(true);
    }

    // YesNo画面を閉じる処理
    public void CloseYesNo()
    {
        YesNoPanel.SetActive(false);
    }

    // アイテムボタンが押されたら、そのアイテムのスクリプトを受け取る
    public void OnClickItemButton(ShopItem item)
    {
        if (item == null) return;
        selectedItem = item;
        OpenYesNo();
    }

    // YesNoパネルで「はい」を押したときの処理
    public void OnClickNext()
    {
        if (selectedItem != null)
        {
            ApplyItemEffect(selectedItem);
        }

        CloseYesNo();
    }

    // アイテムのスクリプトからデータを読み取って効果を適用する
    private void ApplyItemEffect(ShopItem item)
    {
        switch (item.effectType)
        {
            case ShopItem.ItemEffectType.AttackUp:
                int currentAtk = PlayerPrefs.GetInt("Player_Attack", 10);
                PlayerPrefs.SetInt("Player_Attack", currentAtk + item.effectValue);
                Debug.Log($"{item.itemId} を購入！攻撃力が {item.effectValue} アップしました！");
                break;

            case ShopItem.ItemEffectType.DefenseUp:
                int currentDef = PlayerPrefs.GetInt("Player_Defense", 5);
                PlayerPrefs.SetInt("Player_Defense", currentDef + item.effectValue);
                Debug.Log($"{item.itemId} を購入！防御力が {item.effectValue} アップしました！");
                break;

            case ShopItem.ItemEffectType.MaxHPUp:
                int currentHP = PlayerPrefs.GetInt("Player_MaxHP", 100);
                PlayerPrefs.SetInt("Player_MaxHP", currentHP + item.effectValue);
                Debug.Log($"{item.itemId} を購入！最大HPが {item.effectValue} アップしました！");
                break;
        }
        PlayerPrefs.Save();
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("SentakuScene");
    }
}
