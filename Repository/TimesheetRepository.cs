using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using Entities;
using Entities.Extensions;
using Entities.Models;

namespace Repository
{
    public class TimesheetRepository: RepositoryBase<Timesheet>, ITimesheetRepository
    {
        public TimesheetRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Timesheet> GetAllTimesheets()
        {
            return FindAll().OrderBy(t => t.Created);
        }

        public Timesheet GetTimesheetById(Guid timesheetId)
        {
            return FindByCondition(timesheet => timesheet.Id.Equals(timesheetId))
                .DefaultIfEmpty(new Timesheet())
                .FirstOrDefault();
        }

        public void CreateTimesheet(Timesheet timesheet)
        {
            timesheet.Id = Guid.NewGuid();
            timesheet.Created = DateTime.Now;
            timesheet.Updated = DateTime.Now;
            Create(timesheet);
            Save();
        }

        public void UpdateTimesheet(Timesheet dbTimesheet, Timesheet timesheet)
        {
            dbTimesheet.Map(timesheet);
            Update(dbTimesheet);
        }

        public List<Timesheet> ListTimesheetsByUserWithInTwoDates(string userId, string startDate, string endDate)
        {
            var userTimesheetList = new List<Timesheet>();
            var fromDate = Convert.ToDateTime(startDate);
            var toDate = Convert.ToDateTime(endDate);

            var userTimesheets = FindByCondition(t => t.UserId.Equals(userId) && (fromDate.Date <= t.StartDate.Date && t.EndDate.Date <= toDate.Date));
            foreach (var t in userTimesheets)
            {
                var timesheet = new Timesheet();
                timesheet.Map(t);
                userTimesheetList.Add(timesheet);
            }
            return userTimesheetList;
        }
    }
}
