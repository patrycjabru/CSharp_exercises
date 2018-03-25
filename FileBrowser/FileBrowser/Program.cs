using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileBrowser
{
	class Program
	{
		static void Main(string[] args)
		{
			Browser browser = new Browser();
			browser.ListFilesInCurrentDirectory();
			while (true)
			{
				Console.WriteLine(browser.Path);
				Console.Write(browser.Prompt);
				string input = WaitForInput(browser);
				HandleInput(input, browser);
			}
		}
		static void HandleInput(string input, Browser browser)
		{
			Regex cdRegex = new Regex("cd .+");
			if (input == "ls")
				browser.ListFilesInCurrentDirectory();
			else if (input == "cd ..")
				browser.GoUpInDirectoryTree();
			else if (cdRegex.IsMatch(input))
			{
				string newPath = input.Remove(0, 3);
				if (Directory.Exists(browser.Path + @"\" + newPath))
					browser.ChangeCurrentDirectory(newPath);
				else
					Console.WriteLine("Invalid path");
			}
		}
		static string WaitForInput(Browser browser)
		{
			char character = ' ';
			string input = "";
			while (true)
			{
				character = Console.ReadKey(true).KeyChar;
				if (character == 9) //tab
				{
					input = browser.AutoCompletion(input);
					continue;
				}
				if (character == 13) //enter
				{
					return input;
				}
				if (character == 8) //backspace
				{
					if (input.Length > 0)
					{
						input = input.Remove(input.Length - 1, 1);
						Console.Write("\b \b");
					}
					continue;
				}
				input += character;
				Console.Write(character);
			}
		}
	}
	class Browser
	{
		public string Prompt { set; get; } = ">";
		public string Path { set; get; } = @"D:\";
		public void GoUpInDirectoryTree()
		{
			int index = Path.LastIndexOf('\\');
			if (index != -1 && Path.Length>3)
				Path = Path.Remove(index, Path.Length - index);
			index = Path.LastIndexOf('\\');
			if (index != -1 && Path.Length > 3)
			{
				Path = Path.Remove(index, Path.Length - index);
				Path += @"\";
			}
			Console.WriteLine();
		}
		public void ChangeCurrentDirectory(string newPath)
		{
			Path = Path + newPath + @"\";
			Console.WriteLine();
		}
		public void ListFilesInCurrentDirectory()
		{
			string[] listOfFiles = Directory.GetFileSystemEntries(Path);
			foreach (string s in listOfFiles)
			{
				int index = Path.LastIndexOf('\\');
				Console.WriteLine(s.Remove(0, index+1));
			}
		}
		public string AutoCompletion(string input)
		{
			Regex cdRegex = new Regex("cd .+");
			if (!cdRegex.IsMatch(input))
				return input;
			string[] listOfFiles = Directory.GetFileSystemEntries(Path);
			string newPath = input.Remove(0, 3);
			Regex pathRegex = new Regex(newPath + ".*");
			foreach (string file in listOfFiles)
			{
				string fileName = file.Remove(0, Path.Length);
				if (pathRegex.IsMatch(fileName))
				{
					Console.Write(fileName.Remove(0,newPath.Length)); //do poprawienia!
					return "cd "+fileName;
				}
			}
			return input;
		}
	}

}
