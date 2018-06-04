using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOiTZO_linear_programming
{
	class Program
	{
		static void Main(string[] args)
		{
			DataReader.ReadDataFromFile();
			DataReader.Deserialize();
			Calculations calculations = new Calculations(DataReader.DeserializedData);
			calculations.FromLinearToDual();
			Result.CrossingPoints = calculations.CalculateCrossingPoints();
			calculations.CalculateValuesToPoints(Result.CrossingPoints);
			Result.LimitingLines = calculations.CalculateLimitingLines(Result.CrossingPoints);
			Result.LimitingPoints = calculations.CalculateLimitingPoints(Result.CrossingPoints, Result.LimitingLines);
			foreach (Point p in Result.CrossingPoints)
				Console.WriteLine(p);
			Console.WriteLine();
			//foreach (Point p in Result.LimitingPoints)
			//	Console.WriteLine(p);
			//Console.WriteLine();
			Console.WriteLine(Result.LimitingLines.Item1);
			Console.WriteLine(Result.LimitingLines.Item2);
			Result.LowestValue = calculations.FindTheLowestValue(Result.LimitingPoints);
			Tuple<double,double> solvedEquations = calculations.SolveSystemOfEquations(Result.LimitingLines);
			Result.Variables = calculations.CalculateFinalValues(solvedEquations, Result.LowestValue);
			Console.WriteLine(Result.ToString());
			Console.ReadKey();
		}
	}
	class Point
	{
		public double X { get; set; }
		public double Y { get; set; }
		public double Value { get; set; }
		public int FirstLineNumber { get; set; }
		public int SecondLineNumber { get; set; }
		public Point()
		{

		}
		public Point(double x, double y, int i, int j)
		{
			X = x;
			Y = y;
			FirstLineNumber = i;
			SecondLineNumber = j;
		}
		public override string ToString()
		{
			return string.Concat("x=", X, ",	y=", Y, ",	value=", Value, ",	lines: ",FirstLineNumber, ",	",SecondLineNumber);
		}
	}
	class Result
	{
		public static List<Point> CrossingPoints { get; set; } = new List<Point>();
		public static List<Point> LimitingPoints { get; set; } = new List<Point>();
		public static Point LowestValue { get; set; }
		public static List<double> Variables { get; set; } = new List<double>();
		public static Tuple<int, int> LimitingLines { get; set; }
		public new static string ToString()
		{
			String output = string.Empty;
			output += "Punkty ograniczające: \n";
			foreach (Point p in LimitingPoints)
			{
				output += p.ToString() + "\n";
			}
			output += "Punkt optimum:\n";
			output += LowestValue.ToString();
			output += "\n";
			output += "Wartość optimum: " + LowestValue.Value;
			output += "\nWartości zmiennych: \n";
			foreach (double d in Variables)
			{
				output += d.ToString() + "  ";
			}
			return output;
		}
	}
	class Calculations
	{
		LinearInequations linearInequation;
		DualInequations dualInequation;
		public Calculations(LinearInequations linIneq)
		{
			linearInequation = linIneq;
		}
		public void FromLinearToDual()
		{
			dualInequation = new DualInequations();
			dualInequation.InequtionsFators = new double[
							linearInequation.InequtionsFators.GetLength(1), 
							linearInequation.InequtionsFators.GetLength(0)];
			dualInequation.AimEquationFactors = new List<double>();
			dualInequation.LeftValues = new List<double>();
			for (int i = 0; i < linearInequation.InequtionsFators.GetLength(0); i++)
			{
				for (int j = 0; j < linearInequation.InequtionsFators.GetLength(1); j++)
				{
					dualInequation.InequtionsFators[j, i] = linearInequation.InequtionsFators[i, j];
				}
			}
			for (int k = 0; k < linearInequation.InequtionsFators.GetLength(1); k++) {
				dualInequation.LeftValues.Add(linearInequation.AimEquationFactors[k]);
			}

			dualInequation.AimEquationFactors.Add(linearInequation.FirstValue);
			dualInequation.AimEquationFactors.Add(linearInequation.SecondValue);
		}
		public List<Point> CalculateCrossingPoints()
		{
			List<Point> foundPoints = new List<Point>();
			for (int i = 0; i < dualInequation.InequtionsFators.GetLength(0); i++)
			{
				for (int j = 0; j < dualInequation.InequtionsFators.GetLength(0); j++)
				{
					double A1 = dualInequation.InequtionsFators[i, 0];
					double A2 = dualInequation.InequtionsFators[i, 1];
					double B1 = dualInequation.InequtionsFators[j, 0];
					double B2 = dualInequation.InequtionsFators[j, 1];
					double delta = A1 * B2 - A2 * B1;
					if (delta == 0) continue;

					double C1 = dualInequation.LeftValues[i];
					double C2 = dualInequation.LeftValues[j];

					double x = (B2 * C1 - A2 * C2) / delta;
					double y = (A1 * C2 - B1 * C1) / delta;
					
					if (x < 0 || y < 0) continue;
					foundPoints.Add(new Point(x, y, i, j));
				}
			}
			for (int i = 0; i < dualInequation.InequtionsFators.GetLength(0); i++)
			{
				double A1 = dualInequation.InequtionsFators[i, 0];
				double A2 = dualInequation.InequtionsFators[i, 1];
				double B1 = 0;
				double B2 = 1;
				double delta = A1 * B2 - A2 * B1;
				if (delta == 0) continue;

				double C1 = dualInequation.LeftValues[i];
				double C2 = 0;

				double x = (B2 * C1 - A2 * C2) / delta;
				double y = (A1 * C2 - B1 * C1) / delta;
				
				if (x < 0 || y < 0) continue;
				foundPoints.Add(new Point(x, y, i, -1));
			}
			for (int i = 0; i < dualInequation.InequtionsFators.GetLength(0); i++)
			{
				double A1 = dualInequation.InequtionsFators[i, 0];
				double A2 = dualInequation.InequtionsFators[i, 1];
				double B1 = 1;
				double B2 = 0;
				double delta = A1 * B2 - A2 * B1;
				if (delta == 0) continue;

				double C1 = dualInequation.LeftValues[i];
				double C2 = 0;

				double x = (B2 * C1 - A2 * C2) / delta;
				double y = (A1 * C2 - B1 * C1) / delta;
				
				if (x < 0 || y < 0) continue;
				foundPoints.Add(new Point(x, y, i, -1));
			}
			return foundPoints;
		}
		public Tuple<int,int> CalculateLimitingLines(List<Point> crossingPoints)
		{
			List<Point> pointsOnXAxe = new List<Point>();
			List<Point> pointsOnYAxe = new List<Point>();
			foreach(Point p in crossingPoints)
			{
				if (p.X == 0 && p.SecondLineNumber == -1)
					pointsOnXAxe.Add(p);
				if (p.Y == 0 && p.SecondLineNumber == -1)
					pointsOnYAxe.Add(p);
			}
			Point limitingXPoint = new Point();
			limitingXPoint.X = double.MinValue;
			foreach (Point p in pointsOnYAxe)
			{
				if (p.X > limitingXPoint.X)
					limitingXPoint = p;
			}
			Point limitingYPoint = new Point();
			limitingYPoint.Y = double.MinValue;
			foreach (Point p in pointsOnXAxe)
			{
				if (p.Y > limitingYPoint.Y)
					limitingYPoint = p;
			}
			Tuple<int, int> limitingLines = new Tuple<int, int>(limitingXPoint.FirstLineNumber,limitingYPoint.FirstLineNumber);
			return limitingLines;
		} 
		public List<Point> CalculateLimitingPoints(List<Point> points, Tuple<int,int> lines)
		{
			List<Point> limitingPoints = new List<Point>();
			foreach(Point p in points)
			{
				if (p.FirstLineNumber == lines.Item1 && p.Y == 0)
					limitingPoints.Add(p);
				else if (p.FirstLineNumber == lines.Item2 && p.X == 0)
					limitingPoints.Add(p);
				else if (p.FirstLineNumber == lines.Item1 && p.SecondLineNumber == lines.Item2)
					limitingPoints.Add(p);
			}
			return limitingPoints;
		}
		public void CalculateValuesToPoints(List<Point> points)
		{
			foreach(Point p in points)
			{
				p.Value = dualInequation.AimEquationFactors[0] * p.X + dualInequation.AimEquationFactors[1] * p.Y;
			}
		}
		public Point FindTheLowestValue(List<Point> points)
		{
			Point lowestValuePoint = points[0];
			foreach(Point p in points)
			{
				if (p.Value < lowestValuePoint.Value)

					lowestValuePoint = p;
			}
			return lowestValuePoint;
		}
		public Tuple<double,double> SolveSystemOfEquations(Tuple<int,int> limitingLines)
		{
			int numberOfFirstVariable = limitingLines.Item1;
			int numberOfSecondVariable = limitingLines.Item2;
			if(numberOfFirstVariable<numberOfSecondVariable)
			{
				numberOfFirstVariable = limitingLines.Item2;
				numberOfSecondVariable = limitingLines.Item1;
			}

			double A1 = linearInequation.InequtionsFators[0, numberOfFirstVariable];
			double A2 = linearInequation.InequtionsFators[0, numberOfSecondVariable];
			double B1 = linearInequation.InequtionsFators[1, numberOfFirstVariable];
			double B2 = linearInequation.InequtionsFators[1, numberOfSecondVariable];

			double C1 = linearInequation.FirstValue;
			double C2 = linearInequation.SecondValue;

			double mainDeterminant = A1 * B2 - A2 * B1;
			if (mainDeterminant == 0) throw new Exception("No solution");

			double determinantX1 = C1 * B2 - A2 * C2;
			double determinantX2 = A1 * C2 - C1 * B1;

			double firstVariable = determinantX1 / mainDeterminant;
			double secondVariable = determinantX2 / mainDeterminant;

			Tuple<double, double> outputValues = new Tuple<double, double>(firstVariable, secondVariable);
			return outputValues;
		}
		public List<double> CalculateFinalValues(Tuple<double,double> solvedEquations, Point greatestValue)
		{
			List<double> outputValues = new List<double>();
			int firstNonZeroVariable = greatestValue.FirstLineNumber;
			int secondNonZeroVariable = greatestValue.SecondLineNumber;
			for (int i = 0; i<linearInequation.InequtionsFators.GetLength(1); i++)
			{
				if (i == firstNonZeroVariable)
					outputValues.Add(solvedEquations.Item1);
				else if (i == secondNonZeroVariable)
					outputValues.Add(solvedEquations.Item2);
				else
					outputValues.Add(0);
			}
			return outputValues;
		}
	}
	class DualInequations
	{
		public double[,] InequtionsFators { get; set; }
		public List<double> LeftValues { get; set; }
		public List<double> AimEquationFactors { get; set; }
	}
	class LinearInequations
	{
		public double[,] InequtionsFators { get; set; }
		public double FirstValue { get; set; }
		public double SecondValue { get; set; }
		public List<double> AimEquationFactors { get; set; }
	}
	class DataReader
	{
		static string fileToReadPath = @"../../dane.txt";
		static string serializedData;
		public static LinearInequations DeserializedData { get; set; }
		public static void ReadDataFromFile()
		{
			serializedData = System.IO.File.ReadAllText(fileToReadPath);
		}
		public static void Deserialize()
		{
			DeserializedData = JsonConvert.DeserializeObject<LinearInequations>(serializedData);
		}
	}
}
