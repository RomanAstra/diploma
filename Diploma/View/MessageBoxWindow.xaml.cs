﻿using System;
using System.Windows;

namespace Diploma.View
{
    /// <summary>
    /// Логика взаимодействия для MessageBoxWindow.xaml
    /// </summary>
    public partial class MessageBoxWindow : Window
    {
        public MessageBoxWindow(string message)
        {
            InitializeComponent();
	        MainTextBox.Text = message;
			MainButton.Click += delegate{ Close();};
        }
    }
}
