using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GHMatti.Core
{
    public sealed class GHMattiTaskScheduler : TaskScheduler, IDisposable
    {
        private BlockingCollection<Task> tasks = new BlockingCollection<Task>();
        private readonly Thread mainThread = null;

        public GHMattiTaskScheduler()
        {
            mainThread = new Thread(new ThreadStart(Execute));
            if (!mainThread.IsAlive)
            {
                mainThread.Start();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Execute()
        {
            foreach (Task task in tasks.GetConsumingEnumerable())
            {
                TryExecuteTask(task);
            }
        }

        protected override void QueueTask(Task task)
        {
            if (task != null)
                tasks.Add(task);
        }

        private void Dispose(bool dispose)
        {
            if (dispose)
            {
                tasks.CompleteAdding();
                tasks.Dispose();
            }
        }

        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return tasks.ToArray();
        }

        protected override bool TryExecuteTaskInline(Task task, bool wasQueued)
        {
            return false;
        }
    }
}
