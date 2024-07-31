using UniRx;
using UnityEngine;

/// <summary>
/// ScriptableObjectだよ。
/// </summary>
[CreateAssetMenu(fileName = "NewScriptableObject", menuName = "Mamavon Packs/ScriptableObject/NewScriptableObject")]
public class NewScriptableObject : ScriptableObject
{
    public ReactiveProperty<int> testReactiveProperty;
    [SerializeField] int test;

    public int Test
    {
        get { return test; }
    }

    public void TestFunc(int i)
    {
    }

    [ContextMenu("実行テスト")]
    private void TestFunc()
    {
    }
}