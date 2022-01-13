using System;

public static class RandomUtils {
    private static Random random;

    public static void InitRandom(int seed) {
        random = new Random(seed);
    }

    /// <summary>
    /// 返回的随机数包含上限与下限
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int GetRandom(int min, int max) {
        return random.Next(min, max + 1);
    }
}