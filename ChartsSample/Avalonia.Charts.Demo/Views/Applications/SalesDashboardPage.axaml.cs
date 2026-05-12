using Avalonia.Controls;

namespace Avalonia.Charts.Demo.Views.Applications;

public partial class SalesDashboardPage : UserControl
{
    public SalesDashboardPage()
    {
        InitializeComponent();
        SizeChanged += OnSizeChanged;
    }

    private void OnSizeChanged(object? sender, SizeChangedEventArgs e)
    {
        double width = e.NewSize.Width;
        
        // Thresholds
        bool isMobile = width < 800;
        bool isTablet = width >= 800 && width < 1280;
        bool isDesktop = width >= 1280;

        // Apply classes for CSS styling
        if (isMobile)
        {
            if (!Classes.Contains("mobile")) { Classes.Remove("tablet"); Classes.Add("mobile"); }
        }
        else if (isTablet)
        {
            if (!Classes.Contains("tablet")) { Classes.Remove("mobile"); Classes.Add("tablet"); }
        }
        else
        {
            Classes.Remove("mobile");
            Classes.Remove("tablet");
        }

        // Header
        var headerGrid = this.FindControl<Grid>("HeaderGrid");
        var headerTitle = this.FindControl<StackPanel>("HeaderTitlePanel");
        var headerActions = this.FindControl<StackPanel>("HeaderActionsPanel");
        if (headerGrid != null)
        {
            // Stack header if narrow
            bool shouldStackHeader = width < 950;
            if (shouldStackHeader)
            {
                if (headerTitle != null) { Grid.SetColumn(headerTitle, 0); Grid.SetRow(headerTitle, 0); }
                if (headerActions != null) { Grid.SetColumn(headerActions, 0); Grid.SetRow(headerActions, 1); }
                headerGrid.ColumnDefinitions = ColumnDefinitions.Parse("*");
                headerGrid.RowDefinitions = RowDefinitions.Parse("Auto,Auto");
            }
            else
            {
                headerGrid.ColumnDefinitions = ColumnDefinitions.Parse("*,Auto");
                headerGrid.RowDefinitions = RowDefinitions.Parse("Auto");
                if (headerTitle != null) { Grid.SetColumn(headerTitle, 0); Grid.SetRow(headerTitle, 0); }
                if (headerActions != null) { Grid.SetColumn(headerActions, 1); Grid.SetRow(headerActions, 0); }
            }
        }

        // KPI Grid
        var kpiGrid = this.FindControl<Grid>("KpiGrid");
        var kpi1 = this.FindControl<Border>("KpiCard1");
        var kpi2 = this.FindControl<Border>("KpiCard2");
        var kpi3 = this.FindControl<Border>("KpiCard3");
        var kpi4 = this.FindControl<Border>("KpiCard4");

        if (kpiGrid != null)
        {
            if (isDesktop)
            {
                kpiGrid.ColumnDefinitions = ColumnDefinitions.Parse("*,*,*,*");
                kpiGrid.RowDefinitions = RowDefinitions.Parse("Auto");
                if (kpi1 != null) { Grid.SetColumn(kpi1, 0); Grid.SetRow(kpi1, 0); }
                if (kpi2 != null) { Grid.SetColumn(kpi2, 1); Grid.SetRow(kpi2, 0); }
                if (kpi3 != null) { Grid.SetColumn(kpi3, 2); Grid.SetRow(kpi3, 0); }
                if (kpi4 != null) { Grid.SetColumn(kpi4, 3); Grid.SetRow(kpi4, 0); }
            }
            else if (isTablet)
            {
                // Wrap in 2 columns
                if (kpi1 != null) { Grid.SetColumn(kpi1, 0); Grid.SetRow(kpi1, 0); }
                if (kpi2 != null) { Grid.SetColumn(kpi2, 1); Grid.SetRow(kpi2, 0); }
                if (kpi3 != null) { Grid.SetColumn(kpi3, 0); Grid.SetRow(kpi3, 1); }
                if (kpi4 != null) { Grid.SetColumn(kpi4, 1); Grid.SetRow(kpi4, 1); }
                kpiGrid.ColumnDefinitions = ColumnDefinitions.Parse("*,*");
                kpiGrid.RowDefinitions = RowDefinitions.Parse("Auto,Auto");
            }
            else // Mobile
            {
                // Stack in 1 column
                if (kpi1 != null) { Grid.SetColumn(kpi1, 0); Grid.SetRow(kpi1, 0); }
                if (kpi2 != null) { Grid.SetColumn(kpi2, 0); Grid.SetRow(kpi2, 1); }
                if (kpi3 != null) { Grid.SetColumn(kpi3, 0); Grid.SetRow(kpi3, 2); }
                if (kpi4 != null) { Grid.SetColumn(kpi4, 0); Grid.SetRow(kpi4, 3); }
                kpiGrid.ColumnDefinitions = ColumnDefinitions.Parse("*");
                kpiGrid.RowDefinitions = RowDefinitions.Parse("Auto,Auto,Auto,Auto");
            }
        }

        // Charts Grid
        var chartsGrid = this.FindControl<Grid>("ChartsGrid");
        var revenueCard = this.FindControl<Border>("RevenueCard");
        var categoryCard = this.FindControl<Border>("CategoryCard");

        if (chartsGrid != null)
        {
            if (isMobile)
            {
                if (revenueCard != null) { Grid.SetColumn(revenueCard, 0); Grid.SetRow(revenueCard, 0); }
                if (categoryCard != null) { Grid.SetColumn(categoryCard, 0); Grid.SetRow(categoryCard, 1); }
                chartsGrid.ColumnDefinitions = ColumnDefinitions.Parse("*");
                chartsGrid.RowDefinitions = RowDefinitions.Parse("Auto,Auto");
            }
            else
            {
                chartsGrid.ColumnDefinitions = ColumnDefinitions.Parse("2*,420");
                chartsGrid.RowDefinitions = RowDefinitions.Parse("Auto");
                if (revenueCard != null) { Grid.SetColumn(revenueCard, 0); Grid.SetRow(revenueCard, 0); }
                if (categoryCard != null) { Grid.SetColumn(categoryCard, 1); Grid.SetRow(categoryCard, 0); }
            }
        }

        // Regions Grid
        var regionsGrid = this.FindControl<Grid>("RegionsGrid");
        var regionsCard = this.FindControl<Border>("RegionsCard");
        var performanceCard = this.FindControl<Border>("PerformanceCard");

        if (regionsGrid != null)
        {
            if (isMobile || isTablet)
            {
                if (regionsCard != null) { Grid.SetColumn(regionsCard, 0); Grid.SetRow(regionsCard, 0); }
                if (performanceCard != null) { Grid.SetColumn(performanceCard, 0); Grid.SetRow(performanceCard, 1); }
                regionsGrid.ColumnDefinitions = ColumnDefinitions.Parse("*");
                regionsGrid.RowDefinitions = RowDefinitions.Parse("Auto,Auto");
            }
            else
            {
                regionsGrid.ColumnDefinitions = ColumnDefinitions.Parse("*,420");
                regionsGrid.RowDefinitions = RowDefinitions.Parse("Auto");
                if (regionsCard != null) { Grid.SetColumn(regionsCard, 0); Grid.SetRow(regionsCard, 0); }
                if (performanceCard != null) { Grid.SetColumn(performanceCard, 1); Grid.SetRow(performanceCard, 0); }
            }
        }

        // Orders Grid
        var ordersGrid = this.FindControl<Grid>("OrdersGrid");
        var ordersCard = this.FindControl<Border>("OrdersCard");
        var topProductsCard = this.FindControl<Border>("TopProductsCard");

        if (ordersGrid != null)
        {
            if (isMobile || isTablet)
            {
                if (ordersCard != null) { Grid.SetColumn(ordersCard, 0); Grid.SetRow(ordersCard, 0); }
                if (topProductsCard != null) { Grid.SetColumn(topProductsCard, 0); Grid.SetRow(topProductsCard, 1); }
                ordersGrid.ColumnDefinitions = ColumnDefinitions.Parse("*");
                ordersGrid.RowDefinitions = RowDefinitions.Parse("Auto,Auto");
            }
            else
            {
                ordersGrid.ColumnDefinitions = ColumnDefinitions.Parse("*,380");
                ordersGrid.RowDefinitions = RowDefinitions.Parse("Auto");
                if (ordersCard != null) { Grid.SetColumn(ordersCard, 0); Grid.SetRow(ordersCard, 0); }
                if (topProductsCard != null) { Grid.SetColumn(topProductsCard, 1); Grid.SetRow(topProductsCard, 0); }
            }
        }
    }
}
