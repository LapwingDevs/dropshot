namespace DropShot.Infrastructure.BackgroundServices.DeadlinesHandler;

internal record ScheduleItem(
    DateTime ExecuteTime, 
    ScheduleItemType ScheduleItemType, 
    int Id);