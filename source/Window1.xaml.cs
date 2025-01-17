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

namespace SplitTool;
/// <summary>
/// Window1.xaml の相互作用ロジック
/// </summary>
public partial class Window1 : Window {
    public Window1() {
        InitializeComponent();
    }

    public void UpdateProgress(int value, int max) {
        progbar_write.Maximum = max;
        progbar_write.Value = value;
    }

    private void Button_Click(object sender, RoutedEventArgs e) {
        //ウィンドウを閉じる
        this.Close();
    }
}

