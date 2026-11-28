using UnityEngine;
using UnityEngine.SceneManagement; //シーン遷移に必要
using System.Collections;
using System.Collections.Generic;

public class Title : MonoBehaviour
{
    [SerializeField] private Animator MoveObject; //アニメーションを付けたオブジェクトをアタック
    public GameObject Fedeout; //フェードアウト用のパネル

    void Start()
    {
        //最初はパネルを非表示にしておく
        Fedeout.SetActive(false);
    }

    //StartButtonがクリックされたときに呼び出す関数
    public void OnClickStart()
    {
        //1.フェードアウト用のパネルを表示
        Fedeout.SetActive(true);

        //2.アニメーションを再生（""はアニメーション名）
        MoveObject.Play("FeedPanelAnimation");

        //3.2秒後にシーン遷移を実行する
        Invoke("ChangeScene", 2.0f);
    }

    private void ChangeScene()
    {
        //""の中は遷移したいシーン名
        SceneManager.LoadScene("WaitingRoom");
    }


    ///////////////////////////////////////////////////////////////


    [SerializeField] private GameObject SaveDataDeletePanel;

    //SaveDataDeleteBottonを押したときに呼び出すSaveDataDeletePanel
    public void OpenSaveDataDeletePanel()
    {
        SaveDataDeletePanel.SetActive(true); //パネルを表示する
    }

    //NoBottonを押したときに呼び出す
    public void CloseSaveDataDeletePanel()
    {
        SaveDataDeletePanel.SetActive(false);
    }

    //YesBottonを押したときに呼び出す
    public void OnClickSaveDataDelete()
    {
        LoadSaveGameData.DeleteGame();
        Debug.Log("セーブデータを削除しました！"); // 確認用ログ（必要に応じて）
        CloseSaveDataDeletePanel(); // 削除後にパネルを閉じる
    }

    
    ///////////////////////////////////////////////////////////////////
  

    [SerializeField] private GameObject confirmPanel;
    
    //おわるボタンを押したときに呼び出すSettingPanel
    public void OpenconfirmPanel()
    {
        confirmPanel.SetActive(true); //パネルを表示する
    }

    //いいえボタンを押したときに呼び出す
    public void CloseConfirmPanel()
    {
        confirmPanel.SetActive(false);
    }

    //はいボタンを押したときに呼び出す
    public void OnClickQuit()
    {
#if UNITY_EDITOR
UnityEditor.EditorApplication.isPlaying = false;//エディタのみ再生終了
#else
        Application.Quit();//ビルドしたアプリを終了
#endif
    }
}
