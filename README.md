Kitchen Chaos: Modular System Implementation

🛠 Project Overview

This project is a high-fidelity implementation of a chaotic kitchen management game, developed to master decoupled C# architectures and scalable game logic within Unity.

While the core concept was guided by industry-standard patterns (via Code Monkey), this repository serves as a foundation for my own technical extensions and architectural experiments.



🧪 Key Technical Architectures

As a researcher, I focused on implementing systems that are robust and easy to extend:

The Observer Pattern & Decoupling: Mastering C# Events to ensure the game logic remains entirely independent of UI and Audio systems—essential for building bug-resistant, testable research tools.

Dependency Inversion & Interfaces: Implementing an IInteractable architecture that allows the player to engage with any world object through a single, abstract gateway rather than messy, hard-coded references.

Modular Data Structures: Utilizing ScriptableObjects to create a "data-driven" design, allowing for the easy addition of new content and recipes without modifying the core engine logic.

Complex Finite State Machines: Architecting reliable state transitions for objects (like the multi-stage cooking and burning process) to prevent "logic-leaks" in chaotic environments.



🚀 Upcoming Extensions:

I am currently iterating on this codebase to include:

 ✅ An update version to Unity 6.3

 ✅ The Rush Hour Event: An observer-pattern implementation that shifts game state and "vibe" as the timer runs low.

 Dynamic Power-Up System: A random spawner system utilizing custom Blender assets.

