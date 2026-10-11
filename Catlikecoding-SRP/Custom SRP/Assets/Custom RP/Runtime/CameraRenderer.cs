using UnityEngine;
using UnityEngine.Rendering;

public class CameraRenderer
{
    const string bufferName = "Render Camera";

    static ShaderTagId unlitShaderTagId = new ShaderTagId("SRPDefaultUnlit");

	CommandBuffer buffer = new CommandBuffer {
		name = bufferName
	};

	ScriptableRenderContext context;
	Camera camera;

    CullingResults cullingResults;


	public void Render (ScriptableRenderContext context, Camera camera)
    {
		this.context = context;
		this.camera = camera;
        
        if (!Cull()) { return; }

        Setup();
		DrawVisibleGeometry();
        Submit();
	}

    bool Cull ()
    {
		if (camera.TryGetCullingParameters(out ScriptableCullingParameters p))
        {
            cullingResults = context.Cull(ref p);
			return true;
		}
		return false;
	}

    void Setup ()
    {
        context.SetupCameraProperties(camera);
        buffer.BeginSample(bufferName);
        buffer.ClearRenderTarget(true, true, Color.clear);
        ExecuteBuffer();
	}

    void Submit ()
    {
        buffer.EndSample(bufferName);
		ExecuteBuffer();
		context.Submit();
	}

    void ExecuteBuffer ()
    {
		context.ExecuteCommandBuffer(buffer);
		buffer.Clear();
	}

    void DrawVisibleGeometry ()
    {
        var sortingSettings = new SortingSettings(camera);
		var drawingSettings = new DrawingSettings(
			unlitShaderTagId, sortingSettings
		);
		var filteringSettings = new FilteringSettings(RenderQueueRange.all);

		context.DrawRenderers(
			cullingResults, ref drawingSettings, ref filteringSettings
		);

		context.DrawSkybox(camera);
	}
}