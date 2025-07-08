using System.Collections.Generic;
public class EnemyManager : SingletonBehaviour<EnemyManager>
{
    public List<EnemyCharacter> Enemies { get; } = new List<EnemyCharacter>();

    public void Start()
    {
        EnemyCharacter[] enemyInScene = FindObjectsOfType<EnemyCharacter>();
        foreach (EnemyCharacter enemy in enemyInScene)
        {
            AddEnemy(enemy);
        }
    }

    public void AddEnemy(EnemyCharacter enemy)
    {
        Enemies.Add(enemy);
    }

    public void RemoveEnemy(EnemyCharacter enemy)
    {
        Enemies.Remove(enemy);
    }
}
