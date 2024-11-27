# Project structure

`GenerateRandomCoordinateList` inside `GameState`, creates the Graph.
`EnterGameplay` in `ExprimentManager` and in `GameplayManager` triggers the setup of the platforms.

The primary button on `GameplayGUI` children call `BeginGame` on GameplayManager

**Who triggers the platform activation?**
- 