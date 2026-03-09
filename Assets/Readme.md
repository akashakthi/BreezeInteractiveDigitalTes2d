# Ninja FSM Test

## Unity Version
Unity 2022.3.62f1

## Features
- Finite State Machine for Ninja character
- States:
  - Idle
  - Run
  - Jump
  - Attack
  - Hurt
  - Die
- Script-driven animation calls through FSM
- Dummy target with health system
- Fire hazard with damage over time
- World-space health bar for player and enemy

## Controls
- A / D or Left / Right Arrow: Move
- Space: Jump
- J: Attack

## Architecture Notes
- Runtime scripts organized by feature/domain
- FSM separated into base state, state machine, and concrete states
- Health bar uses generic health source abstraction (`IHealthSource`)
- Damageable entities implement `IDamageable`

## Demo Scene
- Player can move, jump, attack dummy
- Dummy receives damage and dies
- Fire hazard damages player over time and triggers hurt/die states