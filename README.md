# 🛠️ Unity Robust UI Architecture | Minecraft-Inspired Menu

![Unity](https://img.shields.io/badge/Unity-6_LTS-000000?style=for-the-badge&logo=unity&logoColor=white)
![C#](https://img.shields.io/badge/C%23-Clean_Code-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![Architecture](https://img.shields.io/badge/Architecture-Event--Driven-blue?style=for-the-badge)

## 📌 About the Project
This project is a high-level UI prototype designed to recreate the iconic **Minecraft Java Edition** main menu and settings flow within **Unity 6**. 

However, this is not just a visual clone. The primary goal of this repository is to showcase **Software Engineering best practices applied to Game Development**. It features a highly modular, decoupled, and scalable UI architecture built for production, heavily relying on the **Observer Pattern**, **ScriptableObjects (Data-Driven Design)**, and **SOLID principles**.

<br>



<br>

## ⚙️ Core Architecture & Engineering Highlights

### 1. Event-Driven System (Observer Pattern)
UI elements never check for state changes inside an `Update()` loop. Instead, the project uses a centralized `GameSettingsSO` (ScriptableObject) acting as the Single Source of Truth. 
* When a setting (e.g., Language, Audio) changes, it fires a C# Action.
* UI components (Sliders, Toggles, Labels) subscribe to these events and re-render instantly, ensuring **zero CPU overhead** while idle.

### 2. Hybrid Localization System
A robust localization infrastructure that doesn't rely on "magic strings".
* **Data-Driven:** Translations are stored in a `LocalizationTableSO`.
* **Dynamic Prefixes & Suffixes:** UI components dynamically combine translated base keys (e.g., "Video_ExclusiveFullscreen") with engine-resolved suffixes (e.g., "ON/OFF") locally, preventing race conditions via EndOfFrame coroutines (`MarqueeTextUI`).

### 3. Absolute Modularity (SRP & KISS)
Every script has a single reason to change. 
* E.g., `UISoundFeedback.cs` handles audio feedback independently. It doesn't know what a button does; it only knows how to react to Unity's `EventSystem` click events.
* Sliders are universally controlled by a generic `AudioSliderUI.cs` that dynamically connects to the `AudioMixer` via an `AudioChannel` enum.

### 4. Responsive & Bulletproof Layouts
The UI uses a combination of Unity's Canvas Scaler, advanced `Vertical Layout Groups`, and `Content Size Fitters`.
* **Dynamic Grid Spacing:** The layout automatically calculates safe screen heights. 
* **Spacer System:** Buttons remain mathematically centered regardless of aspect ratio, while absolute elements (like the Logo) are anchored to the top without overlaps, preventing UI clipping at extreme resolutions.

<br>

## 🎮 Features Implemented
- **Video Settings:** Exclusive Fullscreen toggle, GUI Scale integer limits, and dynamic Background FPS reduction (AFK/Minimized states).
- **Audio Settings:** Universal Sliders mapped to Master, Music, and UI channels.
- **Accessibility:** Marquee text logic (ping-pong scrolling effect) for labels that overflow their button containers dynamically based on the selected language.
- **UI Audio:** Modular sound feedback system integrated with the AudioMixer to provide classic interaction sounds.

## 🚀 How to Run
1. Clone this repository.
2. Open the project in **Unity 6 LTS** (or newer).
3. Open the `MainMenu_Scene`.
4. Press Play and interact using the Mouse.

## 👤 Author
Developed as a portfolio piece by Bip.
Dedicated to building robust game systems from the ground up, utilizing event-driven architecture and clean code practices for maximum performance and maintainability.
