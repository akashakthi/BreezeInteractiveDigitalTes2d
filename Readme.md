# Ninja FSM Programming Test

## Unity Version
Unity **2022.3.62f1**

## Overview
This project implements a **Finite State Machine (FSM)** for a Ninja character in Unity as part of the programming test.

The implementation focuses on demonstrating:
- Clear FSM architecture
- State transition handling
- Character control and input handling
- Damage and death flow
- Clean and maintainable code organization

The Ninja character supports the following states:

- Idle
- Run
- Jump
- Attack
- Hurt
- Die

---

# Implemented Features

- Finite State Machine for Ninja character behaviour
- Character movement and jump system
- Attack system with hit detection
- Hurt and death state transitions
- Script-driven animation control through FSM
- Dummy target with health system for combat testing
- Fire hazard with damage-over-time
- Sprites health bars for both player and enemy

---

# Controls

## Keyboard
- **A / D** or **Left / Right Arrow** → Move
- **Space** → Jump
- **J** → Attack

## Controller
- **Left Stick / D-Pad Horizontal** → Move
- **Cross / X** → Jump
- **Square** → Attack

---

# Demo Scene

The demo scene contains a simple playable setup designed to demonstrate the FSM behaviour:

- Controllable Ninja character
- Dummy enemy target to test attack and death flow
- Fire hazard that applies damage over time
- Sprites health bars for both player and enemy

These elements are included to clearly showcase the FSM state transitions and gameplay interactions.

---

# Architecture Notes

The codebase is structured to keep responsibilities separated and easy to maintain.

Key design decisions:

- Scripts are organized by **feature/domain**
- FSM implementation is separated into:
  - Base state class
  - State machine
  - Concrete states
- Animation calls are triggered through FSM state logic using a dedicated animator wrapper
- Damageable entities implement the **`IDamageable`** interface
- UI health bars read from a generic health abstraction via **`IHealthSource`**

---

# Project Structure
Runtime
├── Common
│ └── Combat
├── Player
│ └── Ninja
├── Enemy
│ └── Dummy
├── Hazard
└── UI
└── WorldSpace