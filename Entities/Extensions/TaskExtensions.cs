using System;
using Entities.Models;
namespace Entities.Extensions
{
    public static class TaskExtensions
    {
        public static void Map(this Task dbTask, Task task)
        {
            dbTask.Id = task.Id;
            dbTask.Description = task.Description;
            dbTask.EstimatedTime = task.EstimatedTime;
            dbTask.UserId = task.UserId;
            dbTask.Created = task.Created;
            dbTask.Updated = task.Updated;
        }
    }
}
