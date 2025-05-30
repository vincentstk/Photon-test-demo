using System;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameHUD : MonoBehaviour
{
    public static MainGameHUD Instance { get; private set; }

    #region Component Configs

    [SerializeField] private NetworkRunner runner;
    [SerializeField] private GameObject pnlStart;
    [SerializeField] private GameObject btnStart;
    [SerializeField] private GameObject btnOK;

    #endregion
    #region UI Elements

    public TMP_InputField iptNameInput;

    #endregion
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #region UI Element Events

    public void OnClickStart()
    {
        var sceneRef =  SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
        NetworkSceneInfo info = new NetworkSceneInfo();
        info.AddSceneRef(sceneRef, LoadSceneMode.Single);

        runner.StartGame(new StartGameArgs()
        {
            Scene = info,
            GameMode = GameMode.Shared,
        });
        pnlStart.SetActive(false);
    }

    public void OnClickOK()
    {
        PlayerRef localPlayer = runner.LocalPlayer;
        runner.TryGetPlayerObject(localPlayer, out NetworkObject playerObject);
        playerObject.GetComponent<Player>().playerName = iptNameInput.text;
        pnlStart.SetActive(false);
    }

    public void OnClickChangeName()
    {
        pnlStart.SetActive(true);
        btnStart.gameObject.SetActive(false);
        btnOK.gameObject.SetActive(true);
    }

    #endregion
}