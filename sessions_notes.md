# Session notes

Here is the revised version of the README with typos and clarity improved:

---

## Session 2024-11-05: Adding Graph and Distances

- **Objective:** Implement the algorithms to determine which platform to teleport to.
- Create the cycle graph data structure.
- Create the list of distances `{ -3, -2, -1, 1, 2, 3 }`, shuffle it, and store the values in a stack.
- Use the graph to query the color that will be prompted to the user.
- Pop distances from the stack to determine the color.

The graph’s purpose is to represent colors that we can query using abstract distances or steps when the player is prompted to move to a new location. This is independent of real-world distances. Each vertex/node in the graph corresponds to a platform color. The only relationship between the graph and the physical platform layout is the color (number) of the current platform the player is standing on, which allows us to determine the starting node for movement.

Given the hexagonal layout of the six platforms (one at each vertex), the order of platforms should match the graph nodes. Thus, the adjacency list in the graph reflects how the platforms are distributed relative to each other.

```bash
Platform -> red(0), blue(1), green(2), ...
Graph    -> { 0, 1, 2, ... }
```

Since the graph does not exist as a GameObject in the scene, it does not need a visual representation or to be affected by transformations. Therefore, it can be implemented as a `ScriptableObject`. 
