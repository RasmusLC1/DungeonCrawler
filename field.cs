using System;

public class Field{
    private const int size = 10;
    private int X;
    private int Y;
    private bool wall;
    private bool monsterSpawner;
    private bool treasure;
    private bool player;
    private bool enemy;
    public int Cost { get; set; }
	private int distance;
	public int CostDistance => Cost + distance;
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
    public int getdistance {
        get{return distance;}
    }
    public int getsize {
        get{return size;}
    }
    public bool getwall {
        get{return wall;}
    }
    public bool gettreasure {
        get{return treasure;}
    }
    public bool getmonsterSpawner {
        get{return monsterSpawner;}
    }
    public bool getplayer {
        get{return player;}
    }
    public bool getenemy {
        get{return enemy;}
    }
    public Field(int x, int y, bool Wall, bool MonsterSpawner, bool Treasure,
        bool Player, bool Enemy){
        X = x;
        Y = y;
        wall = Wall;
        monsterSpawner = MonsterSpawner;
        treasure = Treasure;
        player = Player;
        enemy = Enemy;
    }
    public void UpdateX(int newX){
        X = newX;
    }
    public void UpdateY(int newY){
        Y = newY;
    }
    public void SetDistance(int targetX, int targetY) {
		distance = Math.Abs(targetX - X) + Math.Abs(targetY - Y);
	}
    internal void updateTreasure(bool status){
        treasure = status;
    }
    internal void updateplayer(bool status){
        player = status;
    }
    internal void updateEnemy(bool status){
        enemy = status;
    }
    internal void updateWall(bool status){
        wall = status;
    }
    internal void updateMonsterSpawner(bool status){
        monsterSpawner = status;
    }
}