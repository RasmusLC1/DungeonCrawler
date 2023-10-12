using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.CompilerServices;
using ZombieGame.Entities;

namespace ZombieGame.Levels { 
    public class LevelCreator {
        private Movement movement = new Movement();
        private List<char> signs = new List<char>();
        private List<string> image = new List<string>();
        private MapConverter mapdata = new MapConverter(); 
        private List<List<Field>> fields = new List<List<Field>>();
        private List<Field> fieldsRow;
        private Player player;
        private int row = 0;
        private int col = 0;
        public List<List<Field>> getfields{
            get {return fields;}
        }
        public Player getPlayer{
            get {return player;}
        }
        public int getrow{
            get {return row;}
        }
        public int getcol{
            get {return col;}
        }
        public void LevelBuilder(string value){
            mapdata.MapDataSetup(value);
            fieldsCreator();
            for (int i = 1; i < mapdata.getlevel.Count-1; i++){
                for (int j = 0; j < mapdata.getlevel[i].Length; j++){
                    if (mapdata.getlevel[i][j] == '|'){
                        for (int k = 0; k < 5; k++){
                            fields[j*5][(i-1)*5+k].updateWall(true);
                        }
                        if (j < mapdata.getlevel[i].Length-1){
                            //Fill in space between | and -
                            if (mapdata.getlevel[i][j+1] == '-'){
                                for (int k = 1; k < 5; k++){
                                    fields[j*5+k][(i-1)*5].updateWall(true);   
                                }
                            }
                        }
                    } else if (mapdata.getlevel[i][j] == '-'){
                        for (int k = 0; k < 5; k++){
                            fields[j*5+k][(i-1)*5].updateWall(true);
                            
                        }
                    } else if (mapdata.getlevel[i][j] == 'M'){
                        if (i < mapdata.getlevel.Count-2){
                            fields[j*5][i*5].updateMonsterSpawner(true);
                        }
                    } else if (mapdata.getlevel[i][j] == 'T'){
                        if (i < mapdata.getlevel.Count-2){
                            fields[j*5][i*5].updateTreasure(true);
                        }
                    }
                }
            }
            player = new Player(150, 150, 10, 10, 5, 5, 0, 0);

        }
        public void fieldsCreator(){
            for (int i = 0; i < movement.getWidth; i+=10){
                col = 0;
                fieldsRow = new List<Field>();
                for (int j = 0; j < movement.getHeight; j+=10){
                    fieldsRow.Add(new Field(i, j, false, false, false, false, false));
                    col++;
                }
                fields.Add(fieldsRow);
                row++;
            }
        }
        public void ClearLevel() {
            signs.Clear();
            image.Clear();
        }
    }
}