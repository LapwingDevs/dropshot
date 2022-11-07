namespace DropShot.Infrastructure.BackgroundServices;

internal record ScheduleItem(DateTime ExecuteTime, ScheduleItemType ScheduleItemType, int Id);