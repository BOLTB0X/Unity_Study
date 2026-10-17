using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Rendering/Custom Render Pipeline")]
public class CustomRenderPipelineAsset : RenderPipelineAsset
{
	[SerializeField]
    bool useGPUInstancing = true;
	[SerializeField]
	bool useSRPBatcher = true;

	protected override RenderPipeline CreatePipeline ()
	{
		return new CustomRenderPipeline(useGPUInstancing, useSRPBatcher);
	}
}