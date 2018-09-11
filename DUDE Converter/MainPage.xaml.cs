using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x407 dokumentiert.

namespace DUDE_Converter
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        bool ready = false;
        private void InputTBx_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckType();
            if (InputTBx.Text == String.Empty) ConvertBtn.IsEnabled = false;
            else ConvertBtn.IsEnabled = true;
        }

        private void TypeModeRBChecked(object sender, RoutedEventArgs e)
        {
            if (ready == false) ready = true;
            else CheckType();
        }

        private void CheckType()
        {
            if (DecRB.IsChecked == true)
            {
                if (Regex.IsMatch(InputTBx.Text, @"^[0-9]+$")) { InputFailureTB.Visibility = Visibility.Collapsed; ConvertBtn.IsEnabled = true; }
                else if (String.IsNullOrEmpty(InputTBx.Text)) { InputFailureTB.Visibility = Visibility.Collapsed; }
                else { InputFailureTB.Visibility = Visibility.Visible; ConvertBtn.IsEnabled = false; }
            }
            if (DualRB.IsChecked == true)
            {
                if (Regex.IsMatch(InputTBx.Text, @"^[0-1]+$")) { InputFailureTB.Visibility = Visibility.Collapsed; ConvertBtn.IsEnabled = true; }
                else { InputFailureTB.Visibility = Visibility.Visible; ConvertBtn.IsEnabled = false; }
                if (InputTBx.Text.Length > 8) { TooLongTB.Visibility = Visibility.Visible; TooShortTB.Visibility = Visibility.Collapsed; ConvertBtn.IsEnabled = false; }
                else if (InputTBx.Text.Length < 8) { TooShortTB.Visibility = Visibility.Visible; TooLongTB.Visibility = Visibility.Collapsed; ConvertBtn.IsEnabled = false; }
                else { TooLongTB.Visibility = Visibility.Collapsed; TooShortTB.Visibility = Visibility.Collapsed; ConvertBtn.IsEnabled = true; }
                if (String.IsNullOrEmpty(InputTBx.Text)) { InputFailureTB.Visibility = Visibility.Collapsed; TooShortTB.Visibility = Visibility.Collapsed; TooLongTB.Visibility = Visibility.Collapsed; }
            }
        }

        private void ConvertBtn_Click(object sender, RoutedEventArgs e)
        {
            DoConversion();
        }

        private void InputTBx_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (ConvertBtn.IsEnabled == true)
            {
                switch (e.Key) { case VirtualKey.Enter: DoConversion(); break; }
            }
        }

        private void DoConversion()
        {
            OutputTB.Text = String.Empty;

            if (DecRB.IsChecked == true)
            {
                int inpt = Convert.ToInt32(InputTBx.Text);
                if (inpt != 0)
                {
                    while (inpt >= 255)
                    {
                        OutputTB.Text = OutputTB.Text + "11111111 ";
                        inpt = inpt - 255;
                    }
                    if (inpt >= 128) { OutputTB.Text = OutputTB.Text + "1"; inpt = inpt - 128; }
                    else OutputTB.Text = OutputTB.Text + "0";
                    if (inpt >= 64) { OutputTB.Text = OutputTB.Text + "1"; inpt = inpt - 64; }
                    else OutputTB.Text = OutputTB.Text + "0";
                    if (inpt >= 32) { OutputTB.Text = OutputTB.Text + "1"; inpt = inpt - 32; }
                    else OutputTB.Text = OutputTB.Text + "0";
                    if (inpt >= 16) { OutputTB.Text = OutputTB.Text + "1"; inpt = inpt - 16; }
                    else OutputTB.Text = OutputTB.Text + "0";
                    if (inpt >= 8) { OutputTB.Text = OutputTB.Text + "1"; inpt = inpt - 8; }
                    else OutputTB.Text = OutputTB.Text + "0";
                    if (inpt >= 4) { OutputTB.Text = OutputTB.Text + "1"; inpt = inpt - 4; }
                    else OutputTB.Text = OutputTB.Text + "0";
                    if (inpt >= 2) { OutputTB.Text = OutputTB.Text + "1"; inpt = inpt - 2; }
                    else OutputTB.Text = OutputTB.Text + "0";
                    if (inpt >= 1) { OutputTB.Text = OutputTB.Text + "1"; inpt = inpt - 1; }
                    else OutputTB.Text = OutputTB.Text + "0";
                }
                else OutputTB.Text = "00000000";
            }

            if (DualRB.IsChecked == true)
            {
                string inptstr = InputTBx.Text;
                int no0 = Convert.ToInt32(inptstr.Substring(0, 1));
                int no1 = Convert.ToInt32(inptstr.Substring(1, 1));
                int no2 = Convert.ToInt32(inptstr.Substring(2, 1));
                int no3 = Convert.ToInt32(inptstr.Substring(3, 1));
                int no4 = Convert.ToInt32(inptstr.Substring(4, 1));
                int no5 = Convert.ToInt32(inptstr.Substring(5, 1));
                int no6 = Convert.ToInt32(inptstr.Substring(6, 1));
                int no7 = Convert.ToInt32(inptstr.Substring(7, 1));

                int oint = 0;

                if (no0 == 1) oint = oint + 128;
                if (no1 == 1) oint = oint + 64;
                if (no2 == 1) oint = oint + 32;
                if (no3 == 1) oint = oint + 16;
                if (no4 == 1) oint = oint + 8;
                if (no5 == 1) oint = oint + 4;
                if (no6 == 1) oint = oint + 2;
                if (no7 == 1) oint = oint + 1;

                OutputTB.Text = oint.ToString();
            }
        }

        private static bool IsCtrlKeyPressed()
        {
            var ctrlState = CoreWindow.GetForCurrentThread().GetKeyState(VirtualKey.Control);
            return (ctrlState & CoreVirtualKeyStates.Down) == CoreVirtualKeyStates.Down;
        }

        private void Grid_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            switch (e.Key)
            {
                case VirtualKey.F1: DecRB.IsChecked = true; break;
                case VirtualKey.F2: DualRB.IsChecked = true; break;
            }
        }
    }
}
