using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace SubjectHelper.Views;

public partial class ScheduleMakerScheduleView : UserControl
{
    private const int StartHour = 9;
    private const int EndHour = 21;

    private const int HoursDisplayed = EndHour - StartHour;
    private const int SectionsPerHour = 6;

    private const int DaysColumnWidth = 128;
    private const int SectionColumnWidth = 16;

    private const int Days = 5;

    private const int RowSpacerColSpan = HoursDisplayed * SectionsPerHour + 1;
    
    private const int ColSpacerRowSpan = Days + 1;

    private const int HourMarkColSpan = 4;
    
    // + 1 since there is a row for hours / There is extra column for days
    private int _rows = Days + 1;
    private int _cols = HoursDisplayed * SectionsPerHour + 1;
    
    public ScheduleMakerScheduleView()
    {
        InitializeComponent();
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);

        GenerateRows();
        GenerateColumns();
        GenerateSpacers();
        GenerateDays();
        GenerateHourMarks();
    }

    private void GenerateRows()
    {
        for(var i = 0; i < _rows; i++)
            MainGrid.RowDefinitions.Add(new RowDefinition(GridLength.Star));
    }

    private void GenerateColumns()
    {
        MainGrid.ColumnDefinitions.Add(new ColumnDefinition(new GridLength(DaysColumnWidth)));

        for (var i = 1; i < _cols; i++)
            MainGrid.ColumnDefinitions.Add(new ColumnDefinition(new GridLength(SectionColumnWidth)));
    }

    private void GenerateHourMarks()
    {

        var startHourMark = new TextBlock
        {
            Text = $"{StartHour:D2}:00",
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Bottom,
        };

        Grid.SetRow(startHourMark, 0);
        Grid.SetColumn(startHourMark, 0);
        Grid.SetColumnSpan(startHourMark, 9);
        
        var endHourMark = new TextBlock
        {
            Text = $"{EndHour:D2}:00",
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Bottom,
        };

        Grid.SetRow(endHourMark, 0);
        Grid.SetColumn(endHourMark, _cols - 3);
        Grid.SetColumnSpan(endHourMark, 4);
        
        MainGrid.Children.Add(startHourMark);
        MainGrid.Children.Add(endHourMark);
        
        // Can handle middle marks okay, but needs manual intervention for the first and last one
        for (var i = StartHour + 1; i < EndHour; i++)
        {
            var hourMark = new TextBlock
            {
                Text = $"{i:D2}:00",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
            };
            
            Grid.SetRow(hourMark, 0);
            Grid.SetColumn(hourMark, (i - StartHour) * SectionsPerHour - 1);
            Grid.SetColumnSpan(hourMark, 4);

            // Grid.SetRow(hourMark, 0);
            // Grid.SetColumn(hourMark, (i - StartHour) * SectionsPerHour);
            // Grid.SetColumnSpan(hourMark, HourMarkColSpan);

            MainGrid.Children.Add(hourMark);
        }
    }

    private void GenerateSpacers()
    {
        for (var i = 0; i < _rows; i++)
        {
            var verticalSpacer = new Rectangle
            {
                Fill = Brushes.White,
                Height = 1d,
                VerticalAlignment = VerticalAlignment.Bottom,
            };
            
            Grid.SetRow(verticalSpacer, i);
            Grid.SetColumn(verticalSpacer, 0);
            Grid.SetColumnSpan(verticalSpacer, RowSpacerColSpan);
            MainGrid.Children.Add(verticalSpacer);
        }
        
        var horizontalSpacer = new Rectangle
        {
            Fill = Brushes.White,
            Width = 1d,
            HorizontalAlignment = HorizontalAlignment.Left,
        };
        
        Grid.SetRow(horizontalSpacer, 1);
        Grid.SetRowSpan(horizontalSpacer, ColSpacerRowSpan);
        Grid.SetColumn(horizontalSpacer, 0);
        MainGrid.Children.Add(horizontalSpacer);

        for (var i = 0; i < _cols; i += 6)
        {
            horizontalSpacer = new Rectangle
            {
                Fill = Brushes.White,
                Width = 1d,
                HorizontalAlignment = HorizontalAlignment.Right,
            };
            
            Grid.SetRow(horizontalSpacer, 1);
            Grid.SetRowSpan(horizontalSpacer, ColSpacerRowSpan);
            Grid.SetColumn(horizontalSpacer, i);
            MainGrid.Children.Add(horizontalSpacer);
        }
    }

    private void GenerateDays()
    {

        List<string> days = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday"];

        for (var i = 0; i < days.Count; i++)
        {
            var dayMark = new TextBlock
            {
                Text = days[i],
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };

            Grid.SetRow(dayMark, i + 1);
            Grid.SetColumn(dayMark, 0);
            MainGrid.Children.Add(dayMark);
        }
    }
}