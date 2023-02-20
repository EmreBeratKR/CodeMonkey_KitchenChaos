using System;
using System.Collections.Generic;
using EmreBeratKR.ServiceLocator;
using UI;

public class UIManager : ServiceBehaviour
{
    public static event Action OnAllMenusClosed;
    
    
    private readonly Stack<IMenu> m_MenuStack = new();


    private void OnEnable()
    {
        GameManager.OnUnPauseInput += OnUnPauseInput;
    }

    private void OnDisable()
    {
        GameManager.OnUnPauseInput -= OnUnPauseInput;
    }


    private void OnUnPauseInput()
    {
        if (!m_MenuStack.TryPop(out var menu)) return;
        
        menu.Close();

        if (m_MenuStack.Count <= 0)
        {
            OnAllMenusClosed?.Invoke();
        }
    }
    

    public void PushMenu(IMenu menu)
    {
        m_MenuStack.Push(menu);
    }
}