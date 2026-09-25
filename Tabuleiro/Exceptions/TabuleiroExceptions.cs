namespace tabuleiro.Exception;
public class TabuleiroException : System.Exception
{
    public TabuleiroException() { }
    public TabuleiroException(string message) : base(message) { }
    public TabuleiroException(string message, System.Exception inner) : base(message, inner) { }
   /* protected TabuleiroExceptionsException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }*/
}