using System;
using System.Windows.Forms;
using Strangelights;

namespace CSApp
{
    public partial class FibForm : Form
    {
        public FibForm()
        {
            InitializeComponent();
        }

        private void calculate_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(input.Text);
            n = Fibonacci.get(n);
            result.Text = n.ToString();
        }
    }
}