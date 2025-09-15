## 🚀 Backlog and Future work

Sound feedback, dynamic Stroop rules, and adjustments to attention-demanding stimuli are critical next steps for enhancing the prototype’s functionality.

- [ ]  Logging variables in CSV and store the file in the Headset
- [ ]  Sound feedback when color prompt changes
- [ ]  Sound feedback when a platform gets activated
- [ ]  Sound feedback when arriving at the wrong platform
- [x] Finish gameloop
    - [x] Connect the condition with the game state
    - [x] Update the game loop to have a practice and trial color sequence
    - [x] GUI for *Start Practice*, then for "Practice complete now" *Start Trial*.
    - [x] GUI once the Trial is completed (Condition fulfilled), *Back to conditions*
    - [x] The color prompt shows "COLOR" or "WORD" depending on the presence of the congnitive interference
- [x] Menu to hide the conditions
- [x] Confirmation feature to manually reset, terminate and fulfill each condition and the experiment
- [x] Test the conditions manu in VR
- [x] Hide and show the conditions menu
- [x] Begin the experiment with a subject ID, set the ID either by keyboard input, or random.

## Known issues
- The color prompter sometimes gives a color that corresponds to the current platform the player is standing.
- The teleportation orientation feature disables object grabbing when both functionalities are active in the same scene.