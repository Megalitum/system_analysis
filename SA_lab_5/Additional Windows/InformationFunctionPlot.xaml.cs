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
using System.Windows.Shapes;

using OxyPlot;
using SA_lab_5.Cell_Logic;

namespace SA_lab_5.Additional_Windows
{
    /// <summary>
    /// Interaction logic for InformationFunctionPlot.xaml
    /// </summary>
    public partial class InformationFunctionPlot : Window
    {
        public InformationViewModel Model { get; private set; }
        public InformationFunctionPlot(BaseCell cell)
        {
            InitializeComponent();
            
        }
    }
}
