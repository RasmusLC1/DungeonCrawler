using System;

public class Field{
    private const int size = 10;
    private int X;
    private int Y;
    private bool wall = false;
    private bool monsterSpawner = false;
    private bool treasure = false;
    private bool player = false;
    private bool enemy = false;
    private bool torch = false;
    private double torchLight = 0;
    private int lightLevel = 0;
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
    public int getlightLevel {
        get{return lightLevel;}
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
    public bool gettorch {
        get{return torch;}
    }
    public double gettorchLight {
        get{return torchLight;}
    }
    public Field(int x, int y){
        X = x;
        Y = y;
    }
    public void UpdateX(int newX){
        X = newX;
    }
    public void UpdateY(int newY){
        Y = newY;
    }
    internal void updateLightLevel(int value){
        lightLevel = value;
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
    internal void updateTorch(bool status){
        torch = status;
    }
    internal void updateTorchLight(double value){
        torchLight = value;
    }
}