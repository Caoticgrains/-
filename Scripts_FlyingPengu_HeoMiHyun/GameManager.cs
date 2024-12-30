using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public static GameManager instance;
   public int MaxNuberOfShots = 3;
   [SerializeField] private float _secondsToWaitBeforeDeathCheck = 3f;
   [SerializeField] private GameObject _restartScreenObject;
   [SerializeField] private SlingShotHandler _slingShotHandler;
   private int _usedNuberOfShots;
   private IconHandler _iconHandler;
   private List<Gangsters> _gangsters = new List<Gangsters>();

   private void Awake()
   {
      if (instance == null)
      {
         instance = this;
      }
      _iconHandler = FindObjectOfType<IconHandler>();
      
      Gangsters[] gangsters = FindObjectsOfType<Gangsters>();
      for (int i = 0; i < gangsters.Length; i++)
      {
         _gangsters.Add(gangsters[i]);
      }
   }

   public void UseShot()
   {
      _usedNuberOfShots++;
      _iconHandler.UseShot(_usedNuberOfShots);
      
      CheckForLastShots();
   }

   public bool HasEnoughShots()
   {
      if (_usedNuberOfShots < MaxNuberOfShots)
      {
         return true;
      }
      else
      {
         return false;
      }
   }

   public void CheckForLastShots()
   {
      if (_usedNuberOfShots == MaxNuberOfShots)
      {
         StartCoroutine(CheckAftherWaitTime());
      }
   }

   private IEnumerator CheckAftherWaitTime()
   {
      yield return new WaitForSeconds(_secondsToWaitBeforeDeathCheck);

      if (_gangsters.Count == 0)
      {
         WinGame();
      }
      else
      {
         RestartGame();
      }
   }


   public void RemoveGangster(Gangsters gangster)
   {
      _gangsters.Remove(gangster);
      CheckForAllGangsters();
   }

   private void CheckForAllGangsters()
   {
      if (_gangsters.Count == 0)
      {
         //win
      }
   }
   
   #region Win/Lose

   private void WinGame()
   {
      _restartScreenObject.SetActive(true);
      _slingShotHandler.enabled = false;
   }

   public void RestartGame()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }
   
   #endregion
   
}

