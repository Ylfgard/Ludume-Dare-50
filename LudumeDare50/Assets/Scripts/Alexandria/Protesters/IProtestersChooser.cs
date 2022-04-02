using UnityEngine;
using City;

namespace Protesters
{
    public delegate void ProtestersChoosed(int people, int power, Vector3 position, Square square);

    public interface IProtestersChooser
    {
        public event ProtestersChoosed ProtestersChoosed;
    }
}