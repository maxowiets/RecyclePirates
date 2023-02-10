using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonSpawner : MonoBehaviour
{
    public Person personPrefab;
    List<Person> persons = new List<Person>();
    public int amountOfPersonsPerDay = 1;
    public List<Transform> spawnLocations;

    public void SpawnNewPersons()
    {
        for (int i = persons.Count - 1; i >= 0; i--)
        {
            Person tempPerson = persons[i];
            DestroyPerson(tempPerson);
        }

        List<Transform> tempList = new List<Transform>(spawnLocations);
        for (int i = 0; i < amountOfPersonsPerDay; i++)
        {
            int rand = Random.Range(0, tempList.Count);
            persons.Add(Instantiate(personPrefab, tempList[rand].position, Quaternion.identity));
            tempList.RemoveAt(rand);
        }
    }

    public void DestroyPerson(Person personToDestroy)
    {
        persons.Remove(personToDestroy);
        Destroy(personToDestroy.gameObject);
    }
}
