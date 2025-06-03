# Project Documentation

## Project Overview

**Protocol: Run** is a 2D (almost) endless runner game built in Unity for Mobile devices, designed around UI-based elements. The player runs forward automatically, avoiding hazards and obstacles that scroll from right to left. The goal is to reach a certain score without running into any hazards in order to win.

---

## Architecture & Design Patterns

### Project Architecture
The project is built with **modular components** that separate responsibilities between gameplay, UI and scene-related logic. The main architecture follows **single-responsibility principles** to allow for better maintenance, testing and scalability. Some of the key architectural decisions include:

- **Single-scene structure:** All gameplay and UI logic run within a single Unity scene, simplifying scene transitions and data persistence.
- **ScriptableObject-based configurations:** Game and Player settings are defined using ScriptableObjects, allowing for easy tuning without modifying the code directly.
- **Behavior-driven components:** Player, tiles and obstacles follow self-contained behavior logic, ensuring maintainable and extendable code.
- **Decoupled systems:** Input, Game State transitions, UI and Score management are decoupled via event-driven or observer-based logic, minimizing dependencies and promoting clean communication across systems.

### Key Design Patterns Used

| Pattern               | Purpose                                                                          |
|-----------------------|----------------------------------------------------------------------------------|
| <p align="center">**State Pattern**</p>     | Manages the game flow (Main Menu, Playing, Game Over, etc).                      |
| <p align="center">**Object Pooling**</p>    | Used for reusing tiles and obstacle objects for performance - to be implemented. |
| <p align="center">**Strategy Pattern**</p>  | Used to handle different behaviors (ie the tile generation logic).               |
| <p align="center">**Observer Pattern**</p>  | Handles score updates and other conditions reactively.                           |
| <p align="center">**ScriptableObjects**</p> | Centralized configurations for game rules and player behavior.                   |

### Main Systems
- **GameManager**: Controls the current game state and its transitions. Also holds references to other main components.
- **PlayerController**: Handles player movement, jumping and collision, as well as its respective animations.
- **FloorScroller**: Moves the background and tile elements based on the preset configurations.
- **TileSpawner**: Spawns/despawns tile elements and controls the obstacles logic.
- **MainMenuView**: Displays the Main Menu with a Play button to start the gameplay.
- **GameOverMenuView**: Displays score and win/loss result based on the preset configurations.

### Planned Future Updates
- Improve the UI with better assets and animations.
- Add sound effects and background music.
- Improve the scenario with a proper background and parallax effects.
- Add a countdown before the player starts running.
- Improve the tile spawning/despawning system to use object pooling.
- Include some kind of onboarding when the player is playing for the first time.
- Add more tiles and obstacles variations.
- Make it possible for the player to shoot obstacles.
- Add animations and unique effects for hitting each obstacle.
- Improve and add more juice to the score display.
- Balance the obstacle generation to be fairer to the player.
- Add an Endless Mode that players can choose from the Main Menu.
- Expose the Configs into some kind of Debug menu.
- Include Leaderboards and other score based features.
- Use Sprite Atlases to improve performance and resources management.
- Add an Achievement system to reward players for specific accomplishments.

---

## Time Spent on Project
> Around 12 hours total as of the latest update – 8 hours of development, 2 hours of testing and another 2 hours of polishing and fixing bugs.

---

## Major Difficulties
> The main difficulty I had with the project was actually deciding on what kind of game to build with the base assets and the available time, and then deciding the best setup for building it. As I chose to use a more UI-centered approach, I had to make some adaptations for it to be as performative as possible, while avoiding common Mobile-related UI issues such as making sure it was adaptable for all kinds of screens and different ratios.

## How to Play

- Press **Play** on the Main Menu.
- The player starts running immediately (a countdown will be implemented on a future update).
- Press **Space** or the **Left Mouse Button** (if playing on Unity) or **tap the screen** (if playing on Mobile) to jump.
- Avoid hazards such as the spike and acid pits, as well as the obstacles (barrels) on the way.
- Reach the target score to win - default is 1000.