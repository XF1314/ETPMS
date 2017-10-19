using System;

namespace ETPMS.Infrastructure.Utilities
{
    public static class Ensure
    {
        /// <summary>
        /// 确保参数满足条件表达式
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="func">条件表达式</param>
        /// <param name="argument">参数值</param>
        /// <param name="message">验证失败后返回的消息</param>
        public static void Meet<T>(Func<T, bool> func, T argument, string message) where T : class
        {
            if (!func(argument))
                throw new ArgumentException(message);
        }

        /// <summary>
        /// 确保参数不为空
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="argument">参数值</param>
        /// <param name="argumentName">参数名</param>
        public static void NotNull<T>(T argument, string argumentName) where T : class
        {
            if (argument == null)
                throw new ArgumentNullException(argumentName + "不应该为空~");
        }

        /// <summary>
        /// 确保参数不为空且长度大于0
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="argument">参数值</param>
        /// <param name="argumentName">参数名</param>
        public static void NotNullOrEmpty(string argument, string argumentName)
        {
            if (string.IsNullOrEmpty(argument))
                throw new ArgumentNullException(argument, argumentName + "不应该为空字串~");
        }

        /// <summary>
        /// 确保参数为正数
        /// </summary>
        /// <param name="argument">参数值</param>
        /// <param name="argumentName">参数名</param>
        public static void Positive(int number, string argumentName)
        {
            if (number <= 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentName + "应该大于零~");
        }

        /// <summary>
        /// 确保参数为正数
        /// </summary>
        /// <param name="argument">参数值</param>
        /// <param name="argumentName">参数名</param>
        public static void Positive(long number, string argumentName)
        {
            if (number <= 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentName + "应该大于零~");
        }

        /// <summary>
        /// 确保参数为非负数
        /// </summary>
        /// <param name="argument">参数值</param>
        /// <param name="argumentName">参数名</param>
        public static void NonNegative(long number, string argumentName)
        {
            if (number < 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentName + "应该为非负数~");
        }

        /// <summary>
        /// 确保参数为非负数
        /// </summary>
        /// <param name="argument">参数值</param>
        /// <param name="argumentName">参数名</param>
        public static void NonNegative(int number, string argumentName)
        {
            if (number < 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentName + "应该为非负数~");
        }

        /// <summary>
        /// 确保Guid非空
        /// </summary>
        /// <param name="guid">Guid值</param>
        /// <param name="argumentName">参数名</param>
        public static void NotEmptyGuid(Guid guid, string argumentName)
        {
            if (Guid.Empty == guid)
                throw new ArgumentException(argumentName, argumentName + "GUID不应该为空~");
        }

        /// <summary>
        /// 确保参数等于期望值
        /// </summary>
        /// <param name="expected">期望值</param>
        /// <param name="actual">实际值</param>
        /// <param name="argumentName">参数名</param>
        public static void Equal(int expected, int actual, string argumentName)
        {
            if (expected != actual)
                throw new ArgumentException(string.Format("{0} 期望值: {1}, 实际值: {2}", argumentName, expected, actual));
        }

        /// <summary>
        /// 确保参数等于期望值
        /// </summary>
        /// <param name="expected">期望值</param>
        /// <param name="actual">实际值</param>
        /// <param name="argumentName">参数名</param>
        public static void Equal(long expected, long actual, string argumentName)
        {
            if (expected != actual)
                throw new ArgumentException(string.Format("{0} 期望值: {1}, 实际值: {2}", argumentName, expected, actual));
        }

        /// <summary>
        /// 确保参数等于期望值
        /// </summary>
        /// <param name="expected">期望值</param>
        /// <param name="actual">实际值</param>
        /// <param name="argumentName">参数名</param>
        public static void Equal(bool expected, bool actual, string argumentName)
        {
            if (expected != actual)
                throw new ArgumentException(string.Format("{0} 期望值: {1}, 实际值: {2}", argumentName, expected, actual));
        }
    }
}