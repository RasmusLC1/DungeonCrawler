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
            for (int i = 0; i < mapdata.getlevel.Count; i++){
                for (int j = 0; j < mapdata.getlevel[i].Length; j++){
                    if (mapdata.getlevel[i][j] == 'W'){
                        fields[j][i].updateWall(true);
                    } else if (mapdata.getlevel[i][j] == 'L'){
                        fields[j][i].updateMonsterSpawner(true);
                    } else if (mapdata.getlevel[i][j] == 'T'){
                        fields[j][i].updateTreasure(true);
                    }
                }
            }
            InitialisePlayer();
            ClearLevel();
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
        public void InitialisePlayer(){
            player = new Player(150, 150, 10, 10, 5);
            player.updateDeltaX(Math.Cos(player.GetViewAngle)*player.getplayerSpeed);
            player.updateDeltaY(Math.Sin(player.GetViewAngle)*player.getplayerSpeed);
        }
        public void ClearLevel() {
            signs.Clear();
            image.Clear();
        }
    }
}