namespace Calculator_Desktop_Application
{
    public partial class CalculatorDesktopApplicationForm : Form
    {
        double currentOperand = 0;
        double storedOperand = 0;
        char storedOperation = ' ';
        bool isNewOperand = true;

        public CalculatorDesktopApplicationForm()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += CalculatorDesktopApplicationForm_KeyDown;
        }
        private void Calcuolator_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;

            ChangeFocus();
        }
        private void ChangeFocus()
        {
            foreach (Control control in this.Controls)
            {
                control.TabStop = false;
            }

            this.TabStop = true;

            this.ActiveControl = null;

            this.Focus();
        }
        private void CalculatorDesktopApplicationForm_KeyDown(object sender, KeyEventArgs e)
        {
            bool shiftPressed = e.Shift;
            switch (e.KeyCode)
            {
                case Keys.D0:
                case Keys.NumPad0:
                    DigitButton_Click(Number0, EventArgs.Empty);
                    break;
                case Keys.D1:
                case Keys.NumPad1:
                    DigitButton_Click(Number1, EventArgs.Empty);
                    break;
                case Keys.D2:
                case Keys.NumPad2:
                    DigitButton_Click(Number2, EventArgs.Empty);
                    break;
                case Keys.D3:
                case Keys.NumPad3:
                    DigitButton_Click(Number3, EventArgs.Empty);
                    break;
                case Keys.D4:
                case Keys.NumPad4:
                    DigitButton_Click(Number4, EventArgs.Empty);
                    break;
                case Keys.D5:
                case Keys.NumPad5:
                    DigitButton_Click(Number5, EventArgs.Empty);
                    break;
                case Keys.D6:
                case Keys.NumPad6:
                    DigitButton_Click(Number6, EventArgs.Empty);
                    break;
                case Keys.D7:
                case Keys.NumPad7:
                    DigitButton_Click(Number7, EventArgs.Empty);
                    break;
                case Keys.D8:
                case Keys.NumPad8:
                    if (shiftPressed)
                    {
                        OperatorButton_Click(TimesButton, EventArgs.Empty);
                        break;
                    }
                    DigitButton_Click(Number8, EventArgs.Empty);
                    break;
                case Keys.D9:
                case Keys.NumPad9:
                    DigitButton_Click(Number9, EventArgs.Empty);
                    break;
                case Keys.Add:
                    OperatorButton_Click(PlusButton, EventArgs.Empty);
                    break;
                case Keys.Oemplus:
                    if (shiftPressed)
                    {
                        OperatorButton_Click(PlusButton, EventArgs.Empty);
                        break;
                    }
                    EqualsButton_Click(EqualSign, EventArgs.Empty);
                    break;
                case Keys.Subtract:
                case Keys.OemMinus:
                    OperatorButton_Click(MinusButton, EventArgs.Empty);
                    break;
                case Keys.Multiply:
                    break;
                case Keys.Divide:
                case Keys.OemQuestion:
                    OperatorButton_Click(DivideButton, EventArgs.Empty);
                    break;
                case Keys.Decimal:
                case Keys.OemPeriod:
                    DecimalButton_Click(DecimalPoint, EventArgs.Empty);
                    break;
                case Keys.Back:
                    ClearButton_Click(ClearButton, EventArgs.Empty);
                    break;
                case Keys.Enter:
                    EqualsButton_Click(EqualSign, EventArgs.Empty);
                    break;
            }
        }
        private void DigitButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string digit = button.Text;
            if (isNewOperand)
            {
                ConvertScreen.Text = "";
                OutputScreen.Text = digit;
                isNewOperand = false;
            }
            else
            {
                OutputScreen.Text += digit;
            }
        }
        private void OperatorButton_Click(object sender, EventArgs e)
        {
            if (OutputScreen.Text == "Division by zero is not allowed.")
            {
                OutputScreen.Text = "0";
            }
            Button button = (Button)sender;
            char operation = button.Text[0];
            if (!isNewOperand)
            {
                ConvertScreen.Text = "";
                if (storedOperation != ' ')
                {
                    PerformOperation();
                }
                storedOperand = double.Parse(OutputScreen.Text);
                storedOperation = operation;
                isNewOperand = true;
            }
            else
            {
                storedOperation = operation;
            }
            OperationScreen.Text = storedOperation.ToString();
        }
        private void EqualsButton_Click(object sender, EventArgs e)
        {
            if (OutputScreen.Text != "Division by zero is not allowed.")
            {
                if (!isNewOperand && storedOperation != ' ')
                {
                    PerformOperation();
                    if (OutputScreen.Text != "Division by zero is not allowed.")
                    {
                        storedOperand = double.Parse(OutputScreen.Text);
                        OperationScreen.Text = "";
                        storedOperation = ' ';
                        isNewOperand = true;
                    }
                }
            }
            else
            {
                ClearAll();
            }         
        }
        private void PerformOperation()
        {
            double current = double.Parse(OutputScreen.Text);
            switch (storedOperation)
            {
                case '+':
                    currentOperand = storedOperand + current;
                    break;
                case '-':
                    currentOperand = storedOperand - current;
                    break;
                case '*':
                    currentOperand = storedOperand * current;
                    break;
                case '/':
                    if (current != 0)
                    {
                        currentOperand = storedOperand / current;
                    }
                    else
                    {
                        ClearAll();
                        OutputScreen.Text = "Division by zero is not allowed.";
                    }
                    break;
            }
            if (OutputScreen.Text != "Division by zero is not allowed.")
            {
                OutputScreen.Text = currentOperand.ToString();
            }
        }
        private void ClearButton_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        private void ClearAll()
        {
            OutputScreen.Text = "0";
            OperationScreen.Text = "";
            ConvertScreen.Text = "";
            currentOperand = 0;
            storedOperand = 0;
            storedOperation = ' ';
            isNewOperand = true;
        }
        private void DecimalButton_Click(object sender, EventArgs e)
        {
            if (!OutputScreen.Text.Contains("."))
            {
                OutputScreen.Text += ".";
            }
        }
        private void BinaryButton_Click(object sender, EventArgs e)
        {
            ConvertToBinary();
        }
        private void HexadecimalButton_Click(object sender, EventArgs e)
        {
            ConvertToHexadecimal();
        }
        private void ConvertToBinary()
        {
            if (OutputScreen.Text.Contains("."))
            {
                ConvertScreen.Text = "CANNOT CONVERT WITH DECIMAL POINT";
                return;
            }
            else
            {
                double number = double.Parse(OutputScreen.Text);
                if (number > 255 || number < 0)
                {
                    ConvertScreen.Text = "OUT OF RNG";
                    return;
                }
                string binary = Convert.ToString((int)number, 2);
                ConvertScreen.Text = binary;
            }
        }
        private void ConvertToHexadecimal()
        {
            if (OutputScreen.Text.Contains("."))
            {
                ConvertScreen.Text = "CANNOT CONVERT WITH DECIMAL POINT";
                return;
            }
            else
            {
                double number = double.Parse(OutputScreen.Text);
                if (number > 4294967295 || number < 0)
                {
                    ConvertScreen.Text = "OUT OF RNG";
                    return;
                }
                string hexadecimal = Convert.ToString((uint)number, 16).ToUpper();
                ConvertScreen.Text = hexadecimal;
            }
        }
    }
}