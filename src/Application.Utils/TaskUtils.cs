using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Utils
{
    public static class TaskUtils
    {
        /// <summary>
        /// Given a list of tasks, run them in parallel batch by batch
        /// </summary>
        /// <param name="sources">Input for each async operation</param>
        /// <param name="action">Async operation definition</param>
        /// <param name="batchSize">Run these many tasks per batch</param>
        /// <param name="delayPerBatch">Millseconds that you want to wait before running next batch</param>
        public static async Task<IEnumerable<TOutput>> RunBatch<TInput, TOutput>(
            IEnumerable<TInput> sources, 
            Func<TInput, Task<TOutput>> action, 
            int batchSize = 5,
            int delayPerBatch = 0)
        {
            var sourcesList = sources.ToList();
            var totalPages = (int)Math.Ceiling((double)sourcesList.Count / batchSize);
            var results = new List<TOutput>();

            for (var i = 1; i <= totalPages; i++)
            {
                var batch = sourcesList.Skip(batchSize * (i - 1)).Take(batchSize).ToArray();

                var batchResults = 
                    await Task.WhenAll(
                            batch.Select(action)
                                 .AsParallel()
                                 .Select(async t => await t));

                results.AddRange(batchResults);

                if (delayPerBatch > 0 && i < totalPages)
                {
                    Thread.Sleep(delayPerBatch);
                }
            }

            return results;
        }
    }
}
