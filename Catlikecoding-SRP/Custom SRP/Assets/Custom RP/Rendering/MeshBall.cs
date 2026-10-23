using UnityEngine;

public class MeshBall : MonoBehaviour
{
    static int baseColorId = Shader.PropertyToID("_BaseColor"),
		metallicId = Shader.PropertyToID("_Metallic"),
		smoothnessId = Shader.PropertyToID("_Smoothness");

    float[]
		metallic = new float[1023],
		smoothness = new float[1023];

    [SerializeField]
    Mesh mesh = default;

    [SerializeField]
    Material material = default;
    
    Matrix4x4[] matrices = new Matrix4x4[1023];
    Vector4[] baseColors = new Vector4[1023];

    MaterialPropertyBlock block;

    void Awake ()
    {
        for (int i = 0; i < matrices.Length; i++) {
            float x = (i % 10) * 1.2f - 5.5f; 
            float z = (i / 10) * 1.2f - 4.5f; 

            matrices[i] = Matrix4x4.TRS(
                new Vector3(x, 0f, z),
                Quaternion.identity,
                Vector3.one 
            );
            
            baseColors[i] = new Vector4(
					Random.value, Random.value, Random.value,
					Random.Range(0.5f, 1f)
			);
			metallic[i] = Random.value < 0.25f ? 1f : 0f;
			smoothness[i] = Random.Range(0.05f, 0.95f);
        }
    }

    void Update ()
    {
        if (block == null)
        {
            block = new MaterialPropertyBlock();
            block.SetVectorArray(baseColorId, baseColors);
            block.SetFloatArray(metallicId, metallic);
			block.SetFloatArray(smoothnessId, smoothness);
        }
        Graphics.DrawMeshInstanced(mesh, 0, material, matrices, 76, block);
    }
}