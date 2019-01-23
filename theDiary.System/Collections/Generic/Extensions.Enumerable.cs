using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace System.Collections.Generic
{
    /// <summary>
    /// Contains extension methods used by <see cref="IEnumerable"/> or <see cref="T:IEnumerable"/> instances.
    /// </summary>
    public static class EnumerableExtensions
    {
        #region Public Extension Methods & Functions
        public static IList<T> WhereIn<T>(this IEnumerable<object> items, IEnumerable<object> others, Func<T, dynamic, bool> predicate)
        {
            return items.OfType<T>().Where(op => others.Any(op2 => predicate(op, (dynamic)op2))).ToList();
        }

        /// <summary>
        /// Determines if a sequence is <c>Null</c> or has no elements.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of the sequence.</typeparam>
        /// <param name="source">The sequence to check.</param>
        /// <returns><c>True</c> if the sequence is <c>Null</c> or is empty.</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return (source == null || source.Count() == 0);
        }

        /// <summary>
        /// Determines if a sequence is <c>Null</c> or has no elements.
        /// </summary>
        /// <param name="source">The sequence to check.</param>
        /// <returns><c>True</c> if the sequence is <c>Null</c> or is empty.</returns>
        public static bool IsNullOrEmpty(this IEnumerable source)
        {
            bool result = true;
            IEnumerator enumerator = null;
            if (source != null && (enumerator = source.GetEnumerator()).MoveNext())
                result = false;

            return result;
        }

        /// <summary>
        /// Creates a <see cref="Array"/> of <typeparamref name="T"/> containing a the <paramref name="instance"/>..
        /// </summary>
        /// <typeparam name="T">The underlying <see cref="Type"/> of the <see cref="Array"/>.</typeparam>
        /// <param name="instance">The value contained in the <see cref="Array"/>..</param>
        /// <returns><see cref="Array"/> containing the specified <paramref name="instance"/>.</returns>
        public static T[] MakeArray<T>(this T instance)
        {
            if (instance == null)
                return new T[]{};

            return new T[] 
            { 
                instance 
            };
        }

        /// <summary>
        /// Performs the specified <paramref name="action"/> in a parallel, on each element of the <see cref="T:IEnumerable"/>.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of element in the <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence of elements.</param>
        /// <param name="action">The <see cref="T:Action"/> delegate to perform on each element of the <see cref="T:IEnumerable"/>.</param>
        /// <exception cref="ArgumentNullException">thrown if the <paramref name="source"/> or <paramref name="action"/> is <c>Null</c>.</exception>
        public static void ForEachAsParallel<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source.IsNull())
                throw new ArgumentNullException("source");

            if (action.IsNull())
                throw new ArgumentNullException("action");

            source.AsParallel().ForAll(item => action(item));
        }

        public static IEnumerable<T> SelectAsParallel<T>(this IEnumerable<T> source, Func<T, T> action)
        {
            if (source.IsNull())
                throw new ArgumentNullException("source");

            if (action.IsNull())
                throw new ArgumentNullException("action");

            return source.AsParallel().AsOrdered().Select(item => action(item));
        }

        /// <summary>
        /// Performs the specified <paramref name="action"/> on each element of the <see cref="T:IEnumerable"/>.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of element in the <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence of elements.</param>
        /// <param name="action">The <see cref="T:Action"/> delegate to perform on each element of the <see cref="T:IEnumerable"/>.</param>
        /// <exception cref="ArgumentNullException">thrown if the <paramref name="source"/> or <paramref name="action"/> is <c>Null</c>.</exception>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source.IsNull())
                throw new ArgumentNullException("source");

            if (action.IsNull())
                throw new ArgumentNullException("action");

            foreach (var item in source)
                action(item);
        }

        /// <summary>
        ///  Determines whether a sequence contains a specified element by using the specified <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence in which to locate a value.</param>
        /// <param name="predicate">The <see cref="T:Predicate"/> used to match the element.</param>
        /// <returns><c>True</c> if the sequence contains a match; otherwise <c>False</c>.</returns>
        /// <exception cref="ArgumentNullException">thrown if the <paramref name="source"/> or <paramref name="predicate"/> is <c>Null</c>.</exception>
        public static bool Contains<TSource>(this IEnumerable<TSource> source, Predicate<TSource> predicate)
        {
            return source.Contains(predicate, true);
        }

        /// <summary>
        ///  Determines whether a sequence contains a specified element by using the specified <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence in which to locate a value.</param>
        /// <param name="predicate">The <see cref="T:Predicate"/> used to match the element.</param>
        /// <param name="asParallel">Value indicating if the <paramref name="source"/> should be checked in parallel query.</param>
        /// <returns><c>True</c> if the sequence contains a match; otherwise <c>False</c>.</returns>
        /// <exception cref="ArgumentNullException">thrown if the <paramref name="source"/> or <paramref name="predicate"/> is <c>Null</c>.</exception>
        public static bool Contains<TSource>(this IEnumerable<TSource> source, Predicate<TSource> predicate, bool asParallel)
        {
            if (source.IsNull())
                throw new ArgumentNullException("source");

            if (predicate.IsNull())
                throw new ArgumentNullException("predicate");

            foreach (var item in source.Query(asParallel))
                if (predicate(item))
                    return true;

            return false;
        }

        /// <summary>
        /// Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the first occurrence within the entire <see cref="T:List"/>.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of the <see cref="T:IList"/>.</typeparam>
        /// <param name="items">The sequence of items to search.</param>
        /// <param name="match">The <see cref="T:Predicate"/> delegate that defines the conditions of the element to search for.</param>
        /// <returns>The zero-based index of the first occurrence of an element that matches the conditions defined by match, if found; otherwise, –1.</returns>
        public static int FindIndex<T>(this IList<T> items, Predicate<T> match)
        {
            if (items.IsNull())
                throw new ArgumentNullException("source");

            if (match.IsNull())
                throw new ArgumentNullException("match");

            for (int index = 0; index < items.Count; index++)
                if (match(items[index]))
                    return index;

            return -1;
        }

        /// <summary>
        /// Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the first occurrence within the entire <see cref="T:List"/>.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of the <see cref="T:IList"/>.</typeparam>
        /// <param name="items">The sequence of items to search.</param>
        /// <param name="match">The <see cref="T:Predicate"/> delegate that defines the conditions of the element to search for.</param>
        /// <param name="index">The zero-based index of the first occurrence of an element that matches the conditions defined by match, if found; otherwise, –1.</param>
        /// <returns><c>True</c> if the <paramref name="items"/> conditions defined by the <paramref name="match"/> where met; otherwise <c>False</c>.</returns>
        public static bool TryFindIndex<T>(this IList<T> items, Predicate<T> match, out int index)
        {
            if (items.IsNull())
                throw new ArgumentNullException("source");

            if (match.IsNull())
                throw new ArgumentNullException("match");

            index = items.FindIndex(match);

            return (index != -1);
        }

        public static IEnumerable<T> AppendToBegining<T>(this IEnumerable<T> items, T source)
        {
            return new T[] { source }.Concat(items);
        }

        public static IEnumerable<T> Append<T>(this IEnumerable<T> items, T source)
        {
            return items.Concat(new T[] { source }); ;
        }
        #endregion

        #region Private Extension Methods & Functions
        private static IEnumerable<T> Query<T>(this IEnumerable<T> items, bool asParallel)
        {
            if (asParallel)
                return items.AsParallel();

            return items;
        }
        #endregion
    }
}
