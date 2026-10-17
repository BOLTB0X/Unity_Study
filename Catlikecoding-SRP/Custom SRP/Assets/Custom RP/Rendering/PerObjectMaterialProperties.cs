using UnityEngine;

[DisallowMultipleComponent]
public class PerObjectMaterialProperties : MonoBehaviour
{
    static int baseColorId = Shader.PropertyToID("_BaseColor");
    static MaterialPropertyBlock block;
    
    [SerializeField]
    Color baseColor = Color.white; // 인스펙터에 나타나야 할 변수

    // 게임이 시작될 때 속성을 적용
    void Awake () {
        OnValidate();
    }

    // 에디터 인스펙터에서 값을 바꿀 때마다 즉시 반영
    void OnValidate () {
        if (block == null) {
            block = new MaterialPropertyBlock();
        }
        block.SetColor(baseColorId, baseColor);
        GetComponent<Renderer>().SetPropertyBlock(block);
    }
}