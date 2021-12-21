namespace Bridge.Commons.Redis.Enums
{
    /// <summary>
    ///     Estruturação de dados
    /// </summary>
    public enum EDataStructure
    {
        /// <summary>
        ///     Strings are the most basic kind of Redis value. Redis Strings are binary safe, this means that a Redis string can
        ///     contain any kind of data, for instance a JPEG image or a serialized Ruby object.
        ///     A String value can be at max 512 Megabytes in length.
        ///     You can do a number of interesting things using strings in Redis, for instance you can:
        ///     Use Strings as atomic counters using commands in the INCR family: INCR, DECR, INCRBY.
        ///     Append to strings with the APPEND command.
        ///     Use Strings as a random access vectors with GETRANGE and SETRANGE.
        ///     Encode a lot of data in little space, or create a Redis backed Bloom Filter using GETBIT and SETBIT.
        ///     Check all the available string commands for more information, or read the introduction to Redis data types.
        ///     <seealso cref="https://redis.io/topics/data-types" />
        /// </summary>
        SINGLE = 0,

        /// <summary>
        ///     Redis Lists are simply lists of strings, sorted by insertion order.
        ///     It is possible to add elements to a Redis List pushing new elements on the head (on the left) or on the tail (on
        ///     the right) of the list.
        ///     The LPUSH command inserts a new element on the head, while RPUSH inserts a new element on the tail.
        ///     A new list is created when one of this operations is performed against an empty key.
        ///     Similarly the key is removed from the key space if a list operation will empty the list.
        ///     These are very handy semantics since all the list commands will behave exactly like they were called with an empty
        ///     list if called with a non-existing key as argument.
        ///     <seealso cref="https://redis.io/topics/data-types" />
        /// </summary>
        LIST = 1,

        /// <summary>
        ///     Fila
        /// </summary>
        QUEUE = 2,

        /// <summary>
        ///     Stack
        /// </summary>
        STACK = 3,

        /// <summary>
        ///     Redis Hashes are maps between string fields and string values, so they are the perfect data type to represent
        ///     objects (e.g. A User with a number of fields like name, surname, age, and so forth):
        ///     A hash with a few fields (where few means up to one hundred or so) is stored in a way that takes very little space,
        ///     so you can store millions of objects in a small Redis instance.
        ///     While Hashes are used mainly to represent objects, they are capable of storing many elements, so you can use Hashes
        ///     for many other tasks as well.
        ///     Every hash can store up to 232 - 1 field-value pairs (more than 4 billion).
        ///     <seealso cref="https://redis.io/topics/data-types" />
        /// </summary>
        HASHSET = 4,

        /// <summary>
        ///     Redis Sets are an unordered collection of Strings.
        ///     It is possible to add, remove, and test for existence of members in O(1) (constant time regardless of the number of
        ///     elements contained inside the Set).
        ///     Redis Sets have the desirable property of not allowing repeated members.
        ///     Adding the same element multiple times will result in a set having a single copy of this element.
        ///     Practically speaking this means that adding a member does not require a check if exists then add operation.
        ///     A very interesting thing about Redis Sets is that they support a number of server side commands to compute sets
        ///     starting from existing sets, so you can do unions, intersections, differences of sets in very short time.
        ///     The max number of members in a set is 232 - 1 (4294967295, more than 4 billion of members per set).
        ///     <seealso cref="https://redis.io/topics/data-types" />
        /// </summary>
        SET = 5,

        /// <summary>
        ///     Redis Sorted Sets are, similarly to Redis Sets, non repeating collections of Strings.
        ///     The difference is that every member of a Sorted Set is associated with score, that is used in order to take the
        ///     sorted set ordered, from the smallest to the greatest score.
        ///     While members are unique, scores may be repeated.
        ///     With sorted sets you can add, remove, or update elements in a very fast way (in a time proportional to the
        ///     logarithm of the number of elements).
        ///     Since elements are taken in order and not ordered afterwards, you can also get ranges by score or by rank
        ///     (position) in a very fast way.
        ///     Accessing the middle of a sorted set is also very fast, so you can use Sorted Sets as a smart list of non repeating
        ///     elements where you can quickly access
        ///     everything you need: elements in order, fast existence test, fast access to elements in the middle!
        ///     In short with sorted sets you can do a lot of tasks with great performance that are really hard to model in other
        ///     kind of databases.
        /// </summary>
        SORTEDSET = 6
    }
}