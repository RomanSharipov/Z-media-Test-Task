using Cysharp.Threading.Tasks;
using System;

public interface IUiScreen
{
    public UniTask InitializeAsync();
    public UniTask Show();
    public UniTask Hide();
}