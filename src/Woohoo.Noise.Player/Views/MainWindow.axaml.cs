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

    private void Window_DoubleTapped(object? sender, Avalonia.Input.TappedEventArgs e)
    {
        this.ToggleFullScreen();
    }

    private void Window_KeyUp(object? sender, Avalonia.Input.KeyEventArgs e)
    {
        if (e.Key == Avalonia.Input.Key.F11)
        {
            this.ToggleFullScreen();
        }
        else if (e.Key == Avalonia.Input.Key.Escape && this.WindowState == WindowState.FullScreen)
        {
            this.WindowState = WindowState.Normal;
            this.UpdateFullScreenState();
        }
    }

    private void MenuItem_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        this.ToggleFullScreen();
    }

    private void ToggleFullScreen()
    {
        this.WindowState = this.WindowState != WindowState.FullScreen ? WindowState.FullScreen : WindowState.Normal;
        this.UpdateFullScreenState();
    }

    private void UpdateFullScreenState()
    {
        if (this.DataContext is MainWindowViewModel vm)
        {
            vm.IsFullScreen = this.WindowState == WindowState.FullScreen;
        }
    }
}
