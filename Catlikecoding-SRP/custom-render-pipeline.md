# Custom Render Pipeline Taking Control of Rendering

1. `RenderPipelineAsset`
    
   -> 어떤 RenderPipeline을 사용할지 연결

2. **RenderPipeline**

   -> 전체 프레임의 렌더링 흐름

3. `Camera`

   -> 카메라 하나를 독립적으로 렌더링

4. `SetupCameraProperties()`

   -> Camera 의 렌더링 상태/행렬 설정

5. **Culling**

   -> Camera가 볼 수 있는 Renderer 추출

6. **Draw**

   -> Filtering + Sorting + Shader Pass로 실제 렌더링

7. `Submit()`

   -> 쌓인 렌더링 명령을 실행하도록 제출


```text
              Unity
                │
                ▼
       B0XRenderPipeline
                │
                ▼
             Camera
                │
                ▼
       SetupCameraProperties
                │
                ▼
             Culling
                │
                ▼
         CullingResults
                │
        ┌───────┴────────┐
        ▼                ▼
      Opaque           Transparent
        │
        ▼
      Skybox
        │
        └───────┬────────┘
                ▼
              Submit
                │
                ▼
               GPU
```

## Org

- [catlikecoding - Custom Render Pipeline Taking Control of Rendering](https://catlikecoding.com/unity/tutorials/custom-srp/custom-render-pipeline/)