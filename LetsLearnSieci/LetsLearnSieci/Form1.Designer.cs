using System.Windows.Forms;

namespace LetsLearnSieci
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.title = new System.Windows.Forms.Label();
			this.questionLabel = new System.Windows.Forms.Label();
			this.showAnswerButton = new System.Windows.Forms.Button();
			this.answerLabel = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.maxRange = new System.Windows.Forms.TextBox();
			this.minRange = new System.Windows.Forms.TextBox();
			this.rangeLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// title
			// 
			this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.title.ForeColor = System.Drawing.SystemColors.WindowText;
			this.title.Location = new System.Drawing.Point(3, 112);
			this.title.Name = "title";
			this.title.Size = new System.Drawing.Size(650, 31);
			this.title.TabIndex = 0;
			this.title.Text = "PYTANIE";
			this.title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// questionLabel
			// 
			this.questionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.questionLabel.Location = new System.Drawing.Point(-2, 158);
			this.questionLabel.Name = "questionLabel";
			this.questionLabel.Size = new System.Drawing.Size(655, 113);
			this.questionLabel.TabIndex = 1;
			this.questionLabel.Text = "label1";
			this.questionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// showAnswerButton
			// 
			this.showAnswerButton.Location = new System.Drawing.Point(205, 274);
			this.showAnswerButton.Name = "showAnswerButton";
			this.showAnswerButton.Size = new System.Drawing.Size(231, 50);
			this.showAnswerButton.TabIndex = 2;
			this.showAnswerButton.Text = "Odpowiedź";
			this.showAnswerButton.UseVisualStyleBackColor = true;
			this.showAnswerButton.Click += new System.EventHandler(this.button1_Click);
			// 
			// answerLabel
			// 
			this.answerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.answerLabel.Location = new System.Drawing.Point(-2, 359);
			this.answerLabel.Name = "answerLabel";
			this.answerLabel.Size = new System.Drawing.Size(655, 39);
			this.answerLabel.TabIndex = 3;
			this.answerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.answerLabel.Click += new System.EventHandler(this.answerLabel_Click);
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.button1.Location = new System.Drawing.Point(10, 433);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(264, 43);
			this.button1.TabIndex = 4;
			this.button1.Text = "ŹLE";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// button2
			// 
			this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
			this.button2.Location = new System.Drawing.Point(376, 433);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(264, 43);
			this.button2.TabIndex = 5;
			this.button2.Text = "DOBRZE";
			this.button2.UseVisualStyleBackColor = false;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// maxRange
			// 
			this.maxRange.Location = new System.Drawing.Point(569, 12);
			this.maxRange.Name = "maxRange";
			this.maxRange.Size = new System.Drawing.Size(71, 20);
			this.maxRange.TabIndex = 6;
			this.maxRange.TextChanged += new System.EventHandler(this.maxRange_TextChanged);
			// 
			// minRange
			// 
			this.minRange.Location = new System.Drawing.Point(492, 12);
			this.minRange.Name = "minRange";
			this.minRange.Size = new System.Drawing.Size(71, 20);
			this.minRange.TabIndex = 7;
			this.minRange.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
			// 
			// rangeLabel
			// 
			this.rangeLabel.AutoSize = true;
			this.rangeLabel.Location = new System.Drawing.Point(373, 15);
			this.rangeLabel.Name = "rangeLabel";
			this.rangeLabel.Size = new System.Drawing.Size(69, 13);
			this.rangeLabel.TabIndex = 8;
			this.rangeLabel.Text = "Zakres pytań";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(652, 488);
			this.Controls.Add(this.rangeLabel);
			this.Controls.Add(this.minRange);
			this.Controls.Add(this.maxRange);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.answerLabel);
			this.Controls.Add(this.showAnswerButton);
			this.Controls.Add(this.questionLabel);
			this.Controls.Add(this.title);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Label title;
		private Label questionLabel;
		private Button showAnswerButton;
		private Label answerLabel;
		private Button button1;
		private Button button2;
		private TextBox maxRange;
		private TextBox minRange;
		private Label rangeLabel;
	}
}

