using UnityEngine;

public class EnemyMusicTrigger : MonoBehaviour
{
    public bool tocarAdaptativa = true;
    public int stingerNumero = 1;

    private bool ativou = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (ativou) return;

        if (other.CompareTag("Player"))
        {
            ativou = true;

            if (tocarAdaptativa)
                MusicManager.instance.TocarAdaptativa();

            if (stingerNumero == 1)
                MusicManager.instance.Stinger1();
            else if (stingerNumero == 2)
                MusicManager.instance.Stinger2();
        }
    }
}
