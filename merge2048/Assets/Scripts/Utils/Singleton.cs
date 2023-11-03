using System;
using System.Reflection;

public class Singleton<T> where T : class, new()
{
	 private static T instance;

    protected Singleton()
    {
        // 기본 생성자를 외부에서 호출하지 못하도록 보호
		Init();
    }

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }

	protected virtual void Init() {

	}
}