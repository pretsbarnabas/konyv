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

namespace konyv
{
    /// <summary>
    /// Interaction logic for Listelement.xaml
    /// </summary>
    public partial class Listelement : UserControl
    {
        public Listelement()
        {
            InitializeComponent();
        }
        
        void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            border1.BorderBrush = Brushes.Black;
        }
        void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            border1.BorderBrush = Brushes.Beige;
        }
    }
}
