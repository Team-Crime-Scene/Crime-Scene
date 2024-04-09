using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class TitleScene : BaseScene
{
    [SerializeField] Button continueButton;

    private void Start()
    {
        bool exist = Manager.Data.ExistData();
        // �ҷ����� ������ ���� �ȵ�
        continueButton.interactable = exist;
    }
    public override IEnumerator LoadingRoutine()
    {
        yield return null;
    }
    public void ContinueGame()
    {
        Manager.Scene.LoadScene("Refatoring");
    }

    public void StartGame()
    {
        Manager.Data.NewData();
        Manager.Scene.LoadScene("Refatoring");
    }
}
