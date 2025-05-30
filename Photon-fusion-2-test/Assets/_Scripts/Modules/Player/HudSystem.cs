using UnityEngine;

public class HudSystem : MonoBehaviour
{
    #region Defines

    private const string HUD = "HUD";

    #endregion
    #region Component Configs
    
    [SerializeField]
    private Vector3 namePositionOffset;
    [SerializeField]
    private PlayerUI playerUI;

    private Transform ownerTransform;
    private Camera mainCam;
    private PlayerUI uiElements;

    #endregion

    public void Init()
    {
        ownerTransform = transform;
        mainCam = Camera.main;
        GameObject hudParent = GameObject.FindGameObjectWithTag(HUD);
        uiElements = Instantiate(playerUI, hudParent.transform);
    }

    public void ShowUI(bool isShow)
    {
        uiElements.ShowUI(isShow);
    }

    public void SetPlayerName(string playerName)
    {
        uiElements.SetPlayerName(playerName);
    }
    public void Tick()
    {
        uiElements.txtPlayerName.transform.position = mainCam.WorldToScreenPoint(ownerTransform.position + namePositionOffset);
    }
}
