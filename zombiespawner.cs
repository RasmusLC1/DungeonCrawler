using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ZombieGame.Levels;
using ZombieGame.Entities;

public class ZombieSpawner {
    private List<Zombie> zombies = new List<Zombie>();
    private Random rnd = new Random();
    private Stopwatch spawnTimer = new Stopwatch();
    private int Difficulty = 0;
    private int spawnerLocations = 0;
    private int ranNum;
    private List<Tuple<int, int>> spawnerList = new List<Tuple<int, int>>();
    public List<Zombie> getzombies{
        get {return zombies;}
    }
    public void zombieSpawner() {
        if (zombies.Count == 0 || spawnTimer.ElapsedMilliseconds > 5000) {
            spawnTimer.Restart();
            for (int i = 0; i < rnd.Next(1, 4);) {
                ranNum = rnd.Next(0,spawnerLocations);
                int item1 = spawnerList[ranNum].Item1;
                int item2 = spawnerList[ranNum].Item2;
                int x = rnd.Next(item1, item1+10);
                int y = rnd.Next(item2, item2+10);
                int speed = rnd.Next(2, 3);
                int health = rnd.Next(1, 3);
                zombies.Add(new Zombie(x, y, 10, 10, health, speed));
                i++;
            }
        }
    }
    public void SpawnerList(List<List<Field>> fields){
        for (int i = 0; i < fields.Count-1; i++){
            for (int j = 0; j < fields[i].Count-1; j++){
                Field field = fields[i][j];
                if (field.getmonsterSpawner){
                    spawnerList.Add(Tuple.Create(field.getX, field.getY)); // Add the tuple to the list.
                    spawnerLocations++;
                }
            }
        }
    }

    public void RemoveZombie(List<Zombie> zombies, int i){
        zombies.RemoveAt(i);
    }
}
