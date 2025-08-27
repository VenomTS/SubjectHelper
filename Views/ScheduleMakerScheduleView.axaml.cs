using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using SubjectHelper.ViewModels;

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

    private const int ScheduleStartingColumn = 1;
    
    // + 1 since there is a row for hours / There is extra column for days
    private const int Rows = Days + 1;
    private const int Columns = HoursDisplayed * SectionsPerHour + 1;

    private ScheduleMakerScheduleViewModel? _viewModel;
    
    public ScheduleMakerScheduleView()
    {
        InitializeComponent();
    }

    private void GenerateRows()
    {
        for(var i = 0; i < Rows; i++)
            MainGrid.RowDefinitions.Add(new RowDefinition(GridLength.Star));
    }

    private void GenerateColumns()
    {
        MainGrid.ColumnDefinitions.Add(new ColumnDefinition(new GridLength(DaysColumnWidth)));

        for (var i = 1; i < Columns; i++)
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
        Grid.SetColumn(endHourMark, Columns - 3);
        Grid.SetColumnSpan(endHourMark, HourMarkColSpan);
        
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
            Grid.SetColumnSpan(hourMark, HourMarkColSpan);

            // Grid.SetRow(hourMark, 0);
            // Grid.SetColumn(hourMark, (i - StartHour) * SectionsPerHour);
            // Grid.SetColumnSpan(hourMark, HourMarkColSpan);

            MainGrid.Children.Add(hourMark);
        }
    }

    private void GenerateSpacers()
    {
        for (var i = 0; i < Rows; i++)
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

        for (var i = 0; i < Columns; i += 6)
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

    private void MainGrid_OnAttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
    {
        GenerateRows();
        GenerateColumns();
        GenerateHourMarks();
        GenerateDays();
        GenerateSpacers();
    }

    private void Control_OnLoaded(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not ScheduleMakerScheduleViewModel vm) return;

        _viewModel = vm;
        
        _viewModel.OnCurrentScheduleChanged += OnScheduleChanged;

        _viewModel.TriggerScheduleChange();
    }

    private void OnScheduleChanged(object? sender, ScheduleChangedEventArgs e)
    {
        var rectangleChildren = MainGrid.Children.Where(x => x.Classes.Contains("ScheduleItem"));

        MainGrid.Children.RemoveAll(rectangleChildren);

        foreach (var display in e.Schedule)
        {
            var text = new TextBlock
            {
                Text = $"{display.SectionFullName}\n{display.StartTime} - {display.EndTime}",
                Background = new SolidColorBrush(GenerateRandomColor()),
                TextAlignment = TextAlignment.Center,
                Classes = { "ScheduleItem" },
            };

            var row = GetRowFromDay(display.Day);
            var col = GetColumnFromStartTime(display.StartTime);
            var colSpan = GetColumnSpanFromStartEndTime(display.StartTime, display.EndTime);
            
            Grid.SetRow(text, row);
            Grid.SetColumn(text, col);
            Grid.SetColumnSpan(text, colSpan);

            MainGrid.Children.Add(text);
        }
    }
    
    // NOTE:
    // Move 6 for each hour, move 1 for 10 minutes
    private int GetColumnSpanFromStartEndTime(TimeOnly startTime, TimeOnly endTime)
    {
        var hourDifference = endTime.Hour - startTime.Hour;
        var minuteDifference = endTime.Minute - startTime.Minute;

        if (minuteDifference < 0)
        {
            minuteDifference += 60;
            hourDifference -= 1;
        }

        return (int) (hourDifference * 6 + Math.Ceiling(minuteDifference / 10d));
    }

    // NOTE:
    // Move 6 for each hour, move 1 for 10 minutes
    private int GetColumnFromStartTime(TimeOnly startTime)
    {
        var hourDifference = startTime.Hour - StartHour;
        var minuteDifference = startTime.Minute;
        return (int)(ScheduleStartingColumn + hourDifference * 6 + Math.Ceiling(minuteDifference / 10d));
    }

    private int GetRowFromDay(DayOfWeek day)
    {
        return day switch
        {
            DayOfWeek.Monday => 1,
            DayOfWeek.Tuesday => 2,
            DayOfWeek.Wednesday => 3,
            DayOfWeek.Thursday => 4,
            DayOfWeek.Friday => 5,
            _ => throw new Exception("Day out of range of rows")
        };
    }
    
    private Color GenerateRandomColor()
    {
        var random = new Random();

        var r = (byte) random.Next(0, 256);
        var g = (byte) random.Next(0, 256);
        var b = (byte) random.Next(0, 256);

        const int alpha = 255;

        return new Color(alpha, r, g, b);
    }
}