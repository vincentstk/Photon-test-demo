using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    #region UI Elements
    
    public CanvasGroup cvsgrUI;
    public TextMeshProUGUI txtPlayerName;

    #endregion

    public void ShowUI(bool isShow)
    {
        cvsgrUI.alpha = isShow ? 1 : 0;
        cvsgrUI.interactable = isShow;
        cvsgrUI.blocksRaycasts = isShow;
    }
    public void SetPlayerName(string playerName)
    {
        txtPlayerName.SetText(playerName);
    }
}
