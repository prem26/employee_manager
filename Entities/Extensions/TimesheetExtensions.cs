using System;
using Entities.Models;
namespace Entities.Extensions
{
    public static class TimesheetExtensions
    {
        public static void Map(this Timesheet dbTimesheet, Timesheet timesheet)
        {
            dbTimesheet.Id = timesheet.Id;
            //dbTimesheet.Description = timesheet.Description;
            dbTimesheet.StartDate = timesheet.StartDate;
            dbTimesheet.EndDate = timesheet.EndDate;
            dbTimesheet.StartTime = timesheet.StartTime;
            dbTimesheet.EndTime = timesheet.EndTime;
            dbTimesheet.UserId = timesheet.UserId;
            dbTimesheet.TaskId = timesheet.TaskId;
            dbTimesheet.Created = timesheet.Created;
            dbTimesheet.Updated = timesheet.Updated;
        }
    }
}
