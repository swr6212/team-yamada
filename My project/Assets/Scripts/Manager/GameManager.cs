using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject resultUI;
    [SerializeField] private string titleSceneName; // タイトルシーン名（Tキーで移動）
    [SerializeField] private TMP_Text scoreText; // スコア表示用のTextMeshProUGUI
    [SerializeField] private TMP_Text defeatedEnemiesText; // 倒した敵の数表示用のTextMeshProUGUI
    [SerializeField] private TMP_Text elapsedTimeText; // 経過時間表示用のTextMeshProUGUI
    private bool isGameOver = false;
    static private int defeatedEnemies = 0; // 倒した敵の数を保持する静的変数
    static private int score = 0; // スコアを保持する静的変数
    private float startTime = 0f; // ゲーム開始からの経過時間を保持する変数
    private int elapsedSeconds = 0; // ゲーム開始からの経過時間を保持する変数
    private int minutes = 0; // 経過時間の分を保持する変数
    private int seconds = 0;

    void Start()
    {
        Time.timeScale = 1f; // ゲームを開始する
        defeatedEnemies = 0; // 倒した敵の数をリセット
        score = 0; // スコアをリセット
        startTime = Time.time; // ゲーム開始時の時間を記録
    }

    public void ShowResult()
    {
        if(isGameOver) return; // すでにゲームオーバーの場合は処理を行わない
        isGameOver = true;
        resultUI.SetActive(true);
        Time.timeScale = 0f; // ゲームを止める
        elapsedSeconds = (int)(Time.time - startTime);
        minutes = elapsedSeconds / 60;
        seconds = elapsedSeconds % 60;
        Debug.Log("SCORE: " + score + " DEFEATED: " + defeatedEnemies + " TIME: " + minutes.ToString("00") + ":" + seconds.ToString("00"));
        // スコア、倒した敵の数、経過時間をUIに表示
        scoreText.text = "SCORE: " + score;
        defeatedEnemiesText.text = "DEFEATED: " + defeatedEnemies;
        elapsedTimeText.text = "TIME: " + minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    void Update()
    {
        if(isGameOver)
        {
            if(Keyboard.current.rKey.wasPressedThisFrame)
            {
                // 結果UIが表示されている状態でRキーが押された場合、ゲームを再起動する
                Time.timeScale = 1f; // ゲームを再開する
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if(Keyboard.current.tKey.wasPressedThisFrame)
            {
                Time.timeScale = 1f; // ゲームを再開する
                SceneManager.LoadScene(titleSceneName);
            }
        }
#if UNITY_EDITOR
        if(Keyboard.current.f1Key.wasPressedThisFrame)
        {
            ShowResult();
        }
        
        if(Keyboard.current.f2Key.wasPressedThisFrame)
        {
            Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
            foreach(Enemy e in enemies)
            {
                e.TakeDamage(999);
            }
        }
#endif

    }

    public static void AddScore(int points)
    {
        score += points;
    }

    public static void AddDefeatedEnemy()
    {
        defeatedEnemies++;
    }
}
