#if UNITY_EDITOR
using System;
namespace Mamavon.Funcs
{
    public static class TupleExtensions
    {
        static void TestCode()
        {
            (int min, int big) valueInt = (20, 10).SortMinMaxTaple();
            valueInt = valueInt.SwapTuple();
            (float min, float big) valueFloat = (MathF.PI, MathF.E).SortMinMaxTaple();
            (valueFloat = valueFloat.SwapTuple()).Debuglog("float型タプル");
        }
        /// <summary>
        /// タプル型を最小と最大でソートする拡張メソッド
        /// (3,2)で渡したら(2,3)で帰ってくる
        /// </summary>
        /// <typeparam name="T">値型</typeparam>
        /// <returns>(最小値、最大値)</returns>
        public static (T, T) SortMinMaxTaple<T>(this (T, T) getTuple) where T : IComparable<T>
        {
            T a = getTuple.Item1;
            T b = getTuple.Item2;

            if (a.CompareTo(b) <= 0)
            {
                return (a, b); // a が小さいか等しい場合
            }
            else
            {
                return (b, a); // b が小さい場合
            }
        }
        public static (T, T) SwapTuple<T>(this (T, T) getTuple) where T : IComparable<T>
        {
            return (getTuple.Item2, getTuple.Item1);
        }
    }
}
#endif