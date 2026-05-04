using Avalonia.Controls;

namespace Avalonia.Charts.Demo.Views.Applications;

public partial class CryptoDashboardPage : UserControl
{
    public CryptoDashboardPage()
    {
        InitializeComponent();
        SizeChanged += OnSizeChanged;
    }

    private void OnSizeChanged(object? sender, SizeChangedEventArgs e)
    {
        double width = e.NewSize.Width;
        bool isMobile = width < 750;
        bool isTablet = width >= 750 && width < 1200;
        bool isDesktop = width >= 1200;
        
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
            bool shouldStackHeader = width < 900;
            if (shouldStackHeader)
            {
                if (headerTitle != null) { Grid.SetColumn(headerTitle, 0); Grid.SetRow(headerTitle, 0); }
                if (headerActions != null) 
                { 
                    Grid.SetColumn(headerActions, 0); 
                    Grid.SetRow(headerActions, 1);
                    headerActions.HorizontalAlignment = Layout.HorizontalAlignment.Left;
                }
                headerGrid.ColumnDefinitions = ColumnDefinitions.Parse("*");
                headerGrid.RowDefinitions = RowDefinitions.Parse("Auto,Auto");
            }
            else
            {
                headerGrid.ColumnDefinitions = ColumnDefinitions.Parse("*,Auto");
                headerGrid.RowDefinitions = RowDefinitions.Parse("Auto");
                if (headerTitle != null) { Grid.SetColumn(headerTitle, 0); Grid.SetRow(headerTitle, 0); }
                if (headerActions != null) 
                { 
                    Grid.SetColumn(headerActions, 1); 
                    Grid.SetRow(headerActions, 0);
                    headerActions.HorizontalAlignment = Layout.HorizontalAlignment.Right;
                }
            }
        }

        // Top Row
        var topGrid = this.FindControl<Grid>("TopGrid");
        var balanceCard = this.FindControl<Border>("BalanceCard");
        var priceChartCard = this.FindControl<Border>("PriceChartCard");

        if (topGrid != null)
        {
            if (isMobile || isTablet)
            {
                // Set indices FIRST to avoid OOR when column is removed
                if (balanceCard != null) { Grid.SetColumn(balanceCard, 0); Grid.SetRow(balanceCard, 0); }
                if (priceChartCard != null) { Grid.SetColumn(priceChartCard, 0); Grid.SetRow(priceChartCard, 1); }
                topGrid.ColumnDefinitions = ColumnDefinitions.Parse("*");
                topGrid.RowDefinitions = RowDefinitions.Parse("Auto,Auto");
            }
            else
            {
                topGrid.ColumnDefinitions = ColumnDefinitions.Parse("*,1.4*");
                topGrid.RowDefinitions = RowDefinitions.Parse("Auto");
                if (balanceCard != null) { Grid.SetColumn(balanceCard, 0); Grid.SetRow(balanceCard, 0); }
                if (priceChartCard != null) { Grid.SetColumn(priceChartCard, 1); Grid.SetRow(priceChartCard, 0); }
            }
        }

        // Bottom Row
        var bottomGrid = this.FindControl<Grid>("BottomGrid");
        var tradesCard = this.FindControl<Border>("TradesCard");
        var walletCard = this.FindControl<Border>("WalletCard");

        if (bottomGrid != null)
        {
            if (isMobile)
            {
                if (tradesCard != null) { Grid.SetColumn(tradesCard, 0); Grid.SetRow(tradesCard, 0); }
                if (walletCard != null) { Grid.SetColumn(walletCard, 0); Grid.SetRow(walletCard, 1); }
                bottomGrid.ColumnDefinitions = ColumnDefinitions.Parse("*");
                bottomGrid.RowDefinitions = RowDefinitions.Parse("Auto,Auto");
            }
            else
            {
                bottomGrid.ColumnDefinitions = ColumnDefinitions.Parse("*,1.2*");
                bottomGrid.RowDefinitions = RowDefinitions.Parse("Auto");
                if (tradesCard != null) { Grid.SetColumn(tradesCard, 0); Grid.SetRow(tradesCard, 0); }
                if (walletCard != null) { Grid.SetColumn(walletCard, 1); Grid.SetRow(walletCard, 0); }
            }
        }

        // Wallet Grid
        var walletGrid = this.FindControl<Grid>("WalletGrid");
        var innerBtc = this.FindControl<Border>("InnerBtcCard");
        var innerEth = this.FindControl<Border>("InnerEthCard");
        var innerXrp = this.FindControl<Border>("InnerXrpCard");
        var innerLtc = this.FindControl<Border>("InnerLtcCard");

        if (walletGrid != null)
        {
            if (isMobile)
            {
                if (innerBtc != null) { Grid.SetColumn(innerBtc, 0); Grid.SetRow(innerBtc, 0); }
                if (innerEth != null) { Grid.SetColumn(innerEth, 0); Grid.SetRow(innerEth, 1); }
                if (innerXrp != null) { Grid.SetColumn(innerXrp, 0); Grid.SetRow(innerXrp, 2); }
                if (innerLtc != null) { Grid.SetColumn(innerLtc, 0); Grid.SetRow(innerLtc, 3); }
                walletGrid.ColumnDefinitions = ColumnDefinitions.Parse("*");
                walletGrid.RowDefinitions = RowDefinitions.Parse("Auto,Auto,Auto,Auto");
            }
            else
            {
                walletGrid.ColumnDefinitions = ColumnDefinitions.Parse("*,*");
                walletGrid.RowDefinitions = RowDefinitions.Parse("Auto,Auto");
                if (innerBtc != null) { Grid.SetColumn(innerBtc, 0); Grid.SetRow(innerBtc, 0); }
                if (innerEth != null) { Grid.SetColumn(innerEth, 1); Grid.SetRow(innerEth, 0); }
                if (innerXrp != null) { Grid.SetColumn(innerXrp, 0); Grid.SetRow(innerXrp, 1); }
                if (innerLtc != null) { Grid.SetColumn(innerLtc, 1); Grid.SetRow(innerLtc, 1); }
            }
        }
    }
}
