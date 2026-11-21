using UnityEngine;

public class LoadButton : MonoBehaviour
{
    [SerializeField] private TextAsset csv;
   public void OnClick()
    {
        if(csv == null)
        {
            Debug.LogError("CSV file is not assigned in the inspector.");
            return;
        }
        PlayerData.SetStatusList(PlayerStatusLoader.Load(csv));
        LoadSaveGameData.LoadGame();
    }
}
