#ifndef CUSTOM_LIGHTING_INCLUDED
#define CUSTOM_LIGHTING_INCLUDED

#include "Surface.hlsl"
#include "Light.hlsl"
#include "BRDF.hlsl"

float3 IncomingLight (Surface surface, Light light)
{
	return saturate(dot(surface.normal, light.direction)) * light.color * light.attenuation;
}

float3 GetLighting (Surface surface, BRDF brdf, Light light)
{
	return IncomingLight(surface, light) * DirectBRDF(surface, brdf, light);
}

float3 GetLighting (Surface surface, BRDF brdf)
{
	float3 color = 0.0;
	for (int i = 0; i < GetDirectionalLightCount(); i++) {
		color += GetLighting(surface, brdf, GetDirectionalLight(i));
	}
	for (int i = 0; i < GetPointLightCount(); i++) {
		color += GetLighting(surface, brdf, GetPointLight(i, surface.position));
	}
	for (int i = 0; i < GetSpotLightCount(); i++) {
		color += GetLighting(surface, brdf, GetSpotLight(i, surface.position));
	}
	return color;
}

#endif
