using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LetsLearnSieci
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		
		public static KeyValuePair<string, string> qAndA;
		[STAThread]
		static void Main()
		{
			FileHandler file = new FileHandler();
			Dictionary<string, string> questionsAndAnswers = file.GetDataFromFile();
			QuestionRandomizer randomizer = new QuestionRandomizer(questionsAndAnswers);

			qAndA = QuestionRandomizer.ChooseQuestion();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
	class FileHandler {
		string filePath = @"..\..\DataBase\pytania.tsv";
		public Dictionary<string, string> GetDataFromFile()
		{
			string[] lines = System.IO.File.ReadAllLines(filePath);
			Dictionary<string, string> questionsAndAnswers = new Dictionary<string, string>();
			foreach (string singleLine in lines)
			{
				int separatorIndex = singleLine.IndexOf("	");
				if (separatorIndex < 10) continue;
				string question = singleLine.Substring(0, separatorIndex);
				string answer = singleLine.Substring(separatorIndex+1);
				if (questionsAndAnswers.ContainsKey(question)) continue;
				questionsAndAnswers.Add(question, answer);
			}
			return questionsAndAnswers;
		}
	}
	class QuestionRandomizer
	{
		static Dictionary<string, string> questionsAndAnswers;
		public static int QuestionsCount => questionsAndAnswers.Count;
		public static int Min { set; get; } = 0;
		public static int Max { set; get; }
		public QuestionRandomizer (Dictionary<string, string> qa)
		{
			questionsAndAnswers = qa;
			Max = questionsAndAnswers.Count;
		}
		public static KeyValuePair<string, string> ChooseQuestion()
		{
			Random rand = new Random();
			int randomValue = rand.Next(Min,Max);
			KeyValuePair<string, string> output = questionsAndAnswers.ElementAt(randomValue);
			return output;
		}
		public static void RemoveQuestion(string key)
		{
			questionsAndAnswers.Remove(key);
		}

	}

}
