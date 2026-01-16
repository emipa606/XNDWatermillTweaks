# GitHub Copilot Instructions for [XND] Watermill Tweaks (Continued)

## Mod Overview and Purpose

The [XND] Watermill Tweaks mod enhances the mechanics of the watermill generator in RimWorld, adding layers of complexity and strategy. The primary aim is to offer a more nuanced decision-making process regarding power generation, considering environmental factors such as temperature, seasonality, and a unique in-game event, "Turbulent Waters."

## Key Features and Systems

- **Adjusted Power Output**: Watermills now generate 1400W of power, up from the default 1100W. 
- **Temperature Variation**: Power output varies with extreme outdoor temperatures, enhancing gameplay realism. 
- **Seasonal Variation**: Different seasons yield varying power outputs, with spring and fall generating more power while winter generates less.
- **Turbulent Waters Incident**: This new gameplay element increases power generation by 50% but damages watermills over time. It occurs only on maps with certain river types and varied likelihoods based on rainfall levels.

## Coding Patterns and Conventions

- **Naming Conventions**: Classes and methods use PascalCase, making it easy to identify types and behavior.
- **Single Responsibility**: Methods and classes are designed to have clear, focused responsibilities.
- **Static Utility Classes**: Classes such as `WatermillUtility` are used to hold methods commonly accessed by multiple classes.
- **Extension Methods**: Utilized in some utility classes for enhanced readability and structure.

## XML Integration

- XML is used to define game objects, incidents, and other components directly within RimWorld. 
- Ensure any modifications to XML files maintain compatibility with existing save games and other mods unless explicitly noted.

## Harmony Patching

- Harmony Patches are extensively used to integrate this mod’s features into the vanilla game.
- **Targeted Patch Classes**: `HarmonyPatches.cs` includes patches for modifying desired power outputs and any additional inspections to be displayed.
  
  csharp
  internal static class HarmonyPatches
  {
      // Patches and methods here
  }
  

- **Incident Patching**: Specific patches handle the new `Turbulent Waters` incident, altering the game flow when active.

## Suggestions for Copilot

1. **Utility Functions**: Implement small, focused utility functions for repetitive tasks, such as temperature checks for power output variance.
2. **Error Handling**: Enhance error reporting and handling in the mod’s Harmony patches to identify issues efficiently.
3. **Descriptive Comments**: Encourage Copilot to maintain descriptive comments in Harmony patches and complex logic sections.
4. **XML Data File Assistance**: Use Copilot to help with XML file setups by suggesting property names and values based on patterns found in existing XML configurations.
5. **Testing Suggestions**: Implement helper methods within test classes for frequent unit test patterns, like asserting power outputs under specific conditions.

Use these guidelines and instructions to maintain consistent mod updates and streamline development efforts using GitHub Copilot effectively.
