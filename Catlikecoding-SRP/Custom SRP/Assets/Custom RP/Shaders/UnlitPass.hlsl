#ifndef CUSTOM_UNLIT_PASS_INCLUDED
#define CUSTOM_UNLIT_PASS_INCLUDED

#include "../ShaderLibrary/Common.hlsl"

UNITY_INSTANCING_BUFFER_START(UnityPerMaterial)
	UNITY_DEFINE_INSTANCED_PROP(float4, _BaseColor)
	UNITY_DEFINE_INSTANCED_PROP(float, _Cutoff)
UNITY_INSTANCING_BUFFER_END(UnityPerMaterial)

struct Attributes
{
	float3 positionOS : POSITION;
	UNITY_VERTEX_INPUT_INSTANCE_ID
};

// float4 UnlitPassVertex (Attributes input) : SV_POSITION
// {
// 	UNITY_SETUP_INSTANCE_ID(input);
// 	float3 positionWS = TransformObjectToWorld(input.positionOS);
// 	return TransformWorldToHClip(positionWS);
// }

struct Varyings
{
	float4 positionCS : SV_POSITION;
	UNITY_VERTEX_INPUT_INSTANCE_ID
};

Varyings UnlitPassVertex (Attributes input)
{
	Varyings output;
	UNITY_SETUP_INSTANCE_ID(input);
	UNITY_TRANSFER_INSTANCE_ID(input, output);
	float3 positionWS = TransformObjectToWorld(input.positionOS);
	output.positionCS = TransformWorldToHClip(positionWS);
	return output;
}

// float4 UnlitPassFragment () : SV_TARGET
// {
// 	return _BaseColor;
// 	//return float4(1.0, 1.0, 0.0, 1.0);
// }

float4 UnlitPassFragment (Varyings input) : SV_TARGET
{
	UNITY_SETUP_INSTANCE_ID(input);
	float4 baseColor = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _BaseColor);
    
    #if defined(_CLIPPING)
        clip(baseColor.a - UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _Cutoff));
    #endif
    
    return baseColor;
}

#endif