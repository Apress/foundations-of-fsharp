using System;
using System.Windows.Forms;

namespace Strangelights.Forms
{
    public partial class FibForm : Form
    {
        public FibForm()
        {
            InitializeComponent();
        }

        public Button Calculate
        {
            get { return calculate; }
        }

        public Label Result
        {
            get { return result; }
        }

        public TextBox Input
        {
            get { return input; }
        }
    }
}