# Ninja FSM Programming Test

## Unity Version
Unity 2022.3.62f1

## Overview
This project implements a finite state machine (FSM) for a Ninja character in Unity, based on the programming test brief.

The character supports the following states:
- Idle
- Run
- Jump
- Attack
- Hurt
- Die

The main goal of this test is to demonstrate:
- FSM architecture
- state transition handling
- character input and movement
- damage and death flow
- organized and maintainable code structure

## Implemented Features
- Finite State Machine for Ninja character states
- Character controller with movement, jump, attack, hurt, and death handling
- Script-driven animation control through FSM/state logic
- Dummy target with health system for attack testing
- Fire hazard with damage-over-time for hurt/death testing
- World-space health bars for player and dummy target

## Controls
### Keyboard
- A / D or Left / Right Arrow: Move
- Space: Jump
- J: Attack

### Controller
- Left Stick / D-Pad Horizontal: Move
- Cross / X: Jump
- Square: Attack

## Demo Scene
The demo scene includes:
- A controllable Ninja character
- A dummy target to test attack and death flow
- A fire hazard to test damage, hurt, and die state transitions

## Architecture Notes
- Scripts are organized by feature/domain under `Runtime`
- FSM is separated into:
  - base state
  - state machine
  - concrete states
- Animation playback is triggered from FSM/state logic through a dedicated animator wrapper
- Damageable objects implement `IDamageable`
- Health bar UI reads from a generic health abstraction via `IHealthSource`

## Project Structure
- `Common/Combat` → shared interfaces
- `Player/Ninja` → main character systems
- `Enemy/Dummy` → dummy target for combat test
- `Hazard` → fire damage zone
- `UI/WorldSpace` → health bar and world-space follower logic

## Notes
Additional simple gameplay elements such as dummy target, fire hazard, and health bars were added only to make the FSM, damage flow, and state transitions easier to demonstrate and test.