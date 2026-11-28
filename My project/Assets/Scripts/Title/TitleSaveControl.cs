using UnityEngine;
using UnityEngine.UI;
public class TitleSaveControl : MonoBehaviour
{
    [Header("Loadbuttonをセット")]
    [SerializeField] private Button continueButton;

    [Header("チェックを入れると非表示、外すとグレーアウト")]
    [SerializeField] private bool hideWhenNoDate = false;

    private void Start()
    {
        //セーブデータがあるかどうか確認
        bool hasSaveDate = LoadSaveGameData.SaveAvailability();

        if (hideWhenNoDate)
        {
            //データがないときはボタンごと非表示にする
            continueButton.gameObject.SetActive(hasSaveDate);

        }
        else
        {
            //データがないときはボタンを押せなくする
            continueButton.interactable = hasSaveDate;
        }
    }
}
