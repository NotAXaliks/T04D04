using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using CampusBenchDesktop.Models;
using CampusBenchDesktop.Services;

namespace CampusBenchDesktop.Pages;

public partial class OrdersPage : UserControl
{
    private List<Order> _orders;

    public List<Order> Orders
    {
        get { return _orders; }
        set { _orders = value; }
    }
    public OrdersPage()
    {
        InitializeComponent();
        Refresh();
    }

    private async void Refresh()
    {
        try
        {
            _orders = await NetManager.Get<List<Order>>("api/Order");
            DGOrder.ItemsSource = Orders;
        }
        catch
        {
            // Я хз как лучше сделать обработчик ошибок
        }
    }

    private void BAdd_OnClick(object? sender, RoutedEventArgs e)
    {
        App.mainWindow.MainFrame.Content = new AddEditOrderPage(new Order());
    }

    private void BEdit_OnClick(object? sender, RoutedEventArgs e)
    {
        if (DGOrder.SelectedItem is Order order)
        {
            App.mainWindow.MainFrame.Content = new AddEditOrderPage(order);
        }
    }

    private async void BRemove_OnClick(object? sender, RoutedEventArgs e)
    {
        if (DGOrder.SelectedItem is Order order)
        {
            try
            {
                await NetManager.Delete($"api/Order/{order.id}");
            }
            catch
            {
                // Я хз как лучше сделать обработчик ошибок
            }

            Refresh();
        }
    }

    
}