using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UI : MonoBehaviour
{
	// �|�[�Y���Q��
	[SerializeField]
	private GameObject PauseUI = null;

	// �I�v�V�������Q��
	[SerializeField]
	private GameObject OptionUI = null;

	// SE���Q��
	[SerializeField]
	private SE Se = null;

	// pause���̏����I����Ԃɐݒ肷��{�^�����w��
	[SerializeField]
	private Selectable FastButton = null;

	// Option���J���ꂽ���ɑI�������{�^�����w��
	[SerializeField]
	private Selectable OptionButton = null;

	Animator animator;

	// �J�n����UI���\��
	void Start()
	{
		PauseUI.SetActive(false);
		OptionUI.SetActive(false);
		FastButton.Select();

		animator = GetComponent<Animator>();
	}

	// pause��ʂ�\��
	public void Control()
	{
		if (!PauseUI.activeSelf)
        {
			// �|�[�Y�̕\��
			PauseUI.SetActive(true);
			// UI���J���ꂽ�������Đ�
			Se.OpenUI();
			// ��~
			Time.timeScale = 0f;
			
		}
		else if (PauseUI.activeSelf && !OptionUI.activeSelf)
        {
			// �|�[�Y�̔�\��
			PauseUI.SetActive(false);
			// UI������ꂽ�������Đ�
			Se.CloseUI();
			// �ĊJ
			Time.timeScale = 1f;
		}
        else if (PauseUI.activeSelf && OptionUI.activeSelf)
		{
			// option�̔�\��
			OptionUI.SetActive(false);
			// UI������ꂽ�������Đ�
			Se.CloseUI();
			FastButton.Select();

			animator.SetTrigger("Show");
		}
	}

	// Option�{�^���������ꂽ����Option��ʂ̕\��
	public void Option()
	{
		if (PauseUI.activeInHierarchy) 
		{
			OptionUI.SetActive(true);
			// UI���J���ꂽ�������Đ�
			Se.OpenUI();
			// Option���I�����ꂽ�ꍇ�ɂ��̃{�^����I����Ԃɂ��܂�
			OptionButton.Select();

			animator.SetTrigger("Hide");
		} 
	}

	// Home�������ꂽ��scene��Title�ֈڍs����
	public void Home()
	{
		SceneManager.LoadScene("Title");
	}
}
