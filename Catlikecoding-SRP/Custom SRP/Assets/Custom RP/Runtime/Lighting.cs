using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Lighting
{
	const string bufferName = "Lighting";
	const int maxLightCount = 4;
	CommandBuffer buffer = new CommandBuffer { name = bufferName };

	static readonly int
		dirLightCountId = Shader.PropertyToID("_DirectionalLightCount"),
		dirLightColorsId = Shader.PropertyToID("_DirectionalLightColors"),
		dirLightDirectionsId = Shader.PropertyToID("_DirectionalLightDirections"),
		pointLightCountId = Shader.PropertyToID("_PointLightCount"),
		pointLightColorsId = Shader.PropertyToID("_PointLightColors"),
		pointLightPositionsId = Shader.PropertyToID("_PointLightPositions"),
		spotLightCountId = Shader.PropertyToID("_SpotLightCount"),
		spotLightColorsId = Shader.PropertyToID("_SpotLightColors"),
		spotLightPositionsId = Shader.PropertyToID("_SpotLightPositions"),
		spotLightDirectionsId = Shader.PropertyToID("_SpotLightDirections"),
		spotLightAnglesId = Shader.PropertyToID("_SpotLightAngles");

	static readonly Vector4[]
		dirLightColors = new Vector4[maxLightCount],
		dirLightDirections = new Vector4[maxLightCount],
		pointLightColors = new Vector4[maxLightCount],
		pointLightPositions = new Vector4[maxLightCount],
		spotLightColors = new Vector4[maxLightCount],
		spotLightPositions = new Vector4[maxLightCount],
		spotLightDirections = new Vector4[maxLightCount],
		spotLightAngles = new Vector4[maxLightCount];

	CullingResults cullingResults;

	public void Setup (ScriptableRenderContext context, CullingResults cullingResults)
	{
		this.cullingResults = cullingResults;
		buffer.BeginSample(bufferName);
		SetupLights();
		buffer.EndSample(bufferName);
		context.ExecuteCommandBuffer(buffer);
		buffer.Clear();
	}

	void SetupLights ()
	{
		NativeArray<VisibleLight> visibleLights = cullingResults.visibleLights;
		int dirCount = 0, pointCount = 0, spotCount = 0;

		for (int i = 0; i < visibleLights.Length; i++)
		{
			VisibleLight light = visibleLights[i];
			switch (light.lightType)
			{
				case LightType.Directional:
					if (dirCount < maxLightCount)
						dirLightDirections[dirCount] = -light.localToWorldMatrix.GetColumn(2);
					if (dirCount < maxLightCount) dirLightColors[dirCount++] = light.finalColor;
					break;
				case LightType.Point:
					if (pointCount < maxLightCount)
					{
						pointLightColors[pointCount] = light.finalColor;
						Vector4 position = light.localToWorldMatrix.GetColumn(3);
						float range = light.range;
						pointLightPositions[pointCount] = new Vector4(position.x, position.y, position.z, 1f / Mathf.Max(range * range, 0.0001f));
						pointCount++;
					}
					break;
				case LightType.Spot:
					if (spotCount < maxLightCount)
					{
						spotLightColors[spotCount] = light.finalColor;
						Vector4 position = light.localToWorldMatrix.GetColumn(3);
						float range = light.range;
						spotLightPositions[spotCount] = new Vector4(position.x, position.y, position.z, 1f / Mathf.Max(range * range, 0.0001f));
						spotLightDirections[spotCount] = -light.localToWorldMatrix.GetColumn(2);
						float outerAngle = Mathf.Cos(0.5f * light.spotAngle * Mathf.Deg2Rad);
						float innerAngle = light.light != null ? Mathf.Cos(0.5f * light.light.innerSpotAngle * Mathf.Deg2Rad) : outerAngle;
						spotLightAngles[spotCount] = new Vector4(innerAngle, outerAngle, 0f, 0f);
						spotCount++;
					}
					break;
			}
		}

		buffer.SetGlobalInt(dirLightCountId, Mathf.Min(dirCount, maxLightCount));
		buffer.SetGlobalVectorArray(dirLightColorsId, dirLightColors);
		buffer.SetGlobalVectorArray(dirLightDirectionsId, dirLightDirections);
		buffer.SetGlobalInt(pointLightCountId, pointCount);
		buffer.SetGlobalVectorArray(pointLightColorsId, pointLightColors);
		buffer.SetGlobalVectorArray(pointLightPositionsId, pointLightPositions);
		buffer.SetGlobalInt(spotLightCountId, spotCount);
		buffer.SetGlobalVectorArray(spotLightColorsId, spotLightColors);
		buffer.SetGlobalVectorArray(spotLightPositionsId, spotLightPositions);
		buffer.SetGlobalVectorArray(spotLightDirectionsId, spotLightDirections);
		buffer.SetGlobalVectorArray(spotLightAnglesId, spotLightAngles);
	}
}
