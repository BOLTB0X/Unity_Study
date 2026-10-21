#ifndef CUSTOM_LIGHT_INCLUDED
#define CUSTOM_LIGHT_INCLUDED

#define MAX_DIRECTIONAL_LIGHT_COUNT 4
#define MAX_POINT_LIGHT_COUNT 4
#define MAX_SPOT_LIGHT_COUNT 4

CBUFFER_START(_CustomLight)
	int    _DirectionalLightCount;
	float4 _DirectionalLightColors[MAX_DIRECTIONAL_LIGHT_COUNT];
	float4 _DirectionalLightDirections[MAX_DIRECTIONAL_LIGHT_COUNT];
	int _PointLightCount;
	float4 _PointLightColors[MAX_POINT_LIGHT_COUNT];
	float4 _PointLightPositions[MAX_POINT_LIGHT_COUNT];
	int _SpotLightCount;
	float4 _SpotLightColors[MAX_SPOT_LIGHT_COUNT];
	float4 _SpotLightPositions[MAX_SPOT_LIGHT_COUNT];
	float4 _SpotLightDirections[MAX_SPOT_LIGHT_COUNT];
	float4 _SpotLightAngles[MAX_SPOT_LIGHT_COUNT];
CBUFFER_END

struct Light
{
	float3 color;
	float3 direction;
	float attenuation;
};

int GetDirectionalLightCount ()
{
	return _DirectionalLightCount;
}

Light GetDirectionalLight (int index)
{
	Light light;
	light.color = _DirectionalLightColors[index].rgb;
	light.direction = _DirectionalLightDirections[index].xyz;
	light.attenuation = 1.0;
	return light;
}

int GetPointLightCount () { return _PointLightCount; }
int GetSpotLightCount () { return _SpotLightCount; }

Light GetPointLight (int index, float3 positionWS)
{
	Light light;
	float3 ray = _PointLightPositions[index].xyz - positionWS;
	float distanceSqr = max(dot(ray, ray), 0.0001);
	light.direction = ray * rsqrt(distanceSqr);
	float rangeFade = saturate(1.0 - distanceSqr * _PointLightPositions[index].w);
	light.attenuation = rangeFade * rangeFade / distanceSqr;
	light.color = _PointLightColors[index].rgb;
	return light;
}

Light GetSpotLight (int index, float3 positionWS)
{
	Light light;
	float3 ray = _SpotLightPositions[index].xyz - positionWS;
	float distanceSqr = max(dot(ray, ray), 0.0001);
	light.direction = ray * rsqrt(distanceSqr);
	float rangeFade = saturate(1.0 - distanceSqr * _SpotLightPositions[index].w);
	float coneCos = dot(-light.direction, _SpotLightDirections[index].xyz);
	float2 angles = _SpotLightAngles[index].xy;
	float coneFade = saturate((coneCos - angles.y) / max(angles.x - angles.y, 0.0001));
	light.attenuation = rangeFade * rangeFade * coneFade * coneFade / distanceSqr;
	light.color = _SpotLightColors[index].rgb;
	return light;
}

#endif
