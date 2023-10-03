namespace Id3Fixer.Extensions;

public static class StringArrayExtnsion
{
    public static string? GetSafe(this string[] array, int index)
    {
        if (array.Length <= index)
        {
            return null;
        }

        return array[index];
    }
}
