# Yu-Gi-Oh! Life Counter (.NET 8 + .NET MAUI)

A cross-platform **Yu-Gi-Oh! life points tracker** built with **.NET 8** and **.NET MAUI** (Windows + mobile).  
This project is a faithful migration of an original **Next.js / React (TSX)** prototype, keeping the same UX and rules while adopting a clean, testable C# architecture.

> ⚠️ Disclaimer: This is a fan-made utility app. Yu-Gi-Oh! is a trademark of its respective owners. This project is not affiliated with or endorsed by Konami.

---

## Features (ported from the original TSX app)

### Core duel flow
- **2 players** with configurable **starting Life Points** (default: 8000)
- **Tap to edit LP** using a calculator-style input
- **Game log** with:
  - automatic history entries per change
  - **Undo** / **Redo**
  - **Clear log**
  - **Export log to `.txt`** (same format as the original)
- **Reset duel** (clears LP + history)

### Calculator / expression parser
Supports the same shortcuts and behavior as the TSX version:
- Absolute: `8000` → set to 8000
- Relative: `+1000`, `-800`
- Multiplication: `*2`, `*3`
- Division: `/2`, `/3`
- Percentage shortcut: `%2` (divide current LP by 2)
- Thousands suffix: `8k`, `+2k`
- Result is **floored** and LP never goes below **0**

### Duel tools
- Timer (start/pause/reset)
- Dice roll (1–6)
- Coin flip (heads/tails)
- Phase tracker (Draw → Standby → Main 1 → Battle → Main 2 → End)
- Counters (spell counters, tokens, turns, custom)
- Notes (auto-saved locally)

### Settings
- Starting LP presets (8000 / 4000 / 2000)
- Theme: Dark / Light / Auto (system)
- Haptic feedback (mobile)
- Sound effects (optional)
- Keep screen on (optional)
- “Clear all data” (local-only)

### Local-first
- All data is stored locally on the device.
- No backend. No tracking.

---

## Migration notes (TSX → .NET MAUI)

The original app used:
- Next.js + React (TSX)
- Zustand + persistence (localStorage)
- UI built with a mobile-first layout and a battery-friendly dark theme

This MAUI version keeps the same functional rules, but changes the internals to be:
- **strongly typed**
- **unit-testable**
- **cross-platform (Windows + Android/iOS)**

### Important adaptation for mobile performance
- The original TSX prototype did not enforce a strict history size.
- In this MAUI port we enforce a **hard cap of 1000 log entries per duel** to avoid memory/perf issues on mobile devices.

---

## Solution structure (Option A)

This repository follows a simple, clean separation of concerns:

- **YugiohLifeCounter** (MAUI UI)
  - Pages, Views, ViewModels (MVVM)
  - Dependency Injection setup
- **YugiohLifeCounter.Application**
  - Use-cases/services (orchestrates domain operations)
  - App-level abstractions (storage, vibration, sound, etc.)
- **YugiohLifeCounter.Core**
  - Domain models + pure logic (game engine, calculator, state transitions)
  - No MAUI dependencies
- **YugiohLifeCounter.Core.Tests**
  - Unit tests for the pure domain logic (xUnit)

---

## Tech stack

- .NET 8
- .NET MAUI
- MVVM pattern (CommunityToolkit.Mvvm)
- Dependency Injection (built-in .NET host)
- xUnit (tests)

---

## Prerequisites

### Recommended (Windows dev)
- **Visual Studio 2022** with workloads:
  - “.NET Multi-platform App UI development”
- **.NET 8 SDK** (recent patch recommended)
- **Windows 10/11 SDK** (10.0.19041+)

If you see MVVM Toolkit errors about Windows SDK references, update your .NET SDK / Visual Studio.
(As a temporary workaround, you can pin `WindowsSdkPackageVersion` in the MAUI `.csproj`.)

### CLI alternative
- Install MAUI workload:
  - `dotnet workload install maui`

---

## Getting started

### Run on Windows (recommended for early development)
1. Open the solution in Visual Studio.
2. Set startup project: `YugiohLifeCounter`
3. Choose target: **Windows Machine**
4. Run (F5)

### Run on Android
1. Install Android emulator (via Android Device Manager) or connect a device.
2. Select target: Android emulator/device
3. Run (F5)

### Build from CLI (Windows)
```bash
dotnet restore
dotnet build ./src/YugiohLifeCounter/YugiohLifeCounter.csproj -f net8.0-windows10.0.19041.0

### Run tests

```bash
dotnet test
```

---

## Export log format (TXT)

The export matches the original behavior:

* Header with duel title
* Current LP per player
* Duel duration
* History ordered from first change to last change, including timestamp, change, resulting LP, and optional expression

Example:

```
Yu-Gi-Oh! Duel Log
===================

Player 1: 6500 LP
Player 2: 8000 LP
Duration: 3m 12s

History:
--------
1. [15:02:01] Player 1: -1500 → 6500 LP (-1500)
```

---

## Roadmap (high level)

* [ ] Finish 1:1 UI parity with the TSX prototype (layout, spacing, typography)
* [ ] Implement local persistence for:

  * game state + undo stack
  * notes
  * settings
* [ ] Optional: sound + haptics toggles wired per platform
* [ ] Optional: “keep screen on” wiring per platform
* [ ] Polish + packaging (Windows MSIX / Android APK/AAB)

---

## Contributing

* Keep domain logic inside **Core** (pure + testable)
* Keep UI logic inside **MAUI** project (ViewModels bind to services)
* Add tests for any domain rule changes (`YugiohLifeCounter.Core.Tests`)
* Prefer small commits with clear messages

---

## Credits

* Original functional reference: Next.js / React (TSX) prototype
* UI inspiration: mobile-first duel tracker design principles
* Built as a clean, testable port to .NET MAUI

---

```
::contentReference[oaicite:0]{index=0}
```
