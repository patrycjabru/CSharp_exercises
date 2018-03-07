using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiczbyPierwsze
{
	class Program
	{
		static void Main(string[] args)
		{
			int range = 1000;
			List<int> numbers = new List<int>();
			for (int i=0;i<range;i++)
			{
				numbers.Add(i + 1);
			}
			for (int i = 1; i < numbers.Count; i++)
			{
				int currentNumberInList = numbers[i];
				for (int j=2;j<numbers.Count; j++)
				{

					int notPrimeNumber = currentNumberInList*j;
					numbers.Remove(notPrimeNumber);
				}
			}
			foreach (var n in numbers) {
				Console.Write(n + "  ");
			}
			Console.ReadKey();
		}
	}
}
