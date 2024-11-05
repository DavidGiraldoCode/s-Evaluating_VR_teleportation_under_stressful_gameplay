# Session notes

## Session 2024-11-05: Adding graph and distances

- **Objective:** Implement the algorithms to prompt what platform to teleport to.
- Create the cycle graph data structure
- Create the distances list `{-3, -2, -1, 1, 2, 3}` and shuffle it and store them in a stack
- Use the graph to query the color that will be promted to the user
- Pop out the distances from the color stack.

The graph objective is to represent colors that we can query using abstract distances, or steps, once the player is prompt to get to a new location. It is not related or dependent to the real distance in world space. Although, each vertex/node of the graph has a number that is homologous to the color of a platform. The only strong relationship between the graph and the platform in world space is the color (number) of the current platfrom the player is stading, and thus we can know from which node to start moving.

Considering the hexagon layout of the 6 platform (each at every vertex). The order of the platfrom needs to match the ones of the Graph nodes. So the adjacenty list of the graph relects how the platforms are distributed relative to eachother.
```bash
Platform -> red(0), blue(1), gree(2), ...
Graph    -> {0, 1, 2, ...}
```

As the graph does not live in the scene as a GameObject, does not NEED a visual representation, or been affected by transformations. It can be a Scriptable Object.

