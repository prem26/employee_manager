using System;
using Contracts;
using Entities;
namespace Repository
{
    public class RepositoryWrapper: IRepositoryWrapper
    {
        private RepositoryContext _repositoryContext;
        private IUserRepository _user;
        private ITaskRepository _task;
        private ITimesheetRepository _timesheet;

        public RepositoryWrapper(RepositoryContext repoContext)
        {
            _repositoryContext = repoContext;
        }

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_repositoryContext);
                }
                return _user;
            }
        }

        public ITaskRepository Task
        {
            get
            {
                if (_task == null)
                {
                    _task = new TaskRepository(_repositoryContext);
                }
                return _task;
            }
        }

        public ITimesheetRepository Timesheet
        {
            get
            {
                if (_timesheet == null)
                {
                    _timesheet = new TimesheetRepository(_repositoryContext);
                }
                return _timesheet;
            }
        }

        public void Save()
        {
            _repositoryContext.SaveChanges();
        }

    }
}
