using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cavewood.Models
{
    public class SpawnPoint
    {
        [SerializeField] public string m_Scene;
        [SerializeField] public string m_PointName;

        public SpawnPoint()
        {

        }
        public SpawnPoint(string _Scene, string _PointName)
        {
            this.m_Scene = _Scene;
            this.m_PointName = _PointName;
        }
    }
}
