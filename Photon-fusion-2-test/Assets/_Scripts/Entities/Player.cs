using System;
using Fusion;
using NUnit.Framework.Internal;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : NetworkBehaviour
{
    #region Component Configs

    private PlayerMovementSystem _movementSystem;
    private InputCallbackSystem _inputCallbackSystem;
    private PlayerGfxSystem _gfxSystem;
    private HudSystem _hudSystem;

    [Networked, OnChangedRender(nameof(ChangeName))] public string playerName { get; set; }

    #endregion

    private void Awake()
    {
        _inputCallbackSystem = new InputCallbackSystem();
        _movementSystem = GetComponent<PlayerMovementSystem>();
        _gfxSystem = GetComponent<PlayerGfxSystem>();
        _hudSystem = GetComponent<HudSystem>();
        _inputCallbackSystem.Init();
        _movementSystem.Init();
        _gfxSystem.Init();
        _hudSystem.Init();
    }

    private void Start()
    {
        _inputCallbackSystem.OnMove += _movementSystem.Move;
        _inputCallbackSystem.OnJump += _movementSystem.Jump;
        _inputCallbackSystem.OnSprint += _movementSystem.Sprint;
        _inputCallbackSystem.OnChangeColor += _gfxSystem.ChangeColorNetwork;
    }

    private void LateUpdate()
    {
        _hudSystem.Tick();
    }

    public override void FixedUpdateNetwork()
    {
        _movementSystem.Tick();
    }

    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            Camera cam = Camera.main;
            cam?.GetComponent<CameraFollow>().SetLocalPlayer(transform);
            playerName = MainGameHUD.Instance.iptNameInput.text;
        }
        ChangeName();
    }
    private void ChangeName()
    {
        _hudSystem.SetPlayerName(playerName);
    }
}