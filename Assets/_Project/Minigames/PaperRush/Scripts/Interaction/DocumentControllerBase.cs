public abstract class DocumentControllerBase: Interactable
{
    public abstract object GetDocument();
    public abstract void close();
    public abstract void open();

}