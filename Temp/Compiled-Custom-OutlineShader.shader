// Compiled shader for Windows, Mac, Linux

//////////////////////////////////////////////////////////////////////////
// 
// NOTE: This is *not* a valid shader file, the contents are provided just
// for information and for debugging purposes only.
// 
//////////////////////////////////////////////////////////////////////////
// Skipping shader variants that would not be included into build of current scene.

Shader "Custom/OutlineShader" {
Properties {
 _Color ("Main Color", Color) = (1.000000,1.000000,1.000000,1.000000)
 _OutlineColor ("Outline Color", Color) = (0.000000,0.000000,0.000000,1.000000)
 _OutlineWidth ("Outline Width", Range(0.000000,0.100000)) = 0.030000
}
SubShader { 
 Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline" }
 Pass {
  Name "MainPass"
  Tags { "LIGHTMODE"="UniversalForward" "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline" }
  Blend SrcAlpha OneMinusSrcAlpha
  //////////////////////////////////
  //                              //
  //      Compiled programs       //
  //                              //
  //////////////////////////////////
//////////////////////////////////////////////////////
Keywords: <none>
-- Hardware tier variant: Tier 1
-- Vertex shader for "metal":
Uses vertex data channel "Vertex"
Uses vertex data channel "Normal"

Constant Buffer "VGlobals" (64 bytes) on slot 0 {
  Matrix4x4 unity_MatrixVP at 0
}
Constant Buffer "UnityPerDraw" (720 bytes) on slot 1 {
  Matrix4x4 unity_ObjectToWorld at 0
  Matrix4x4 unity_WorldToObject at 64
}

Shader Disassembly:
#include <metal_stdlib>
#include <metal_texture>
using namespace metal;
struct VGlobals_Type
{
    float4 hlslcc_mtx4x4unity_MatrixVP[4];
};

struct UnityPerDraw_Type
{
    float4 hlslcc_mtx4x4unity_ObjectToWorld[4];
    float4 hlslcc_mtx4x4unity_WorldToObject[4];
    float4 unity_LODFade;
    float4 unity_WorldTransformParams;
    float4 unity_RenderingLayer;
    float4 unity_LightData;
    float4 unity_LightIndices[2];
    float4 unity_ProbesOcclusion;
    float4 unity_SpecCube0_HDR;
    float4 unity_SpecCube1_HDR;
    float4 unity_SpecCube0_BoxMax;
    float4 unity_SpecCube0_BoxMin;
    float4 unity_SpecCube0_ProbePosition;
    float4 unity_SpecCube1_BoxMax;
    float4 unity_SpecCube1_BoxMin;
    float4 unity_SpecCube1_ProbePosition;
    float4 unity_LightmapST;
    float4 unity_DynamicLightmapST;
    float4 unity_SHAr;
    float4 unity_SHAg;
    float4 unity_SHAb;
    float4 unity_SHBr;
    float4 unity_SHBg;
    float4 unity_SHBb;
    float4 unity_SHC;
    float4 unity_RendererBounds_Min;
    float4 unity_RendererBounds_Max;
    float4 hlslcc_mtx4x4unity_MatrixPreviousM[4];
    float4 hlslcc_mtx4x4unity_MatrixPreviousMI[4];
    float4 unity_MotionVectorsParams;
    float4 unity_SpriteColor;
    float4 unity_SpriteProps;
};

struct Mtl_VertexIn
{
    float4 POSITION0 [[ attribute(0) ]] ;
    float3 NORMAL0 [[ attribute(1) ]] ;
};

struct Mtl_VertexOut
{
    float4 mtl_Position [[ position, invariant ]];
    float3 NORMAL0 [[ user(NORMAL0) ]];
};

vertex Mtl_VertexOut xlatMtlMain(
    constant VGlobals_Type& VGlobals [[ buffer(0) ]],
    constant UnityPerDraw_Type& UnityPerDraw [[ buffer(1) ]],
    Mtl_VertexIn input [[ stage_in ]])
{
    Mtl_VertexOut output;
    float4 u_xlat0;
    float4 u_xlat1;
    float u_xlat6;
    u_xlat0 = input.POSITION0.yyyy * UnityPerDraw.hlslcc_mtx4x4unity_ObjectToWorld[1];
    u_xlat0 = fma(UnityPerDraw.hlslcc_mtx4x4unity_ObjectToWorld[0], input.POSITION0.xxxx, u_xlat0);
    u_xlat0 = fma(UnityPerDraw.hlslcc_mtx4x4unity_ObjectToWorld[2], input.POSITION0.zzzz, u_xlat0);
    u_xlat0 = u_xlat0 + UnityPerDraw.hlslcc_mtx4x4unity_ObjectToWorld[3];
    u_xlat1 = u_xlat0.yyyy * VGlobals.hlslcc_mtx4x4unity_MatrixVP[1];
    u_xlat1 = fma(VGlobals.hlslcc_mtx4x4unity_MatrixVP[0], u_xlat0.xxxx, u_xlat1);
    u_xlat1 = fma(VGlobals.hlslcc_mtx4x4unity_MatrixVP[2], u_xlat0.zzzz, u_xlat1);
    output.mtl_Position = fma(VGlobals.hlslcc_mtx4x4unity_MatrixVP[3], u_xlat0.wwww, u_xlat1);
    u_xlat0.x = dot(input.NORMAL0.xyz, UnityPerDraw.hlslcc_mtx4x4unity_WorldToObject[0].xyz);
    u_xlat0.y = dot(input.NORMAL0.xyz, UnityPerDraw.hlslcc_mtx4x4unity_WorldToObject[1].xyz);
    u_xlat0.z = dot(input.NORMAL0.xyz, UnityPerDraw.hlslcc_mtx4x4unity_WorldToObject[2].xyz);
    u_xlat6 = dot(u_xlat0.xyz, u_xlat0.xyz);
    u_xlat6 = max(u_xlat6, 1.17549435e-38);
    u_xlat6 = rsqrt(u_xlat6);
    output.NORMAL0.xyz = float3(u_xlat6) * u_xlat0.xyz;
    return output;
}


-- Hardware tier variant: Tier 1
-- Fragment shader for "metal":
Constant Buffer "UnityPerMaterial" (36 bytes) on slot 0 {
  Vector4 _Color at 0
}

Shader Disassembly:
#include <metal_stdlib>
#include <metal_texture>
using namespace metal;
constant uint32_t rp_output_remap_mask [[ function_constant(1) ]];
constant const uint rp_output_remap_0 = (rp_output_remap_mask >> 0) & 0xF;
struct UnityPerMaterial_Type
{
    float4 _Color;
    float4 _OutlineColor;
    float _OutlineWidth;
};

struct Mtl_FragmentOut
{
    float4 SV_Target0 [[ color(rp_output_remap_0) ]];
};

fragment Mtl_FragmentOut xlatMtlMain(
    constant UnityPerMaterial_Type& UnityPerMaterial [[ buffer(0) ]])
{
    Mtl_FragmentOut output;
    output.SV_Target0 = UnityPerMaterial._Color;
    return output;
}


 }
 Pass {
  Name "OutlinePass"
  Tags { "LIGHTMODE"="ALWAYS" "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline" }
  Cull Front
  Stencil {
   Ref 1.000000
   Pass Replace
  }
  //////////////////////////////////
  //                              //
  //      Compiled programs       //
  //                              //
  //////////////////////////////////
//////////////////////////////////////////////////////
Keywords: <none>
-- Hardware tier variant: Tier 1
-- Vertex shader for "metal":
Uses vertex data channel "Vertex"
Uses vertex data channel "Normal"

Constant Buffer "VGlobals" (64 bytes) on slot 0 {
  Matrix4x4 unity_MatrixVP at 0
}
Constant Buffer "UnityPerDraw" (720 bytes) on slot 1 {
  Matrix4x4 unity_ObjectToWorld at 0
  Matrix4x4 unity_WorldToObject at 64
}
Constant Buffer "UnityPerMaterial" (20 bytes) on slot 2 {
  Float _OutlineWidth at 16
}

Shader Disassembly:
#include <metal_stdlib>
#include <metal_texture>
using namespace metal;
struct VGlobals_Type
{
    float4 hlslcc_mtx4x4unity_MatrixVP[4];
};

struct UnityPerDraw_Type
{
    float4 hlslcc_mtx4x4unity_ObjectToWorld[4];
    float4 hlslcc_mtx4x4unity_WorldToObject[4];
    float4 unity_LODFade;
    float4 unity_WorldTransformParams;
    float4 unity_RenderingLayer;
    float4 unity_LightData;
    float4 unity_LightIndices[2];
    float4 unity_ProbesOcclusion;
    float4 unity_SpecCube0_HDR;
    float4 unity_SpecCube1_HDR;
    float4 unity_SpecCube0_BoxMax;
    float4 unity_SpecCube0_BoxMin;
    float4 unity_SpecCube0_ProbePosition;
    float4 unity_SpecCube1_BoxMax;
    float4 unity_SpecCube1_BoxMin;
    float4 unity_SpecCube1_ProbePosition;
    float4 unity_LightmapST;
    float4 unity_DynamicLightmapST;
    float4 unity_SHAr;
    float4 unity_SHAg;
    float4 unity_SHAb;
    float4 unity_SHBr;
    float4 unity_SHBg;
    float4 unity_SHBb;
    float4 unity_SHC;
    float4 unity_RendererBounds_Min;
    float4 unity_RendererBounds_Max;
    float4 hlslcc_mtx4x4unity_MatrixPreviousM[4];
    float4 hlslcc_mtx4x4unity_MatrixPreviousMI[4];
    float4 unity_MotionVectorsParams;
    float4 unity_SpriteColor;
    float4 unity_SpriteProps;
};

struct UnityPerMaterial_Type
{
    float4 _OutlineColor;
    float _OutlineWidth;
};

struct Mtl_VertexIn
{
    float4 POSITION0 [[ attribute(0) ]] ;
    float3 NORMAL0 [[ attribute(1) ]] ;
};

struct Mtl_VertexOut
{
    float4 mtl_Position [[ position, invariant ]];
};

vertex Mtl_VertexOut xlatMtlMain(
    constant VGlobals_Type& VGlobals [[ buffer(0) ]],
    constant UnityPerDraw_Type& UnityPerDraw [[ buffer(1) ]],
    constant UnityPerMaterial_Type& UnityPerMaterial [[ buffer(2) ]],
    Mtl_VertexIn input [[ stage_in ]])
{
    Mtl_VertexOut output;
    float4 u_xlat0;
    float4 u_xlat1;
    float u_xlat6;
    u_xlat0.x = dot(input.NORMAL0.xyz, UnityPerDraw.hlslcc_mtx4x4unity_WorldToObject[0].xyz);
    u_xlat0.y = dot(input.NORMAL0.xyz, UnityPerDraw.hlslcc_mtx4x4unity_WorldToObject[1].xyz);
    u_xlat0.z = dot(input.NORMAL0.xyz, UnityPerDraw.hlslcc_mtx4x4unity_WorldToObject[2].xyz);
    u_xlat6 = dot(u_xlat0.xyz, u_xlat0.xyz);
    u_xlat6 = max(u_xlat6, 1.17549435e-38);
    u_xlat6 = rsqrt(u_xlat6);
    u_xlat0.xyz = float3(u_xlat6) * u_xlat0.xyz;
    u_xlat1.xyz = input.POSITION0.yyy * UnityPerDraw.hlslcc_mtx4x4unity_ObjectToWorld[1].xyz;
    u_xlat1.xyz = fma(UnityPerDraw.hlslcc_mtx4x4unity_ObjectToWorld[0].xyz, input.POSITION0.xxx, u_xlat1.xyz);
    u_xlat1.xyz = fma(UnityPerDraw.hlslcc_mtx4x4unity_ObjectToWorld[2].xyz, input.POSITION0.zzz, u_xlat1.xyz);
    u_xlat1.xyz = u_xlat1.xyz + UnityPerDraw.hlslcc_mtx4x4unity_ObjectToWorld[3].xyz;
    u_xlat0.xyz = fma(u_xlat0.xyz, float3(UnityPerMaterial._OutlineWidth), u_xlat1.xyz);
    u_xlat1 = u_xlat0.yyyy * VGlobals.hlslcc_mtx4x4unity_MatrixVP[1];
    u_xlat1 = fma(VGlobals.hlslcc_mtx4x4unity_MatrixVP[0], u_xlat0.xxxx, u_xlat1);
    u_xlat0 = fma(VGlobals.hlslcc_mtx4x4unity_MatrixVP[2], u_xlat0.zzzz, u_xlat1);
    output.mtl_Position = u_xlat0 + VGlobals.hlslcc_mtx4x4unity_MatrixVP[3];
    return output;
}


-- Hardware tier variant: Tier 1
-- Fragment shader for "metal":
Constant Buffer "UnityPerMaterial" (20 bytes) on slot 0 {
  Vector4 _OutlineColor at 0
}

Shader Disassembly:
#include <metal_stdlib>
#include <metal_texture>
using namespace metal;
constant uint32_t rp_output_remap_mask [[ function_constant(1) ]];
constant const uint rp_output_remap_0 = (rp_output_remap_mask >> 0) & 0xF;
struct UnityPerMaterial_Type
{
    float4 _OutlineColor;
    float _OutlineWidth;
};

struct Mtl_FragmentOut
{
    float4 SV_Target0 [[ color(rp_output_remap_0) ]];
};

fragment Mtl_FragmentOut xlatMtlMain(
    constant UnityPerMaterial_Type& UnityPerMaterial [[ buffer(0) ]])
{
    Mtl_FragmentOut output;
    output.SV_Target0 = UnityPerMaterial._OutlineColor;
    return output;
}


 }
}
}