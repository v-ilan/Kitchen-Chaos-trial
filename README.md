Kitchen Chaos: Modular System Implementation

🛠 Project Overview

This project is a high-fidelity implementation of a chaotic kitchen management game, developed to master decoupled C# architectures and scalable game logic within Unity.

While the core concept was guided by industry-standard patterns (via Code Monkey), this repository serves as a foundation for my own technical extensions and architectural experiments.



🧪 Key Technical Architectures

As a researcher, I focused on implementing systems that are robust and easy to extend:

 Event-Driven Communication: Leveraged C# Events to ensure the Game Logic and UI are completely decoupled.
 
 Interface-Based Interaction: Implemented an IInteractable system, allowing the player to interact with any object (Counters, Trash Bins, Stoves) through a single interface.
 
 ScriptableObject Databases: Data-driven design for recipes and kitchen objects, making it possible to add new content without changing a single line of code.
 
 State Machine Logic: Managed complex object states (e.g., the Stove’s Idle -> Frying -> Fried -> Burned transition) using clean, maintainable logic.



🚀 Upcoming Extensions:

I am currently iterating on this codebase to include:

 ✅ An update version to Unity 6.3

 The Rush Hour Event: An observer-pattern implementation that shifts game state and "vibe" as the timer runs low.

 Dynamic Power-Up System: A random spawner system utilizing custom Blender assets.

