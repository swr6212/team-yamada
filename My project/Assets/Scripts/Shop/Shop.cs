using UnityEditor.Rendering;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject confirmPanel;

  //ShopBottonを押したときに呼び出す
  public void OpenConfirmPanel()
    {
        confirmPanel.SetActive(true); //パネルを表示する
    }

    //CloseBottonを押したときに呼び出す
    public void CloseConfirmPanel()
    {
        confirmPanel.SetActive(false); //パネルを非表示にする
    }
}
