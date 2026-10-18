# Draw Calls: Shaders and Batches

1. **Draw Call**
    
    -> CPU가 GPU에게 머티리얼 세팅과 함께 "이것을 그려라"라고 내리는 명령 (병목의 주원인)

2. **SRP Batcher**

    -> 셰이더 속성을 `UnityPerMaterial` 상수 버퍼(CBUFFER)에 묶어, 머티리얼 상태 변경(Setup Cost)에 들어가는 오버헤드를 극적으로 제거

3. **MaterialPropertyBlock**

    -> 머티리얼을 **복제(Material Instance)** 하지 않고, 개별 오브젝트 단위로 속성(색상 등)만 덮어씌워 배칭이 깨지는 것을 방지

4. **GPU Instancing**

    -> `Graphics.DrawMeshInstanced`를 통해 흩날리는 잎사귀, 파편 등 수많은 동일 메시를 단 한 번의 드로우 콜로 GPU에 전송

5. **Alpha Clipping (Cutoff)**

    -> 픽셀의 투명도(a)가 특정 수치 이하면 `clip()` 함수로 렌더링을 완전히 취소하여 가볍게 처리

6. **Alpha Blending (Transparent)**

    -> `Blend` 모드와 `ZWrite Off`를 조합하여 배경 픽셀과 현재 픽셀을 부드럽게 합성


```
Scene / Game Logic
                     │
          ┌──────────┴──────────┐
          ▼                     ▼
     하이어라키 배치         코드 다이렉트 렌더링
   (개별 GameObject)          (MeshBall.cs)
          │                     │
          ▼                     ▼
 MaterialPropertyBlock   Matrix & Color Arrays
          │                     │
          └──────────┬──────────┘
                     ▼
             B0XRenderPipeline
           (Culling & Sorting 설정)
                     │
          ┌──────────┴──────────┐
          ▼                     ▼
     SRP Batcher          GPU Instancing
 (상태 변경 오버헤드 최소화)  (수천 개를 1번의 Draw로)
          │                     │
          └──────────┬──────────┘
                     ▼
           Shader (UnlitPass.hlsl)
        (UnityPerMaterial 버퍼 매크로)
                     │
         ┌───────────┼───────────┐
         ▼           ▼           ▼
       Opaque      Cutoff   Transparent
    (ZWrite On) (clip 연산)  (Blend 조절)
         │           │           │
         └───────────┬───────────┘
                     ▼
                    GPU
```

## org

- [catlikecoding - Draw Calls](https://catlikecoding.com/unity/tutorials/custom-srp/draw-calls/)