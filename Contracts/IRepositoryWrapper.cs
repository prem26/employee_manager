using System;
namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        ITaskRepository Task { get; }
        ITimesheetRepository Timesheet { get; }
        void Save();
    }
}
