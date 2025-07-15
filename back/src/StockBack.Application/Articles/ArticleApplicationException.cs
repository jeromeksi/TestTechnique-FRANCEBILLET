namespace StockBack.Application.Articles;

public class ArticleAlreadyExistException : Exception
{
    public ArticleAlreadyExistException(string message, Exception? inner)
       : base(message, inner) { }
    public ArticleAlreadyExistException(Exception inner)
       : base(inner.Message, inner) { }
}


public class ArticleUnknowException : Exception
{
    public ArticleUnknowException(string message, Exception? inner)
       : base(message, inner) { }
    public ArticleUnknowException(Exception inner)
       : base(inner.Message, inner) { }
}


public class ArticlePriceInvalidException : Exception
{
    public ArticlePriceInvalidException(string message, Exception? inner)
       : base(message, inner) { }
    public ArticlePriceInvalidException(Exception inner)
       : base(inner.Message, inner) { }
}


public class ArticleStorageInvalidException : Exception
{
    public ArticleStorageInvalidException(string message, Exception? inner)
       : base(message, inner) { }
    public ArticleStorageInvalidException(Exception inner)
       : base(inner.Message, inner) { }
}