using UnityEngine;

public class SaveButton : MonoBehaviour
{
    public void OnClick()
    {
        LoadSaveGameData.SaveGame();
    }
}
