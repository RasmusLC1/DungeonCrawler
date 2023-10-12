using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using ZombieGame.Entities;
using ZombieGame.Levels;
public class HitDetection{
    private double difPlayerZomb;
    private double difZombBullet;
    private int playerHealth = 5;
    private Stopwatch biteTimer = new Stopwatch();
    bool starttimer = true;
    private Coordinate coordinate = new Coordinate();
    private List<List<Field>> fields;
    // private ShortestPathFinder pathfind = new ShortestPathFinder();
    public void BulletHitZombie(ZombieSpawner zombiespawner, Shooting shooting) {
    List<int> zombiesToRemove = new List<int>();
    List<int> shotsToRemove = new List<int>();

    for (int i = 0; i < zombiespawner.getzombies.Count; i++)
    {
        for (int j = 0; j < shooting.shots.Count; j++){
            double difZombBullet = coordinate.DistanceTo(zombiespawner.getzombies[i].getX,
             shooting.shots[j].getX, zombiespawner.getzombies[i].getY, shooting.shots[j].getY);
            if (difZombBullet < 12 && difZombBullet > -10){
                    zombiespawner.getzombies[i].Hit();
                    shooting.shots.RemoveAt(j);
            }
            if (zombiespawner.getzombies[i].gethitPoints == 0){
                zombiespawner.RemoveZombie(zombiespawner.getzombies, i);
                i--;
                break;
            }
        }
    }


}

    public void ZombieHitPlayer (Player player, ZombieSpawner zombiespawner){
        if (starttimer){
            biteTimer.Start();
        }
        foreach (var zombie in zombiespawner.getzombies) {
                difPlayerZomb = coordinate.DistanceTo(zombie.getX,
                    player.getX, zombie.getY, player.getY);

                if (difPlayerZomb < 10 && difZombBullet > -10 &&
                        biteTimer.ElapsedMilliseconds > 1000){
                    biteTimer.Restart();
                    playerHealth--;
                    if (playerHealth == 0){
                        System.Console.WriteLine("Game over");
                    }
                }
        }
    }
    public void FieldDetection(LevelCreator level, Player player,
                ZombieSpawner zombiespawner, Shooting shooting, KeyPress keypress){
        fields = level.getfields;
        for (int i = 0; i < fields.Count; i++){
            for (int j = 0; j < fields[i].Count; j++) {
                Field field = fields[i][j];
                if (field.getwall){
                    // if (coordinate.DistanceTo(player.getX, field.getX, player.getY, field.getY) < 15){
                    //     if (keypress.getwPressed){
                    //         player.updateYpos(player.getY + 5);
                    //     } else if (keypress.getsPressed){
                    //         player.updateYpos(player.getY - 5);
                    //     } else if (keypress.getaPressed){
                    //         player.updateXpos(player.getX + 5);
                    //     } else if (keypress.getdPressed){
                    //         player.updateXpos(player.getX - 5);
                    //     }
                    // }
                    for (int n = 0; n < shooting.shots.Count; n++){
                        if (coordinate.DistanceTo(shooting.shots[n].getX, field.getX,
                        shooting.shots[n].getY, field.getY) < 12){
                            shooting.shots.RemoveAt(n);
                            break;
                        }
                    }
                    // for (int n = 0; n < zombiespawner.getzombies.Count; n++){
                    //     Zombie zombie = zombiespawner.getzombies[n];
                    //     if (coordinate.DistanceTo(zombie.getX, field.getX, zombie.getY, field.getY) < 15){
                    //         System.Console.WriteLine("test");
                    //         if (fields[i-1][j-1].wall || fields[i+1][j-1].wall){
                    //             zombie.updateXpos(zombie.getX - zombie.getzombieSpeed);
                    //             zombie.updateYpos(zombie.getY + zombie.getzombieSpeed);
                    //         }
                    //         if (fields[i-1][j+1].wall || fields[i+1][j+1].wall){
                    //             zombie.updateXpos(zombie.getX + zombie.getzombieSpeed);
                    //             zombie.updateYpos(zombie.getY - zombie.getzombieSpeed);
                    //         }
                    //     }
                    // }
                }else if (field.gettreasure && coordinate.DistanceTo(player.getX, field.getX, player.getY, field.getY)< 15){
                        System.Console.WriteLine("treasure found!!!");
                        player.updateWeapoon("huntingrifle");
                        field.updateTreasure(false);

                }
            }
        }
    }

}




