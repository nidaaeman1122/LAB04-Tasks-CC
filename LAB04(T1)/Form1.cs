using System;
using System.Windows.Forms;

namespace LexicalAnalyzerApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            string input = txtInput.Text;  // Get the input from TextBox
            LexicalAnalyzer analyzer = new LexicalAnalyzer(input);  // Create an instance of LexicalAnalyzer
            var tokens = analyzer.Analyze();  // Tokenize the input

            lstTokens.Items.Clear();  // Clear any previous tokens
            foreach (var token in tokens)
            {
                lstTokens.Items.Add(token);  // Display tokens in the ListBox
            }
        }
    }
}
