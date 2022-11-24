namespace DropShot.Infrastructure.BackgroundServices.DeadlinesHandler.Models;

internal record ScheduleItem(
    DateTime ExecuteTime, 
    ScheduleItemType ScheduleItemType, 
    int Id);