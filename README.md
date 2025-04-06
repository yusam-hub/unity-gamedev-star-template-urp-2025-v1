### Project
- Unity Game Dev Star Template URP 2025 v1
- Github https://github.com/yusam-hub/unity-gamedev-star-template-urp-2025-v1

### Unity
- https://unity.com/releases/editor/archive
- Unity 6000.0.36f1

### Author
- https://t.me/iurii_sam

### Changes

### 2025-04-06 (v1.0.1)

- Обновлены компоненты (36 файлов)
- Добавлен компонент YuCoFieldOfView (пример позже будет)
- Добавлен компонент YuCoTurret (пример позже будет)
- Добавлены компоненты для работы со звоком MusicPool + EffectPool + TestMusic + TestEffect (пример позже будет)
- Добавлено разное для облегчения сборки (пример позже будет)

#### 2025-03-28 (v1.0.2)

- Обновлены компоненты
  
#### 2025-03-24 (v1.0.1)

- Ветка master_2022 больше не поддерживается - весь коде в мастере для Unity 6
- Вышло обновление v1.0.1 - Assets/Www_ChangeLog.md

#### 2025-03-23 (v1)

- В Unity 6000 Render Graph API поддерживается только в HDRP. Если в проекте URP использоватлся Render Graph API, то работать он не будет в будущем. Сейчас в проект включен режим совместимости. Нужно сделать миграции.
- The project currently uses the compatibility mode where the Render Graph API is disabled. Support for this mode will be removed in future Unity versions. Migrate existing ScriptableRenderPasses to the new RenderGraph API. 
- After the migration, disable the compatibility mode in Edit > Projects Settings > Graphics > Render Graph.
