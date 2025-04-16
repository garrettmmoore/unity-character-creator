# Unity Character Creator
A character customization scene built in Unity.

## Overview
This project provides a customizable character creation system that allows users to personalize their avatars directly through an intuitive in-game UI, enabling adjustments to various character attributes and features.

## Features
- Character customization options
- Interactive UI for selecting character features
- Live character preview with rotation controls
- Simple and intuitive design

## Structure
The project includes these core scripts:
- [CharacterCustomizationUI.cs](Assets/_Project/_Scripts/CharacterCustomizationUI.cs) - Handles the UI interactions for character customization
- [PlayerCharacterCustomized.cs](Assets/_Project/_Scripts/PlayerCharacterCustomized.cs) - Manages the player character's customizable features
- [CharacterRotator.cs](Assets/_Project/_Scripts/CharacterRotator.cs) - Allows the character preview to be rotated during customization

## Requirements
- Unity 2020.3 or newer
- **Note: Character model and UI assets are not included and are required to run the project locally**

## Getting Started
1. Open the project in Unity
2. Import your character model and UI components to the Assets folder
3. Navigate to the main scene in the Assets/Scenes folder
4. Create a prefab variant of the Character prefab and make the following changes:
   1. Replace the placeholder character model with your character model
   2. Assign various character outfits and attachments in the PlayerCharacterCustomized script component
   3. Adjust settings in the CharacterRotator script component
   4. Add an idle animation (if any) for your character in the Animator component
   5. Drag and drop the prefab variant into the main scene and delete the old character prefab
5. In the main scene, assign your UI components in the `CharacterCreatorContainer -> CharacterCustomizationUI` GameObject
6. Press Play to test the character customization interface


