using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace CurrencyConverter
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.GetLength(0)!=3)
			{
				Console.WriteLine("Niewłaściwa ilość argumentów");
				Console.ReadKey();
				Environment.Exit(1);
			}
			CurrencyDataManagment first = new CurrencyDataManagment(args[0]);
			CurrencyDataManagment second = new CurrencyDataManagment(args[2]);
			double amountOfMoneyToConvert = 0;
			try
			{
				amountOfMoneyToConvert = Convert.ToDouble(args[1]);
			} catch(FormatException)
			{
				Console.WriteLine("Zły format kwoty");
				Console.ReadKey();
				Environment.Exit(2);
			}
			Console.WriteLine("Kurs waluty " + first.CurrencySymbol + " wynosi: " + first.CurrencyValue);
			Console.WriteLine("Kurs waluty " + second.CurrencySymbol + " wynosi: " + second.CurrencyValue);
			Console.WriteLine(args[1] + " " + first.CurrencySymbol + " = " + Converter.Convert(amountOfMoneyToConvert, first, second) + " " + second.CurrencySymbol);
			Console.ReadKey();
		}
	}
	class Converter
	{
		public static double Convert(double amountOfMoney, CurrencyDataManagment primary, CurrencyDataManagment secondary)
		{
			return amountOfMoney * primary.CurrencyValue / secondary.CurrencyValue;
		}
	}
	class CurrencyDataManagment
	{
		public string CurrencySymbol { get; }
		public double CurrencyValue { get; private set; }
		string filePath = "D:\\PROJEKTY\\C#\\CurrencyConverter\\ExchangeRatesHistory.txt";
		public CurrencyDataManagment(string symbol)
		{
			CurrencySymbol = symbol;
			if (CurrencySymbol == "pln")
				CurrencyValue = 1;
			else
				GetData();
		}
		void GetData()
		{
			if (!GetDataFromFile())
				GetDataFromServer();
		}
		void GetDataFromServer() 
		{
			string url = "http://api.nbp.pl/api/exchangerates/rates/a/" + CurrencySymbol;
			string currencyJson = null;
			try
			{
				currencyJson = new WebClient().DownloadString(url);
			} catch (System.Net.WebException)
			{
				Console.WriteLine("Zły format waluty");
				Console.ReadKey();
				Environment.Exit(404);
			}
			CurrencyJson deserializedCurrency = JsonConvert.DeserializeObject<CurrencyJson>(currencyJson);
			CurrencyValue = deserializedCurrency.rates[0].mid;
			SaveToFile();
		}
		bool GetDataFromFile()
		{
			string[] lines = System.IO.File.ReadAllLines(filePath);
			string date = DateTime.Now.ToString("dd-MM-yyyy");
			Regex linePattern = new Regex(CurrencySymbol + ";" + date + ".*");
			double value;
			foreach (string line in lines)
			{
				if (linePattern.IsMatch(line))
				{
					value = Convert.ToDouble(line.Remove(0, 15));
					CurrencyValue = value;
					return true;
				}
			}
			return false;
		} 
		void SaveToFile()
		{
			string date = DateTime.Now.ToString("dd-MM-yyyy");
			string line = CurrencySymbol + ";" + date + ";" + CurrencyValue;
			using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, true))
			{
				file.WriteLine(line);
			}
		}
	}
	class CurrencyJson
	{

		public class Rates
		{
			public string no { get; set; }
			public string effectiveDate { get; set; }
			public float mid { get; set; }
		}
		public string table { get; set; }
		public string currency { get; set; }
		public string code { get; set; }
		public Rates[] rates { get; set; } 

	}

}
