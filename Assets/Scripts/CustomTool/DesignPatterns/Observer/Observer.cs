namespace Scripts.CustomTool.DesignPatterns
{
    public delegate void ObserverDelegate(object arg);
    public abstract class Subject
    {
        event ObserverDelegate observers;
        public void Register(ObserverDelegate observer)
        {
            observers += observer;
        }
        public void Unregister(ObserverDelegate observer)
        {
            observers -= observer;
        }
        public virtual void Notify(object arg)
        {
            if (observers != null)
                observers.Invoke(arg);
        }
    }
    public abstract class SubjectSingleton<T> : Subject, ISingleton<T> where T : Subject
    {
        public static T Instance { get => ISingleton<T>.Instance; }
    }
}