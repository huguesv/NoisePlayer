// Copyright (c) Hugues Valois. All rights reserved.
// Licensed under the MIT license. See LICENSE in the project root for license information.

namespace Woohoo.Noise.Player.Views;

using Avalonia.Controls;
using Avalonia.Threading;
using Woohoo.Noise.Player.ViewModels;

public partial class MainWindow : Window
{
    private readonly DispatcherTimer timer;

    public MainWindow()
    {
        this.InitializeComponent();

        this.timer = new DispatcherTimer();
        this.timer.Interval = TimeSpan.FromSeconds(1);
        this.timer.Tick += this.DispatcherTimer_Tick;
        this.timer.Start();
    }

    private void DispatcherTimer_Tick(object? sender, EventArgs e)
    {
        if (this.DataContext is MainWindowViewModel vm)
        {
            vm.UpdateClockText();
        }
    }
}
