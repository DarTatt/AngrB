﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace AngryBirds
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        void Button_Click_Play(object sender, RoutedEventArgs e)
        {
            Window1 GameWindow = new Window1();
            GameWindow.Show();
            this.Close();
        }
        void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
