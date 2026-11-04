using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private bool isGameOver = false;
    [SerializeField] private GameObject gameOverUI;

    void Start()
    {
        Time.timeScale = 1f; // ゲームを開始する
    }

    public void GameOver()
    {
        if(isGameOver) return; // すでにゲームオーバーの場合は処理を行わない
        isGameOver = true;
        gameOverUI.SetActive(true);
        Time.timeScale = 0f; // ゲームを止める
    }

    void Update()
    {
        if(isGameOver)
        {
            if(Keyboard.current.rKey.wasPressedThisFrame)
            {
                // ゲームオーバーUIが表示されている状態でRキーが押された場合、ゲームを再起動する
                Time.timeScale = 1f; // ゲームを再開する
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            }
        }
    }
}
