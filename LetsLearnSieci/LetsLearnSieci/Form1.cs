using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LetsLearnSieci
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			rangeLabel.Text += $" (0 - {QuestionRandomizer.QuestionsCount})"; 
			questionLabel.Text = Program.qAndA.Key;

		}

		private void button1_Click(object sender, EventArgs e)
		{
			answerLabel.Text = Program.qAndA.Value;
		}

		private void answerLabel_Click(object sender, EventArgs e)
		{

		}

		private void button1_Click_1(object sender, EventArgs e)
		{
			setNextQuestion();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			QuestionRandomizer.RemoveQuestion(Program.qAndA.Key);
			setNextQuestion();
		}
		private void setNextQuestion()
		{
			Program.qAndA = QuestionRandomizer.ChooseQuestion();
			questionLabel.Text = Program.qAndA.Key;
			answerLabel.Text = string.Empty;
		}

		private void textBox2_TextChanged(object sender, EventArgs e)
		{
			Int32.TryParse(minRange.Text, out int min);
			QuestionRandomizer.Min = min;
		}

		private void maxRange_TextChanged(object sender, EventArgs e)
		{
			Int32.TryParse(maxRange.Text, out int max);
			QuestionRandomizer.Max = max;
		}
	}
}
