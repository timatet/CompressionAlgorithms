namespace AlgorithmsLibrary
{
    /// <summary>
    /// Сжатая строка.
    /// </summary>
    /// <typeparam name="T">Тип данных, с помощью которых данная строка представлена.</typeparam>
    public interface IAlgmEncoded<T>
    {
        string ToString();
        T GetAnswer();
    }

    /// <summary>
    /// Сжатая строка.
    /// </summary>
    /// <typeparam name="T1">Тип данных, с помощью которых данная строка представлена.</typeparam>
    /// <typeparam name="T2">Словарь частот.</typeparam>
    public interface IAlgmEncoded<T1, T2>
    {
        string ToString();
        T1 GetAnswer();
        T2 GetData();
    }
}
