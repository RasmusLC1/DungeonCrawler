using System;

public class Field{
    private const int size = 10;
    public int X;
    public int Y;
    public bool wall;
    public bool monsterSpawner;
    public bool treasure;
    public int Cost { get; set; }
	public int Distance { get; set; }
	public int CostDistance => Cost + Distance;
    public int cost = 0;
	public Field Parent { get; set; }

	//The distance is essentially the estimated distance, ignoring walls to our target. 
	//So how many tiles left and right, up and down, ignoring walls, to get there. 
	
    public int getX {
        get{return X;}
    }
    public int getY {
        get{return Y;}
    }
    public int getsize {
        get{return size;}
    }
    public bool getwall {
        get{return wall;}
    }
    public bool getmosterSpawner {
        get{return monsterSpawner;}
    }
    public Field(int x, int y, bool Wall, bool MonsterSpawner, bool Treasure){
        X = x;
        Y = y;
        wall = Wall;
        monsterSpawner = MonsterSpawner;
        treasure = Treasure;
    }
    public void SetDistance(int targetX, int targetY) {
		this.Distance = Math.Abs(targetX - X) + Math.Abs(targetY - Y);
	}
}