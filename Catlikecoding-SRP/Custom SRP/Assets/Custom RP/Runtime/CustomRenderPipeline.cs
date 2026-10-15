using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CustomRenderPipeline : RenderPipeline
{
    CameraRenderer renderer = new CameraRenderer();

    private List<Camera> _cameraList = new List<Camera>();

    public CustomRenderPipeline ()
    {
		GraphicsSettings.useScriptableRenderPipelineBatching = true;
	}

    protected override void Render(
		ScriptableRenderContext context, Camera[] cameras
	)
    {
        _cameraList.Clear();
        _cameraList.AddRange(cameras);

        Render(context, _cameraList);
    }

    protected override void Render (
		ScriptableRenderContext context, List<Camera> cameras
	)
    {
        for (int i = 0; i < cameras.Count; i++) {
			renderer.Render(context, cameras[i]);
		}
    }
}