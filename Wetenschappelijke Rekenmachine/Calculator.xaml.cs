using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wetenschappelijke_Rekenmachine
{
    /// <summary>
    /// Interaction logic for Calculator.xaml
    /// </summary>
    public partial class Calculator : Window
    {
        public Calculator()
        {
            InitializeComponent();
            this.Textbox.Text = "0";
        }

        int OperatorCount = 0;
        int ClearEntry = 0;

        double Number1 = 0.0;
        double Number2 = 0.0;
        double Result = 0.0;

        string Input = string.Empty;
        string NextInput = string.Empty;
        string Operators = string.Empty;
        string OperatorDetected = string.Empty;
        string Detected = string.Empty;
        string RealOperator = string.Empty;
        string FirstOperator = string.Empty;
        string PreviousOperator = string.Empty;
        string CalculationString = string.Empty;

        List<string> OperatorsArray = new List<string>();

        string EquallityInTheMeantime()
        {
            if (OperatorCount > 2)
            {
                switch (PreviousOperator)
                {
                    case "+":
                        Result += Number2;
                        break;
                    case "-":
                        Result -= Number2;
                        break;
                    case "x":
                        Result *= Number2;
                        break;
                    case "÷":
                        Result /= Number2;
                        break;
                }
            }

            if (OperatorCount == 2)
            {
                switch (FirstOperator)
                {
                    case "+":
                        Result = Number1 + Number2;
                        break;
                    case "-":
                        Result = Number1 - Number2;
                        break;
                    case "x":
                        Result = Number1 * Number2;
                        break;
                    case "÷":
                        Result = Number1 / Number2;
                        break;
                }
            }

            this.Textbox.Text = Result.ToString();
            return this.Textbox.Text;
        }

        private void Clear_Entry(object sender, RoutedEventArgs e)
        {
            if (this.Textbox.Text.Length == 0 || this.Textbox.Text.Length == 1)
            {
                this.Textbox.Text = "0";
                NextInput = string.Empty;
            }
            else
            {
                this.Textbox.Text = this.Textbox.Text.Remove(0, ClearEntry);
                this.Textbox.Text = "0";
                NextInput = string.Empty;
            }            
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            this.Textbox.Text = "0";
            OperatorCount = 0;
            Number2 = 0;
            NextInput = string.Empty;
            Operators = string.Empty;
            Input = string.Empty;
            this.LabelTextBox.Text = "";
        }

        private void Backspace(object sender, RoutedEventArgs e)
        {   
            this.Textbox.Text = this.Textbox.Text.Remove(this.Textbox.Text.Length - 1);
            if (this.Textbox.Text.Length == 0)
            {
                this.Textbox.Text = "0";
            }

            Input = string.Empty;
            Operators = string.Empty;
        }        

        public void ClickedNumber(object sender, RoutedEventArgs e)
        {
            if (OperatorCount == 0)
            {
                this.Textbox.Text = "";
                Button Detected = (Button)sender;
                Input += Detected.Content.ToString();
                Number1 = Convert.ToDouble(Input);
                this.Textbox.Text = Input;
                this.LabelTextBox.Text = this.Textbox.Text;
            }            

            if (OperatorCount >= 1)
            {
                Button Detected = (Button)sender;
                NextInput += Detected.Content.ToString();
                ClearEntry = NextInput.Length;
                Number2 = Convert.ToDouble(NextInput);
                this.Textbox.Text = NextInput;                
            }            
        }        

        public void Operator_Clicked(object sender, RoutedEventArgs e)
        {
            OperatorCount++;
            this.LabelTextBox.Text += " " + NextInput;
            CalculationString = this.LabelTextBox.Text;
            Button Operators = (Button)sender;
            string OperatorDetected = Operators.Content.ToString();
            RealOperator = OperatorDetected;
            OperatorsArray.Add(RealOperator);
            
            if (OperatorCount == 1)
            {
                FirstOperator = RealOperator;
                this.LabelTextBox.Text = this.Textbox.Text + " " + OperatorDetected;
                CalculationString = this.LabelTextBox.Text;
            }

            if (OperatorCount > 1)
            {
                for (int i = OperatorsArray.Count - 1; i < OperatorsArray.Count; i++)
                {
                    PreviousOperator = OperatorsArray[i - 1];
                }

                this.LabelTextBox.Text = CalculationString + " " + OperatorDetected;
                EquallityInTheMeantime();
            }
            
            NextInput = "";
        }

        public void Equals_Clicked(object sender, RoutedEventArgs e)
        {
            switch (RealOperator)
            {
                case "+":
                    if (OperatorCount > 1)
                    {
                        Result += Number2;

                    }
                    else
                    {
                        Result = Number1 + Number2;
                    }
                    break;

                case "-":
                    if (OperatorCount > 1)
                    {
                        Result -= Number2;

                    }
                    else
                    {
                        Result = Number1 - Number2;
                    }
                    break;

                case "÷":
                    if (OperatorCount > 1)
                    {
                        Result /= Number2;

                    }
                    else
                    {
                        Result = Number1 / Number2;
                    }
                    break;

                case "x":
                    if (OperatorCount > 1)
                    {
                        Result *= Number2;
                    }
                    else
                    {
                        Result = Number1 * Number2;
                    }
                    break;

                default:
                    this.Textbox.Text = "Invalid Input";
                    break;
            }

            this.LabelTextBox.Text = "";
            this.Textbox.Text = Result.ToString();
            Number2 = 0;
            NextInput = string.Empty;
        }
    }

    //16-07 - Afronden van laatste knoppen rondom de rekenmachine bij standaardversie.
    //17-07 - Menu opties + History toevoegen.
}