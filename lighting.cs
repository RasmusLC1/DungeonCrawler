using System;
using System.Collections.Generic;

public class Lighting {
    int fieldRow;
    int fieldColumn;
    // public void ActivateTorch(List<List<Field>> levelFields, Field torchField) {
    //     for (int i = -5; i < 5; i++) {
    //         for (int j = -5; j < 5; j++) {
    //             int xOffset = Math.Abs(torchField.getX/10 + i);
    //             int yOffset = Math.Abs(torchField.getY/10 + j);
    //             levelFields[xOffset][yOffset].SetDistance(torchField.getX, torchField.getY);
    //             levelFields[xOffset][yOffset].updateLightLevel(levelFields[xOffset][yOffset].getdistance*3);
    //             levelFields[xOffset][yOffset].updateInReachOfTorch(true);
    //         }
    //     }
        
        
    // }
    public void FieldOfViewTorch(List<List<Field>> levelFields, Field torchfield) {
        
        // Define the number of rays for FOV
        int numRays = 500;

        //Distance that the torch can see
        int distance = 100;
        double fovAngle = Math.PI; // Change this value to adjust the field of view

        // Calculate the angle increment between rays
        double angleIncrement = fovAngle / (numRays - 1);

        for (int i = 0; i < numRays; i++) {
            // Calculate the angle for the current ray by spreading the direction
            double angle = Math.Atan2(torchfield.getX + 5, torchfield.getY) + (i - (numRays - 1) / 2) * angleIncrement;

            // Initialize the ray's position at the player's position
            double rayXFront = torchfield.getX;
            double rayYFront = torchfield.getY;
            double rayXBack = torchfield.getX;
            double rayYBack = torchfield.getY;
            double rayDistance = 0;

            // Cast the front ray
            while (rayDistance < distance) {
                // Update the ray's position
                rayXFront += Math.Cos(angle);
                rayYFront += Math.Sin(angle);
                rayDistance += 1;
                fieldRow = (int)rayXFront / 10;
                fieldColumn =(int)rayYFront / 10;
                Field field = levelFields[fieldRow][fieldColumn];
                // Check if the ray hit a wall in the level
                if (field.getwall){
                    break; // Stop the ray when it hits a wall
                } 
            }
            levelFields[fieldRow][fieldColumn].updateTorchLight(distance - rayDistance);
            rayDistance = 0;
            // Cast the backwards rays
            while (rayDistance < distance) {
                rayXBack -= Math.Cos(angle);
                rayYBack -= Math.Sin(angle);
                rayDistance += 1; // Increment the ray's distance
                fieldRow = (int)rayXBack / 10;
                fieldColumn =(int)rayYBack / 10;
                Field field = levelFields[fieldRow][fieldColumn];

                // Check if the ray hit a wall in the level
                if (field.getwall){
                    break; // Stop the ray when it hits a wall
                }
            }
            levelFields[fieldRow][fieldColumn].updateTorchLight(distance - rayDistance);
        }
    }
}
