namespace AlgorithmsLibrary
{
    /// <summary>
    /// Декодированное сообщение с битом, в котором была допущена ошибка.
    /// Если ошибки допущено не было, то ErrorBit = -1.
    /// В остальных ситуациях его значение - позиция бита с ошибкой в sourceMessageWithError.
    /// </summary>
    public class DecodedMessage
    {
        /// <summary>
        /// Исправленное декодированное сообщение.
        /// </summary>
        public string decodedMessage { get; private set; }
        /// <summary>
        /// Передаваемое сообщение с возможно допущенной ошибкой.
        /// </summary>
        public string sourceMessageWithError { get; private set; }
        /// <summary>
        /// Позиция бита в передаваемом сообщении, в котором возможно была допущена ошибка.
        /// Если ошибки допущено не было, то его значение 0.
        /// </summary>
        public int ErrorBit { get; private set; }

        public DecodedMessage(string decodedMessage, string sourceMessageWithError, int errorBit)
        {
            this.decodedMessage = decodedMessage;
            this.sourceMessageWithError = sourceMessageWithError;
            ErrorBit = errorBit;
        }
    }
}
