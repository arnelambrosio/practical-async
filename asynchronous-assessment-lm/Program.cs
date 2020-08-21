using System;
using System.Threading;
using System.Threading.Tasks;

/**
 * INSTRUCTIONS:
 *  1. Modify the codes below and make it asynchronous
 *  2. After your modification, explain what makes it asynchronous
**/


namespace asynchronous_assessment_lm
{
    class Program
    {
        static void Main(string[] args)
        {
            Coffee cup = PourCoffee();
            Console.WriteLine("Coffee is ready");

            Egg eggs = AsyncUtil.RunSync(() => FryEggs(2));
            Console.WriteLine("Eggs are ready");

            Bacon bacon = AsyncUtil.RunSync(() => FryBacon(3));
            Console.WriteLine("Bacon is ready");

            Toast toast = AsyncUtil.RunSync(() => ToastBread(2));
            ApplyButter(toast);
            ApplyJam(toast);
            Console.WriteLine("toast is ready");

            Juice orange = PourOJ();
            Console.WriteLine("Orange juice is ready");
            Console.WriteLine("Breakfast is ready!");

        }

        private static Juice PourOJ()
        {
            Console.WriteLine("Pouring orange juice");
            return new Juice();
        }

        private static void ApplyJam(Toast toast) =>
            Console.WriteLine("Putting jam on the toast");

        private static void ApplyButter(Toast toast) =>
            Console.WriteLine("Putting butter on the toast");

        private async static Task<Toast> ToastBread(int slices)
        {
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("Putting a slice of bread in the toaster");
            }
            Console.WriteLine("Start toasting...");
            await Task.Delay(3000);
            Console.WriteLine("Remove toast from toaster");

            return new Toast();
        }

        private async static Task<Bacon> FryBacon(int slices)
        {
            Console.WriteLine($"putting {slices} slices of bacon in the pan");
            Console.WriteLine("cooking first side of bacon...");
            await Task.Delay(3000);
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("flipping a slice of bacon");
            }
            Console.WriteLine("cooking the second side of bacon...");
            await Task.Delay(3000);


            return new Bacon();
        }

        private async static Task<Egg> FryEggs(int count)
        {
            Console.WriteLine("Warming the egg pan...");
            await Task.Delay(3000);
            Console.WriteLine($"cracking {count} eggs");
            Console.WriteLine("cooking the eggs ...");
            await Task.Delay(3000);
            Console.WriteLine("Put eggs on plate");

            return new Egg();
        }

        private static Coffee PourCoffee()
        {
            Console.WriteLine("Pouring coffee");
            return new Coffee();
        }

        private class Coffee
        {
        }

        private class Egg
        {
        }

        private class Bacon
        {
        }

        private class Toast
        {
        }

        private class Juice
        {
        }

        /// <summary>
        /// Helper class to run async methods within a sync process.
        /// </summary>
        public static class AsyncUtil
        {
            private static readonly TaskFactory _taskFactory = new
                TaskFactory(CancellationToken.None,
                            TaskCreationOptions.None,
                            TaskContinuationOptions.None,
                            TaskScheduler.Default);

            /// <summary>
            /// Executes an async Task method which has a void return value synchronously
            /// USAGE: AsyncUtil.RunSync(() => AsyncMethod());
            /// </summary>
            /// <param name="task">Task method to execute</param>
            public static void RunSync(Func<Task> task)
                => _taskFactory
                    .StartNew(task)
                    .Unwrap()
                    .GetAwaiter()
                    .GetResult();

            /// <summary>
            /// Executes an async Task<T> method which has a T return type synchronously
            /// USAGE: T result = AsyncUtil.RunSync(() => AsyncMethod<T>());
            /// </summary>
            /// <typeparam name="TResult">Return Type</typeparam>
            /// <param name="task">Task<T> method to execute</param>
            /// <returns></returns>
            public static TResult RunSync<TResult>(Func<Task<TResult>> task)
                => _taskFactory
                    .StartNew(task)
                    .Unwrap()
                    .GetAwaiter()
                    .GetResult();
        }
    }
}
