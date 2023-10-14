using System;
using System.CodeDom;
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
    public Tuple<double, double> Pathfinder(List<List<Field>> grid, Field start, Field finish) {
        //Find distance between zombie and player
        

        var activeFields = new List<Field>();
        activeFields.Add(start);

        var visitedFields = new List<Field>();
        List<Tuple<double, double>> path = new List<Tuple<double, double>>(); // Store the path coordinates.

        while (activeFields.Any()) {
            Field checkField = activeFields.OrderBy(f => f.CostDistance).First(); //Order fields from lowest to highest cost

            if (checkField.getX == finish.getX && checkField.getY == finish.getY) { // Path found; reconstruct and add coordinates to the path.
                
                Field field = checkField; 
                while (true) {
                    path.Add(Tuple.Create((double)field.getX, (double)field.getY)); // Add the coordinates to the path.
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
                if (visitedFields.Any(f => f.getX == walkableField.getX && f.getY == walkableField.getY))
                    continue;

                // Check if the field is already in the active list.
                if (activeFields.Any(f => f.getX == walkableField.getX && f.getY == walkableField.getY)) {
                    var existingField = activeFields.First(f => f.getX == walkableField.getX && f.getY == walkableField.getY);
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
        return Tuple.Create((double)start.getX, (double)start.getY); // Return an empty list if no path is found.
    }

    private List<Field> GetWalkableTiles(List<List<Field>> grid, Field currentField, Field targetField) {
    int maxX = grid.Count - 1;
    int maxY = grid[0].Count - 1;

    int x = currentField.getX;
    int y = currentField.getY;

    List<Field> possibleFields = new List<Field>() {
        new Field(x, y - 1),   // Up
        new Field(x, y + 1),   // Down
        new Field(x - 1, y),   // Left
        new Field(x + 1, y),   // Right
        // new Field(x - 1, y - 1), // Diagonal Up-Left
        // new Field(x + 1, y - 1), // Diagonal Up-Right
        // new Field(x - 1, y + 1), // Diagonal Down-Left
        // new Field(x + 1, y + 1)  // Diagonal Down-Right
    };

    // Copy the layout from grid to set wall and player.
    foreach (var field in possibleFields) {
        int fieldX = field.getX;
        int fieldY = field.getY;

        // if (fieldX >= 0 && fieldX <= maxX && fieldY >= 0 && fieldY <= maxY) {
            field.updateWall(grid[fieldX][fieldY].getwall);
            field.updateplayer(grid[fieldX][fieldY].getplayer);
        // }
    }

    for (int i = 0; i < possibleFields.Count; i++) {
        possibleFields[i].SetDistance(targetField.getX, targetField.getY);
        possibleFields[i].Parent = currentField;
    }

    return possibleFields
        .Where(field => field.getX >= 0 && field.getX <= maxX)
        .Where(field => field.getY >= 0 && field.getY <= maxY)
        .Where(field => !field.getwall)
        .Where(field => !field.getplayer)
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
