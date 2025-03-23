### Project
- Unity Game Dev Star Template URP 2025 v1

### Unity
- https://unity.com/releases/editor/archive
- Unity 6000.0.36f1 - branch name -> master
- Version 2022.3.45f1 - branch name -> master_2022

### Author
- https://t.me/iurii_sam

### Changes

#### 2025-03-23

- В Unity 6000 Render Graph API поддерживается только в HDRP. Если ваш проект использует URP или Built-in Render Pipeline, то Render Graph API работать не будет в будущем. Сейчас в проект включен режим совместимости.
- The project currently uses the compatibility mode where the Render Graph API is disabled. Support for this mode will be removed in future Unity versions. 
- Migrate existing ScriptableRenderPasses to the new RenderGraph API. After the migration, disable the compatibility mode in Edit > Projects Settings > Graphics > Render Graph.