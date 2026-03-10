public abstract class DocumentControllerBase: Interactable
{
    public abstract Document GetDocument();
    public abstract void close();
    public abstract void open();

}