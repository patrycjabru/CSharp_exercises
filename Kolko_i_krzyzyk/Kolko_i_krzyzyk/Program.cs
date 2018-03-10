using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolko_i_krzyzyk
{

	class Program
	{
		
		static void Main(string[] args)
		{
			Board board = new Board();
			Players players = new Players();
			bool progress = false;
			bool isVictory = false;
			while (!isVictory) {
				board.Display();
				int activePlayer = players.GetActivePlayer();
				while (!progress)
				{
					char input = Console.ReadKey().KeyChar;
					progress = board.HandleInput(input, activePlayer);
				}
				progress = false;
				isVictory = board.CheckVictory();
				players.ChangeActivePlayer();
			}
			Console.ReadKey();
		}
		
	}
}
class Board
{
	int[,] board;
	int numberOfMovesLeft = 9;
	public Board()
	{
		board = new int[3, 3];
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				board[i,j] = 0;
			}
		}
	}
	public void Display()
	{
		Console.WriteLine();
		for (int i=2;i>-1; i--)
		{
			for (int j=0;j<3;j++)
			{
				if (board[i, j] == 0)
					Console.Write("   ");
				else if (board[i, j] == 1)
					Console.Write(" O ");
				else
					Console.Write(" X ");
				Console.Write("|");
				if (j == 2)
					Console.WriteLine("");
			}
			Console.WriteLine("------------");
		}
		Console.WriteLine();
	}
	public bool HandleInput(char inputChar, int activePlayer)
	{
		int numberOfField = (int)Char.GetNumericValue(inputChar);
		numberOfField--;
		//Console.WriteLine("inputChar: " + inputChar);
		//Console.WriteLine("numberOfField: " + numberOfField);
		if (numberOfField < 0 || numberOfField > 8)
			return false;
		if (board[numberOfField / 3, numberOfField % 3] != 0)
			return false;
		if (activePlayer == 1)
			board[numberOfField / 3, numberOfField % 3] = 1;
		else
			board[numberOfField / 3, numberOfField % 3] = -1;
		numberOfMovesLeft--;
		return true;
	}
	public bool CheckVictory()
	{
		string firstPlayerWinsMessage = "Wygrywa gracz pierwszy";
		string seconPlayerWinsMessage = "Wygrywa gracz drugi";
		string tieMessage = "Remis";
		if (board[0,0]+board[0,1]+board[0,2]==3)
		{
			Console.WriteLine(firstPlayerWinsMessage);
			return true;
		}
		if (board[0, 0] + board[0, 1] + board[0, 2] == -3)
		{
			Console.WriteLine(seconPlayerWinsMessage);
			return true;
		}
		if (board[1, 0] + board[1, 1] + board[1, 2] == 3)
		{
			Console.WriteLine(firstPlayerWinsMessage);
			return true;
		}
		if (board[1, 0] + board[1, 1] + board[1, 2] == -3)
		{
			Console.WriteLine(seconPlayerWinsMessage);
			return true;
		}
		if (board[2, 0] + board[2, 1] + board[2, 2] == 3)
		{
			Console.WriteLine(firstPlayerWinsMessage);
			return true;
		}
		if (board[2, 0] + board[2, 1] + board[2, 2] == -3)
		{
			Console.WriteLine(seconPlayerWinsMessage);
			return true;
		}
		if (board[0, 0] + board[1, 0] + board[2, 0] == 3)
		{
			Console.WriteLine(firstPlayerWinsMessage);
			return true;
		}
		if (board[0, 0] + board[1, 0] + board[2, 0] == -3)
		{
			Console.WriteLine(seconPlayerWinsMessage);
			return true;
		}
		if (board[0, 1] + board[1, 1] + board[2, 1] == 3)
		{
			Console.WriteLine(firstPlayerWinsMessage);
			return true;
		}
		if (board[0, 1] + board[1, 1] + board[2, 1] == -3)
		{
			Console.WriteLine(seconPlayerWinsMessage);
			return true;
		}
		if (board[0, 2] + board[1, 2] + board[2, 2] == 3)
		{
			Console.WriteLine(firstPlayerWinsMessage);
			return true;
		}
		if (board[0, 2] + board[1, 2] + board[2, 2] == -3)
		{
			Console.WriteLine(seconPlayerWinsMessage);
			return true;
		}
		if (board[0, 0] + board[1, 1] + board[2, 2] == 3)
		{
			Console.WriteLine(firstPlayerWinsMessage);
			return true;
		}
		if (board[0, 0] + board[1, 1] + board[2, 2] == -3)
		{
			Console.WriteLine(seconPlayerWinsMessage);
			return true;
		}
		if (board[0, 2] + board[1, 1] + board[2, 0] == 3)
		{
			Console.WriteLine(firstPlayerWinsMessage);
			return true;
		}
		if (board[0, 2] + board[1, 1] + board[2, 0] == -3)
		{
			Console.WriteLine(seconPlayerWinsMessage);
			return true;
		}
		if (numberOfMovesLeft==0)
		{
			Console.WriteLine(tieMessage);
			return true;
		}
		return false;
	}
}
class Players
{
	int firstPlayerPoints;
	int secondPlayerPoints;
	int activePlayer;
	public Players()
	{
		firstPlayerPoints = 0;
		secondPlayerPoints = 0;
		activePlayer = 1;
		Console.WriteLine("Pierwszy gracz ma kółka,a drugi krzyżyki.");
	}
	public int GetActivePlayer()
	{
		return activePlayer;
	}
	public void ChangeActivePlayer()
	{
		if (activePlayer == 1)
			activePlayer = 2;
		else
			activePlayer = 1;
	}
}
