using System;
using System.Collections.Generic;
using Entities.Models;

namespace Contracts
{
    public interface ITimesheetRepository: IRepositoryBase<Timesheet>
    {
        IEnumerable<Timesheet> GetAllTimesheets();
        Timesheet GetTimesheetById(Guid timesheetId);
        void CreateTimesheet(Timesheet timesheet);
        void UpdateTimesheet(Timesheet dbTimesheet, Timesheet timesheet);
        List<Timesheet> ListTimesheetsByUserWithInTwoDates(string userId, string startDate, string endDate);
    }
}
