using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;


class ShortestPath{
    private Coordinate coords = new Coordinate();
    /// <summary>
    /// A* algorithm based on Dijkstra
    /// </summary>
    /// <param name="grid">The grid that the entities move around</param>
    /// <param name="startX">Starting position X</param>
    /// <param name="startY">Starting position Y</param>
    /// <param name="endX">Ending position X</param>
    /// <param name="endY">Ending position Y</param>
    /// <returns></returns>
    public Tuple<double, double> Pathfinder(List<List<Field>> grid, double startX, double startY, double endX, double endY) {
        if (startX == endX && startY == endY){
            return Tuple.Create((double)startX, (double)startY); // Return start values
        }
        Field start = new Field((int)startX, (int)startY, false, false, false); // Change the coordinates to match the starting point.
        Field finish = new Field((int)endX, (int)endY, false, false, false); // Change the coordinates to match the finishing point.
        

        start.SetDistance(finish.X, finish.Y); //Find distance between zombie and player

        var activeFields = new List<Field>();
        activeFields.Add(start);

        var visitedFields = new List<Field>();
        List<Tuple<double, double>> path = new List<Tuple<double, double>>(); // Store the path coordinates.

        while (activeFields.Any()) {
            Field checkField = activeFields.OrderBy(f => f.CostDistance).First(); //Order fields from lowest to highest cost

            if (checkField.X == finish.X && checkField.Y == finish.Y) { // Path found; reconstruct and add coordinates to the path.
                
                Field field = checkField; 
                while (true) {
                    path.Add(Tuple.Create((double)field.X, (double)field.Y)); // Add the coordinates to the path.
                    field = field.Parent;
                    if (field == null) {
                        path.Reverse(); // Reverse the path to get it in the correct order.
                        return path[1];
                    }
                }
            }

            visitedFields.Add(checkField);
            activeFields.Remove(checkField);

            var walkableFields = GetWalkableTiles(grid, checkField, finish);

            foreach (var walkableField in walkableFields) {
                // Check if the field has already been visited.
                if (visitedFields.Any(f => f.X == walkableField.X && f.Y == walkableField.Y))
                    continue;

                // Check if the field is already in the active list.
                if (activeFields.Any(f => f.X == walkableField.X && f.Y == walkableField.Y)) {
                    var existingField = activeFields.First(f => f.X == walkableField.X && f.Y == walkableField.Y);
                    if (existingField.CostDistance > checkField.CostDistance) {
                        activeFields.Remove(existingField);
                        activeFields.Add(walkableField);
                    }
                }
                else {
                    // Add the walkable field to the active list.
                    activeFields.Add(walkableField);
                }
            }
        }

        Console.WriteLine("No Path Found!");
        return Tuple.Create((double)startX, (double)startY); // Return an empty list if no path is found.
    }

    private List<Field> GetWalkableTiles(List<List<Field>> grid, Field currentField, Field targetField) {
    int maxX = grid.Count - 1;
    int maxY = grid[0].Count - 1;

    int x = currentField.X;
    int y = currentField.Y;

    List<Field> possibleFields = new List<Field>() {
        new Field(x, y - 1, false, false, false),   // Up
        new Field(x, y + 1, false, false, false),   // Down
        new Field(x - 1, y, false, false, false),   // Left
        new Field(x + 1, y, false, false, false),   // Right
        // new Field(x - 1, y - 1, false, false, false), // Diagonal Up-Left
        // new Field(x + 1, y - 1, false, false, false), // Diagonal Up-Right
        // new Field(x - 1, y + 1, false, false, false), // Diagonal Down-Left
        // new Field(x + 1, y + 1, false, false, false)  // Diagonal Down-Right
    };

    // Copy the layout from grid to set IsWall and IsMonsterSpawner.
    foreach (var field in possibleFields) {
        int fieldX = field.X;
        int fieldY = field.Y;

        if (fieldX >= 0 && fieldX <= maxX && fieldY >= 0 && fieldY <= maxY) {
            field.wall = grid[fieldX][fieldY].getwall;
        }
    }

    for (int i = 0; i < possibleFields.Count; i++) {
        possibleFields[i].SetDistance(targetField.X, targetField.Y);
        possibleFields[i].Parent = currentField;
    }

    return possibleFields
        .Where(field => field.X >= 0 && field.X <= maxX)
        .Where(field => field.Y >= 0 && field.Y <= maxY)
        .Where(field => !field.wall)
        .ToList();
}



        class Tile {
            public int X { get; set; }
            public int Y { get; set; }
            public int Cost { get; set; }
            public int Distance { get; set; }
            public int CostDistance => Cost + Distance;
            public Tile Parent { get; set; }

    }

}
