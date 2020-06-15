using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Node
{
	public int x;
	public int y;
	public Node next;
	public Node prev;
	
	public Node(int x,int y)
	{
		this.x = x;
		this.y = y;
	}
}

public class list
{
	public int size;
	public Node head;
	public Node tail;
	
	public list()
	{
		this.size = 0;
		this.head = new Node(0,0);
		this.tail = new Node(0,0);
	}
	
	public void addNode(int x,int y)
	{
		Node temp = this.head;
		int counter = 0;
		while ( counter < size )
		{
			temp = temp.next;
			counter++;
		}
		this.tail.prev = temp.next = new Node(x,y);
		
		this.size++;
	}
	
	public Node deleteFirst()
	{
		Node temp = this.head.next;
		
		this.head.next = this.head.next.next;
		
		this.size--;
		
		return temp;
	}
	
	public Node deleteLast()
	{
		Node temp = this.tail.prev;
		
		this.tail.prev = this.tail.prev.prev;
		
		this.size--;
		
		return temp;
	}
	
	
}

public class Control : MonoBehaviour {


	public GUISkin theSkin;
	public int startX = 400;//startX += 25
	public int startY = 200;//startY += 40
	public int lengthX = 15;
	public int lengthY = 30;
	
	// lengthX = 25;
	// lengthY = 40;
	
	public Text length;
	
	
	bool up = false;
	bool down = false;
	bool right = false;
	bool left = false;
	
	list snake;
	
	bool start = false;
	
	int appleX;
	int appleY;
	
	bool snakeCollision = false;
	
	bool borderCollision = false;
	
	bool pause = false;
	
	int speed = 9;
	int Nitro = 19;
	
	bool checkSnakeCollision(int x,int y)
	{
		Node temp = snake.head;
		int counter = 0;
		while( counter < snake.size )
		{
			temp = temp.next;
			
			if( temp.x == x && temp.y == y)
			{
				return true;
			}
			
			counter++;
		}
		
		return false;
	}
	
	bool checkBorderCollision(int x,int y)
	{
		//40 --> top wall ( roof )
		//520 --> down wall ( ceil )
		//25 -- > left wall
		//500*2-25 --> right wall
		
		
		// lengthX = 25;
		// lengthY = 40;
		if( y <= lengthY || y >= 640 || x <= lengthX || x >= 500*2-lengthX)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	bool checkAppleIsInSnakeOrNot(int x,int y)
	{
		Node temp = snake.head;
		int counter = 0;
		while( counter < snake.size )
		{
			temp = temp.next;
			
			if( temp.x == x && temp.y == y)
			{
				return true;
			}
			
			counter++;
		}
		
		return false;
	}
	
	void respawnApple()
	{
		appleX = Random.Range(4, 35);
		appleY = Random.Range(4, 15);
		
		//startX += 25;
	    //startY += 40;
		
		// lengthX = 25;
		// lengthY = 40;
		
		appleX *= lengthX;
		appleY *= lengthY;
		
		bool check = checkAppleIsInSnakeOrNot(appleX,appleY);
		
		if( check == true )
			respawnApple();
	}
	
	void makeAllFalse()
	{
		up = down = right = left = false;
	}
	
	public void exit()
	{
		Application.Quit();
	}
	
	void Start () {
		snake = new list();
		
		
		//startX += 25;
	    //startY += 40;
		
		
		// lengthX = 25;
		// lengthY = 40;
		
		snake.addNode(startX-(lengthX*2),startY);
		snake.addNode(startX-(lengthX),startY);
		snake.addNode(startX,startY);
		//myStyle.normal.textColor = Color.black;
		//Application.targetFrameRate = 3;
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = speed;
		
		respawnApple();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey(KeyCode.W))
        {
			if( up == true )
			{
				Application.targetFrameRate = Nitro-5;
			}
			
			if( down == false )
			{
				makeAllFalse();
				up = true;
			}
			
			if(pause == true)
				pause = false;
			
			
			start = true;
        } else if (Input.GetKey(KeyCode.S))
        {
			if( down == true )
			{
				Application.targetFrameRate = Nitro-5;
			}
			
			if( up == false )
			{
				makeAllFalse();
				down = true;				
			}
			
			if(pause == true)
				pause = false;

			start = true;
        } else if (Input.GetKey(KeyCode.A))
        {
			if( left == true )
			{
				Application.targetFrameRate = Nitro;
			}
			
			if( right == false && start == true)
			{
				makeAllFalse();
				left = true;				
			}
			
			if(pause == true)
				pause = false;
			
			//start = true;
        } else if (Input.GetKey(KeyCode.D))
        {
			if( right == true )
			{
				Application.targetFrameRate = Nitro;
			}
			
			if( left == false )
			{
				makeAllFalse();
				right = true;				
			}
			
			if(pause == true)
				pause = false;

			start = true;
        } else if (Input.GetKey(KeyCode.Space))
        {
			pause = !pause;
		} else if (Input.GetKey(KeyCode.Escape))
        {
			
			snakeCollision = false;
			borderCollision = false;
			
			snake = new list();
			
			startX = 400;
			startY = 200;
			
			
			// lengthX = 25;
			// lengthY = 40;
			snake.addNode(startX-(lengthX*2),startY);
			snake.addNode(startX-(lengthX),startY);
			snake.addNode(startX,startY);
			
			length.text = snake.size + "";
			
			respawnApple();
			
			makeAllFalse();
			
			start = false;
        }
		else
		{
			Application.targetFrameRate = speed;
		}
		
		//startX += 25;
	    //startY += 40;
			
		
		
		
		
		if(start == true && snakeCollision == false && pause == false && borderCollision == false)
		{
			
			length.text = snake.size + "";
			
			if( up == true )
			{
				startY -= lengthY;
			} else if( down == true )
			{
				startY += lengthY;
			} else if( left == true )
			{
				startX -= lengthX;
			} else if( right == true )
			{
				startX += lengthX;
			}
			
			
			
			snakeCollision = checkSnakeCollision(startX,startY);
			borderCollision = checkBorderCollision(startX,startY);
			
			if( startX == appleX && startY == appleY )
			{
				print("eat apple");
				respawnApple();
			}
			else 
			{
				snake.deleteFirst();
			}
			
			snake.addNode(startX,startY);
		}
	}
	
	void OnGUI()
    {
		GUI.skin = theSkin;
		GUI.skin.label.fontSize = 25;
		
		GUI.contentColor = Color.black;
		
		// lengthX = 25;
		// lengthY = 40;
		
		for(int i=lengthX;i<(500*2)-lengthX;i+=lengthX)
			GUI.Label(new Rect(i, lengthY, lengthX, lengthY), "#");
		
		
		for(int i=lengthX;i<(500*2);i+=lengthX)
			GUI.Label(new Rect(i, 640, lengthX, lengthY), "#");
		
		for(int i=lengthY;i<620;i+=lengthY)
			GUI.Label(new Rect(lengthX, i, lengthX, lengthY), "#");
		
		
		for(int i=lengthY;i<620;i+=lengthY)
			GUI.Label(new Rect(500*2-lengthX, i, lengthX, lengthY), "#");
		
		
		
		GUI.contentColor = Color.black;
		
		Node temp = snake.head;
		int counter = 0;
		while( counter < snake.size )
		{
			temp = temp.next;
			
			if( counter == snake.size-1 )
			{
				GUI.contentColor = Color.red;
			}
			else
			{
				GUI.contentColor = Color.black;
			}
			
			
			GUI.Label(new Rect(temp.x, temp.y, lengthX, lengthY), "0");
			
			counter++;
		}
		//print("headX : " + snake.head.next.x + "   headY : " + snake.head.next.y);
		
		GUI.contentColor = Color.black;
		GUI.Label(new Rect(appleX, appleY, lengthX, lengthY), "#");
		//print("appleX : " + appleX + "   appleY : " + appleY);
		
		//if ( GUI.Button(new Rect(Screen.width/2-50, 10, 100, 30), "Exit") )
        //    Application.Quit();
		
		
		//GUI.Label(new Rect(440, 600, 300, 200), "length : " + snake.size);
		
		//startX += 25;
	    //startY += 40;
		
		
		
    }
}
