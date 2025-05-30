using Fusion;
using UnityEngine;

public class PlayerGfxSystem : NetworkBehaviour
{
    private static readonly int COLOR = Shader.PropertyToID("_BaseColor");

    #region Component Configs

    [SerializeField] private MeshRenderer render;
    
    [Networked, OnChangedRender(nameof(ChangeColor))] public Color networkColor { get; set; }

    private MaterialPropertyBlock propBlock;

    #endregion

    public void Init()
    {
        propBlock = new MaterialPropertyBlock();
        render.GetPropertyBlock(propBlock);
    }
    
    private void ChangeColor()
    {
        propBlock.SetColor(COLOR, networkColor);
        render.SetPropertyBlock(propBlock);
    }

    public void ChangeColorNetwork()
    {
        networkColor = Random.ColorHSV();
    }
}
